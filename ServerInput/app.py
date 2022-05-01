#!/usr/bin/env python
# encoding: utf-8
import os
import base64
from imageio import imread
import json
from flask import Flask, request, jsonify
from simple_facerec import SimpleFacerec
from simple_emotionrec import SimpleEmozionrec
import cv2
import io
import face_recognition
import requests
app = Flask(__name__)


def inviaVoltoTrovato(name, keyk, img,idDispositivo,face_encodings):
    files = {'immagine': (img, open(img, 'rb'), 'image/jpg', {'Expires': '0'})}
    x=requests.post('http://80.22.36.186/'+keyk+'/voltoTrovato/add', data={'idDispositivo': idDispositivo, 'idVolto': name,'vettVolto':face_encodings}, files=files)
    print(x.text)

def inviaEmozioneTrovato(IdEmozione, keyk,idDispositivo):
    x=requests.post('http://80.22.36.186/'+keyk+'/emozioni/add', data={'tipo': IdEmozione, 'idDispositivo': idDispositivo})
    print(x.text)

sfr = SimpleFacerec()
sfr.load_encoding_images()
emo=SimpleEmozionrec()
@app.route('/riconosciVolto', methods=['POST'])
def riconosci_volto():
    print(format(request.form))
    richiesta=request.form
    if request.files.get('immagine') is None:
        return jsonify({"success":False})
    cv2_img = cv2.cvtColor(imread(request.files['immagine']), cv2.COLOR_RGB2BGR)
    print("ci siamo")
    img_encodings = face_recognition.face_encodings(cv2_img)
    name=-1
    if len(img_encodings)>0:
        face_encoding=img_encodings[0]
        name =sfr.detect_known_faces(face_encoding)
        print("nome volto trovato="+str(name))
        cv2.imwrite("temp.jpg", cv2_img)
        inviaVoltoTrovato(name, richiesta['KeyUtente'],"temp.jpg", richiesta['IdDispositivo'],face_encoding)
        os.remove("temp.jpg")
        
    if name==-1:
        return jsonify({"success":False,"testo":"nessun volto trovato"})
    else:
        return jsonify({"success":True,"nome":name})
@app.route('/memorizzaVolto', methods=['POST'])
def memorizza_volto():
    sfr.add_encoding_image(request.form.get('Id'),request.form.get('path'))
    return jsonify({"success":True,"testo":"inserimento avvenuto con successo"})

@app.route('/riconosciEmozione', methods=['POST'])
def riconosci_emozione():
    print(format(request.form))
    richiesta=request.form
    
    if request.files.get('immagine') is None:
        return jsonify({"success":False})
    f=request.files['immagine']
    f.save("temp.wav")
    indice=emo.detect_emotion( "temp.wav")
    print(indice)
    inviaEmozioneTrovato(indice,richiesta['KeyUtente'], richiesta['IdDispositivo'])
    if indice==-1:
        return jsonify({"success":False,"testo":"nessun volto trovato"})
    else:
        return jsonify({"success":True,"idEmozione":indice})
    
app.run(debug=True, host="0.0.0.0", port=12345)