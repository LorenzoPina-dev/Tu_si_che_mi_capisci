
from __future__ import print_function
from pygrabber.dshow_graph import FilterGraph
from multiprocessing import Process, Pipe
from video import GestioneCam
import speech_recognition as s_r
import json
import socket
import os
import cv2
bufferSize  = 1024
def f(conn):
    print(conn.recv())   # prints "[42, None, 'hello']"
    conn.close()

if __name__ == '__main__':
    localIP = "127.0.0.1"

    localPort   = 12345
    UDPServerSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    UDPServerSocket.bind((localIP, localPort))
    parent_conn, child_conn = Pipe()
    p = Process(target=GestioneCam, args=(child_conn,))
    p.start()
 
    while(True):
        bytesAddressPair = UDPServerSocket.recvfrom(bufferSize)
        message = bytesAddressPair[0]
        message=message.decode("utf-8")
        print(message)
        if message=="getDispositivi/video":
                graph = FilterGraph()
                UDPServerSocket.sendto(str.encode(str(json.dumps({'dispositivi':graph.get_input_devices()}))),bytesAddressPair[1])
        elif message=="getDispositivi/audio":
            UDPServerSocket.sendto(str.encode(str(json.dumps({'dispositivi':s_r.Microphone.list_microphone_names()}))),bytesAddressPair[1])
        else:
            parent_conn.send(message)
    p.join()
    parent_conn.close()