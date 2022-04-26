import os
import glob
from urllib.request import urlopen
import requests
import json
from keras.models import Sequential, Model, model_from_json
import matplotlib.pyplot as plt
import keras 
import pickle
import wave  # !pip install wave
import os
import pandas as pd
import numpy as np
import sys
import warnings
import librosa
import librosa.display
import IPython.display as ipd  # To play sound in the notebook
import pyaudio
import tensorflow as tf

keyk="a2444840-cb9a-479d-bd3f-a4fa4a2f23f8"
host="80.22.36.186"
idEmozioni=["angry","happy","sad","disgust","fear","surprise","neutral"]
file_model="D:\scuola\gestioneProgetto/test/model_json.json"
file_weiht="D:\scuola\gestioneProgetto/test\saved_models/Emotion_Model.h5"
filename = 'D:\scuola\gestioneProgetto/test/labels'
class SimpleEmozionrec:
    def __init__(self):
        json_file = open(file_model, 'r')
        loaded_model_json = json_file.read()
        json_file.close()
        self.loaded_model = model_from_json(loaded_model_json)

        # load weights into new model
        self.loaded_model.load_weights(file_weiht)
        print("Loaded model from disk")

        # the optimiser
        optim = tf.keras.optimizers.RMSprop(learning_rate=0.0001)
        self.loaded_model.compile(loss='categorical_crossentropy', optimizer=optim, metrics=['accuracy'])
        
    def add_encoding_image(self,id,path_img):
        try:
            img=GetImmagine(path_img)
            img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)
            img_encoding = face_recognition.face_encodings(img)[0]
            SetEncodingVolto(id,img_encoding)
            self.known_face_encodings.append(img_encoding)
            self.known_face_names.append(id)
        except Exception as e:
            print("errore"+ format(e))
        print("Encoding images loaded")

    def detect_emotion(self, file):
        X, sample_rate = librosa.load(file
                                    ,res_type='kaiser_fast'
                                    ,duration=2.5
                                    ,sr=44100
                                    ,offset=0.5
                                    )

        sample_rate = np.array(sample_rate)
        mfccs = np.mean(librosa.feature.mfcc(y=X, sr=sample_rate, n_mfcc=13),axis=0)
        newdf = pd.DataFrame(data=mfccs).T
        print(newdf)

        # Apply predictions
        newdf= np.expand_dims(newdf, axis=2)
        newpred = loaded_model.predict(newdf, 
                                batch_size=16, 
                                verbose=1)

        print(newpred)


        
        infile = open(filename,'rb')
        lb = pickle.load(infile)
        infile.close()

        # Get the final predicted label
        final = newpred.argmax(axis=1)
        final = final.astype(int).flatten()
        final = (lb.inverse_transform((final)))
        print(final) #emo(final) #gender(final) 
        return idEmozioni.index(final[0].split("_")[1])