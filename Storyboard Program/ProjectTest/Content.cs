using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectTest
{
    class Content : StoryboardItem
    {
        public EventListner entList;
        private int contentType;
        public Resize re;
        private BitmapImage bitmap;
        private double width_dis;
        private double height_dis;
        public Component superCnt;

        

        public List<BitmapImage> listView;
        public List<string> listBox;
        public string Text;
        public Size contentSize;

        public Content(int _contentType,double width, double height, Point _position, Component _super){
            entList = new EventListner();
            listView = new List<BitmapImage>();
            listBox = new List<string>();
            contentSize = new Size();
            this.BeginInit();
            this.Width = width;
            this.Height = height;
            contentType = _contentType;
            setImage();
            superCnt = _super;
        }

        public Component getSuperClass()
        {
            return superCnt;
        }
        public void setDistans(double _width, double _height)
        {
            width_dis = _width;
            height_dis = _height;
        }
        public double getWidthDistance()
        {
            return width_dis;
        }
        public double getHeightDistance()
        {
            return height_dis;
        }
        private void setImage()
        {
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            
            switch (contentType)
            {
                case 0://Button
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\btn.png");
                    break;
                case 1://Text Box
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\textbox.png");
                    break;
                case 2://Text Block
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\textblock.png");
                    break;
                case 3://List Box
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\window_background.png");
                    break;
                case 4://List View
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\window_background.png");
                    break;
                case 5://Combo Box
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\combo.png");
                    break;
                case 6://Slide View
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\sildeview.png");
                    break;
                case 7://Check Box
                    this.Width = 40;
                    this.Height = 45;
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\checkbox.png");
                    break;
            }
            bitmap.EndInit();
            this.Stretch = Stretch.Fill;
            this.Source = bitmap;
            this.EndInit();
        }

        public void setMove(int zindex)
        {
            Canvas.SetLeft(this, position.X);
            Canvas.SetTop(this, position.Y);
            Canvas.SetZIndex(this, zindex);
        }

        public void setMove(Point _position)
        {
            position = _position;
            Canvas.SetLeft(this, position.X);
            Canvas.SetTop(this, position.Y);
        }

        public int getItemNumber()
        {
            return contentType;
        }

        public string getItemType()
        {
            switch (contentType)
            {
                case 0://Button
                    return "button";
                case 1://Text Box
                    return "textbox";
                case 2://Text Block
                    return "textblock";
                case 3://List Box
                    return "listbox";
                case 4://List View
                    return "listview";
                case 5://Combo Box
                    return "combobox";
                case 6://Slide View
                    return "slideview";
                case 7://Check Box
                    return "checkbox";
            }
            return "";
        }

        
    }
}
