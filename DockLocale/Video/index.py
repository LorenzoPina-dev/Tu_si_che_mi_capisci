
import cv2 as cv
import face_recognition
import socket
import requests
import json
from threading import Thread
import time
import base64
#"80.22.36.186"
serverAddressPort= ("localhost", 12345)
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
    video_capture = cv.VideoCapture(nome["ip"])
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
                    j=json.dumps({"Tipo":"video","KeyUtente":keyk,"Dati":b64_string,"IdDispositivo":nome["Id"]})
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
        for video in arr:
            if (video in t)==False:
                termina[video["Id"]]=False
                t[video["Id"]]=Thread(target=GestisciCam, args=(video,))
                t[video["Id"]].start()
        time.sleep(60)
            
while TerminaMain==False:
    tGest=Thread(target=GestisciDisp)
    tGest.start()
    key = input("exit per uscire")
    if key == "exit":
        for K in t:
            termina[K]=True
        TerminaMain=True
            
cv.destroyAllWindows()