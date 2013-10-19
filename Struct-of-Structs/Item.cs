using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs
{
    class Item
    {
        protected readonly uint ID;
        protected static uint _nextID = 0;
        
        public string Name
        {
            get;
            protected set;
        }

        public Item(string name)
        {
            ID = _nextID++;
            Name = name;
        }

        public static bool operator ==(Item i1, Item i2)
        {
            bool ret;

            if ((object) i1 == null && (object) i2 == null)
            {
                ret = true;
            }
            else if ((object) i1 == null || (object) i2 == null)
            {
                ret = false;
            }
            else
            {
                ret = i1.ID == i2.ID;
            }

            return ret;
        }

        public static bool operator !=(Item i1, Item i2)
        {
            return !(i1 == i2);
        }
    }
}
