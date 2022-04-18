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
import time
from watchdog.observers import Observer
from watchdog.events import PatternMatchingEventHandler


keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"

host="80.22.36.186"
def GetDispositivi():
    x = requests.get('http://'+host+'/'+keyk+'/dispositivi?tipo=1');
    y = json.loads(x.text)
    arr= y['result']['dispositivo']
    return arr;


def inviaVoltoTrovato(name, img,idDispositivo,face_encodings):
    files = {'immagine': (img, open(img, 'rb'), 'image/jpg', {'Expires': '0'})}
    if name!=None:
        requests.post('http://'+host+'/'+keyk+'/voltoTrovato/add', data={'idDispositivo': idDispositivo, 'idVolto': name,'vettVolto':face_encodings}, files=files)
    else:
        requests.post('http://'+host+'/'+keyk+'/voltoTrovato/add', data={'idDispositivo': idDispositivo, 'vettVolto':face_encodings}, files=files)







def on_created(event):
    time.sleep(4)
    try:
        print(f"hey, {event.src_path} has been created!")
        img_path=event.src_path.replace('\\','/')
        with open(img_path, "r") as f:
            inp=json.loads(f.readlines()[0])
            cv2_img = cv2.cvtColor(imread(io.BytesIO(base64.b64decode(inp["Dati"][0]))), cv2.COLOR_RGB2BGR)
            print("immagine decodificata "+img_path)
            f.close()
            face_locations = face_recognition.face_locations(cv2_img)
            face_encodings = face_recognition.face_encodings(cv2_img, face_locations)[0]
            name =sfr.detect_known_faces(face_encodings)
            print(img_path)
            cv2.imwrite("temp.jpg", cv2_img)
            print(name)
            inviaVoltoTrovato(name,"temp.jpg",inp["IdDispositivo"][0],face_encodings)
            os.remove(img_path)
    except: 
        print("err lettura")


def on_modified(event):
    time.sleep(4)
    try:
        print(f"hey, {event.src_path} has been changed!")
        img_path=event.src_path.replace('\\','/')
        if img_path=="./addVolto.json":
            with open(img_path, "r") as f:
                inp=json.loads(f.readlines()[0])
                f.close()
                sfr.add_encoding_image(inp["Id"],inp["Immagine"])
                
    except:
        print("err lettura")



if __name__ == "__main__":
    sfr = SimpleFacerec()
    sfr.load_encoding_images()
    
    patterns = ["*"]
    ignore_patterns = None
    ignore_directories = False
    case_sensitive = True
    my_event_handler = PatternMatchingEventHandler(patterns, ignore_patterns, ignore_directories, case_sensitive)
    my_event_handler.on_created = on_created
    my_event_handler.on_modified = on_modified
    path = "./"
    go_recursively = True
    my_observer = Observer()
    my_observer.schedule(my_event_handler, path, recursive=go_recursively)
    my_observer.start()
    try:
        while True:
            time.sleep(1)
    except KeyboardInterrupt:
        my_observer.stop() 
        my_observer.join() 










