
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
import pyaudio
import wave
#"80.22.36.186"
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
 
FORMAT = pyaudio.paInt16
CHANNELS = 1
RATE = 44100
CHUNK = 512
RECORD_SECONDS = 5
device_index = 2
audio = pyaudio.PyAudio()

    
def gestisci_mic(args):
    nome=args[0]
    termina=args[1]
    print("partito")
    ip=nome["Ip"]
    try:
        if "http" not in ip:
            ip=int(ip)
    except:
        print("non valido")
    print(ip)
        
    stream = audio.open(format=FORMAT, channels=CHANNELS,
                    rate=RATE, input=True,input_device_index = ip,
                    frames_per_buffer=CHUNK)
    print ("recording started")
    i=0
    
    while termina[nome["Id"]]==False:
        Recordframes = []
        for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
            data = stream.read(CHUNK)
            Recordframes.append(data)
        print ("recording stopped")
        #print(Recordframes)
        
        waveFile = wave.open("temp.wav", 'wb')
        waveFile.setnchannels(CHANNELS)
        waveFile.setsampwidth(audio.get_sample_size(FORMAT))
        waveFile.setframerate(RATE)
        waveFile.writeframes(b''.join(Recordframes))
        waveFile.close()

        Server.invia_risultati_audio("temp.wav",keyk,nome["Id"])
    stream.stop_stream()
    stream.close()
    audio.terminate()

def gestisci_dispositivi(args):
    termina_main=args[0]
    t=args[1]
    termina={}
    while termina_main==False:
        arr=Server.get_dispositivi(keyk,"1")
        for video in arr:
            if (video["Id"] in t)==False:
                print(t)
                print(video)
                termina[video["Id"]]=False
                t[video["Id"]]=Thread(target=gestisci_mic, args=([video,termina],))
                t[video["Id"]].start()
            time.sleep(20)
            
def GestioneMic(conn):
    t={}
    termina_main=False
    tGest=Thread(target=gestisci_key, args=([termina_main,conn],))
    tGest.start()
    tGest=Thread(target=gestisci_dispositivi, args=([termina_main,t],))
    tGest.start()
                
    cv.destroyAllWindows()

parent_conn, child_conn = Pipe()
GestioneMic(child_conn)

