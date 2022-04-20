import face_recognition
import cv2
import os
import glob
import numpy as np
from urllib.request import urlopen
import requests
import json

keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"


def GetVoltiRegistrati():
    x = requests.get('http://80.22.36.186/'+keyk+'/voltoRegistrato');
    y = json.loads(x.text)
    arr= y['result']['voltiRegistrati']
    return arr

def SetEncodingVolto(record,punti):
    x=requests.put('http://80.22.36.186/'+keyk+'/voltoRegistrato/addPunti', data={'id': record["Id"], 'punti': str(punti.tolist())})

def GetImmagine(basename):
    req = urlopen('http://80.22.36.186/'+keyk+'/immagine/voltoregistrato?nomefile='+basename)
    arr = np.asarray(bytearray(req.read()), dtype=np.uint8)
    img = cv2.imdecode(arr, -1) # 'Load it as it is'
    return img

class SimpleFacerec:
    def __init__(self):
        self.known_face_encodings = []
        self.known_face_names = []

        # Resize frame for a faster speed
        self.frame_resizing = 0.25

    def load_encoding_images(self):
        VoltiRegistrati=GetVoltiRegistrati()
        i=0
        for volto in VoltiRegistrati:
            try:
                img=GetImmagine(volto["Immagine"])
                if(volto["VettoreVolto"]!=null):
                    img_encoding=volto["VettoreVolto"]
                else:
                    img_encoding = face_recognition.face_encodings(img)[0]
                    SetEncodingVolto(volto,img_encoding)
                self.known_face_encodings.append(img_encoding)
                self.known_face_names.append(volto["Nome"])
            except:
                print("errore")
        print("Encoding images loaded")

    def detect_known_faces(self, face_encoding):
        matches = face_recognition.compare_faces(self.known_face_encodings, face_encoding)
        face_names = "Unknown"
        face_distances = face_recognition.face_distance(self.known_face_encodings, face_encoding)
        best_match_index = np.argmin(face_distances)
        if matches[best_match_index]:
            face_names = self.known_face_names[best_match_index]
        return face_names
