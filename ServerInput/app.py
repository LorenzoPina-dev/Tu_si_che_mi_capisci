#!/usr/bin/env python
# encoding: utf-8
import os
import base64
from imageio import imread
import json
from flask import Flask, request, jsonify
from simple_facerec import SimpleFacerec
import cv2
import io
import face_recognition
import requests
app = Flask(__name__)


def inviaVoltoTrovato(name, keyk, img,idDispositivo,face_encodings):
    files = {'immagine': (img, open(img, 'rb'), 'image/jpg', {'Expires': '0'})}
    x=requests.post('http://80.22.36.186/'+keyk+'/voltoTrovato/add', data={'idDispositivo': idDispositivo, 'idVolto': name,'vettVolto':face_encodings}, files=files)
    print(x.text)

sfr = SimpleFacerec()
sfr.load_encoding_images()
@app.route('/riconosciVolto', methods=['GET'])
def riconosci_volto():
    richiesta=json.loads(request.data)
    dati=richiesta['Dati']
    cv2_img = cv2.cvtColor(imread(io.BytesIO(base64.b64decode(dati))), cv2.COLOR_RGB2BGR)
    print("ci siamo")
    face_locations = face_recognition.face_locations(cv2_img)
    face_encoding = face_recognition.face_encodings(cv2_img, face_locations)[0]
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
    sfr.add_encoding_image(request.args.get('Id'),request.args.get('path'))
    return jsonify({"success":True,"testo":"inserimento avvenuto con successo"})

@app.route('/riconosciEmozione', methods=['GET'])
def update_record():
    return jsonify({"success":False,"testo":"non ancora implementato"})
    
app.run(debug=True, host="127.0.0.1", port=12345)