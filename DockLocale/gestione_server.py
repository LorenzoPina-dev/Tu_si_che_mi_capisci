import cv2 as cv
import face_recognition
import socket
import requests
import json
from threading import Thread
import time
import base64
from multiprocessing import Process, Pipe

class Server:
    def get_dispositivi(keyk,tipo):
        x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo='+tipo);
        y = json.loads(x.text)
        arr= y['result']['dispositivo']
        return arr

    def invia_risultati(img,user_key,id_dispositivo):
        dati={'IdDispositivo':id_dispositivo,'KeyUtente':user_key} 
        x = requests.post('http://80.22.36.186:12345/riconosciVolto',files = {'immagine': (img, open(img, 'rb'), 'image/jpg', {'Expires': '0'})},data=dati);
        print(x.text)
        
    def invia_risultati_audio(img,user_key,id_dispositivo):
        dati={'IdDispositivo':id_dispositivo,'KeyUtente':user_key}
        x = requests.post('http://80.22.36.186:12345/riconosciEmozione',files = {'immagine': (img, open(img, 'rb'), 'audio/mpeg', {'Expires': '0'})},data=dati);
        print(x.text)
        
def gestisci_key(args):
    try:
        while args[0]==False:
            keyk=args[1].recv()
            print(keyk)
        args[1].close()
    except Exception as e:
        print("chiuso ascolto udp")
            