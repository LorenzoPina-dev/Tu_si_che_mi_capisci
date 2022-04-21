
from gestione_server import Server,gestisci_key
from threading import Thread
import time

keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"

"""
per avere i dispositivi da cui ascoltare l'audio:
arr=Server.get_dispositivi(keyk)


per inviare i risultati è:
Server.invia_risultati(b64_string,keyk,nome["Id"])
nome= oggetto derivato fa un foreach sul risultato del get_dispositivi()


bozza su come ottenere il flusso audio è nel file ./Audio/index.py
"""

def gestione_mic(conn):
    t={}
    termina_main=False
    t_key=Thread(target=GestisciKey, args=([termina_main,conn],))
    t_key.start()
    
"""
parent_conn, child_conn = Pipe()
gestione_mic(child_conn)

"""