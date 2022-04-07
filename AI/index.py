import numpy as np
import cv2

from simple_facerec import SimpleFacerec

#Load camera
cap = cv2.VideoCapture(0)


while True:
    ret, frame = cap.read()

    cv2.imshow("Frame", frame)

    key = cv2.waitKey(1)
    