import Component
import Item
import tkinter
import Event
WindowForm1=Component.Component("parent",526,336)
photo0=tkinter.PhotoImage(file='./pythonimage/SlideView2_0.png')
photo1=tkinter.PhotoImage(file='./pythonimage/SlideView2_1.png')
SlideView2image=[photo0,photo1]
SlideView2=Item.MySlideview(WindowForm1,126,200,325,70,SlideView2image)
SlideView2.placeItem()
tkinter.mainloop()
