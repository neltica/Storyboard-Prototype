using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ProjectTest
{
    class ItemList : List<Image>
    {
        public List<Image> component_image;
        public ItemList()
        {
            component_image = new List<Image>();

            BitmapImage bitmap;
            Image img;
            
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\btn_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "button";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\textbox_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "textbox";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\text_block_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "textblock";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\listbox_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "listbox";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\list_view_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "listview";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\combobox_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "combobox";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\sildeview_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "slideview";
            img.Source = bitmap;
            img.Width = 100;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            component_image.Add(img);

            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\test\checkbox_menu.png");
            bitmap.EndInit();
            img = new Image();
            img.BeginInit();
            img.Name = "checkbox";
            img.Source = bitmap;
            img.Width = 100 ;
            img.Height = 30;
            img.Stretch = Stretch.Fill;
            img.EndInit();
            //component_image.Add(img);


            for (int i = 0; i < component_image.Count; i++)
            {
                Add(component_image[i]);
            }
        }
    }
}
