from tkinter import *
class Component():
    """description of class"""
    component=''
    def __init__(self,type,width,height):
        size=str(width)+'x'+str(height)
        if type=="parent":
            self.component=Tk()
            self.component.geometry(size)
        elif type=="child":
            self.component=Toplevel()
            self.component.withdraw()
            self.component.geometry(size)
    def __del__(self):
        pass

