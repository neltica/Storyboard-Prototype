from tkinter import *

class Event(object):
    """description of class"""
    event=''

    def __init__(self,type,item,src,dst):
        if type=="onclick":
            def click(event):
                dst.component.deiconify()
                src.component.withdraw()
            self.event=item.item.bind("<Button-1>",click)

    def __del__(self):
        pass



