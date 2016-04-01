using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace ProjectTest
{
    class Parser
    {
        string type;
        Point point;
        Size size;
        string text;
        string name;
        string category;

        string str, value;
        StreamReader reader;

        private List<ComponentItem> clickCmpItem;

        StoryboardWindow win;
        Canvas canvas;
        public Parser()
        {
            clickCmpItem = new List<ComponentItem>();
            str = "";
            reader = new StreamReader("cpp-home.txt");
            start();
        }

        public Parser(string filename, Canvas _canvas, StoryboardWindow _win)
        {
            clickCmpItem = new List<ComponentItem>();
            str = "";
            reader = new StreamReader(filename);
            win = _win;
            canvas = _canvas;
            start();

        }

        public List<ComponentItem> getParsingData()
        {
            return clickCmpItem;
        }

        public void start()
        {
            value = get_StartTag();
            check_Value(value, "<Information>");
            //   <-
            cut_StartTag();

            value = get_StartTag();
            check_Value(value, "<type>");
            cut_StartTag();

            value = get_Value();
            //   <-
            type = value;
            cut_Value();

            value = get_EndTag();  //</type>
            cut_EndTag();
            value = get_EndTag();  //</Informantion>
            cut_EndTag();

            value = get_StartTag();
            while (check_Value(value, "<Component>") == 0)   //if Component
            {
                cut_StartTag();   // component태그가 들어왔으면 잘라내라
                Component cmpt = new Component(new ComponentType(ComponentType.WINDOW_FORM));

                value = get_StartTag();
                check_Value(value, "<name>");
                cut_StartTag();

                value = get_Value();
                name = value;
                cut_Value();
                cmpt.Name = name;

                value = get_EndTag();  //</name>
                cut_EndTag();

                value = get_StartTag();
                check_Value(value, "<position>");
                cut_StartTag();

                value = get_Value();
                string[] position = value.Split(',');

                cut_Value();

                value = get_EndTag();  //</position>
                cut_EndTag();

                value = get_StartTag();
                check_Value(value, "<size>");
                cut_StartTag();

                value = get_Value();
                string[] size = value.Split(',');
                cmpt.Height = double.Parse(size[1]);
                cmpt.Width = double.Parse(size[0]);
                str = cut_Value();

                value = get_EndTag();  //</size>
                cut_EndTag();
                cmpt.setSize(double.Parse(size[0]), double.Parse(size[1]));
                cmpt.setComponent();
                for (int i = 0; i < cmpt.viewDrow.Count; i++)
                {
                    canvas.Children.Add(cmpt.viewDrow[i]);
                    int zindex = Canvas.GetZIndex(canvas);
                    //Canvas.SetZIndex(cmpt.viewDrow[i], zindex + i);
                    Canvas.SetLeft(cmpt.viewDrow[i], double.Parse(position[0]));
                    Canvas.SetTop(cmpt.viewDrow[i], double.Parse(position[1]));
                    cmpt.viewDrow[i].Visibility = Visibility.Visible;
                    cmpt.viewDrow[i].super = cmpt;
                    cmpt.viewDrow[i].PreviewMouseDown += new MouseButtonEventHandler(win.componentItem_PreviewMouseDown);
                    cmpt.viewDrow[i].PreviewMouseMove += new MouseEventHandler(win.componentItem_PreviewMouseMove);
                    cmpt.viewDrow[i].PreviewMouseUp += new MouseButtonEventHandler(win.componentItem_PreviewMouseUp);
                }
                setting_Resize(cmpt.viewDrow[0]);
                cmpt.re = cmpt.viewDrow[0].re;
                cmpt.viewDrow[0].SizeChanged += new SizeChangedEventHandler(win.Component_SizeChanged);
                //set_MouseEvent(cmpt);
                Canvas.SetLeft(cmpt.viewDrow[1], double.Parse(position[0]) + 7);
                Canvas.SetTop(cmpt.viewDrow[1], double.Parse(position[1]) + 24);
                clickCmpItem.Add(cmpt.viewDrow[0]);

                value = get_StartTag();
                check_Value(value, "<itemList>");
                cut_StartTag();

                value = get_StartTag();
                while (check_Value(value, "<item>") == 0)
                {
                    cut_StartTag();
                    ////////////////////////////////////////////////////////////////////
                    value = get_StartTag();
                    check_Value(value, "<category>");
                    cut_StartTag();

                    value = get_Value();
                    category = value;

                    cut_Value();

                    value = get_EndTag();       //</category>
                    cut_EndTag();

                    value = get_StartTag();
                    check_Value(value, "<name>");
                    cut_StartTag();

                    value = get_Value();
                    name = value;
                    cut_Value();

                    value = get_EndTag();       //</name>
                    cut_EndTag();

                    value = get_StartTag();
                    check_Value(value, "<position>");
                    cut_StartTag();

                    value = get_Value();
                    position = value.Split(',');
                    cut_Value();

                    value = get_EndTag();       //</position>
                    cut_EndTag();

                    value = get_StartTag();
                    check_Value(value, "<size>");
                    cut_StartTag();

                    value = get_Value();
                    size = value.Split(',');
                    cut_Value();

                    string[] ContentName = { "button", "textbox", "textblock", "listbox", "listvew", "combobox", "slideview", "checkbox" };
                    int i;
                    for (i = 0; i < ContentName.Length; i++)
                    {
                        if(ContentName[i].Equals(category))
                            break;
                    }

                    Content cnt = new Content(i, double.Parse(size[0]), double.Parse(size[1]), new Point(double.Parse(position[0]), double.Parse(position[1])), cmpt);
                    cnt.PreviewMouseDown += new MouseButtonEventHandler(win.content_PreviewMouseDown);
                    cnt.PreviewMouseMove += new MouseEventHandler(win.content_PreviewMouseMove);
                    cnt.PreviewMouseUp += new MouseButtonEventHandler(win.content_PreviewMouseUp);
                    cmpt.childen.Add(cnt);
                    cnt.setMove(Canvas.GetZIndex(canvas));
                    setting_Resize(cnt);
                    cnt.setDistans((double)cnt.GetValue(Canvas.LeftProperty) - (double)cmpt.viewDrow[0].GetValue(Canvas.LeftProperty)
                        , (double)cnt.GetValue(Canvas.TopProperty) - (double)cmpt.viewDrow[0].GetValue(Canvas.TopProperty));
                    Canvas.SetLeft(cnt, double.Parse(position[0]) + (double)cmpt.viewDrow[0].GetValue(Canvas.LeftProperty));
                    Canvas.SetTop(cnt, double.Parse(position[1]) + (double)cmpt.viewDrow[0].GetValue(Canvas.TopProperty));
                    cnt.SizeChanged += new SizeChangedEventHandler(win.Content_SizeChanged);
                    canvas.Children.Add(cnt);
                    cnt.Name = name;
                    value = get_EndTag();       //</size>
                    cut_EndTag();
                    ////////////////////////////////////////////////////////////
                    //독립성분
                    if (category == "button")
                    {
                        value = get_StartTag();
                        check_Value(value, "<text>");
                        cut_StartTag();

                        value = get_Value();
                        cnt.Text = value;
                        text = value;
                        cut_Value();

                        value = get_EndTag();       //</text>
                        cut_EndTag();
                    }
                    else if (category == "textbox")
                    {
                        value = get_StartTag();
                        check_Value(value, "<text>");
                        cut_StartTag();

                        value = get_Value();
                        cnt.Text = value;
                        text = value;
                        cut_Value();

                        value = get_EndTag();       //</text>
                        cut_EndTag();
                    }
                    else if (category == "textblock")
                    {
                        value = get_StartTag();
                        check_Value(value, "<text>");
                        cut_StartTag();

                        value = get_Value();
                        cnt.Text = value;
                        text = value;
                        cut_Value();

                        value = get_EndTag();       //</text>
                        cut_EndTag();
                    }
                    else if (category == "combobox")
                    {
                        value = get_StartTag();
                        check_Value(value, "<text>");
                        cut_StartTag();

                        value = get_Value();
                        //<-
                        text = value;
                        cut_Value();

                        value = get_EndTag();       //</text>
                        cut_EndTag();
                    }
                    else if (category == "checkbox")
                    {
                        value = get_StartTag();
                        check_Value(value, "<text>");
                        cut_StartTag();

                        value = get_Value();
                        //<-
                        text = value;
                        cut_Value();

                        value = get_EndTag();       //</text>
                        cut_EndTag();
                    }
                    else if (category == "listbox")
                    {
                        value = get_StartTag();
                        check_Value(value, "<BoxList>");
                        cut_StartTag();

                        value = get_StartTag();
                        while (check_Value(value, "<bl>") == 0)
                        {
                            cut_StartTag();
                            /////////////////////////////////////////////////////////
                            value = get_Value();
                            cnt.listBox.Add(value);
                            cut_Value();
                            /////////////////////////////////////////////////////////
                            value = get_EndTag();   //</bl>
                            cut_EndTag();
                            value = get_StartTag();  //<bl> or </BoxList>
                        }
                    }
                    else if (category == "listview")
                    {
                        value = get_StartTag();
                        check_Value(value, "<Table>");
                        cut_StartTag();

                        value = get_Value();
                        //<-
                        cut_Value();

                        value = get_EndTag();  //</Table>
                        cut_EndTag();

                        value = get_StartTag();
                        check_Value(value, "<TableSize>");
                        cut_StartTag();

                        value = get_Value();
                        size = value.Split(',');
                        cnt.contentSize.Width = double.Parse(size[0]);
                        cnt.contentSize.Height = double.Parse(size[1]);
                        cut_Value();

                        value = get_EndTag();  //</TableSize>
                        cut_EndTag();

                        value = get_StartTag();
                        check_Value(value, "<ViewList>");
                        cut_StartTag();

                        value = get_StartTag();
                        while (check_Value(value, "<vl>") == 0)
                        {
                            cut_StartTag();
                            //////////////////////////////////////////////////
                            value = get_StartTag();
                            check_Value(value, "<size>");
                            cut_StartTag();


                            value = get_Value();
                            

                            cut_Value();

                            value = get_EndTag();
                            cut_EndTag();

                            value = get_StartTag();
                            check_Value(value, "<binary>");
                            cut_StartTag();

                            value = get_Value();
                            //<-
                            cut_Value();

                            value = get_EndTag();
                            cut_EndTag();

                            //////////////////////////////////////////////////
                            value = get_EndTag();   //</vl>
                            cut_EndTag();
                            value = get_StartTag();  //<vl> or </ViewList>
                        }


                    }
                    else if (category == "slideview")
                    {
                        value = get_StartTag();
                        check_Value(value, "<ViewList>");
                        cut_StartTag();

                        value = get_StartTag();
                        while (check_Value(value, "<vl>") == 0)
                        {
                            cut_StartTag();
                            //////////////////////////////////////////////////
                            value = get_StartTag();
                            check_Value(value, "<size>");
                            cut_StartTag();

                            value = get_Value();
                            int len = Int32.Parse(value);
                            cut_Value();

                            value = get_EndTag();
                            cut_EndTag();

                            value = get_StartTag();
                            check_Value(value, "<binary>");
                            cut_StartTag();

                            value = get_Value();
                            string[] image = value.Split(' ');
                            cut_Value();

                            value = get_EndTag();
                            cut_EndTag();

                            Stream stream = new MemoryStream();
                            BitmapImage img = new BitmapImage();
                            img.BeginInit();
                            for (int k = 0; k < len; k++)
                            {
                                stream.WriteByte((byte)Int32.Parse(image[k]));
                            }
                            img.StreamSource = stream;
                            img.EndInit();
                            cnt.listView.Add(img);
                            //////////////////////////////////////////////////
                            value = get_EndTag();   //</vl>
                            cut_EndTag();
                            value = get_StartTag();  //<vl> or </ViewList>
                        }
                        cut_StartTag();
                    }

                    ////////////////////////////////////////////////////////////
                    value = get_StartTag();
                    check_Value(value, "<eventList>");
                    cut_StartTag();

                    value = get_StartTag();
                    while (check_Value(value, "<el>") == 0)
                    {
                        cut_StartTag();
                        ///////////////////////////////////////////////
                        //이벤트 독립
                        value = get_StartTag();
                        check_Value(value, "<category>");
                        cut_StartTag();

                        value = get_Value();
                        //<-
                        cut_Value();

                        value = get_EndTag();
                        cut_EndTag();

                        value = get_StartTag();
                        check_Value(value, "<act>");
                        cut_StartTag();

                        value = get_Value();

                        cut_Value();
                        cnt.entList.setEventType(0);
                        value = get_EndTag();
                        cut_EndTag();

                        value = get_StartTag();
                        check_Value(value, "<passComponent>");
                        cut_StartTag();

                        value = get_StartTag();
                        check_Value(value, "<name>");
                        cut_StartTag();

                        value = get_Value();
                        cnt.entList.Name = name;
                        cut_Value();

                        value = get_EndTag();
                        cut_EndTag();

                        value = get_EndTag();
                        cut_EndTag();
                        ///////////////////////////////////////////////
                        value = get_EndTag();
                        cut_EndTag();
                        value = get_StartTag();//</el> or </eventList>
                    }
                    ////////////////////////////////////////////////////////////
                    value = get_EndTag();
                    cut_EndTag();
                    value = get_StartTag();   //<item>  or </itemList>
                    cut_StartTag();
                    value = get_StartTag();
                }


                value = get_EndTag();  //</component>
                cut_EndTag();
                value = get_StartTag();   //<compoenent>
                cut_StartTag();
                value = get_StartTag();
                if (value == null)
                    return;
            }







        }


        public int check_Value(string dst, string _src)
        {
            if (dst.CompareTo(_src) != 0)
            {
                Console.WriteLine("Error");
                return -1;
            }
            return 0;
        }


        public string get_StartTag()
        {
            if (str.Length == 0)
            {
                str = reader.ReadLine();
                if (str == null)
                    return null; ;
                str = str.Trim();
            }
            string text;
            int start = str.IndexOf('<');
            int end = str.IndexOf('>') + 1;
            if (start < 0)
            {
                start = 0;
            }
            if (end < 0)
            {
                end = 0;
            }
            text = str.Substring(start, end);
            return text;
        }

        public string cut_StartTag()
        {
            str = str.Substring(str.IndexOf('>') + 1);
            return str;
        }

        public string get_Value()
        {
            if (str.Length == 0)
            {
                str = reader.ReadLine();
                str = str.Trim();
            }
            string text;
            int end = str.IndexOf('<');
            if (end >= 0)
            {
                text = str.Substring(0, end);
                text = text.Trim();
                return text;
            }
            else
            {
                return text = str;
            }
        }

        public string cut_Value()
        {
            int start = str.IndexOf('<');
            if (start >= 0)
            {
                str = str.Substring(start);
                return str;
            }
            else
            {
                str = str.Substring(0, 0);
                return str;
            }
        }

        public string get_EndTag()
        {
            if (str.Length == 0)
            {
                str = reader.ReadLine();
                str = str.Trim();
            }
            string text;
            int start = str.IndexOf('<');
            int end = str.IndexOf('>') + 1;
            if (start < 0)
            {
                start = 0;
            }
            if (end < 0)
            {
                end = 0;
            }
            text = str.Substring(start, end);
            return text;
        }
        public string cut_EndTag()
        {
            str = str.Substring(str.IndexOf('>') + 1);
            return str;
        }

        void setting_Resize(Component _cmp)
        {
            _cmp.re = new Resize(_cmp, new Point(Canvas.GetLeft(_cmp), Canvas.GetTop(_cmp)));
            int zindex = canvas.Children.Count;
            for (int i = 0; i < 4; i++)
            {
                Line li = _cmp.re.get_Line(i);
                canvas.Children.Add(li);
                li.Visibility = Visibility.Visible;
                Canvas.SetZIndex(li, zindex + i);
                Canvas.SetLeft(li, _cmp.re.get_RectSize(_cmp.re.LINE_ARRY[i]).X);
                Canvas.SetTop(li, _cmp.re.get_RectSize(_cmp.re.LINE_ARRY[i]).Y);
            }
            for (int i = 0; i < 8; i++)
            {
                Image rt = _cmp.re.get_Rectangle(i);
                canvas.Children.Add(rt);
                rt.Visibility = Visibility.Visible;
                Canvas.SetZIndex(rt, zindex + i + 6);
                Canvas.SetLeft(rt, _cmp.re.get_RectSize(i).X);
                Canvas.SetTop(rt, _cmp.re.get_RectSize(i).Y);
            }
            Image _rt = _cmp.re.get_Rectangle(8);
            canvas.Children.Add(_rt);
            _rt.Visibility = Visibility.Hidden;
            Canvas.SetZIndex(_rt, zindex + 1);
            Canvas.SetLeft(_rt, _cmp.re.get_RectSize(8).X);
            Canvas.SetTop(_rt, _cmp.re.get_RectSize(8).Y);
            _cmp.re.visiblie(Visibility.Hidden);
        }

        void setting_Resize(Content _cmp)
        {
            _cmp.re = new Resize(_cmp, new Point(Canvas.GetLeft(_cmp), Canvas.GetTop(_cmp)));
            int zindex = canvas.Children.Count;
            for (int i = 0; i < 4; i++)
            {
                Line li = _cmp.re.get_Line(i);
                canvas.Children.Add(li);
                li.Visibility = Visibility.Visible;
                Canvas.SetZIndex(li, zindex + i);
                Canvas.SetLeft(li, _cmp.re.get_RectSize(_cmp.re.LINE_ARRY[i]).X);
                Canvas.SetTop(li, _cmp.re.get_RectSize(_cmp.re.LINE_ARRY[i]).Y);
            }
            for (int i = 0; i < 8; i++)
            {
                Image rt = _cmp.re.get_Rectangle(i);
                canvas.Children.Add(rt);
                rt.Visibility = Visibility.Visible;
                Canvas.SetZIndex(rt, zindex + i + 6);
                Canvas.SetLeft(rt, _cmp.re.get_RectSize(i).X);
                Canvas.SetTop(rt, _cmp.re.get_RectSize(i).Y);
            }
            Image _rt = _cmp.re.get_Rectangle(8);
            canvas.Children.Add(_rt);
            _rt.Visibility = Visibility.Hidden;
            Canvas.SetZIndex(_rt, zindex + 1);
            Canvas.SetLeft(_rt, _cmp.re.get_RectSize(8).X);
            Canvas.SetTop(_rt, _cmp.re.get_RectSize(8).Y);
            _cmp.re.visiblie(Visibility.Hidden);
        }


    }
}
