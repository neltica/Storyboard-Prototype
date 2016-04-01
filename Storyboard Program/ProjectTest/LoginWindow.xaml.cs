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
using System.Windows.Shapes;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;

namespace ProjectTest
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {

        private String id;
        private String password;

        private string file_name;

        private const string login_URL = "http://210.118.69.110/application/login.php";
        private const string file_upload_URL = "http://210.118.69.110/application/file_receive.php";

        private const string file_upload_test_URL = "http://210.118.69.110/application/file_receive.php";
        private const string file_download_URL = "http://210.118.69.110/application/file_send.php";

        private const string login_get_name_URL = "http://210.118.69.110/application/get_name.php";

        private int login_state;

        private MenuWindow menu;

        public LoginWindow()
        {
            InitializeComponent();

        }
         
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                this.login_go();
            });
        }

        private void AddImage(Image image)
        {

        }

        private void file_download()
        {
            WebClient webClient = new WebClient();

            byte[] responseBytes = webClient.DownloadData("http://210.118.69.110/application/file_test/111.jpg");
            FileStream _filestream = new FileStream("save.jpg", FileMode.Create, FileAccess.Write);
            _filestream.Write(responseBytes, 0, responseBytes.Length);
            _filestream.Close();
            //BitmapImage bit = new BitmapImage();
            //bit.BeginInit();
            //bit.StreamSource = _filestream;
            //bit.EndInit();
            //Image image = new Image();
            //image.BeginInit();
            //image.Source = bit;
            //image.Width = 100;
            //image.Height = 100;
            //image.Stretch = Stretch.Fill;
            //image.EndInit();
            //canvas_temp.Children.Add(image);
            //Canvas.SetZIndex(image, Canvas.GetZIndex(canvas_temp) + 1);
            //Canvas.SetLeft(image, 1);
            //Canvas.SetTop(image, 1);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            Console.WriteLine(responsefromserver.Length+"");
            //Console.WriteLine(responsefromserver);
            webClient.Dispose();
        }

        private void login_go()
        {
            try
            {

                WebClient webClient = new WebClient();
                NameValueCollection formData = new NameValueCollection();
                id = id_box.Text;
                password = pwd_box.Password;
                formData["id"] = id;
                formData["password"] = password;

                byte[] responseBytes = webClient.UploadValues(login_URL, "POST", formData);
                string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                login_state = Int32.Parse(responsefromserver);
                Console.WriteLine(responsefromserver);
                webClient.Dispose();
                if (login_state == 1)
                {
                    webClient = new WebClient();
                    formData = new NameValueCollection();
                    formData["id"] = id;

                    responseBytes = webClient.UploadValues(login_get_name_URL, "POST", formData);
                    responsefromserver = Encoding.UTF8.GetString(responseBytes);
                    //login_state = Int32.Parse(responsefromserver);
                    //Console.WriteLine(responsefromserver);
                    webClient.Dispose();
                    MessageBox.Show(responsefromserver + "님 환영합니다!");
                    menu = new MenuWindow(id, responsefromserver);
                    App.Current.MainWindow = menu;
                    menu.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("아이디 혹은 비밀번호를 확인하세요.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void login_check()
        {

        }

        //private void file_upload()
        //{
        //    //Stream writeStream = null;
        //    //FileStream readStream = null;
        //    //byte[] buffer = new byte[10000000];
        //    //System.Net.HttpWebRequest webRequest = HttpWebRequest.Create(file_upload_URL) as HttpWebRequest;
        //    //webRequest.Method = "POST";
        //    //webRequest.Accept = "*/*";
        //    //webRequest.UserAgent = "DLNADOC/1.50";
        //    //webRequest.Timeout = System.Threading.Timeout.Infinite;
        //    //webRequest.KeepAlive = true;
        //    //webRequest.SendChunked = true;
        //    //writeStream = webRequest.GetRequestStream();
        //    //readStream = new FileStream(file_name, FileMode.Open);
        //    //int count = 0;
        //    //while ((count = readStream.Read(buffer, 0, buffer.Length)) > 0)
        //    //{
        //    //    writeStream.Write(buffer, 0, count);
        //    //}

        //    //if (readStream != null)
        //    //{
        //    //    try
        //    //    {
        //    //        readStream.Close();
        //    //        readStream.Dispose();
        //    //    }
        //    //    catch { }
        //    //}

        //    //if (writeStream != null)
        //    //{
        //    //    try
        //    //    {
        //    //        writeStream.Close();
        //    //        writeStream.Dispose();
        //    //    }
        //    //    catch { }
        //    //}

        //    //if (webRequest != null)
        //    //{
        //    //    try
        //    //    {
        //    //        System.Net.WebResponse response = webRequest.GetResponse();
        //    //        response.Close();
        //    //    }
        //    //    catch { }
        //    //}

        //    WebClient webClient = new WebClient();

        //    NameValueCollection formData = new NameValueCollection();
        //    webClient.Headers.Add("Content-Type", "binary/octet-stream");
        //    byte[] responseBytes = webClient.UploadFile(file_upload_URL, "POST", FileName.Text);
        //    string responsefromserver = Encoding.UTF8.GetString(responseBytes);
        //    Console.WriteLine(responsefromserver);
        //    webClient.Dispose();
        //}


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
        }

        private void test_thread_start()
        {
            CookieContainer cookies = new CookieContainer();
            //add or use cookies
            NameValueCollection querystring = new NameValueCollection();
            querystring["id"] = "test1";
            querystring["project_name"] = "test3";
            string uploadfile;// set to file to upload
            uploadfile = "C:\\Users\\JeongKW\\Documents\\file.xml";

            //everything except upload file and url can be left blank if needed
            test_upload(uploadfile, file_upload_test_URL, "file", null, querystring, cookies);
        }



//        private void serch_Click(object sender, RoutedEventArgs e)
//        {
//            // Create OpenFileDialog
//            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

//            // Set filter for file extension and default file extension
//            dlg.DefaultExt = ".txt";
////            dlg.Filter = "Text documents (.txt)|*.txt";

//            // Display OpenFileDialog by calling ShowDialog method
//            Nullable<bool> result = dlg.ShowDialog();

//            // Get the selected file name and display in a TextBox
//            if (result == true)
//            {

//                // Open document
//                string filename = dlg.FileName;
//                FileName.Text = filename;
//                file_name = filename;
//            }
//        }

//        private void filepass_Click(object sender, RoutedEventArgs e)
//        {
//            if (FileName.Text.Equals(""))
//            {
//                Thread test = new Thread(this.test_thread_start);
//                test.Start();
//            }
//        }

//        private void button3_Click(object sender, RoutedEventArgs e)
//        {
//            canvas_temp.Dispatcher.BeginInvoke(new Action(this.file_download));
//        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Process process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\Google\\Chrome\\Application\\chrome.exe", "http://210.118.69.110");
            process.Start();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                {
                    this.login_go();
                });
            }
        }

    }

    public class PasswordBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));



        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = d as PasswordBox;
            if (pb == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
            else
            {
                pb.PasswordChanged -= PasswordChanged;
            }
        }

        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }
    }
}