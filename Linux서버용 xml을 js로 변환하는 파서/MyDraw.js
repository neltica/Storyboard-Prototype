function MyDraw(){
var str='';
str+=MyComponent(str,0,0,100,200,0);
str+=MyComponent(str,200,200,100,200,0);
str+=MyButton(str,10,10,10,20,"button1",0);
str+=MyButton(str,0,0,100,200,"button2",0);
document.getElementById("svgTag").innerHTML = str;}
