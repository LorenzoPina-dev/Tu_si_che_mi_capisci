
import cv2 as cv
import face_recognition
import socket
import requests
import json
from threading import Thread
import time
import base64
from multiprocessing import Process, Pipe
from gestione_server import Server,gestisci_key
#"80.22.36.186"
frame_width = 680
frame_height = 480
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
fps = 30.0
face_cascade = cv.CascadeClassifier('haarcascade_frontalface_default.xml')
 
    
def gestisci_cam(args):
    nome=args[0]
    termina=args[1]
    print("partito")
    ip=nome["Ip"]
    if "http" not in ip:
        ip=int(ip)
    print(ip)
    video_capture = cv.VideoCapture(ip)
    print(video_capture.getBackendName())
    video_capture.set(cv.CAP_PROP_FRAME_WIDTH, frame_width)
    video_capture.set(cv.CAP_PROP_FRAME_HEIGHT, frame_height)
    size = (int(video_capture.get(cv.CAP_PROP_FRAME_WIDTH)), int(video_capture.get(cv.CAP_PROP_FRAME_HEIGHT)))
    i=0
    
    while termina[nome["Id"]]==False:
        if video_capture.isOpened():
            ret, img = video_capture.read()
            gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
            faces = face_cascade.detectMultiScale(gray,scaleFactor=1.2,minNeighbors=5,minSize=(30, 30))
            if i%10==1:
                for (x, y, w, h) in faces:
                    ROI = gray[y-25:y+h+25, x-25:x+w+25]
                    reduced = cv.resize(ROI, dsize=(100, 100), interpolation=cv.INTER_CUBIC)
                    cv.imshow("Reduced", reduced);
                    _, bts = cv.imencode('.jpeg', reduced)
                    print(len(bts))
                    b64_bytes = base64.b64encode(bts)
                    b64_string = b64_bytes.decode()
                    print(nome)
                    Server.invia_risultati(b64_string,keyk,nome["Id"])
            i+=1
    video_capture.release()

def gestisci_dispositivi(args):
    termina_main=args[0]
    t=args[1]
    termina={}
    while termina_main==False:
        arr=Server.get_dispositivi(keyk)
        for video in arr:
            if (video["Id"] in t)==False:
                print(t)
                print(video)
                termina[video["Id"]]=False
                t[video["Id"]]=Thread(target=gestisci_cam, args=([video,termina],))
                t[video["Id"]].start()
            time.sleep(500)
            
def GestioneCam(conn):
    t={}
    termina_main=False
    tGest=Thread(target=gestisci_key, args=([termina_main,conn],))
    tGest.start()
    tGest=Thread(target=gestisci_dispositivi, args=([termina_main,t],))
    tGest.start()
                
    cv.destroyAllWindows()
"""
parent_conn, child_conn = Pipe()
GestioneCam(child_conn)

"""