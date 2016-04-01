using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace ProjectTest
{
    public class StoryboardItem : Image
    {
        private ComponentType com_type;
        private Point location;
        private int cnt_type;

        public StoryboardItem super;
        public Resize resize { get; set; }
        public Point position { get; set; }
    }
}
