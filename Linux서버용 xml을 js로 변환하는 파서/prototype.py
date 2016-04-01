import Component
import Item
import tkinter
import Event
main=Component.Component("parent",100,200)
second=Component.Component("child",100,200)
button1=Item.MyButton(main,"ok",10,10,10,20,"white",image)
button2=Item.MyButton(second,"cancel",0,0,10,20,"white",image)
Event.Event("onclick",button1,second)
Event.Event("onclick",button2,main)
button1.placeItem()
button2.placeItem()
tkinter.mainloop()
