import csv
import matplotlib.pyplot
import pandas

# import serial

# port, speed for test microController
# ser = serial.Serial("COM3", 115200)
###
# while True:
#    cc = str(ser.readline())
#    print(cc[2:][:-5])
###
time = []
altitude = []


with open("DataFromMicroController.txt", "r") as file:
    les = csv.reader(file, delimiter=" ")
    next(les)  # skips first line
    for line in les:
        print(line)
        time.append(float(line[0]))
        altitude.append(float(line[3]))


df = pandas.DataFrame({
        "altitude": altitude,
        "time": time
    })

matplotlib.pyplot.plot("time", "altitude", data=df, linestyle='-')
matplotlib.pyplot.show()
