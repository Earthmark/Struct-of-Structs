using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Struct_of_Structs.Items;

namespace Struct_of_Structs.Players
{
    class Player
    {
        public int Level
        {
            get;
            protected set;
        }

        public int MaxHP
        {
            get;
            protected set;
        }

        public int CurHP
        {
            get;
            protected set;
        }

        public int MaxMana
        {
            get;
            protected set;
        }

        public int CurMana
        {
            get;
            protected set;
        }

        public int AttackPts
        {
            get;
            protected set;
        }

        public int DefPts
        {
            get;
            protected set;
        }

        public int SpeeedPts
        {
            get;
            protected set;
        }

        public Inventory<Item> Inventory
        {
            get;
            protected set;
        }

        //public Inventory TokenInventory
        //{
        //    get;
        //    protected set;
        //}

        public Player(int level)
        {
            Random r = new Random();
            MaxHP = 10;
            for (int i = 0; i < level; ++i)
            {
                MaxHP += r.Next(6);
            }
            CurHP = MaxHP;

            MaxMana = 2 + r.Next(2);

            for (int i = 0; i < level; ++i)
            {
                MaxHP += r.Next(2) + 1;
            }
            CurMana = MaxMana;


        }
    }
}
