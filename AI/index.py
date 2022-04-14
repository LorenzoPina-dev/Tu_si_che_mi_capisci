import cv2

from simple_facerec import SimpleFacerec

#le immagini che vengono salvate devono chiamarsi nomedellapersona.ext

#funzione per importare tutti i volti che sono nella cartella
sfr = SimpleFacerec()
sfr.load_encoding_images("images/") #nome della cartella dove sono le foto



#Load camera
cap = cv2.VideoCapture(0) #indice della camera, (se più camere)


#otteniamo lo stream in real time
while True:
    ret, frame = cap.read()

    face_locationes, face_names =sfr.detect_known_faces(frame)
    for face_loc, name in zip(face_locationes, face_names):
        #print(face_loc) #la posizione del volto nella cam
        y1, x1, y2, x2 = face_loc[0], face_loc[1], face_loc[2], face_loc[3]

        cv2.putText(frame, name, (x1,y1 - 10), cv2.FONT_HERSHEY_DUPLEX, 1, (0,0,0), 2)
        cv2.rectangle(frame, (x1,y1), (x2,y2), (0,0,255), 4)


    cv2.imshow("Frame", frame)

    key = cv2.waitKey(1) #ogni quanti millisecondi andare al frame successivo

cap.release()
cv2.destroyAllWindows() #mostra in una finestra cosa vede la cam


    