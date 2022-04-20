
import cv2 as cv
import face_recognition
import socket
import requests
import json
from threading import Thread
import time
import base64
from multiprocessing import Process, Pipe
#"80.22.36.186"
frame_width = 680
frame_height = 480
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
fps = 30.0
face_cascade = cv.CascadeClassifier('haarcascade_frontalface_default.xml')
def GestisciCam(args):
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
            for (x, y, w, h) in faces:
                cv.rectangle(img, (x, y), (x+w, y+h), (255, 0, 0), 2)
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
                    dati=json.dumps({'Dati':b64_string,"IdDispositivo":nome["Id"],"KeyUtente":keyk}) 
                    x = requests.get('http://localhost:12345/riconosciVolto',data=dati);
                    print(x.text)
            i+=1
    video_capture.release()

def GestisciDisp(args):
    TerminaMain=args[0]
    t=args[1]
    termina={}
    while TerminaMain==False:
        x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo=0');
        y = json.loads(x.text)
        arr= y['result']['dispositivo']
        for video in arr:
            if (video["Id"] in t)==False:
                print(t)
                print(video)
                termina[video["Id"]]=False
                t[video["Id"]]=Thread(target=GestisciCam, args=([video,termina],))
                t[video["Id"]].start()
            time.sleep(500)
            
def GestisciKey(args):
    try:
        while args[0]==False:
            keyk=args[1].recv()
            print(keyk)
        args[1].close()
    except Exception as e:
        print("chiuso ascolto udp")
            
def GestioneCam(conn):
    t={}
    TerminaMain=False
    tGest=Thread(target=GestisciKey, args=([TerminaMain,conn],))
    tGest.start()
    tGest=Thread(target=GestisciDisp, args=([TerminaMain,t],))
    tGest.start()
                
    cv.destroyAllWindows()
    
"""
parent_conn, child_conn = Pipe()
GestioneCam(child_conn)

"""