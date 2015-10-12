from PicCapProc import APOD, Bing

__author__ = 'hirep'
try:
    a = APOD()
    a.get_image()
    print("NASA downloaded")
except:
    print("Failed downloading NASA")

try:    
    b = Bing()
    b.get_image()
    print("BING downloaded")
except:
    print("Failed downloading BING.")
