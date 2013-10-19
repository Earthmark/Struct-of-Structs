using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Items
{
    class Potion : Item
    {
        internal enum PotionType
        {
            None,
            HpRestore,
            ManaRestore,
            AttackBoost,
            DefenseBoost,
            SpeedBoost
        };

        public PotionType Type
        {
            get;
            protected set;
        }

        public Potion(string name, PotionType type = PotionType.None) : base(name)
        {
            Type = type;
        }
    }
}
