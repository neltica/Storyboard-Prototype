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
using System.Windows.Threading;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.IO;

namespace ProjectTest
{
    /// <summary>
    /// MenuWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuWindow : Window
    {
        private string login_ID;
        private string name;

        private const string project_list_URL = "http://210.118.69.110/application/call_project_list.php";
        private const string storyboard_list_URL = "http://210.118.69.110/application/call_storyboard_list.php";


        private const string new_project_temp_URL = "http://210.118.69.110/application/make_project.php";
        private string new_project_URL;

        private const string project_create_URL = "http://210.118.69.110/application/ProjectCreate.php";
        private const string storyboard_view_URL = "http://210.118.69.110/application/storyboard.php";

        private const string storyboard_down_URL = "http://210.118.69.110/application/getxml.php";

        private LoginWindow login_window;
        private StoryboardWindow story_window;
        private ObservableCollection<string> myList;


        public MenuWindow()
        {
            InitializeComponent();
            frist_btn.Visibility = Visibility.Hidden;
        }

        public MenuWindow(string _id, string _name)
        {
            InitializeComponent();
            login_ID = _id;
            name = _name;
            wellcome.Text = name + "님 환영합니다.";
            new_project_URL = new_project_temp_URL + "?id=" + login_ID;
            //storyboard_webbrowser.Navigating += new System.Windows.Navigation.NavigatingCancelEventHandler(storyboard_webbrowser_Navigating);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                project_list_update();
            });
            frist_btn.Visibility = Visibility.Hidden;
            button2.Visibility = Visibility.Hidden;

        }

        void storyboard_webbrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Console.WriteLine(e.Uri.AbsoluteUri);
            if (new Uri(new_project_URL).AbsolutePath != e.Uri.AbsolutePath)
            {
                project_list_update();
            }
        }

        private void test_thread_start()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                this.storyboard_xml_download();
            });
        }

        private void storyboard_xml_download()
        {
            WebClient webClient = new WebClient();
            //webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            NameValueCollection formData = new NameValueCollection();
            formData["id"] = login_ID;
            formData["project_name"] = project_list.SelectedItem.ToString();
            formData["storyboard"] = storyboard_listbox.SelectedIndex.ToString();


            byte[] responseBytes = webClient.UploadValues("storyboard_down_URL", "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);
            //webClient.DownloadFile("http://210.118.69.110/application/getxml.php", "20141011_2.xml");
            //webClient.
            //webClient.
            Console.WriteLine(responsefromserver);
            Console.WriteLine("sucess?");
        }

        void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("tesge");
        }

        private void project_list_update()
        {
            storyboard_listbox.SelectedIndex = -1;
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            formData["id"] = login_ID;

            byte[] responseBytes = webClient.UploadValues(project_list_URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine(responsefromserver);
            webClient.Dispose();
            DataContext = this;
            string[] temp = responsefromserver.Split('#');
            project_list.Items.Clear();
            project_list.BeginInit();
            project_list.Items.Add("새로운 프로젝트 만들기");
            for (int i = 0; i < temp.Length; i++)
            {
                project_list.Items.Add(temp[i]);
            }
            project_list.EndInit();
        }

        private void setProjectCreate(Visibility visible)
        {
            if (visible.Equals(Visibility.Visible))
            {
                textBlock1.Visibility = Visibility.Visible;
                textBlock2.Visibility = Visibility.Visible;
                textBlock3.Visibility = Visibility.Visible;
                project_name.Visibility = Visibility.Visible;
                project_explain.Visibility = Visibility.Visible;
                access_state.Visibility = Visibility.Visible;
                button1.Visibility = Visibility.Visible;
                storyboard_webbrowser.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlock1.Visibility = Visibility.Hidden;
                textBlock2.Visibility = Visibility.Hidden;
                textBlock3.Visibility = Visibility.Hidden;
                project_name.Visibility = Visibility.Hidden;
                project_explain.Visibility = Visibility.Hidden;
                access_state.Visibility = Visibility.Hidden;
                button1.Visibility = Visibility.Hidden;
                storyboard_webbrowser.Visibility = Visibility.Visible;
            }
        }

        private void project_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null)
            {
                return;
            }
            if (((ComboBox)sender).SelectedItem.ToString().Equals("새로운 프로젝트 만들기"))
            {
                frist_btn.Visibility = Visibility.Hidden;
                frist_btn.Content = "New Storyboard";
                setProjectCreate(Visibility.Visible);
                //storyboard_webbrowser.Source = new Uri(new_project_URL);
                //storyboard_webbrowser.
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                {
                    setProjectCreate(Visibility.Hidden);
                    storyboard_list(((ComboBox)sender).SelectedItem.ToString());
                    frist_btn.Content = "Open Storyboard";
                });
            }
            //MessageBox.Show(((ComboBox)sender).SelectedItem.ToString());
        }

        private void storyboard_list(string selectItem)
        {
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            formData["project_name"] = selectItem;
            formData["id"] = login_ID;

            byte[] responseBytes = webClient.UploadValues(storyboard_list_URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine(responsefromserver);
            webClient.Dispose();
            DataContext = this;
            string[] temp = responsefromserver.Split('#');
            myList = new ObservableCollection<string>();
            int state = Int32.Parse(temp[0]);
            switch (state)
            {
                case 0:
                    frist_btn.Visibility = Visibility.Hidden;
                    break;
                case 1: // Project manger
                    frist_btn.Visibility = Visibility.Visible;
                    myList.Add("새로운 Storyboard 만들기");
                    break;
            }
            for (int i = 1; i < temp.Length; i++)
            {
                myList.Add(temp[i]);
                Console.WriteLine(temp[i]);
            }
            this.storyboard_listbox.ItemsSource = myList;
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Logout 하시겠습니까?", "Logout", MessageBoxButton.OKCancel);
            if (mbr.Equals(MessageBoxResult.OK))
            {
                login_window = new LoginWindow();
                App.Current.MainWindow = login_window;
                login_window.Show();
                this.Close();
            }
        }

        private void storyboard_listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frist_btn.Visibility = Visibility.Visible;
            if (((ListBox)sender).SelectedItem.ToString().Equals("새로운 Storyboard 만들기"))
            {
                frist_btn.Content = "New Storyboard";
            }
            else if (((ListBox)sender).SelectedItem.ToString().Equals("2"))
            {
                frist_btn.Visibility = Visibility.Hidden;
            }
            else
            {
                frist_btn.Content = "Open Storyboard";
            }
            if(!((ListBox)sender).SelectedItem.ToString().Equals("새로운 Storyboard 만들기"))
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                {
                    storyboard_webbrowser.Source = new Uri(storyboard_view_URL + "?id=" + login_ID + "&project_name=" + project_list.SelectedItem.ToString() + "&storyboard=" + ((ListBox)sender).SelectedIndex);
                });
            }
        }
        public int DownloadFile(String remoteFilename, String localFilename)
        {

            int bytesProcessed = 0;

            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            try
            {
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                    response = request.GetResponse();
                    if (response != null)
                    {
                        remoteStream = response.GetResponseStream();

                        localStream = File.Create(localFilename);

                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        do
                        {
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            localStream.Write(buffer, 0, bytesRead);

                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            return bytesProcessed;
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            test_thread_start();
            if (storyboard_webbrowser.Visibility.Equals(Visibility.Visible))
            {
                storyboard_webbrowser.Visibility = Visibility.Hidden;
                project_grid.Visibility = Visibility.Visible;
            }
            else
            {
                storyboard_webbrowser.Visibility = Visibility.Visible;
                project_grid.Visibility = Visibility.Hidden;
            }
        }

        private void project_create(string _project_name,string explain, string state)
        {
            WebClient webClient = new WebClient();
            NameValueCollection formData = new NameValueCollection();
            if (state.Equals("Public"))
            {
                state = "Open";
            }
            else
            {
                state = "Close";
            }
            formData["ProjectName"] = _project_name;
            formData["ProjectExplain"] = explain;
            formData["OpenClose"] = state;
            formData["id"] = login_ID;

            Console.WriteLine(login_ID + " " + explain + " " + _project_name + " " + state + " " + project_create_URL);
            byte[] responseBytes = webClient.UploadValues(project_create_URL, "POST", formData);
            string responsefromserver = Encoding.UTF8.GetString(responseBytes);

            Console.WriteLine(responsefromserver);
            webClient.Dispose();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("Project를 생성하시겠습니까?", "Project Create", MessageBoxButton.OKCancel);
            if (mbr.Equals(MessageBoxResult.OK))
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                {
                    Console.WriteLine(access_state.SelectionBoxItem.ToString());
                    project_create(project_name.Text,project_explain.Text,access_state.SelectionBoxItem.ToString());
                    project_name.Text = project_explain.Text = "";
                    //project_list_update();
                    project_list.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
                    {
                        project_list_update();
                    });
                });
                

            }
        }

        private void frist_btn_Click(object sender, RoutedEventArgs e)
        {
            if (frist_btn.Content.Equals("New Storyboard"))
            {
                MessageBoxResult mbr = MessageBox.Show("새로운 Storyboard를 만드시겠습니까?", "New Storyboard", MessageBoxButton.OKCancel);
                if (mbr.Equals(MessageBoxResult.OK))
                {
                    story_window = new StoryboardWindow(login_ID, project_list.SelectedItem.ToString(), this);
                    //App.Current.MainWindow = story_window;
                    story_window.Show();
                    this.Hide();
                }
            }
            else
            {
                story_window = new StoryboardWindow(login_ID, project_list.SelectedItem.ToString(), this,"dse");
                //App.Current.MainWindow = story_window;
                story_window.Show();
                this.Hide();
            }
        }

    }
}
