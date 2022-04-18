import face_recognition
import cv2
import os
import glob
import numpy as np
from urllib.request import urlopen
import requests
import json

keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
host="80.22.36.186"

def GetVoltiRegistrati():
    x = requests.get('http://'+host+'/'+keyk+'/voltoRegistrato');
    y = json.loads(x.text)
    arr= y['result']['voltiRegistrati']
    return arr

def SetEncodingVolto(Id,punti):
    x=requests.put('http://'+host+'/'+keyk+'/voltoRegistrato/addPunti', data={'id': Id, 'punti': str(punti.tolist())})

def GetImmagine(basename):
    req = urlopen('http://'+host+'/'+keyk+'/immagine/voltoregistrato?nomefile='+basename)
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
                #if(volto["VettoreVolto"]!=None):
                    #img_encoding=np.asarray(volto["VettoreVolto"])
                #else:
                img=GetImmagine(volto["Immagine"])
                img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)
                img_encoding = face_recognition.face_encodings(img)[0]
                SetEncodingVolto(volto["Id"],img_encoding)
                self.known_face_encodings.append(img_encoding)
                self.known_face_names.append(volto["Id"])
            except Exception as e:
                print("errore"+ format(e))
        print("Encoding images loaded")
        
    def add_encoding_image(self,id,path_img):
        try:
            img=GetImmagine(path_img)
            img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)
            img_encoding = face_recognition.face_encodings(img)[0]
            SetEncodingVolto(id,img_encoding)
            self.known_face_encodings.append(img_encoding)
            self.known_face_names.append(id)
        except Exception as e:
            print("errore"+ format(e))
        print("Encoding images loaded")

    def detect_known_faces(self, face_encoding):
        print(self.known_face_encodings)
        matches = face_recognition.compare_faces(self.known_face_encodings, face_encoding)
        face_names = None
        face_distances = face_recognition.face_distance(self.known_face_encodings, face_encoding)
        best_match_index = np.argmin(face_distances)
        if matches[best_match_index]:
            face_names = self.known_face_names[best_match_index]
        return face_names
