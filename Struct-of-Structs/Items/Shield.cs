using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Items
{

    class Shield : Item
    {
        public int Defense
        {
            get;
            protected set;
        }

        public Shield(string name, int defense) : base(name)
        {
            Defense = defense;
        }
    }
}
