import serial
import time
import sys
import Zebra

sp = serial.Serial('/dev/ttyUSB0', 9600)
def main():
    while True:
        count = sp.inWaiting()
        if count != 0:
            recv = sp.read(count)
            k = float(recv)
            print(k)
            f = open('test.txt', 'w')
            f.write(str(k) + '\n') 
            f.close()
            Zebra.print('test.txt')
            sp.flushInput()
            time.sleep(0.1)

if __name__ == '__main__':
    try:
        main()
    except Exception as e:
        print(e)
    
