from tkinter import *
from tkinter.ttk import *


class MyButton():
    """description of class"""
    item=''
    frame=''
    anboim=''
    posX=int()
    posY=int()
    width=int()
    height=int()
    rgb=''
    image=''
    def __init__(self,mst,name,_posX,_posY,_width,_height,_rgb,_image):
        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.height=_height
        self.image=_image
        self.rgb=_rgb
        self.anboim=PhotoImage(file='anboim.png')


        self.frame=Frame(master=mst.component,width=self.width,height=self.height)
        self.frame.propagate(False)

        self.item=Button(master=self.frame,text=name,image=self.anboim,compound=CENTER)
        if self.image!='0':
            self.item.config(image=self.image,compound=CENTER)
            
            

        self.item.pack(fill=BOTH)

    def __del__(self):
        pass

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)
        

class MyTextbox():
    item=''
    frame=''
    posX=int()
    posY=int()
    width=int()
    height=int()
    rgb=''
    def __init__(self,mst,_posX,_posY,_width,_height,_rgb):
        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.height=_height
        self.rgb=_rgb


        self.frame=Frame(master=mst.component,width=self.width,height=self.height)
        self.frame.propagate(False)

        self.item=Text(master=self.frame,width=self.width,height=self.height)
        if self.rgb!='0':
            self.item.config(bg=self.rgb)

        self.item.pack()

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)

class MyTextblock():
    item = ''
    frame = ''
    posX = int()
    posY = int()
    width = int()
    height = int()
    rgb = ''
    image = ''
    text=''
    anboim=''
    
    def __init__(self,mst,_posX,_posY,_width,_height,_rgb,_image,_text):
        self.posX = _posX
        self.posY = _posY
        self.width = _width
        self.height = _height
        self.image = _image
        self.rgb = _rgb
        self.text=_text
        self.anboim=PhotoImage(file='anboim.png')

        self.frame = Frame(master=mst.component,width=self.width,height=self.height)
        self.frame.propagate(False)

        self.item = Label(master=self.frame,width=300,text=self.text,image=self.anboim,compound=CENTER)
        if self.rgb!='0':
            self.item.config(bg=self.rgb)
        if self.image!='0':
            self.item.config(image=self.image,compound=CENTER)
            
        self.item.pack(fill=BOTH)

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)

class MyListbox():
    item = ''
    frame = ''
    posX = int()
    posY = int()
    width = int()
    height = int()
    rgb = ''
    values=[]
    def __init__(self,mst,_posX,_posY,_width,_height,_rgb,_values):
        self.posX = _posX
        self.posY = _posY
        self.width = _width
        self.height = _height
        self.rgb = _rgb
        self.values=_values

        self.frame = Frame(master=mst.component,width=self.width,height=self.height)
        self.frame.propagate(False)

        self.item = Listbox(master=self.frame,width=self.width,height=self.height)
        if self.rgb!='0':
            self.item = Listbox(master=self.frame,bg=self.rgb,width=self.width,height=self.height)

        for data in self.values:
            self.item.insert(END,data)

        self.item.pack(fill=BOTH)

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)

class MyListview():
    item=''
    frame=''
    yScroll=''
    posX=int()
    posY=int()
    width=int()
    height=int()
    image=[]
    m=int()
    n=int()
    indexX=int()
    indexY=int()
    rgb=''

    def __init__(self,mst,_posX,_posY,_width,_height,_m,_n,_indexX,_indexY,_rgb,_image):

        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.height=_height
        self.m=_m   #행
        self.n=_n   #열
        self.rgb=_rgb
        self.image=_image
        self.indexX=_indexX   #한칸의 너비
        self.indexY=_indexY   #한칸의 높이

        #self.frame=Frame(mst)   #임시용
        self.frame=Frame(mst.component)  #실제사용될소스

        self.item=Canvas(self.frame,width=self.width,height=self.height) #그려질 캔버스

        for i in range(0,int((len(self.image)/self.n)+2),1):       #라인긋는부분
            print(i)
            self.item.create_line(0,i*self.height,self.width,i*self.height,fill='white')
            if i!=int((len(self.image)/self.n)+1):
                for j in range (0,self.n+1,1):
                    print("j="+str(j))
                    self.item.create_line(j*int(self.width/self.n),i*self.height,j*int(self.width/self.n),(i+1)*self.height,fill='white')

                
        if self.rgb!="0":
            self.item.config(bg=self.rgb)

        if len(self.image)!=0:                                #스크롤바를 오토로 생성함
            if len(self.image)>(self.m*self.n):
                self.yScroll=Scrollbar(self.frame)
                self.yScroll.pack(side=RIGHT,fill=Y)
                self.item.pack(side=LEFT,fill=BOTH)
            
                scrollHeight=int()
                if (len(self.image)%self.n) >0:
                    scrollHeight=int((len(self.image)/self.n)+1)
                else:
                    scrollHeight=int((len(self.image)/self.n))

                self.item.config(yscrollcommand=self.yScroll.set,scrollregion=(0,0,self.width,int(scrollHeight*self.indexY)))
                self.yScroll.config(command=self.item.yview)

            else:
                self.item.pack(fill=BOTH)

            self.drawImage()  #이미지 올리는 함수
            
        else:
            self.item.pack(fill=BOTH)
    def __del__(self):
        pass

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)

    def drawImage(self):   #이미지 올리는 함수
        print("drawstart")

        index=0
        for i in range(1,int((len(self.image)/self.n)+2),1):
            for j in range(1,self.n+1,1):
                x=((self.width/self.n)*j)-((self.width/self.n)/2)
                y=(self.height*i)-(self.height/2)
                print("x="+str(x)+","+"y="+str(y))
                self.item.create_image(x,y,image=self.image[index])
                index+=1
                if(index>=len(self.image)):
                    return
            

class MyCheckbox():
    item=''
    frame=''
    posX=int()
    posY=int()
    width=int()
    height=int()
    text=''
    rgb=''
    def __init__(self,mst,_posX,_posY,_width,_height,_rgb,_text):
        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.height=_height
        self.rgb=_rgb
        self.text=_text

        self.frame=Frame(mst.component,width=self.width,height=self.height)
        self.frame.propagate(False)
        self.item=Checkbutton(self.frame,text=self.text,width=self.width)
        if self.rgb!='0':
            self.item.config(bg=self.rgb)

        self.item.pack(fill=BOTH)
        
    def __del__(self):
        pass

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)



class MyCombobox():
    item=''
    posX=int()
    posY=int()
    width=int()
    text=''
    frame=''
    def __init__(self,mst,_posX,_posY,_width,_text):
        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.text=_text

        self.frame = Frame(master=mst.component,width=self.width,height=17)
        self.frame.propagate(False)
        
        self.item=Combobox(self.frame,width=self.width)
        self.text=tuple(self.text)
        self.item['values']=self.text
        self.item.pack(fill=BOTH)
    def __del__(self):
        pass

    def placeItem(self):
        self.frame.place(x=self.posX,y=self.posY)

class MySlideview():

    item=''
    posX=int()
    posY=int()
    width=int()
    height=int()
    image=[]
    index=0
    postPointX=int()
    postPointY=int()
    prevPointX=int()
    prevPointY=int()
    startPointX=int()
    startPointY=int()
    movePointX=int()
    movePointY=int()
    imageTag=''
    def __init__(self,mst,_posX,_posY,_width,_height,_image):
        self.posX=_posX
        self.posY=_posY
        self.width=_width
        self.height=_height
        self.image=_image

        self.item=Canvas(mst.component,width=self.width,height=self.height,bg='black')
        if len(self.image)!=0:
            x=self.width/2
            y=self.height/2
            self.imageTag=self.item.create_image(x,y,image=self.image[self.index])
            self.item.bind('<Button-1>',self.push)
            self.item.bind('<B1-Motion>',self.mov)
            self.item.bind('<ButtonRelease-1>',self.release)

    def __del__(self):
        pass

    def push(self,event):
        self.postPointX=event.x
        self.postPointY=event.y
        self.startPointX=event.x

    def mov(self,event):
        self.prevPointX=event.x
        self.prevPointY=event.y

        self.movePointX=self.prevPointX-self.postPointX
        self.movePointY=self.prevPointY-self.postPointY

        self.item.move(self.imageTag,self.movePointX,0)
        self.item.update()
        self.postPointX=self.prevPointX
        self.postPointY=self.prevPointY

    def release(self,event):
        
        if event.x-self.startPointX>0:
            if self.index<len(self.image)-1:
                print(self.index)
                self.index+=1
            pass
        elif event.x-self.startPointX<0:
            if self.index>0:
                print(self.index)
                self.index-=1
            pass
        #이미지 이동 및 변경
        x=self.width/2
        y=self.height/2
        self.item.delete(self.imageTag)
        self.imageTag=self.item.create_image(x,y,image=self.image[self.index])

    def placeItem(self):
        self.item.place(x=self.posX,y=self.posY)
