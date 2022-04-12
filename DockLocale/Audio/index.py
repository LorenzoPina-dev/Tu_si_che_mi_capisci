import socket
import requests
import json
from threading import Thread
import time
import speech_recognition as speech_recog

rec = speech_recog.Recognizer()
# Importiamo la classe microphone per verificare la disponibilità del dispositivo
mic_test = speech_recog.Microphone()
# Lista dei microfoni disponibili
print(speech_recog.Microphone.list_microphone_names())

serverAddressPort= ("80.22.36.186", 12345)
bufferSize= 4064
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
frame_width = 680
frame_height = 480
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
fps = 30.0
termina={}
def GestisciCam(nome):
    print("partito")
with speech_recog.Microphone(device_index=1) as source: 
    rec.adjust_for_ambient_noise(source, duration=1)
    print("Reach the Microphone and say something!")
    audio = rec.listen(source)
print(audio);
    
t={}
TerminaMain=False

def GestisciDisp():
    while TerminaMain==False:
        x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo=1');
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