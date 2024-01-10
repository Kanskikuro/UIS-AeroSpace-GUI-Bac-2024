import csv
import serial
from datetime import datetime
import time

# port, speed for test microController
ser = serial.Serial("COM3", 115200)
results = open("DataFromMicroController.txt", "w")

isOpened = False
i = 0
startTime = time.time()
while True:
    i += 1
    dataLine = ""
    cc = str(ser.readline())
    print(time.time() - startTime, cc)
    print()
    if not isOpened:
        isOpened = True
        dataLine = "Time[s] Temp[graderC] Pressure[mbar] altitude[m] accX[mg] accY[mg] accZ[mg] gyroX[degrees/s] gyroY[" \
                   "degrees/s] gyroZ[degrees/s] magX[µT] magY[µT] magZ[µT]\n"
    if isOpened and i >= 2:
        dataLine += str('{0:.2f}'.format(time.time() - startTime)) + " " + cc[2:][:-5] + "\n"

    results.write(dataLine)

