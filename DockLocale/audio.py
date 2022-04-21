
from gestione_server import Server,gestisci_key
from threading import Thread
import time

def gestione_mic(conn):
    t={}
    termina_main=False
    t_key=Thread(target=GestisciKey, args=([termina_main,conn],))
    t_key.start()
    
"""
parent_conn, child_conn = Pipe()
gestione_mic(child_conn)

"""