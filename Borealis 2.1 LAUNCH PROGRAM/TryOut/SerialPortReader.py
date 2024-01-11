import serial

# port, speed for test microController
ser = serial.Serial("COM3", 115200)

while True:
    cc = str(ser.readline())
    print(cc[2:][:-5])
