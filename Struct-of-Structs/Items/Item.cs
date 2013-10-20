using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Items
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

        public override bool Equals(object obj)
        {
            bool ret;
            if (!(obj is Item))
            {
                ret = false;
            }
            else
            {
                ret = (ID == ((Item) obj).ID);
            }

            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Item i1, Item i2)
        {
            return (i1 == null) ? (i2 == null) : i1.Equals(i2);
        }

        public static bool operator !=(Item i1, Item i2)
        {
            return !(i1 == i2);
        }
    }
}
