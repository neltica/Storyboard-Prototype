using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace ProjectTest
{
    class ContentType
    {
        public const int BUTTON = 0;
        public const int TEXTBOX = 1;
        public const int TEXTBLOCK = 2;
        public const int IMAGE = 3;
        public const int SLIDEVIEW = 4;
        public const int LISTVIEW = 5;
        public const int COMBOBOX = 6;
        public const int CHECKBOX = 7;
        public const int SLIDEMENU = 8;
        public const int TOAST = 9;
        public const int VIDEOVIEW = 10;
        public const int WEBVIEW = 11;
        public const int ONOFFBUTTON = 12;

        private int cntType;

        public ContentType(int type)
        {
            cntType = type;
        }

        public BitmapImage getUrl()
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();

            switch (cntType)
            {
                case 0:

                    break;
                case 1:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\start_bt.png");
                    break;
                case 2:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\process_bt.png");
                    break;
                case 3:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\decision_bt.png");
                    break;
                case 4:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\input_bt.png");
                    break;
                case 5:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\print_bt.png");
                    break;
                case 6:
                    //img.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @".\image\line_bt.png");
                    break;
            }

            img.EndInit();
            return img;
        }
    }
}
