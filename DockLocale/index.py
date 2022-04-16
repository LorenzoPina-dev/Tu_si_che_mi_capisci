from multiprocessing import Process, Pipe
from video import GestioneCam
import json
import socket


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
        parent_conn.send(json.loads(message)["key"])
    p.join()
    parent_conn.close()