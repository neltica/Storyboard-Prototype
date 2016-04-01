using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTest
{
    class EventListner
    {
        private int eventType;
        public string Name;
        public EventListner()
        {
            Name = "";
            eventType = 0;
        }
        public void setEventType(int type)
        {
            eventType = type;
        }
        public string getEventType()
        {
            switch (eventType)
            {
                case 0:
                    return "onclick";
            }
            return "";
        }
    }
}
