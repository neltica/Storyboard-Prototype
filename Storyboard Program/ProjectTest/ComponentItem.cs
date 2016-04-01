using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTest
{
    class ComponentItem : Component
    {
        //private Component super;
        public ComponentItem(Component _super)
        {
            super = _super;
        }
        public ComponentItem()
        {
        }

        public Component getSuperItem()
        {
            return (Component)super;
        }
    }
}
