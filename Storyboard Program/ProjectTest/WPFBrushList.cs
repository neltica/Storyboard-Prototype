using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace ProjectTest
{
    class WPFBrushList : List<Image>
    {
        public Image[] component_image;
        public WPFBrushList()
        {
            component_image = new Image[50];

            BitmapImage bitmap;

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\winform.png");
            bitmap.EndInit();
            component_image[0] = new Image();
            component_image[0].BeginInit();
            component_image[0].Name = "activity";
            component_image[0].Source = bitmap;
            component_image[0].Width = 80;
            component_image[0].Height = 30;
            component_image[0].Stretch = Stretch.Fill;
            component_image[0].EndInit();
            Add(component_image[0]);

            

            //Type BrushesType = typeof(Brushes);
            //PropertyInfo[] brushesProperty = BrushesType.GetProperties();
            //foreach (PropertyInfo property in brushesProperty)
            //{
            //    BrushConverter brushConverter = new BrushConverter();
            //    Brush brush = (Brush)brushConverter.ConvertFromString(property.Name);
            //    Add(new WPFBrush(property.Name, brush.ToString()));
            //}
        }
    }
    class WPFBrush
    {
        public WPFBrush(string name, Image temp, string hex)
        {
            Name = name;
            Source = temp;
            //Hex = hex;
        }
        //public string Hex { get; set; }
        public Image Source { get; set; }
        public string Name { get; set; }
    }
}
