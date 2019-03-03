import cups
import time
import subprocess


conn = cups.Connection()
printers = conn.getPrinters()
printer_name = list(printers.keys())[0]

def print(path):
    printID = conn.printFile(printer_name, path, 'ki', {'fit-to-page':'False', 'orientation-requested':'6'})


if __name__ == "__main__":
    print('/home/pi/Pictures/pi_logo_medium.png')
