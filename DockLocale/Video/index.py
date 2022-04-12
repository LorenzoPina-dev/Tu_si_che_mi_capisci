
import cv2 as cv
import face_recognition
import socket
import requests
import json
from threading import Thread
import time

serverAddressPort= ("80.22.36.186", 12345)
bufferSize= 4064
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
frame_width = 680
frame_height = 480
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
fps = 30.0
face_cascade = cv.CascadeClassifier('haarcascade_frontalface_default.xml')
termina={}
def GestisciCam(nome):
    print("partito")
    video_capture = cv.VideoCapture(nome)
    print(video_capture.getBackendName())
    video_capture.set(cv.CAP_PROP_FRAME_WIDTH, frame_width)
    video_capture.set(cv.CAP_PROP_FRAME_HEIGHT, frame_height)
    size = (int(video_capture.get(cv.CAP_PROP_FRAME_WIDTH)), int(video_capture.get(cv.CAP_PROP_FRAME_HEIGHT)))
    i=0
    while termina[nome]==False:
        if video_capture.isOpened():
            ret, img = video_capture.read()
            gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
            faces = face_cascade.detectMultiScale(gray,scaleFactor=1.2,minNeighbors=5,minSize=(30, 30))
            for (x, y, w, h) in faces:
                cv.rectangle(img, (x, y), (x+w, y+h), (255, 0, 0), 2)
            if i%10==1:
                for (x, y, w, h) in faces:
                    ROI = gray[y:y+h, x:x+w]
                    reduced = cv.resize(ROI, dsize=(50, 50), interpolation=cv.INTER_CUBIC)
                    cv.imshow("Reduced", reduced);
                    _, bts = cv.imencode('.jpeg', reduced)
                    arr =[];
                    for b in bts:
                        arr.append(int(b))
                    j=json.dumps({"Tipo":"video","KeyUtente":keyk,"Dati":arr})
                    bytesToSend= str.encode(j)
                    UDPClientSocket.sendto(bytesToSend, serverAddressPort)
            i+=1
    video_capture.release()
t={}
TerminaMain=False

def GestisciDisp():
    while TerminaMain==False:
        x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo=0');
        y = json.loads(x.text)
        arr= len(y['result']['dispositivo'])
        for video in range(arr):
            if (video in t)==False:
                termina[video]=False
                t[video]=Thread(target=GestisciCam, args=(video,))
                t[video].start()
        time.sleep(60)
            
while TerminaMain==False:
    tGest=Thread(target=GestisciDisp, args=())
    tGest.start()
    key = input("exit per uscire")
    if key == "exit":
        for K in t:
            termina[K]=True
        TerminaMain=True
            
cv.destroyAllWindows()