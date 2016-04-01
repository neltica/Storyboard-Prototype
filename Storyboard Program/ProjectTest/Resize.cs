using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Input;

namespace ProjectTest
{
    public class Resize : Image
    {
        public const int NW = 0;
        public const int N = 1;
        public const int NE = 2;
        public const int W = 3;
        public const int E = 4;
        public const int SW = 5;
        public const int S = 6;
        public const int SE = 7;
        
        public const int BODER = 8;

        public const int LINE_DEFAULT = 9;

        public const int L_NW = 0;
        public const int L_NE = 1;
        public const int L_SW = 2;
        public const int L_SE = 3;

        public int[] LINE_ARRY = new int[] {0,2,5,7};



        private const int DRAG_HANDLE_SIZE = 7;

        private Point position;
        private Size size;



        private Image[] point;
        private Point[] point_way;
        private Line[] boder;


        private BitmapImage bitmap;


        private double canvasLeft;
        private double canvasTop;
        private double deltaLeft;
        private double deltaTop;

        private double standardLeft;
        private double standardTop;

        private double WidthSize;
        private double HeightSize;

        public Image baseimage { get; set; }

        public StoryboardItem _baseimage;

        public Boolean isVisible;

        public bool isEvnet;

        public Resize(object sender, Point _position)
        {
            isEvnet = false;

            _baseimage = (StoryboardItem)sender;
            Image img = (Image)sender;
            this.position = _position;
            this.size = new Size(img.Width, img.Height);

            point = new Image[9];
            point_way = new Point[9];
            boder = new Line[4];

            for (int i = 0; i < 8; i++)
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\point.png");
                bitmap.EndInit();

                point[i] = new Image();
                point[i].BeginInit();
                point[i].Source = bitmap;
                point[i].Width = DRAG_HANDLE_SIZE;
                point[i].Height = DRAG_HANDLE_SIZE;
                point[i].Visibility = Visibility.Hidden;
                point[i].EndInit();


            }
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\boder.png");
            bitmap.EndInit();
            point[BODER] = new Image();
            point[BODER].BeginInit();
            point[BODER].Source = bitmap;
            point[BODER].Width = size.Width + DRAG_HANDLE_SIZE;
            point[BODER].Height = size.Height + DRAG_HANDLE_SIZE;
            point[BODER].Stretch = Stretch.Fill;
            //point[BODER].Visibility = Visibility.Visible;
            point[BODER].EndInit();

            for (int i = 0; i < 4; i++)
            {
                boder[i] = new Line();
                SolidColorBrush _brush = new SolidColorBrush();
                _brush.Color = Colors.Black;
                boder[i].Stroke = _brush;
                boder[i].StrokeThickness = 1;
                boder[i].StrokeDashArray = DoubleCollection.Parse("5,5");
                boder[i].Visibility = Visibility.Hidden;
            }

            setting_postion();
            setMouseEvent();
            
        }
        public void SetSize(Size _size)
        {
            size = _size;
        }

        public void SetSize(double _Width, double _Height, double plus)
        {
            Size _size;
            if (_Width == -1)
                _size = new Size(size.Width + plus, _Height);
            else if (_Height == -1)
                _size = new Size(_Width, size.Height + plus);
            else
                _size = new Size(_Width, _Height);
            size = _size;
        }

        public void SetPosition(double _x, double _y, double plus)
        {
            Point _point;
            if (_x == -1)
                _point = new Point(position.X + plus, _y);
            else if (_y == -1)
                _point = new Point(_x, position.Y + plus);
            else if (_y == -1 && _x == -1)
                _point = position;
            else
                _point = new Point(_x, _y);

            position = _point;
            setting_postion();
        }

        public void SetPosition(Point _position)
        {
            position = _position;
            setting_postion();
        }

        private void setting_postion()
        {
            point_way[NW] =
                new Point(
                    position.X - DRAG_HANDLE_SIZE,
                    position.Y - DRAG_HANDLE_SIZE
                    );
            point_way[N] =
                new Point(
                    position.X + size.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    position.Y - DRAG_HANDLE_SIZE
                    );
            point_way[NE] =
                new Point(
                    position.X + size.Width,
                    position.Y - DRAG_HANDLE_SIZE
                    );
            point_way[W] =
                new Point(
                    position.X - DRAG_HANDLE_SIZE,
                    position.Y + size.Height / 2 - DRAG_HANDLE_SIZE / 2
                    );
            point_way[E] =
                new Point(
                    position.X + size.Width,
                    position.Y + size.Height / 2 - DRAG_HANDLE_SIZE / 2
                    );
            point_way[SW] =
                new Point(
                    position.X - DRAG_HANDLE_SIZE,
                    position.Y + size.Height
                    );
            point_way[S] =
                new Point(
                    position.X + size.Width / 2 - DRAG_HANDLE_SIZE / 2,
                    position.Y + size.Height
                    );
            point_way[SE] =
                new Point
                    (position.X + size.Width,
                    position.Y + size.Height
                    );
            point_way[BODER] =
                new Point
                    (position.X - DRAG_HANDLE_SIZE / 2,
                    position.Y - DRAG_HANDLE_SIZE / 2
                    );
            
            boder[L_NW].X1 = 0;
            boder[L_NW].Y1 = DRAG_HANDLE_SIZE / 2;
            boder[L_NW].X2 = size.Width;
            boder[L_NW].Y2 = DRAG_HANDLE_SIZE / 2;

            boder[L_NE].X1 = DRAG_HANDLE_SIZE / 2;
            boder[L_NE].Y1 = 0;
            boder[L_NE].X2 = DRAG_HANDLE_SIZE / 2;
            boder[L_NE].Y2 = size.Height + DRAG_HANDLE_SIZE;

            boder[L_SE].X1 = 0;
            boder[L_SE].Y1 = DRAG_HANDLE_SIZE / 2;
            boder[L_SE].X2 = -size.Width;
            boder[L_SE].Y2 = DRAG_HANDLE_SIZE / 2;


            boder[L_SW].X1 = DRAG_HANDLE_SIZE / 2;
            boder[L_SW].Y1 = 0;
            boder[L_SW].X2 = DRAG_HANDLE_SIZE / 2;
            boder[L_SW].Y2 = -size.Height;

        }

        public Image get_Rectangle(int way)
        {
            return point[way];
        }

        public Line get_Line(int way)
        {
            return boder[way];
        }

        public Point get_RectSize(int way)
        {
            return point_way[way];
        }

        /*
         * 
         * 사이즈 재조정을 위한 이벤트
         * 
         * 
         */
        // ------------------------이벤트 등록 부분---------------------------
        private void setMouseEvent()
        {
            point[NW].PreviewMouseDown += new MouseButtonEventHandler(NW_PreviewMouseDown);
            point[NW].PreviewMouseMove += new MouseEventHandler(NW_PreviewMouseMove);
            point[NW].PreviewMouseUp += new MouseButtonEventHandler(NW_PreviewMouseUp);

            point[N].PreviewMouseDown += new MouseButtonEventHandler(N_PreviewMouseDown);
            point[N].PreviewMouseMove += new MouseEventHandler(N_PreviewMouseMove);
            point[N].PreviewMouseUp += new MouseButtonEventHandler(N_PreviewMouseUp);

            point[NE].PreviewMouseDown += new MouseButtonEventHandler(NE_PreviewMouseDown);
            point[NE].PreviewMouseMove += new MouseEventHandler(NE_PreviewMouseMove);
            point[NE].PreviewMouseUp += new MouseButtonEventHandler(NE_PreviewMouseUp);

            point[W].PreviewMouseDown += new MouseButtonEventHandler(W_PreviewMouseDown);
            point[W].PreviewMouseMove += new MouseEventHandler(W_PreviewMouseMove);
            point[W].PreviewMouseUp += new MouseButtonEventHandler(W_PreviewMouseUp);

            point[E].PreviewMouseDown += new MouseButtonEventHandler(E_PreviewMouseDown);
            point[E].PreviewMouseMove += new MouseEventHandler(E_PreviewMouseMove);
            point[E].PreviewMouseUp += new MouseButtonEventHandler(E_PreviewMouseUp);

            point[SW].PreviewMouseDown += new MouseButtonEventHandler(SW_PreviewMouseDown);
            point[SW].PreviewMouseMove += new MouseEventHandler(SW_PreviewMouseMove);
            point[SW].PreviewMouseUp += new MouseButtonEventHandler(SW_PreviewMouseUp);

            point[S].PreviewMouseDown += new MouseButtonEventHandler(S_PreviewMouseDown);
            point[S].PreviewMouseMove += new MouseEventHandler(S_PreviewMouseMove);
            point[S].PreviewMouseUp += new MouseButtonEventHandler(S_PreviewMouseUp);

            point[SE].PreviewMouseDown += new MouseButtonEventHandler(SE_PreviewMouseDown);
            point[SE].PreviewMouseMove += new MouseEventHandler(SE_PreviewMouseMove);
            point[SE].PreviewMouseUp += new MouseButtonEventHandler(SE_PreviewMouseUp);

            for (int i = 0; i < 9; i++)
            {
                point[i].LostMouseCapture += new MouseEventHandler(Resize_LostMouseCapture);
            }
        }
        // ------------------------이벤트 등록 부분---------------------------


        // -------------------------------공통--------------------------------
        private void Resize_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
        }
        // -------------------------------공통--------------------------------

        // -------------------------------북서--------------------------------
        private void NW_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void NW_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize + (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    _baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize + (standardLeft - mouseCurrent.X)));
                    Console.WriteLine("hight = " + (HeightSize + (standardTop - mouseCurrent.Y)));
                }
                this.SetSize(new Size(WidthSize + (standardLeft - mouseCurrent.X), HeightSize + (standardTop - mouseCurrent.Y)));
                this.SetPosition(new Point(canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE));
                transformation(sender);
                isEvnet = false;
            }
        }
        private void NW_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNWSE;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // -------------------------------북서--------------------------------
        // ------------------------------- 북 --------------------------------
        private void N_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void N_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize + (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize));
                    Console.WriteLine("hight = " + (HeightSize));
                }
                Console.WriteLine("canvasLeft = " + canvasLeft);
                this.SetSize(new Size(WidthSize, HeightSize + (standardTop - mouseCurrent.Y)));
                this.SetPosition(new Point(standardLeft - (WidthSize / 2), canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE));
                transformation(sender);
                isEvnet = false;
            }
        }
        private void N_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNS;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // ------------------------------- 북 --------------------------------
        // -------------------------------북동--------------------------------
        private void NE_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void NE_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize + (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize));
                    Console.WriteLine("hight = " + (HeightSize));
                }
                Console.WriteLine("canvasLeft = " + canvasLeft);
                this.SetSize(new Size(WidthSize - (standardLeft - mouseCurrent.X), HeightSize + (standardTop - mouseCurrent.Y)));
                this.SetPosition(-1, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE, 0);
                transformation(sender);
                isEvnet = false;
            }
        }
        private void NE_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNESW;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // -------------------------------북동--------------------------------
        // ------------------------------- 서 --------------------------------
        private void W_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void W_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize + (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    //_baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    //_baseimage.SetValue(Canvas.HeightProperty, HightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    //_baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    //_baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    //((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    _baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    // _baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (mouseCurrent.X - standardLeft));
                    // _baseimage.SetValue(Canvas.HeightProperty, HightSize + (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize));
                    Console.WriteLine("hight = " + (HeightSize));
                }
                Console.WriteLine("canvasLeft = " + canvasLeft);
                this.SetSize(WidthSize - (mouseCurrent.X - standardLeft), -1, 0);
                this.SetPosition(canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE, -1, 0);
                transformation(sender);
                isEvnet = false;
            }
        }
        private void W_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeWE;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // ------------------------------- 서 --------------------------------
        // ------------------------------- 동 --------------------------------
        private void E_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void E_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize - (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    //_baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    //_baseimage.SetValue(Canvas.HeightProperty, HightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (standardLeft - mouseCurrent.X));
                    //_baseimage.SetValue(Canvas.HeightProperty, HightSize + (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize));
                    Console.WriteLine("hight = " + (HeightSize));
                }
                Console.WriteLine("canvasTop = " + canvasTop);
                this.SetSize(WidthSize - (standardLeft - mouseCurrent.X), -1, 0);
                //this.SetPosition(-1,- 1 , 0);
                this.setting_postion();
                transformation(sender);
                isEvnet = false;
            }
        }
        private void E_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeWE;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // ------------------------------- 동 --------------------------------
        // -------------------------------남서--------------------------------
        private void SW_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void SW_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize - (mouseCurrent.X - standardLeft) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize - (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize - (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    _baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE - HeightSize);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (mouseCurrent.X - standardLeft));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize - (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize + (standardLeft - mouseCurrent.X)));
                    Console.WriteLine("hight = " + (HeightSize + (standardTop - mouseCurrent.Y)));
                }
                this.SetSize(new Size(WidthSize - (mouseCurrent.X - standardLeft), HeightSize - (standardTop - mouseCurrent.Y)));
                this.SetPosition(canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE, -1, 0);
                transformation(sender);
                isEvnet = false;
            }
        }
        private void SW_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNESW;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // -------------------------------남서--------------------------------
        // ------------------------------- 남 --------------------------------
        private void S_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void S_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize + (standardLeft - mouseCurrent.X) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize + (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize + (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    //_baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    //((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft);
                    //((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.WidthProperty, WidthSize + (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize - (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize));
                    Console.WriteLine("hight = " + (HeightSize));
                }
                Console.WriteLine("canvasLeft = " + canvasLeft);
                this.SetSize(new Size(WidthSize, HeightSize - (standardTop - mouseCurrent.Y)));
                //this.SetPosition(new Point(standardLeft - (WidthSize / 2), canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE));
                this.setting_postion();
                transformation(sender);
                isEvnet = false;
            }
        }


        private void S_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNS;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // ------------------------------- 남 --------------------------------
        // -------------------------------남동--------------------------------
        private void SE_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            isEvnet = false;
        }
        private void SE_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                isEvnet = true;
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                if (WidthSize - (mouseCurrent.X - standardLeft) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, 20.0);
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize - (standardTop - mouseCurrent.Y));
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else if (HeightSize - (standardTop - mouseCurrent.Y) <= 20.0)
                {
                    _baseimage.SetValue(Canvas.LeftProperty, Canvas.GetLeft(_baseimage) + Left - deltaLeft);
                    _baseimage.SetValue(Canvas.TopProperty, Canvas.GetTop(_baseimage) + Top - deltaTop);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, 20.0);
                    Mouse.OverrideCursor = null;
                    ((Image)sender).ReleaseMouseCapture();
                }
                else
                {
                    ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                    ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                    //_baseimage.SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE);
                    //_baseimage.SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop + DRAG_HANDLE_SIZE - HeightSize);
                    _baseimage.SetValue(Canvas.WidthProperty, WidthSize - (standardLeft - mouseCurrent.X));
                    _baseimage.SetValue(Canvas.HeightProperty, HeightSize - (standardTop - mouseCurrent.Y));
                    Console.WriteLine("width = " + (WidthSize + (standardLeft - mouseCurrent.X)));
                    Console.WriteLine("hight = " + (HeightSize + (standardTop - mouseCurrent.Y)));
                }
                this.SetSize(new Size(WidthSize - (standardLeft - mouseCurrent.X), HeightSize - (standardTop - mouseCurrent.Y)));
                //this.SetPosition(canvasLeft + Left - deltaLeft + DRAG_HANDLE_SIZE, -1, 0);
                this.setting_postion();
                transformation(sender);
                isEvnet = false;
            }
        }
        private void SE_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isEvnet = true;
            Mouse.OverrideCursor = Cursors.SizeNWSE;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pt = e.GetPosition((Image)sender);
            deltaLeft = pt.X;
            deltaTop = pt.Y;
            pt = e.GetPosition(null);
            standardLeft = pt.X;
            standardTop = pt.Y;
            WidthSize = (double)_baseimage.GetValue(Canvas.WidthProperty);
            HeightSize = (double)_baseimage.GetValue(Canvas.HeightProperty);
            ((Image)sender).CaptureMouse();
            isEvnet = false;
        }
        // -------------------------------남동--------------------------------
        // ---------------------------사이즈 조절-----------------------------
        public void transformation(object sender)
        {
            for (int i = 0; i < 9; i++)
            {
                Point _pit = this.get_RectSize(i);
                Image img = this.get_Rectangle(i);
                img.SetValue(Canvas.LeftProperty, _pit.X);
                img.SetValue(Canvas.TopProperty, _pit.Y);
            }
            for (int i = 0; i < 4; i++)
            {

                Point _pit = this.get_RectSize(LINE_ARRY[i]);
                Line li = this.get_Line(i);
                li.SetValue(Canvas.LeftProperty, _pit.X);
                li.SetValue(Canvas.TopProperty, _pit.Y);
            }
            Image _img = this.get_Rectangle(BODER);
            _img.SetValue(Canvas.WidthProperty, size.Width + DRAG_HANDLE_SIZE);
            _img.SetValue(Canvas.HeightProperty, size.Height + DRAG_HANDLE_SIZE);
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
        }
        // ---------------------------사이즈 조절-----------------------------

        public void visiblie(System.Windows.Visibility visibility)
        {
            if (visibility.Equals(Visibility.Visible))
            {
                Console.WriteLine("test!");
                for (int i = 0; i < 8; i++)
                {
                    Image img = this.get_Rectangle(i);
                    img.Visibility = Visibility.Visible;
                }
                for (int i = 0; i < 4; i++)
                {
                    Line li = this.get_Line(i);
                    li.Visibility = Visibility.Visible;
                }
                isVisible = true;
            }
            else
            {
                Console.WriteLine("test!?");
                for (int i = 0; i < 8; i++)
                {
                    Image img = this.get_Rectangle(i);
                    img.Visibility = Visibility.Hidden;
                }
                for (int i = 0; i < 4; i++)
                {
                    Line li = this.get_Line(i);
                    li.Visibility = Visibility.Hidden;
                }
                isVisible = false;
            }

        }

        public void ResizeUpdateLayout()
        {
            for (int i = 0; i < 8; i++)
            {
                point[i].UpdateLayout();
            }
            point[BODER].UpdateLayout();

            for (int i = 0; i < 4; i++)
            {
                boder[i].UpdateLayout();
            }

        }

        public void setSizeComponent(double width, double height,object sender)
        {
            _baseimage.SetValue(Canvas.WidthProperty, width);
            _baseimage.SetValue(Canvas.HeightProperty,height);
            this.SetSize(new Size(width,height));
            transformation(sender);
            this.setting_postion();
        }

    }
}
