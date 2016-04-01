using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTest
{
    public class ComponentType
    {
        public const int MOBLIE = 0;
        public const int WINDOW_FORM = 1;
        public const int WEB_PAGE = 2;

        public int typename;

        public ComponentType(int _typename)
        {
            typename = _typename;
        }


        public int getType()
        {
            return typename;
        }

    }
}
