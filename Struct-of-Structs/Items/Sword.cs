using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Struct_of_Structs
{
    class Sword : Item
    {
        public int Power
        {
            get;
            protected set;
        }

        public Sword(string name, int power) : base(name)
        {
            Power = power;
        }
    }
}
