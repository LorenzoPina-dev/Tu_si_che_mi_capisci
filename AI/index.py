import cv2
import os
import glob
import json
from simple_facerec import SimpleFacerec
import face_recognition
import base64
from imageio import imread
import io

import requests
import json


keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"


def GetDispositivi():
    x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo=1');
    y = json.loads(x.text)
    arr= y['result']['dispositivo']
    return arr;


def inviaVoltoTrovato(name, img,idDispositivo,face_encodings):
    files = {'immagine': (img, open(img, 'rb'), 'image/jpg', {'Expires': '0'})}
    requests.post('http://80.22.36.186/'+keyk+'/voltoTrovato/add', data={'idDispositivo': idDispositivo, 'nome': name,'vettVolto':face_encodings}, files=files)

sfr = SimpleFacerec()
sfr.load_encoding_images()
while True:
    images_path = glob.glob(os.path.join("Volto/", "*.*"))

    i=0
    # Store image encoding and names
    for img_path in images_path:
        try:
            f=open(img_path)
            inp=json.loads(f.readlines()[0])
            cv2_img = cv2.cvtColor(imread(io.BytesIO(base64.b64decode(inp["Dati"][0]))), cv2.COLOR_RGB2BGR)
            print("immagine decodificata "+img_path)
            f.close()
            face_locations = face_recognition.face_locations(cv2_img)
            face_encodings = face_recognition.face_encodings(cv2_img, face_locations)[0]
            name =sfr.detect_known_faces(face_encodings)
            print(img_path)
            cv2.imwrite("temp.jpg", cv2_img)
            inviaVoltoTrovato(name,"temp.jpg",inp["IdDispositivo"],face_encodings)
            os.remove(img_path)
        except:
            print("err")
        i+=1
        key = cv2.waitKey(1) #ogni quanti millisecondi andare al frame successivo


