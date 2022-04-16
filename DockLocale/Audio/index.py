import pyaudio
import wave
import socket
import requests
import json
from threading import Thread
import time
 
serverAddressPort= ("80.22.36.186", 12345)
bufferSize= 4064
UDPClientSocket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
FORMAT = pyaudio.paInt16
CHANNELS = 1
RATE = 44100
CHUNK = 512
RECORD_SECONDS = 5
WAVE_OUTPUT_FILENAME = "recordedFile.wav"
device_index = 2
audio = pyaudio.PyAudio()

print("----------------------record device list---------------------")
info = audio.get_host_api_info_by_index(0)
numdevices = info.get('deviceCount')
for i in range(0, numdevices):
        if (audio.get_device_info_by_host_api_device_index(0, i).get('maxInputChannels')) > 0:
            print("Input Device id ", i, " - ", audio.get_device_info_by_host_api_device_index(0, i).get('name'))

print("-------------------------------------------------------------")

index = int(input())
print("recording via index "+str(index))

stream = audio.open(format=FORMAT, channels=CHANNELS,
                rate=RATE, input=True,input_device_index = index,
                frames_per_buffer=CHUNK)
print ("recording started")
Recordframes = []
 
for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
    data = stream.read(CHUNK)
    Recordframes.append(data)
print ("recording stopped")
 
stream.stop_stream()
stream.close()
audio.terminate()
 
waveFile = wave.open(WAVE_OUTPUT_FILENAME, 'wb')
waveFile.setnchannels(CHANNELS)
waveFile.setsampwidth(audio.get_sample_size(FORMAT))
waveFile.setframerate(RATE)
waveFile.writeframes(b''.join(Recordframes))
waveFile.close()


def GetDispositivi():
    x = requests.get('http://80.22.36.186/'+keyk+'/dispositivi?tipo=1');
    y = json.loads(x.text)
    arr= y['result']['dispositivo']
    return arr;

def MandaServer(ByteArray):
    arr =[];
    for b in ByteArray:
        arr.append(int(b))
    j=json.dumps({"Tipo":"audio","KeyUtente":keyk,"Dati":arr})
    bytesToSend= str.encode(j)
    UDPClientSocket.sendto(bytesToSend, serverAddressPort)