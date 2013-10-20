using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Spells
{
    class Spell
    {
        internal enum BasicSpellTypes
        {
            Attack,
            Shield,
            Healing
        }

        public string Name
        {
            get;
            protected set;
        }

        public int Cost
        {
            get;
            protected set;
        }

        public int Power
        {
            get;
            protected set;
        }

        public ElementalTypes Element
        {
            get;
            protected set;
        }

        public BasicSpellTypes SpellType
        {
            get;
            protected set;
        }

        public Spell(string name, int power, int cost, ElementalTypes et, BasicSpellTypes st)
        {
            Name = name;
            Power = power;
            Cost = cost;
            Element = et;
            SpellType = st;
        }

    }
}
