using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace ProjectTest
{
    class Component : StoryboardItem
    {
        private ComponentType com_type;

        public List<Content> childen;
        public List<ComponentItem> viewDrow;
        public Resize re;

        private BitmapImage bitmap;
        private ComponentItem img;


        public Component()
        {
            childen = new List<Content>();
            viewDrow = new List<ComponentItem>();
            Width = 100;
            Height = 100;
            com_type = new ComponentType(ComponentType.WINDOW_FORM);
        }


        public Component(ComponentType _com)
        {
            childen = new List<Content>();
            viewDrow = new List<ComponentItem>();
            Width = 100;
            Height = 100;
            com_type = _com;
        }


        public void setComponent()
        {
            switch (com_type.typename)
            {
                case ComponentType.MOBLIE:
                    break;
                case ComponentType.WINDOW_FORM:
                    winform();
                    break;
                case ComponentType.WEB_PAGE:
                    break;
            }
        }

        public void setSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public bool getComponentType()
        {
            if (this.Name.Equals("activity_background"))
            {
                return true;
            }
            return false;
        }

        public void setMoveComponent(Point _position, object sender)
        {
            switch (com_type.typename)
            {
                case ComponentType.MOBLIE:
                    break;
                case ComponentType.WINDOW_FORM:

                    if (((Image)sender).Name.Equals("activity_background"))
                    {
                        for (int i = 0; i < childen.Count; i++)
                        {
                            childen[i].SetValue(Canvas.LeftProperty, _position.X + childen[i].getWidthDistance());
                            childen[i].SetValue(Canvas.TopProperty, _position.Y + childen[i].getHeightDistance());
                            childen[i].SetValue(Canvas.ZIndexProperty, (int)viewDrow[0].GetValue(Canvas.ZIndexProperty) * 10000 + i);
                            childen[i].re.SetPosition(_position.X + childen[i].getWidthDistance(), _position.Y + childen[i].getHeightDistance(), 0);
                            childen[i].re.transformation(childen[i]);
                        }
                        viewDrow[0].SetValue(Canvas.LeftProperty, _position.X);
                        viewDrow[0].SetValue(Canvas.TopProperty, _position.Y);
                        viewDrow[0].re.SetPosition(_position.X, _position.Y, 0);
                        viewDrow[1].SetValue(Canvas.LeftProperty, _position.X + 7);
                        viewDrow[1].SetValue(Canvas.TopProperty, _position.Y + 24);

                    }
                    else
                    {
                        for (int i = 0; i < childen.Count; i++)
                        {
                            childen[i].SetValue(Canvas.LeftProperty, _position.X + childen[i].getWidthDistance() - 7);
                            childen[i].SetValue(Canvas.TopProperty, _position.Y + childen[i].getHeightDistance() - 24);
                            childen[i].SetValue(Canvas.ZIndexProperty, (int)viewDrow[0].GetValue(Canvas.ZIndexProperty) * 10000 + i);
                            childen[i].re.SetPosition(_position.X + childen[i].getWidthDistance() - 7, _position.Y + childen[i].getHeightDistance() - 24, 0);
                            childen[i].re.transformation(childen[i]);
                        }
                        viewDrow[0].SetValue(Canvas.LeftProperty, _position.X - 7);
                        viewDrow[0].SetValue(Canvas.TopProperty, _position.Y - 24);
                        viewDrow[0].re.SetPosition(_position.X - 7, _position.Y - 24, 0);
                        viewDrow[1].SetValue(Canvas.LeftProperty, _position.X);
                        viewDrow[1].SetValue(Canvas.TopProperty, _position.Y);
                    }

                    break;
                case ComponentType.WEB_PAGE:
                    break;
            }


        }

        public void setMoveComponent()
        {
            Point _position;
            _position = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            for (int i = 0; i < viewDrow.Count; i++)
            {
                ComponentItem test = viewDrow[i];
                test.SetValue(Canvas.LeftProperty, _position.X);
                test.SetValue(Canvas.TopProperty, _position.Y);
            }
        }

        public void setMoveItem(Point _position)
        {

            for (int i = 0; i < childen.Count; i++)
            {
                Content test = childen[i];
                test.SetValue(Canvas.LeftProperty, _position.X);
                test.SetValue(Canvas.TopProperty, _position.Y);
            }


        }

        private void activity()
        {

        }
        private void winform()
        {
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\window_background.png");
            bitmap.EndInit();
            img = new ComponentItem();
            img.BeginInit();
            img.Name = "activity_background";
            img.Source = bitmap;
            img.Width = Width;
            img.Height = Height; 
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.super = this;
            img.EndInit();
            viewDrow.Add(img);
            img.SizeChanged += new SizeChangedEventHandler(img_SizeChanged);
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\window_main.png");
            bitmap.EndInit();
            ComponentItem temp = img;
            img = new ComponentItem();
            img.BeginInit();
            img.Name = "activity_main";
            img.Source = bitmap;
            img.Width = Width - 14;
            img.Height = Height - 32;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.super = this;
            img.EndInit();
            viewDrow.Add(img);

        }

        void img_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ComponentItem temp = (ComponentItem)sender;
            ((Component)temp.super).viewDrow[1].SetValue(WidthProperty, temp.Width - 14);
            ((Component)temp.super).viewDrow[1].SetValue(HeightProperty, temp.Height - 32);
            Canvas.SetLeft(((Component)temp.super).viewDrow[1], Canvas.GetLeft(temp) + 7);
            Canvas.SetTop(((Component)temp.super).viewDrow[1], Canvas.GetTop(temp) + 24);
        }
        private void webpage()
        {

        }
    }
}
