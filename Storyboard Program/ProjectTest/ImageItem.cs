using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectTest
{
    public class ImageItem : INotifyPropertyChanged
    {
        private static ImageSource loadingImage;
        private ImageSource image;
        private Task loadingTask;
        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public ImageSource Image
        {
            get
            {
                if (image == null)
                {
                    if (loadingTask == null)
                    {
                        if (loadingImage == null)
                            loadingImage = new BitmapImage(new Uri("loading.png", UriKind.Relative));

                        image = loadingImage;
                        loadingTask = Task.Run<BitmapImage>(() =>
                        {
                            var bi = new BitmapImage();
                            bi.BeginInit();
                            bi.DecodePixelWidth = 64;
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.UriSource = new Uri(path);
                            bi.EndInit();
                            bi.Freeze();
                            return bi;
                        }).ContinueWith(t =>
                        {
                            Image = t.Result;
                            loadingTask = null;
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }

                return image;
            }
            private set
            {
                if (image == value)
                    return;

                image = value;
                OnPropertyChanged("Image");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
