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
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StoryboardWindow : Window
    {
        private const int DRAG_HANDLE_SIZE = 7;

        private Image[] item_image;

        private double canvasLeft;
        private double canvasTop;

        private double deltaTop;
        private double deltaLeft;

        private int imagecount = 0;

        //private Component cmp_temp;

        private Component test;
        private WPFBrushList _brushes;
        private ItemList _item;

        private List<ComponentItem> clickCmpItem;
        private Component catchImage;

        private string login_ID;
        private string project_name;
        MenuWindow menu_window;

        private int select_component;
        private int select_item;

        private Point startPosition;
        private Point endPosition;
        private Line[] drwing;

        //private ComponentItem cathImage;
        private Content contentItem;

        private object base_object;

        private WinForm winForm = new WinForm();
        private WButton wButton = new WButton();
        private WTextblock wTextblock = new WTextblock();
        private WTextbox wTextbox = new WTextbox();
        private WCheckbox wCheckbox = new WCheckbox();
        private WCombobox wCombobox = new WCombobox();
        private WSlideview wSlideview = new WSlideview();
        private WListbox wListbox = new WListbox();
        private WListview wListview = new WListview();

        private object[] ItemArray = { "winForm", "wButton", "wTextbox", "wTextblock", "wCheckbox", "wCombobox", "wSlideview", "wListbox", "wListview" };

        private string[] ComponentName = { "WindowForm" };
        private string[] ContentName = { "Button", "TextBox", "TextBlock", "ListBox", "ListVew", "ComboBox", "SlideView", "CheckBox" };

        private int count;
        private bool additemState;

        private const string file_upload_test_URL = "http://210.118.69.110/application/file_receive.php";

        private Content btnChoice;

        public StoryboardWindow()
        {
            InitializeComponent();
            additemState = false;
            count = 0;
            select_component = -1;
            select_item = -1;
            Console.WriteLine("Storyboard Not Create");
            drwing = new Line[4];
            _brushes = new WPFBrushList();
            ComponentListbox.DataContext = _brushes;
            _item = new ItemList();
            ItemListbox.DataContext = _item;

            login_ID = "test10";
            project_name = "test10";

            this.itemProperty.SelectedObject = null;
            canvas.PreviewKeyDown += new KeyEventHandler(canvas_PreviewKeyDown);

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);

            clickCmpItem = new List<ComponentItem>();
            viewItemList.SelectionChanged += new SelectionChangedEventHandler(viewItemList_SelectionChanged);
            conponetItemList.SelectionChanged += new SelectionChangedEventHandler(conponetItemList_SelectionChanged);
            imageDeletebtn.Content = "Delete Image";
            addItem.Visibility = Visibility.Hidden;
            addEvent.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;

        }


        public StoryboardWindow(string _login_ID, string _project_name, MenuWindow _menu_window)
        {
            InitializeComponent();
            additemState = false;
            count = 0;
            select_component = -1;
            select_item = -1;
            Console.WriteLine("Storyboard Not Create");
            drwing = new Line[4];
            _brushes = new WPFBrushList();
            ComponentListbox.DataContext = _brushes;
            _item = new ItemList();
            ItemListbox.DataContext = _item;

            login_ID = _login_ID;
            project_name = _project_name;
            menu_window = _menu_window;

            this.itemProperty.SelectedObject = null;
            canvas.PreviewKeyDown += new KeyEventHandler(canvas_PreviewKeyDown);

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);

            clickCmpItem = new List<ComponentItem>();
            viewItemList.SelectionChanged += new SelectionChangedEventHandler(viewItemList_SelectionChanged);
            conponetItemList.SelectionChanged += new SelectionChangedEventHandler(conponetItemList_SelectionChanged);
            imageDeletebtn.Content = "Delete Image";
            addItem.Visibility = Visibility.Hidden;
            addEvent.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;
        }

        public StoryboardWindow(string _login_ID, string _project_name, MenuWindow _menu_window , string filename)
        {
            InitializeComponent();
            additemState = false;
            count = 0;
            select_component = -1;
            select_item = -1;
            Console.WriteLine("Storyboard Not Create");
            drwing = new Line[4];
            _brushes = new WPFBrushList();
            ComponentListbox.DataContext = _brushes;
            _item = new ItemList();
            ItemListbox.DataContext = _item;

            login_ID = _login_ID;
            project_name = _project_name;
            menu_window = _menu_window;

            this.itemProperty.SelectedObject = null;
            canvas.PreviewKeyDown += new KeyEventHandler(canvas_PreviewKeyDown);

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);

            clickCmpItem = new List<ComponentItem>();
            viewItemList.SelectionChanged += new SelectionChangedEventHandler(viewItemList_SelectionChanged);
            conponetItemList.SelectionChanged += new SelectionChangedEventHandler(conponetItemList_SelectionChanged);
            imageDeletebtn.Content = "Delete Image";
            addItem.Visibility = Visibility.Hidden;
            addEvent.Visibility = Visibility.Hidden;
            button1.Visibility = Visibility.Hidden;
            loadXML();
        }

        void winForm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine("dd");
        }

        public StoryboardWindow(string _login_ID, string _project_name, MenuWindow _menu_window, int tesd)
        {
            InitializeComponent();
            additemState = false;
            Console.WriteLine("Storyboard Three Create");

            login_ID = _login_ID;
            project_name = _project_name;
            menu_window = _menu_window;

            _brushes = new WPFBrushList();
            ComponentListbox.DataContext = _brushes;

            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(@"c:\users\jeongkw\documents\visual studio 2010\Projects\SVN\ProjectTest\ProjectTest\test\a.jpg");
            //bitmap.EndInit();
            //img.BeginInit();
            //img.Source = bitmap;

            //img.PreviewMouseDown += new MouseButtonEventHandler(img_PreviewMouseDown);
            //img.PreviewMouseMove += new MouseEventHandler(img_PreviewMouseMove);
            //img.PreviewMouseUp += new MouseButtonEventHandler(img_PreviewMouseUp);
            //img.LostMouseCapture += new MouseEventHandler(img_LostMouseCapture);
            //img.SizeChanged += new SizeChangedEventHandler(img_SizeChanged);
            //img.EndInit();

            item_image = new Image[50];



            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\activity_icon.jpg");
            bitmap.EndInit();
            item_image[0] = new Image();
            item_image[0].BeginInit();
            item_image[0].Source = bitmap;
            item_image[0].Width = 50;
            item_image[0].Height = 50;
            item_image[0].EndInit();


            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\tablet_icon.jpg");
            bitmap.EndInit();
            item_image[1] = new Image();
            item_image[1].BeginInit();
            item_image[1].Source = bitmap;
            item_image[1].Width = 50;
            item_image[1].Height = 50;
            item_image[1].EndInit();



            //component 추가할때 로직
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\b.jpg");
            bitmap.EndInit();
            test = new Component();
            test.BeginInit();
            test.Source = bitmap;
            test.Width = 674;
            test.Height = 515;
            test.Stretch = Stretch.Fill;
            //test.PreviewMouseDown += new MouseButtonEventHandler();
            test.EndInit();
            int zindex = canvas.Children.Count;
            canvas.Children.Add(test);
            Canvas.SetZIndex(test, zindex + 12);
            Canvas.SetLeft(test, 50);
            Canvas.SetTop(test, 50);

            setting_Resize(test);

            test.position = new Point(10, 10);
            test.PreviewMouseDown += new MouseButtonEventHandler(cmt_PreviewMouseDown);
            test.PreviewMouseMove += new MouseEventHandler(cmt_PreviewMouseMove);
            test.PreviewMouseUp += new MouseButtonEventHandler(cmt_PreviewMouseUp);
            test.LostMouseCapture += new MouseEventHandler(cmt_LostMouseCapture);
            test.KeyDown += new KeyEventHandler(cmt_KeyDown);
            test.PreviewKeyDown += new KeyEventHandler(cmt_PreviewKeyDown);
            test.PreviewKeyUp += new KeyEventHandler(cmt_PreviewKeyUp);


            canvas.PreviewKeyDown += new KeyEventHandler(canvas_PreviewKeyDown);

            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);

            clickCmpItem = new List<ComponentItem>();
            //clickImage.Add(test);

        }


        private void set_MouseEvent(Image _cmp)
        {
            _cmp.PreviewMouseDown += new MouseButtonEventHandler(cmtitem_PreviewMouseDown);
            _cmp.PreviewMouseMove += new MouseEventHandler(cmtitem_PreviewMouseMove);
            _cmp.PreviewMouseUp += new MouseButtonEventHandler(cmtitem_PreviewMouseUp);
            _cmp.LostMouseCapture += new MouseEventHandler(cmtitem_LostMouseCapture);
            _cmp.KeyDown += new KeyEventHandler(cmt_KeyDown);
            _cmp.PreviewKeyDown += new KeyEventHandler(cmt_PreviewKeyDown);
            _cmp.PreviewKeyUp += new KeyEventHandler(cmt_PreviewKeyUp);
        }


        // Base Event List
        //--------------------------------------------------------------------------------


        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete) && catchImage != null)
            {
                canvas.Children.Remove(((Component)catchImage.super).viewDrow[0]);
                clickCmpItem.Remove(((Component)catchImage.super).viewDrow[0]);
                canvas.Children.Remove(((Component)catchImage.super).viewDrow[1]);
                delete_Resize(((Component)catchImage.super).viewDrow[0]);
                for (int i = 0; ((Component)catchImage.super).viewDrow[0].getSuperItem().childen.Count != 0; )
                {
                    Console.WriteLine("index = " + i + " , " + ((Component)catchImage.super).viewDrow[0].getSuperItem().childen.Count);
                    canvas.Children.Remove(((Component)catchImage.super).viewDrow[0].getSuperItem().childen[i]);
                    delete_Resize(((Component)catchImage.super).viewDrow[0].getSuperItem().childen[i]);
                    ((Component)catchImage.super).viewDrow[0].getSuperItem().childen.Remove(((Component)catchImage.super).viewDrow[0].getSuperItem().childen[i]);
                }
                catchImage = null;
                addItem.Visibility = Visibility.Hidden;
                this.itemProperty.SelectedObject = null;
                propertyTabControl.SelectedIndex = 0;
            }
            else if (e.Key.Equals(Key.Delete) && contentItem != null)
            {
                canvas.Children.Remove(contentItem);
                delete_Resize(contentItem);
                contentItem = null;
                addItem.Visibility = Visibility.Hidden;
                this.itemProperty.SelectedObject = null;
                propertyTabControl.SelectedIndex = 0;
            }
        }

        void canvas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("ddd");
        }


        //--------------------------------------------------------------------------------


        // Set Image
        //--------------------------------------------------------------------------------
        private void set_ImageMove(Point posion, Image _img)
        {
            this.imagecount++;
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

        void delete_Resize(Component _cmp)
        {
            for (int i = 0; i < 4; i++)
            {
                Line li = _cmp.re.get_Line(i);
                canvas.Children.Remove(li);
            }
            for (int i = 0; i < 8; i++)
            {
                Image rt = _cmp.re.get_Rectangle(i);
                canvas.Children.Remove(rt);
            }
            Image _rt = _cmp.re.get_Rectangle(8);
            canvas.Children.Remove(_rt);
        }
        void delete_Resize(Content _cmp)
        {
            for (int i = 0; i < 4; i++)
            {
                Line li = _cmp.re.get_Line(i);
                canvas.Children.Remove(li);
            }
            for (int i = 0; i < 8; i++)
            {
                Image rt = _cmp.re.get_Rectangle(i);
                canvas.Children.Remove(rt);
            }
            Image _rt = _cmp.re.get_Rectangle(8);
            canvas.Children.Remove(_rt);
        }


        //--------------------------------------------------------------------------------

        // component Event List
        //--------------------------------------------------------------------------------
        void cmt_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key.ToString() + "dd");
        }

        void cmt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Console.WriteLine("ddd");
            }
            Console.WriteLine("dd");
        }

        void cmt_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("dd");
        }
        //--------------------------------------------------------------------------------


        // Event Base List
        //--------------------------------------------------------------------------------
        private void img_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = "mouse lost";
        }

        private void img_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = e.GetPosition(this.canvas).ToString();
        }

        private void img_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured)
            {
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                canvasLeft = Canvas.GetLeft(((Image)sender));
                canvasTop = Canvas.GetTop(((Image)sender));
                //output.Text = e.GetPosition(this.canvas).ToString();
            }
        }

        private void img_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pnt = e.GetPosition((Image)sender);
            deltaLeft = pnt.X;
            deltaTop = pnt.Y;
            ((Image)sender).CaptureMouse();

        }
        //--------------------------------------------------------------------------------


        public void componentItem_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = "mouse lost";
        }

        public void componentItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = e.GetPosition(this.canvas).ToString();

            //체크포인트
            if (catchImage != null)
            {
                Console.WriteLine("ok??");
                winForm = new WinForm();
                winForm.String = ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem().Name;
                winForm.Point = new Point((double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                    (double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                winForm.Size = new Size((double)((Component)((Component)catchImage.super)).viewDrow[0].Width,
                    (double)((Component)((Component)catchImage.super)).viewDrow[0].Height);
                object selected = this.GetType().GetField("winForm",
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                this.itemProperty.SelectedObject = selected;
            }
        }

        public void componentItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured && (select_component == -1 && select_item == -1))
            {
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                Component temp = (Component)sender;
                ((Component)((Component)sender).super).setMoveComponent(new Point(canvasLeft + Left - deltaLeft, canvasTop + Top - deltaTop), sender);
                //((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                winForm = new WinForm();
                winForm.String = ((Component)(Component)temp.super).viewDrow[0].getSuperItem().Name;
                winForm.Point = new Point((double)((Component)((Component)temp.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                    (double)((Component)((Component)temp.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                winForm.Size = new Size((double)((Component)((Component)temp.super)).viewDrow[0].Width,
                    (double)((Component)((Component)temp.super)).viewDrow[0].Height);
                object selected = this.GetType().GetField("winForm",
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                this.itemProperty.SelectedObject = selected;
                canvasTop = Canvas.GetTop(((Image)sender));
                canvasLeft = Canvas.GetLeft((Image)sender);
                ((Component)((Component)sender).super).viewDrow[0].re.transformation(sender);
            }
        }

        public void componentItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            addItem.Visibility = Visibility.Hidden;
            addEvent.Visibility = Visibility.Hidden;
            propertyTabControl.SelectedIndex = 0;
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                clickCmpItem[i].re.visiblie(Visibility.Hidden);
                Console.WriteLine("test = " + clickCmpItem[i].Name);
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    clickCmpItem[i].getSuperItem().childen[j].re.visiblie(Visibility.Hidden);
                }
            }
            if ((select_component == -1 && select_item == -1))
            {
                Mouse.OverrideCursor = Cursors.Hand;
                canvasLeft = Canvas.GetLeft(((Image)sender));
                canvasTop = Canvas.GetTop(((Image)sender));
                Point pnt = e.GetPosition((Image)sender);
                deltaLeft = pnt.X;
                deltaTop = pnt.Y;
                ((Image)sender).CaptureMouse();
                Component temp = (Component)sender;
                //output.Text = e.GetPosition(this.canvas).ToString();
                ((Component)((Component)temp.super)).viewDrow[0].re.visiblie(Visibility.Visible);

                //프로퍼티 작업중
                winForm = new WinForm();
                winForm.String = ((Component)(Component)temp.super).viewDrow[0].getSuperItem().Name;
                winForm.Point = new Point((double)((Component)((Component)temp.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                    (double)((Component)((Component)temp.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                winForm.Size = new Size((double)((Component)((Component)temp.super)).viewDrow[0].Width,
                    (double)((Component)((Component)temp.super)).viewDrow[0].Height);
                object selected = this.GetType().GetField("winForm",
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                this.itemProperty.SelectedObject = selected;
                catchImage = (Component)sender;
                contentItem = null;
                Console.WriteLine("ddtted");
            }
            else
            {
                base_object = sender;
                Console.WriteLine(sender.ToString());
            }
        }


        //content Mouse Event
        //---------------------------------------------------------------------
        public void content_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = "mouse lost";
        }

        public void content_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Image)sender).ReleaseMouseCapture();
            //output.Text = e.GetPosition(this.canvas).ToString();
        }

        public void content_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Image)sender).IsMouseCaptured && (select_component == -1 && select_item == -1))
            {
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                ((Image)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                ((Image)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                canvasTop = Canvas.GetTop((Image)sender);
                canvasLeft = Canvas.GetLeft((Image)sender);
                ((Content)sender).re.SetPosition(canvasLeft + Left - deltaLeft, canvasTop + Top - deltaTop, 0);
                ((Content)sender).re.transformation(sender);
                Content cnt = ((Content)sender);
                this.itemProperty.SelectedObject = null;
                object selected = new object();
                //체크포인트2
                Console.WriteLine(cnt.getItemNumber());
                switch (cnt.getItemNumber())
                {
                    case 0://Button
                        wButton = new WButton();
                        wButton.String = cnt.Name;
                        wButton.Text = cnt.Text;
                        wButton.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wButton.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wButton",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 1://Text Box
                        wTextbox = new WTextbox();
                        wTextbox.String = cnt.Name;
                        wTextbox.Text = cnt.Text;
                        wTextbox.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wTextbox.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wTextbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 2://Text Block
                        wTextblock = new WTextblock();
                        wTextblock.String = cnt.Name;
                        wTextblock.Text = cnt.Text;
                        wTextblock.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wTextblock.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wTextblock",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 3://List Box
                        wListbox = new WListbox();
                        wListbox.String = cnt.Name;
                        wListbox.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wListbox.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wListbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 4://List View
                        wListview = new WListview();
                        wListview.String = cnt.Name;
                        wListview.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wListview.Size = new Size(cnt.Width, cnt.Height);
                        wListview.ContentSize = cnt.contentSize;
                        selected = this.GetType().GetField("wListview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 5://Combo Box
                        wCombobox = new WCombobox();
                        wCombobox.String = cnt.Name;
                        wCombobox.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wCombobox.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wCombobox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 6://Slide View
                        wSlideview = new WSlideview();
                        wSlideview.String = cnt.Name;
                        wSlideview.Point = new Point((double)cnt.GetValue(Canvas.LeftProperty), (double)cnt.GetValue(Canvas.TopProperty));
                        wSlideview.Size = new Size(cnt.Width, cnt.Height);
                        selected = this.GetType().GetField("wSlideview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 7://Check Box
                        break;
                }
                this.itemProperty.SelectedObject = selected;
                cnt.setDistans((double)cnt.GetValue(Canvas.LeftProperty) - (double)((Component)(cnt.superCnt)).viewDrow[0].GetValue(LeftProperty)
                        , (double)cnt.GetValue(Canvas.TopProperty) - (double)((Component)(cnt.superCnt)).viewDrow[0].GetValue(TopProperty));
                //output.Text = e.GetPosition(this.canvas).ToString();
            }
        }

        public void content_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                if (catchImage != null)
                    clickCmpItem[i].re.visiblie(Visibility.Hidden);
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    if (clickCmpItem[i].getSuperItem().childen[j] != (Content)sender)
                        clickCmpItem[i].getSuperItem().childen[j].re.visiblie(Visibility.Hidden);
                }
            }
            if ((select_component == -1 && select_item == -1))
            {
                ItemCollection tempItem = propertyTabControl.Items;
                tempItem.Remove(addItem);
                tempItem.Insert(1, addItem);
                propertyTabControl.SelectedIndex = 0;
                addEvent.Visibility = Visibility.Hidden;
                Mouse.OverrideCursor = Cursors.Hand;
                canvasLeft = Canvas.GetLeft(((Image)sender));
                canvasTop = Canvas.GetTop(((Image)sender));
                Point pnt = e.GetPosition((Image)sender);
                deltaLeft = pnt.X;
                deltaTop = pnt.Y;
                ((Image)sender).CaptureMouse();
                Content temp = (Content)sender;
                //output.Text = e.GetPosition(this.canvas).ToString();
                temp.re.visiblie(Visibility.Visible);
                contentItem = (Content)sender;
                object selected = new object();
                addItem.Visibility = Visibility.Hidden;
                switch (contentItem.getItemNumber())
                {
                    case 0://Button
                        wButton = new WButton();
                        wButton.String = contentItem.Name;
                        wButton.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wButton.Size = new Size(contentItem.Width, contentItem.Height);
                        wButton.Text = contentItem.Text;
                        selected = this.GetType().GetField("wButton",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        addEvent.Visibility = Visibility.Visible;
                        tempItem = propertyTabControl.Items;
                        tempItem.Remove(addEvent);
                        tempItem.Insert(1, addEvent);
                        btnChoice = contentItem;
                        setReflushComponentList(btnChoice.superCnt.Name.ToString());
                        if (!contentItem.entList.Name.Equals("") && isSameName(contentItem.entList.Name))
                        {
                            seleteevent.Text = contentItem.entList.Name;
                        }
                        else
                        {
                            seleteevent.Text = contentItem.entList.Name = "";
                        }
                        break;
                    case 1://Text Box
                        wTextbox = new WTextbox();
                        wTextbox.String = contentItem.Name;
                        wTextbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wTextbox.Size = new Size(contentItem.Width, contentItem.Height);
                        wTextblock.Text = contentItem.Text;
                        selected = this.GetType().GetField("wTextbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 2://Text Block
                        wTextblock = new WTextblock();
                        wTextblock.String = contentItem.Name;
                        wTextblock.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wTextblock.Size = new Size(contentItem.Width, contentItem.Height);
                        wTextblock.Text = contentItem.Text;
                        selected = this.GetType().GetField("wTextblock",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 3://List Box
                        addItem.Visibility = Visibility.Visible;
                        setStringInput();
                        wListbox = new WListbox();
                        wListbox.String = contentItem.Name;
                        wListbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wListbox.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wListbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 4://List View
                        addItem.Visibility = Visibility.Visible;
                        setImageInput();
                        wListview = new WListview();
                        wListview.String = contentItem.Name;
                        wListview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wListview.Size = new Size(contentItem.Width, contentItem.Height);
                        wListview.ContentSize = new Size(contentItem.contentSize.Width, contentItem.contentSize.Height);
                        selected = this.GetType().GetField("wListview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        addItem.Visibility = Visibility.Visible;
                        setImageInput();
                        List<Image> inputList = new List<Image>();
                        for (int i = 0; i < contentItem.listView.Count; i++)
                        {
                            Image img = new Image();
                            img.BeginInit();
                            img.Source = contentItem.listView[i];
                            img.Width = contentItem.listView[i].Width;
                            img.Height = contentItem.listView[i].Height;
                            img.EndInit();
                            inputList.Add(img);
                        }
                        viewItemList.DataContext = inputList;
                        viewItemList.UpdateLayout();
                        break;
                    case 5://Combo Box
                        addItem.Visibility = Visibility.Visible;
                        wSlideview = new WSlideview();
                        wSlideview.String = contentItem.Name;
                        wSlideview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wSlideview.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wSlideview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        addItem.Visibility = Visibility.Visible;
                        setImageInput();
                        List<string> stringList = new List<string>();
                        for (int i = 0; i < contentItem.listBox.Count; i++)
                        {
                            stringList.Add(contentItem.listBox[i]);
                        }
                        stringItemList.DataContext = stringList;
                        stringItemList.UpdateLayout();
                        setStringInput();
                        break;
                    case 6://Slide View
                        wSlideview = new WSlideview();
                        wSlideview.String = contentItem.Name;
                        wSlideview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wSlideview.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wSlideview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        addItem.Visibility = Visibility.Visible;
                        setImageInput();
                        inputList = new List<Image>();
                        for (int i = 0; i < contentItem.listView.Count; i++)
                        {
                            Image img = new Image();
                            img.BeginInit();
                            img.Source = contentItem.listView[i];
                            img.Width = contentItem.listView[i].Width;
                            img.Height = contentItem.listView[i].Height;
                            img.EndInit();
                            inputList.Add(img);
                        }
                        viewItemList.DataContext = inputList;
                        viewItemList.UpdateLayout();
                        break;
                    case 7://Check Box
                        break;
                }
                this.itemProperty.SelectedObject = selected;
                catchImage = null;
            }
            else
            {
                base_object = sender;
                Console.WriteLine(sender.ToString());
            }
        }
        //-------------------------------------------------------

        void img_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /*
         * 
         * 이미지 이동 이벤트
         * 
         */



        private void cmt_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Component)sender).ReleaseMouseCapture();
            //((Component)sender).re.visiblie(Visibility.Hidden);
            //output.Text = "mouse lost";
        }


        private void cmt_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Component)sender).ReleaseMouseCapture();
            //output.Text = e.GetPosition(this.canvas).ToString();
        }

        private void cmt_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Component)sender).IsMouseCaptured)
            {
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                ((Component)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                ((Component)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                ((Component)sender).re.SetPosition(canvasLeft + Left - deltaLeft, canvasTop + Top - deltaTop, 0);
                canvasLeft = Canvas.GetLeft(((Image)sender));
                canvasTop = Canvas.GetTop(((Image)sender));
                ((Component)sender).re.transformation(sender);
                //output.Text = e.GetPosition(this.canvas).ToString();
            }
        }

        private void cmt_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (catchImage != null)
                catchImage.re.visiblie(Visibility.Hidden);
            Mouse.OverrideCursor = Cursors.Hand;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pnt = e.GetPosition((Image)sender);
            deltaLeft = pnt.X;
            deltaTop = pnt.Y;
            ((Component)sender).CaptureMouse();
            Component temp = (Component)sender;
            //output.Text = e.GetPosition(this.canvas).ToString();
            temp.re.visiblie(Visibility.Visible);
            catchImage = (Component)sender;

        }



        private void Canvas_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void cmtitem_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Component)sender).ReleaseMouseCapture();
            //((Component)sender).re.visiblie(Visibility.Hidden);
            //output.Text = "mouse lost";
        }


        private void cmtitem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = null;
            ((Component)sender).ReleaseMouseCapture();
            //output.Text = e.GetPosition(this.canvas).ToString();
        }

        private void cmtitem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (((Component)sender).IsMouseCaptured)
            {
                Point mouseCurrent = e.GetPosition(null);
                double Left = mouseCurrent.X - canvasLeft;
                double Top = mouseCurrent.Y - canvasTop;
                ((Component)sender).SetValue(Canvas.LeftProperty, canvasLeft + Left - deltaLeft);
                ((Component)sender).SetValue(Canvas.TopProperty, canvasTop + Top - deltaTop);
                ((Component)sender).setMoveComponent();
                ((Component)sender).re.SetPosition(canvasLeft + Left - deltaLeft, canvasTop + Top - deltaTop, 0);
                canvasLeft = Canvas.GetLeft(((Image)sender));
                canvasTop = Canvas.GetTop(((Image)sender));
                ((Component)sender).re.transformation(sender);
                //output.Text = e.GetPosition(this.canvas).ToString();
            }
        }

        private void cmtitem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
            canvasLeft = Canvas.GetLeft(((Image)sender));
            canvasTop = Canvas.GetTop(((Image)sender));
            Point pnt = e.GetPosition((Image)sender);
            deltaLeft = pnt.X;
            deltaTop = pnt.Y;
            ((Image)sender).CaptureMouse();
            Component temp = (Component)sender;
            //output.Text = e.GetPosition(this.canvas).ToString();
            temp.re.visiblie(Visibility.Visible);
            catchImage = (Component)sender;

        }



        private void CopyName_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentListbox.SelectedIndex != -1)
                Clipboard.SetText(((WPFBrush)ComponentListbox.SelectedItem).Name);
        }

        private void CopyHex_Click(object sender, RoutedEventArgs e)
        {
            if (ComponentListbox.SelectedIndex != -1) ;
            //Clipboard.SetText(((WPFBrush)lsbBrushes.SelectedItem).Hex);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Storyboard를 저장하시겠습니까?", "New Storyboard", MessageBoxButton.OKCancel);
            if (mbr.Equals(MessageBoxResult.OK))
            {
                save_xml();
                test_thread_start();

                App.Current.MainWindow = menu_window;
                menu_window.Show();
            }
            else
            {
                mbr = MessageBox.Show("Storyboard를 저장안하시고 종료하시겠습니까?", "New Storyboard", MessageBoxButton.OKCancel);
                if (mbr.Equals(MessageBoxResult.OK))
                {
                    App.Current.MainWindow = menu_window;
                    menu_window.Show();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            //App.Current.MainWindow = menu_window;
            //menu_window.Show();
            //this.Close();
            //this.Close();
        }





        //작업
        private void canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //component case
            Console.WriteLine(select_item + " , " + select_component);
            //for (int i = 0; i < clickCmpItem.Count; i++)
            //{
            //    if (catchImage != null && !((ComponentItem)(catchImage.super)).viewDrow[0].Equals(clickCmpItem[i]))
            //        clickCmpItem[i].re.visiblie(Visibility.Hidden);
            //}
            if (select_component != -1)
            {
                startPosition = e.GetPosition(canvas);
                for (int i = 0; i < 4; i++)
                {
                    drwing[i] = new Line();
                    SolidColorBrush _brush = new SolidColorBrush();
                    _brush.Color = Colors.Black;
                    drwing[i].Stroke = _brush;
                    drwing[i].StrokeThickness = 1;
                    drwing[i].StrokeDashArray = DoubleCollection.Parse("5,5");
                    canvas.Children.Add(drwing[i]);
                    int zindex = Canvas.GetZIndex(canvas);
                    Canvas.SetZIndex(drwing[i], zindex + i);
                    Canvas.SetLeft(drwing[i], startPosition.X);
                    Canvas.SetTop(drwing[i], startPosition.Y);
                }
                Console.WriteLine(startPosition);
                switch (select_component)
                {
                    case 0://액티비티
                        Mouse.OverrideCursor = Cursors.SizeNWSE;

                        break;
                    case 1://

                        break;



                }
                //select_component = -1;
            }
            if (select_item != -1)
            {
                startPosition = e.GetPosition(canvas);
                for (int i = 0; i < 4; i++)
                {
                    drwing[i] = new Line();
                    SolidColorBrush _brush = new SolidColorBrush();
                    _brush.Color = Colors.Black;
                    drwing[i].Stroke = _brush;
                    drwing[i].StrokeThickness = 1;
                    drwing[i].StrokeDashArray = DoubleCollection.Parse("5,5");
                    canvas.Children.Add(drwing[i]);
                    int zindex = Canvas.GetZIndex(canvas);
                    Canvas.SetZIndex(drwing[i], zindex + i);
                    Canvas.SetLeft(drwing[i], startPosition.X);
                    Canvas.SetTop(drwing[i], startPosition.Y);
                }
                Console.WriteLine(startPosition);

            }
        }

        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton.Equals(MouseButtonState.Pressed) && select_component != -1)
            {
                endPosition = e.GetPosition(canvas);
                drwing[0].X1 = 0;
                drwing[0].Y1 = 0;
                drwing[0].X2 = endPosition.X - startPosition.X;
                drwing[0].Y2 = 0;

                drwing[1].X1 = 0;
                drwing[1].Y1 = 0;
                drwing[1].X2 = 0;
                drwing[1].Y2 = endPosition.Y - startPosition.Y;

                drwing[2].X1 = endPosition.X - startPosition.X;
                drwing[2].Y1 = endPosition.Y - startPosition.Y;
                drwing[2].X2 = endPosition.X - startPosition.X;
                drwing[2].Y2 = 0;


                drwing[3].X1 = 0;
                drwing[3].Y1 = endPosition.Y - startPosition.Y;
                drwing[3].X2 = endPosition.X - startPosition.X;
                drwing[3].Y2 = endPosition.Y - startPosition.Y;
                Console.WriteLine(endPosition);
                switch (select_component)
                {
                    case 0://액티비티
                        //Mouse.OverrideCursor = Cursors.SizeNWSE;

                        break;
                    case 1://

                        break;
                }
            }
            if (e.MouseDevice.LeftButton.Equals(MouseButtonState.Pressed) && select_item != -1)
            {
                endPosition = e.GetPosition(canvas);
                drwing[0].X1 = 0;
                drwing[0].Y1 = 0;
                drwing[0].X2 = endPosition.X - startPosition.X;
                drwing[0].Y2 = 0;

                drwing[1].X1 = 0;
                drwing[1].Y1 = 0;
                drwing[1].X2 = 0;
                drwing[1].Y2 = endPosition.Y - startPosition.Y;

                drwing[2].X1 = endPosition.X - startPosition.X;
                drwing[2].Y1 = endPosition.Y - startPosition.Y;
                drwing[2].X2 = endPosition.X - startPosition.X;
                drwing[2].Y2 = 0;


                drwing[3].X1 = 0;
                drwing[3].Y1 = endPosition.Y - startPosition.Y;
                drwing[3].X2 = endPosition.X - startPosition.X;
                drwing[3].Y2 = endPosition.Y - startPosition.Y;
                Console.WriteLine(endPosition);
            }
        }

        private void canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (select_component != -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    canvas.Children.Remove(drwing[i]);
                }
                switch (select_component)
                {
                    case 0://Window form
                        try
                        {
                            Mouse.OverrideCursor = Cursors.Arrow;
                            for (int i = 0; i < 4; i++)
                            {
                                canvas.Children.Remove(drwing[i]);
                            }
                            endPosition = e.GetPosition(canvas);
                            Component cmpt = new Component(new ComponentType(ComponentType.WINDOW_FORM));
                            cmpt.setSize(endPosition.X - startPosition.X, endPosition.Y - startPosition.Y);
                            cmpt.setComponent();
                            count++;
                            while (isSameName(ComponentName[0] + count))
                            {
                                count++;
                            }
                            cmpt.Name = ComponentName[0] + count;
                            for (int i = 0; i < cmpt.viewDrow.Count; i++)
                            {
                                canvas.Children.Add(cmpt.viewDrow[i]);
                                int zindex = Canvas.GetZIndex(canvas);
                                //Canvas.SetZIndex(cmpt.viewDrow[i], zindex + i);
                                Canvas.SetLeft(cmpt.viewDrow[i], startPosition.X);
                                Canvas.SetTop(cmpt.viewDrow[i], startPosition.Y);
                                cmpt.viewDrow[i].Visibility = Visibility.Visible;
                                cmpt.viewDrow[i].super = cmpt;
                                cmpt.viewDrow[i].PreviewMouseDown += new MouseButtonEventHandler(componentItem_PreviewMouseDown);
                                cmpt.viewDrow[i].PreviewMouseMove += new MouseEventHandler(componentItem_PreviewMouseMove);
                                cmpt.viewDrow[i].PreviewMouseUp += new MouseButtonEventHandler(componentItem_PreviewMouseUp);
                            }
                            setting_Resize(cmpt.viewDrow[0]);
                            cmpt.re = cmpt.viewDrow[0].re;
                            cmpt.viewDrow[0].SizeChanged += new SizeChangedEventHandler(Component_SizeChanged);
                            //set_MouseEvent(cmpt);
                            Canvas.SetLeft(cmpt.viewDrow[1], startPosition.X + 7);
                            Canvas.SetTop(cmpt.viewDrow[1], startPosition.Y + 24);
                            ComponentListbox.SelectedIndex = -1;
                            select_component = -1;
                            clickCmpItem.Add(cmpt.viewDrow[0]);
                            base_object = null;
                        }
                        catch
                        {
                        }
                        break;
                    case 1://

                        break;



                }

            }
            if (select_item != -1 && base_object != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    canvas.Children.Remove(drwing[i]);
                }
                if (select_item != 7)
                {
                    try
                    {
                        Mouse.OverrideCursor = Cursors.Arrow;
                        for (int i = 0; i < 4; i++)
                        {
                            canvas.Children.Remove(drwing[i]);
                        }
                        endPosition = e.GetPosition(canvas);

                    }
                    catch
                    {
                    }
                    if (endPosition.X - startPosition.X > 20 && endPosition.Y - startPosition.Y >= 20)
                    {
                        Content cnt;
                        Component cmpt = ((Component)((Component)((Component)base_object).super).viewDrow[0].getSuperItem());
                        Console.WriteLine("cmpt = " + cmpt.Name);
                        cnt = new Content(select_item, endPosition.X - startPosition.X, endPosition.Y - startPosition.Y, startPosition, ((Component)((Component)((Component)base_object).super).viewDrow[0].getSuperItem()));
                        cnt.PreviewMouseDown += new MouseButtonEventHandler(content_PreviewMouseDown);
                        cnt.PreviewMouseMove += new MouseEventHandler(content_PreviewMouseMove);
                        cnt.PreviewMouseUp += new MouseButtonEventHandler(content_PreviewMouseUp);
                        count++;
                        while (isSameName(ContentName[select_item] + count))
                        {
                            count++;
                        }
                        cnt.Name = ContentName[select_item] + count;
                        // content 이벤트 추가해야함
                        cnt.setMove(Canvas.GetZIndex(canvas));
                        Canvas.SetLeft(cnt, startPosition.X);
                        Canvas.SetTop(cnt, startPosition.Y);
                        canvas.Children.Add(cnt);
                        setting_Resize(cnt);
                        cnt.setDistans((double)cnt.GetValue(Canvas.LeftProperty) - (double)((Component)((Component)base_object).super).viewDrow[0].GetValue(LeftProperty)
                            , (double)cnt.GetValue(Canvas.TopProperty) - (double)((Component)((Component)base_object).super).viewDrow[0].GetValue(TopProperty));
                        ((Component)((Component)base_object).super).childen.Add(cnt);
                        cnt.SizeChanged += new SizeChangedEventHandler(Content_SizeChanged);
                    }
                    base_object = null;

                    ItemListbox.SelectedIndex = -1;
                    select_item = -1;

                }
                else
                {
                }

            }
            try
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                for (int i = 0; i < 4; i++)
                {
                    canvas.Children.Remove(drwing[i]);
                }
                endPosition = e.GetPosition(canvas);
                select_item = -1;
            }
            catch
            {
            }

        }


        // 작업중2
        public void Component_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (catchImage != null)
            {
                Console.WriteLine("sizeChange = " + catchImage.Name);
                if (catchImage.getComponentType())//activity_background
                {
                    winForm = new WinForm();
                    winForm.String = ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem().Name;
                    winForm.Point = new Point((double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                        (double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                    winForm.Size = new Size((double)((Component)((Component)catchImage.super)).viewDrow[0].Width,
                    (double)((Component)((Component)catchImage.super)).viewDrow[0].Height);
                    object selected = this.GetType().GetField("winForm",
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                    this.itemProperty.SelectedObject = selected;
                }
                else
                {
                    winForm = new WinForm();
                    winForm.String = ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem().Name;
                    winForm.Point = new Point((double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                        (double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                    winForm.Size = new Size((double)((Component)((Component)catchImage.super)).viewDrow[0].Width,
                    (double)((Component)((Component)catchImage.super)).viewDrow[0].Height);
                    object selected = this.GetType().GetField("winForm",
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                    this.itemProperty.SelectedObject = selected;
                }
            }
        }
        public void Content_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (contentItem != null)
            {
                Console.WriteLine("Content sizeChange = " + contentItem.Name);
                object selected = new object();
                switch (contentItem.getItemNumber())
                {
                    case 0://Button
                        wButton = new WButton();
                        wButton.String = contentItem.Name;
                        wButton.Text = contentItem.Text;
                        wButton.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wButton.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wButton",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 1://Text Box
                        wTextbox = new WTextbox();
                        wTextbox.String = contentItem.Name;
                        wTextbox.Text = contentItem.Text;
                        wTextbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wTextbox.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wTextbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 2://Text Block
                        wTextblock = new WTextblock();
                        wTextblock.String = contentItem.Name;
                        wTextblock.Text = contentItem.Text;
                        wTextblock.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wTextblock.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wTextblock",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 3://List Box
                        wListbox = new WListbox();
                        wListbox.String = contentItem.Name;
                        wListbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wListbox.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wListbox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 4://List View
                        wListview = new WListview();
                        wListview.String = contentItem.Name;
                        wListview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wListview.Size = new Size(contentItem.Width, contentItem.Height);
                        wListview.ContentSize = new Size(contentItem.contentSize.Width, contentItem.contentSize.Height);
                        selected = this.GetType().GetField("wListview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 5://Combo Box
                        wCombobox = new WCombobox();
                        wCombobox.String = contentItem.Name;
                        wCombobox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wCombobox.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wCombobox",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 6://Slide View
                        wSlideview = new WSlideview();
                        wSlideview.String = contentItem.Name;
                        wSlideview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                        wSlideview.Size = new Size(contentItem.Width, contentItem.Height);
                        selected = this.GetType().GetField("wSlideview",
                            System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        break;
                    case 7://Check Box
                        break;
                }
                this.itemProperty.SelectedObject = selected;
            }
        }

        //listbox select Event
        //----------------------------------------------------------------------------------------------

        public void ComponentListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_component = ((ListBox)sender).SelectedIndex;
            select_item = -1;
            Console.WriteLine(((ListBox)sender).SelectedIndex);
        }


        public void ItemListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_item = ((ListBox)sender).SelectedIndex;
            select_component = -1;
            Console.WriteLine("select_item = " + select_item);
        }

        //----------------------------------------------------------------------------------------------



        //이름 중복 체크용
        private bool isSameName(string _name, Content cnt)
        {
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                if (clickCmpItem[i].getSuperItem().Name.Equals(_name))
                    return true;
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    if (clickCmpItem[i].getSuperItem().childen[j].Name.Equals(_name) && !cnt.Equals(clickCmpItem[i].getSuperItem().childen[j]))
                        return true;
                }

            }
            return false;
        }
        private bool isSameName(string _name)
        {
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                if (clickCmpItem[i].getSuperItem().Name.Equals(_name))
                    return true;
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    if (clickCmpItem[i].getSuperItem().childen[j].Name.Equals(_name))
                        return true;
                }

            }
            return false;
        }


        private bool isSameName(string _name, Component cmt)
        {
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                if (clickCmpItem[i].getSuperItem().Name.Equals(_name) && !cmt.Equals(clickCmpItem[i].getSuperItem()))
                    return true;
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    if (clickCmpItem[i].getSuperItem().childen[j].Name.Equals(_name))
                        return true;
                }

            }
            return false;
        }

        private void Item_Checked(object sender, RoutedEventArgs e)
        {
            if (e.Source is RadioButton)
            {
                Console.WriteLine("ddd + " + (e.Source as RadioButton).Content.ToString());
                object selected = this.GetType().GetField((e.Source as RadioButton).Content.ToString(),
                    System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);

                this.itemProperty.SelectedObject = selected;
            }
        }

        private void itemProperty_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("test");
        }

        private void itemProperty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        public void itemProperty_KeyUp(object sender, KeyEventArgs e)
        {
            if (catchImage != null)
            {
                if (isSameName(winForm.String, ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem()))
                {
                    MessageBox.Show("name값은 고유한 값이여야 합니다.");
                    if (catchImage.getComponentType())//activity_background
                    {
                        winForm = new WinForm();
                        winForm.String = ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem().Name;
                        winForm.Point = new Point((double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                            (double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                        winForm.Size = new Size((double)((Component)((Component)catchImage.super)).viewDrow[0].Width,
                        (double)((Component)((Component)catchImage.super)).viewDrow[0].Height);
                        object selected = this.GetType().GetField("winForm",
                        System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        this.itemProperty.SelectedObject = selected;
                    }
                    else
                    {
                        winForm = new WinForm();
                        winForm.String = ((Component)(Component)catchImage.super).viewDrow[0].getSuperItem().Name;
                        winForm.Point = new Point((double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.LeftProperty),
                            (double)((Component)((Component)catchImage.super)).viewDrow[0].GetValue(Canvas.TopProperty));
                        winForm.Size = new Size((double)((Component)((Component)catchImage.super)).viewDrow[0].Width,
                        (double)((Component)((Component)catchImage.super)).viewDrow[0].Height);
                        object selected = this.GetType().GetField("winForm",
                        System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                        this.itemProperty.SelectedObject = selected;
                    }
                    return;
                }
                Console.WriteLine("Here!");
                if (catchImage.getComponentType())//activity_background
                {
                    Console.WriteLine("Here@");
                    ((Component)catchImage.super).setMoveComponent(winForm.Point, (object)catchImage);
                    ((Component)catchImage.super).viewDrow[0].re.setSizeComponent(winForm.Size.Width, winForm.Size.Height, (object)((Component)catchImage.super).viewDrow[0]);
                    ((Component)catchImage.super).viewDrow[0].getSuperItem().Name = winForm.String;
                    ((Component)catchImage.super).viewDrow[0].re.transformation((object)catchImage);
                    //((Component)catchImage.super).viewDrow[0].re.SetPosition(winForm.Point.X,winForm.Point.Y,0);
                    //((Component)catchImage.super).viewDrow[0].re.ResizeUpdateLayout();
                }
                else
                {
                    Console.WriteLine("Here# ");
                    ((Component)catchImage.super).setMoveComponent(new Point(winForm.Point.X + 7, winForm.Point.Y + 24), (object)catchImage);
                    ((Component)catchImage.super).viewDrow[0].re.setSizeComponent(winForm.Size.Width, winForm.Size.Height, (object)((Component)catchImage.super).viewDrow[0]);
                    ((Component)catchImage.super).viewDrow[0].getSuperItem().Name = winForm.String;
                    ((Component)catchImage.super).viewDrow[0].re.transformation((object)catchImage);
                    //((Component)catchImage.super).viewDrow[0].re.SetPosition(winForm.Point.X + 7, winForm.Point.Y + 24,0);
                    //((Component)catchImage.super).viewDrow[0].re.ResizeUpdateLayout();
                }
            }
            if (contentItem != null)
            {
                Console.WriteLine("D Here!");
                string contentName = "";
                switch (contentItem.getItemNumber())
                {
                    case 0://Button
                        contentName = wButton.String;
                        break;
                    case 1://Text Box
                        contentName = wTextbox.String;
                        break;
                    case 2://Text Block
                        contentName = wTextblock.String;
                        break;
                    case 3://List Box
                        contentName = wListbox.String;
                        break;
                    case 4://List View
                        contentName = wListview.String;
                        break;
                    case 5://Combo Box
                        contentName = wCombobox.String;
                        break;
                    case 6://Slide View
                        contentName = wSlideview.String;
                        break;
                    case 7://Check Box
                        contentName = wCheckbox.String;
                        break;
                }
                if (isSameName(contentName, contentItem))
                {
                    MessageBox.Show("name값은 고유한 값이여야 합니다.");
                    object selected = new object();
                    switch (contentItem.getItemNumber())
                    {
                        case 0://Button
                            wButton = new WButton();
                            wButton.String = contentItem.Name;
                            wButton.Text = contentItem.Text;
                            wButton.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wButton.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wButton",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 1://Text Box
                            wTextbox = new WTextbox();
                            wTextbox.String = contentItem.Name;
                            wTextbox.Text = contentItem.Text;
                            wTextbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wTextbox.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wTextbox",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 2://Text Block
                            wTextblock = new WTextblock();
                            wTextblock.String = contentItem.Name;
                            wTextblock.Text = contentItem.Text;
                            wTextblock.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wTextblock.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wTextblock",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 3://List Box
                            wListbox = new WListbox();
                            wListbox.String = contentItem.Name;
                            wListbox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wListbox.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wListbox",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 4://List View
                            wListview = new WListview();
                            wListview.String = contentItem.Name;
                            wListview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wListview.Size = new Size(contentItem.Width, contentItem.Height);
                            wListview.ContentSize = new Size(contentItem.contentSize.Width, contentItem.contentSize.Height);
                            selected = this.GetType().GetField("wListview",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 5://Combo Box
                            wCombobox = new WCombobox();
                            wCombobox.String = contentItem.Name;
                            wCombobox.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wCombobox.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wCombobox",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 6://Slide View
                            wSlideview = new WSlideview();
                            wSlideview.String = contentItem.Name;
                            // = contentItem.Text;
                            wSlideview.Point = new Point((double)contentItem.GetValue(Canvas.LeftProperty), (double)contentItem.GetValue(Canvas.TopProperty));
                            wSlideview.Size = new Size(contentItem.Width, contentItem.Height);
                            selected = this.GetType().GetField("wSlideview",
                                System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(this);
                            break;
                        case 7://Check Box
                            break;
                    }
                    this.itemProperty.SelectedObject = selected;
                    return;
                }
                switch (contentItem.getItemNumber())
                {
                    case 0://Button
                        Console.WriteLine("Button!");
                        contentItem.Name = wButton.String;
                        contentItem.SetValue(Canvas.LeftProperty, wButton.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wButton.Point.Y);
                        contentItem.Text = wButton.Text;
                        contentItem.Width = wButton.Size.Width;
                        contentItem.Height = wButton.Size.Height;
                        contentItem.re.SetSize(wButton.Size.Width, wButton.Size.Height, 0);
                        contentItem.re.SetPosition(wButton.Point.X, wButton.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 1://Text Box
                        Console.WriteLine("Textbox!");
                        contentItem.Name = wTextbox.String;
                        contentItem.SetValue(Canvas.LeftProperty, wTextbox.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wTextbox.Point.Y);
                        contentItem.Text = wTextbox.Text;
                        contentItem.Width = wTextbox.Size.Width;
                        contentItem.Height = wTextbox.Size.Height;
                        contentItem.re.SetSize(wTextbox.Size.Width, wTextbox.Size.Height, 0);
                        contentItem.re.SetPosition(wTextbox.Point.X, wTextbox.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 2://Text Block
                        Console.WriteLine("Textblock!");
                        contentItem.Name = wTextblock.String;
                        contentItem.SetValue(Canvas.LeftProperty, wTextblock.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wTextblock.Point.Y);
                        contentItem.Text = wTextblock.Text;
                        contentItem.Width = wTextblock.Size.Width;
                        contentItem.Height = wTextblock.Size.Height;
                        contentItem.re.SetSize(wTextblock.Size.Width, wTextblock.Size.Height, 0);
                        contentItem.re.SetPosition(wTextblock.Point.X, wTextblock.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 3://List Box
                        Console.WriteLine("Listbox!");
                        contentItem.Name = wListbox.String;
                        contentItem.SetValue(Canvas.LeftProperty, wListbox.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wListbox.Point.Y);
                        contentItem.Width = wListbox.Size.Width;
                        contentItem.Height = wListbox.Size.Height;
                        contentItem.re.SetSize(wListbox.Size.Width, wListbox.Size.Height, 0);
                        contentItem.re.SetPosition(wListbox.Point.X, wListbox.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 4://List View
                        Console.WriteLine("Listview!");
                        contentItem.Name = wListview.String;
                        contentItem.SetValue(Canvas.LeftProperty, wListview.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wListview.Point.Y);
                        contentItem.Width = wListview.Size.Width;
                        contentItem.Height = wListview.Size.Height;
                        contentItem.contentSize.Height = wListview.ContentSize.Height;
                        contentItem.contentSize.Width = wListview.ContentSize.Width;
                        contentItem.re.SetSize(wListview.Size.Width, wListview.Size.Height, 0);
                        contentItem.re.SetPosition(wListview.Point.X, wListview.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 5://Combo Box
                        Console.WriteLine("Combobox!");
                        contentItem.Name = wCombobox.String;
                        contentItem.SetValue(Canvas.LeftProperty, wCombobox.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wCombobox.Point.Y);
                        contentItem.Width = wCombobox.Size.Width;
                        contentItem.Height = wCombobox.Size.Height;
                        contentItem.re.SetSize(wCombobox.Size.Width, wCombobox.Size.Height, 0);
                        contentItem.re.SetPosition(wCombobox.Point.X, wCombobox.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 6://Slide View
                        Console.WriteLine("Slideview!");
                        contentItem.Name = wSlideview.String;
                        contentItem.SetValue(Canvas.LeftProperty, wSlideview.Point.X);
                        contentItem.SetValue(Canvas.TopProperty, wSlideview.Point.Y);
                        contentItem.Width = wSlideview.Size.Width;
                        contentItem.Height = wSlideview.Size.Height;
                        contentItem.re.SetSize(wSlideview.Size.Width, wSlideview.Size.Height, 0);
                        contentItem.re.SetPosition(wSlideview.Point.X, wSlideview.Point.Y, 0);
                        contentItem.re.transformation((object)contentItem);
                        break;
                    case 7://Check Box
                        break;

                }
            }
        }

        //test용 작업
        private void save_xml()
        {
            FileStream fs = new FileStream("prototype\\pass.xml", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("<Information>");
            sw.WriteLine("<type>Windows</type>");
            sw.WriteLine("</Information>");
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                sw.WriteLine("<Component>");
                sw.Write("<name>");
                sw.Write(clickCmpItem[i].getSuperItem().Name);
                sw.WriteLine("</name>");
                sw.Write("<position>" + Canvas.GetLeft(clickCmpItem[i]));
                sw.WriteLine("," + Canvas.GetTop(clickCmpItem[i]) + "</position>");
                sw.Write("<size>" + clickCmpItem[i].Width);
                sw.WriteLine("," + clickCmpItem[i].Height + "</size>");
                sw.WriteLine("<itemList>");
                for (int j = 0; j < clickCmpItem[i].getSuperItem().childen.Count; j++)
                {
                    sw.WriteLine("<item>");
                    sw.WriteLine("<category>" + clickCmpItem[i].getSuperItem().childen[j].getItemType() + "</category>");
                    sw.WriteLine("<name>" + clickCmpItem[i].getSuperItem().childen[j].Name + "</name>");
                    //sw.Write("<position>" + Canvas.GetLeft(clickCmpItem[i].getSuperItem().childen[j]));
                    //sw.WriteLine("," + Canvas.GetTop(clickCmpItem[i].getSuperItem().childen[j]) + "</position>");
                    sw.Write("<position>" + clickCmpItem[i].getSuperItem().childen[j].getWidthDistance());
                    sw.WriteLine("," + clickCmpItem[i].getSuperItem().childen[j].getHeightDistance() + "</position>");
                    sw.Write("<size>" + clickCmpItem[i].getSuperItem().childen[j].Width);
                    sw.WriteLine("," + clickCmpItem[i].getSuperItem().childen[j].Height + "</size>");
                    switch (clickCmpItem[i].getSuperItem().childen[j].getItemNumber())
                    {
                        case 0://Button
                        case 1://Text Box
                        case 2://Text Block
                            sw.Write("<text>");
                            string temp = "_";
                            if (clickCmpItem[i].getSuperItem().childen[j].Text != null && !clickCmpItem[i].getSuperItem().childen[j].Text.Equals(""))
                            {
                                temp = clickCmpItem[i].getSuperItem().childen[j].Text;
                                //temp.
                            }
                            sw.WriteLine(temp + "</text>");
                            break;
                        case 3://List Box
                            sw.WriteLine("<BoxList>");
                            for (int k = 0; k < clickCmpItem[i].getSuperItem().childen[j].listBox.Count; k++)
                            {
                                sw.WriteLine("<bl>");
                                sw.Write("<text>");
                                sw.Write(clickCmpItem[i].getSuperItem().childen[j].listBox[k]);
                                sw.WriteLine("</text>");
                                sw.WriteLine("</bl>");
                            }
                            sw.WriteLine("</BoxList>");
                            break;
                        case 4://List View
                            sw.Write("<TableSize>" + clickCmpItem[i].getSuperItem().childen[j].Width);
                            sw.WriteLine("," + clickCmpItem[i].getSuperItem().childen[j].Height + "</TableSize>");
                            sw.WriteLine("<ViewList>");
                            for (int k = 0; k < clickCmpItem[i].getSuperItem().childen[j].listView.Count; k++)
                            {
                                sw.WriteLine("<vl>");
                                sw.Write("<size>");
                                sw.WriteLine(getPngFromImageControl(clickCmpItem[i].getSuperItem().childen[j].listView[k]).Length + "</size>");
                                //sw.WriteLine((new FileInfo(clickCmpItem[i].getSuperItem().childen[j].listView[k].UriSource.LocalPath)).Length + "</size>");
                                sw.Write("<binary>");
                                //sw.Close();
                                //fs.Close();
                                //fs = new FileStream("pass.xml", FileMode.Append);
                                //bw = new BinaryWriter(fs);
                                byte[] bytetemp = getPngFromImageControl(clickCmpItem[i].getSuperItem().childen[j].listView[k]);
                                for (int l = 0; l < bytetemp.Length; l++)
                                {
                                    sw.Write((int)bytetemp[l]);
                                    sw.Write(" ");
                                }
                                //bw.Close();
                                //fs.Close();
                                //fs = new FileStream("pass.xml", FileMode.Append);
                                //sw = new StreamWriter(fs);
                                sw.WriteLine("</binary>");
                                sw.WriteLine("</vl>");
                            }
                            sw.WriteLine("</ViewList>");
                            break;
                        case 5://Combo Box
                            sw.WriteLine("<BoxList>");
                            for (int k = 0; k < clickCmpItem[i].getSuperItem().childen[j].listBox.Count; k++)
                            {
                                sw.WriteLine("<bl>");
                                sw.Write("<text>");
                                sw.Write(clickCmpItem[i].getSuperItem().childen[j].listBox[k]);
                                sw.WriteLine("</text>");
                                sw.WriteLine("</bl>");
                            }
                            sw.WriteLine("</BoxList>");
                            break;
                        case 6://Slide View
                            sw.WriteLine("<ViewList>");
                            for (int k = 0; k < clickCmpItem[i].getSuperItem().childen[j].listView.Count; k++)
                            {
                                sw.WriteLine("<vl>");
                                sw.Write("<size>");
                                sw.WriteLine(getPngFromImageControl(clickCmpItem[i].getSuperItem().childen[j].listView[k]).Length + "</size>");
                                //sw.WriteLine((new FileInfo(clickCmpItem[i].getSuperItem().childen[j].listView[k].UriSource.LocalPath)).Length + "</size>");
                                sw.Write("<binary>");
                                //sw.Close();
                                //fs.Close();
                                //fs = new FileStream("pass.xml", FileMode.Append);
                                //bw = new BinaryWriter(fs);
                                byte[] bytetemp = getPngFromImageControl(clickCmpItem[i].getSuperItem().childen[j].listView[k]);
                                for (int l = 0; l < bytetemp.Length; l++)
                                {
                                    sw.Write((int)bytetemp[l]);
                                    sw.Write(" ");
                                }
                                //bw.Close();
                                //fs.Close();
                                //fs = new FileStream("pass.xml", FileMode.Append);
                                //sw = new StreamWriter(fs);
                                sw.WriteLine("</binary>");
                                sw.WriteLine("</vl>");
                            }
                            sw.WriteLine("</ViewList>");
                            break;
                        case 7://Check Box
                            break;

                    }
                    sw.WriteLine("<eventList>");
                    if (!clickCmpItem[i].getSuperItem().childen[j].entList.Name.Equals(""))
                    {
                        sw.WriteLine("<el>");
                        sw.Write("<category>click");
                        sw.WriteLine("</category>");
                        sw.Write("<act>" + clickCmpItem[i].getSuperItem().childen[j].entList.getEventType()); sw.WriteLine("</act>");
                        sw.WriteLine("<passComponent>");
                        sw.WriteLine("<name>" + clickCmpItem[i].getSuperItem().childen[j].entList.Name + "</name>");
                        sw.WriteLine("</passComponent>");

                        sw.WriteLine("</el>");
                    }
                    sw.WriteLine("</eventList>");
                    sw.WriteLine("</item>");
                }
                sw.WriteLine("</itemList>");
                sw.WriteLine("</Component>");
            }
            sw.Close();
            fs.Close();
            //File.Create("gogogo.xml").Close();
            //File.Copy("pass.xml", "gogogo.xml", true);
            //File.Delete("pass.xml");
            Console.WriteLine("sucess");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            loadXML();
            save_xml();
            PrototypeMake pm = new PrototypeMake(0);
            while (pm.result() != 0) ;
            test_thread_start();
        }

        private void loadXML()
        {
            Parser pa = new Parser("prototype\\pass2.xml", canvas, this);
            clickCmpItem = pa.getParsingData();
        }

        private void test_upload(string uploadfile, string url, string fileFormName, string contenttype, NameValueCollection querystring, CookieContainer cookies)
        {
            if ((fileFormName == null) || (fileFormName.Length == 0))
            {
                fileFormName = "file";
            }

            if ((contenttype == null) ||
                (contenttype.Length == 0))
            {
                contenttype = "application/octet-stream";
            }


            string postdata;
            postdata = "?";
            if (querystring != null)
            {
                foreach (string key in querystring.Keys)
                {
                    postdata += key + "=" + querystring.Get(key) + "&";
                }
            }
            Uri uri = new Uri(url + postdata);


            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.CookieContainer = cookies;
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";


            // Build up the post message header
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(System.IO.Path.GetFileName(uploadfile));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself
            byte[] boundaryBytes =
                   Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            FileStream fileStream = new FileStream(uploadfile,
                                        FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length +
                                                   boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // Write out our post header
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents
            byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                     (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
            responce.Close();

        }

        private void test_thread_start()
        {
            CookieContainer cookies = new CookieContainer();
            //add or use cookies
            NameValueCollection querystring = new NameValueCollection();
            querystring["id"] = login_ID;
            querystring["project_name"] = project_name;
            string uploadfile;// set to file to upload
            uploadfile = AppDomain.CurrentDomain.BaseDirectory + @"prototype\pass.xml";
            test_upload(uploadfile, file_upload_test_URL, "file", null, querystring, cookies);
            uploadfile = AppDomain.CurrentDomain.BaseDirectory + @"prototype\js\myDraw.js";
            test_upload(uploadfile, file_upload_test_URL, "file", null, querystring, cookies);
            uploadfile = AppDomain.CurrentDomain.BaseDirectory + @"prototype\js\tooltipssm.js";
            test_upload(uploadfile, file_upload_test_URL, "file", null, querystring, cookies);
            Console.WriteLine("test_thread_start sucess");

        }
        private void setStringInput()
        {
            additemState = false;
            viewItemList.Visibility = Visibility.Hidden;
            souceTextblock.Visibility = Visibility.Hidden;

            addItembtn.Content = "Add Content";

            stringItemList.Visibility = Visibility.Visible;
            souceTextbox.Visibility = Visibility.Visible;
        }
        private void setImageInput()
        {
            additemState = true;
            viewItemList.Visibility = Visibility.Visible;
            souceTextblock.Visibility = Visibility.Visible;

            addItembtn.Content = "Add Image";

            stringItemList.Visibility = Visibility.Hidden;
            souceTextbox.Visibility = Visibility.Hidden;
        }


        //add Item 작업중
        private void addItembtn_Click(object sender, RoutedEventArgs e)
        {
            if (additemState)
            {

                if (viewItemList.SelectedIndex == -1)
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.DefaultExt = ".png";
                    dlg.Filter = "PNG |*.png| JPG |*.jpg| GIF |*.gif";

                    Nullable<bool> result = dlg.ShowDialog();

                    if (result == true)
                    {
                        string selectedFileName = dlg.FileName;
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(selectedFileName);
                        bitmap.EndInit();
                        Console.WriteLine("image size = " + bitmap.Width + " , " + bitmap.Height);
                        contentItem.listView.Add(bitmap);
                        List<Image> inputList = new List<Image>();
                        for (int i = 0; i < contentItem.listView.Count; i++)
                        {
                            Image img = new Image();
                            img.BeginInit();
                            img.Source = contentItem.listView[i];
                            img.Width = contentItem.listView[i].Width;
                            img.Height = contentItem.listView[i].Height;
                            img.EndInit();
                            inputList.Add(img);
                        }
                        viewItemList.DataContext = inputList;
                        viewItemList.UpdateLayout();
                        Console.WriteLine(bitmap.UriSource);
                        Console.WriteLine("image Input =" + contentItem.listView.Count);
                    }
                }
                else
                {
                    viewItemList.SelectedIndex = -1;
                }
            }
            else
            {
                if (stringItemList.SelectedIndex == -1 && souceTextbox.Text != "")
                {
                    List<string> stringList = new List<string>();
                    contentItem.listBox.Add(souceTextbox.Text);
                    for (int i = 0; i < contentItem.listBox.Count; i++)
                    {
                        stringList.Add(contentItem.listBox[i]);
                    }
                    souceTextbox.Text = "";
                    stringItemList.DataContext = stringList;
                    stringItemList.UpdateLayout();
                }
                else
                {

                    stringItemList.SelectedIndex = -1;
                }
            }
        }

        public void viewItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewItemList.SelectedIndex != -1)
            {
                imageDeletebtn.Visibility = Visibility.Visible;
                souceTextblock.Text = "Image" + (viewItemList.SelectedIndex + 1);

            }
            else
            {
                imageDeletebtn.Visibility = Visibility.Hidden;
                souceTextblock.Text = "";
            }
        }

        private void imageDeletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (additemState)
            {
                if (viewItemList.SelectedIndex != -1)
                {
                    contentItem.listView.Remove(contentItem.listView[viewItemList.SelectedIndex]);
                    List<Image> inputList = new List<Image>();
                    for (int i = 0; i < contentItem.listView.Count; i++)
                    {
                        Image img = new Image();
                        img.BeginInit();
                        img.Source = contentItem.listView[i];
                        img.Width = contentItem.listView[i].Width;
                        img.Height = contentItem.listView[i].Height;
                        img.EndInit();
                        inputList.Add(img);
                    }
                    viewItemList.DataContext = inputList;
                    viewItemList.UpdateLayout();
                }
            }
            else
            {
                if (stringItemList.SelectedIndex != -1)
                {
                    contentItem.listBox.Remove(contentItem.listBox[stringItemList.SelectedIndex]);
                    List<string> inputList = new List<string>();
                    for (int i = 0; i < contentItem.listBox.Count; i++)
                    {
                        inputList.Add(contentItem.listBox[i]);
                    }
                    souceTextbox.Text = "";
                    stringItemList.DataContext = inputList;
                    stringItemList.UpdateLayout();
                }
            }
        }

        private byte[] getPngFromImageControl(BitmapImage imageC)
        {
            MemoryStream ms = new MemoryStream();
            PngBitmapEncoder pbe = new PngBitmapEncoder();
            pbe.Frames.Add(BitmapFrame.Create(imageC));
            pbe.Save(ms);
            return ms.GetBuffer();
        }



        private void setReflushComponentList()
        {
            List<string> stringtemp = new List<string>();
            stringtemp.Add("null");
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                stringtemp.Add(clickCmpItem[i].getSuperItem().Name);
            }
            Console.WriteLine(btnChoice.super.Name);
            conponetItemList.DataContext = stringtemp;
        }
        private void setReflushComponentList(string name)
        {
            List<string> stringtemp = new List<string>();
            stringtemp.Add("null");
            for (int i = 0; i < clickCmpItem.Count; i++)
            {
                stringtemp.Add(clickCmpItem[i].getSuperItem().Name);
            }
            stringtemp.Remove(name);
            //Console.WriteLine(btnChoice.super.Name);
            conponetItemList.DataContext = stringtemp;
        }

        void conponetItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (btnChoice != null)
                setReflushComponentList();
            contentItem.entList.Name = seleteevent.Text = ((List<string>)conponetItemList.DataContext)[conponetItemList.SelectedIndex];
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PrototypeMake pm;
            MessageBoxResult mbr = MessageBox.Show("Storyboard를 저장하시겠습니까?", "New Storyboard", MessageBoxButton.OKCancel);
            if (mbr.Equals(MessageBoxResult.OK))
            {
                save_xml();
                pm = new PrototypeMake(1);
                while (pm.result() == -2) ;
                test_thread_start();
            }
            else
            {

            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            PrototypeMake pm;
            MessageBoxResult mbr = MessageBox.Show("Storyboard를 저장하시겠습니까?", "New Storyboard", MessageBoxButton.OKCancel);
            if (mbr.Equals(MessageBoxResult.OK))
            {
                save_xml();
                pm = new PrototypeMake(0);
                while (pm.result() == -2) ;
                test_thread_start();

                File.Delete("prototype\\pass2.xml");

                //File.Create("prototype\\pass2.xml").Close() ;
                File.Copy("prototype\\pass.xml", "prototype\\pass2.xml");
            }
            else
            {
                save_xml();
                pm = new PrototypeMake(0);
            }
            MessageBox.Show("컴파일 중입니다. 컴파일이 완료되면 자동으로 실행됩니다.");
        }


    }
}
