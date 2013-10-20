using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Struct_of_Structs.Items;
using Struct_of_Structs.Spells;

namespace Struct_of_Structs.Players
{
    class Player
    {
        static Random r = new Random();
            
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

        protected int _baseMana;
        protected int _rangeMana;

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

        protected int _baseAttack;
        protected int _rangeAttack;

        public int AttackPts
        {
            get;
            protected set;
        }

        protected int _baseDef;
        protected int _rangeDef;

        public int DefPts
        {
            get;
            protected set;
        }

        protected int _baseSpeed;
        protected int _rangeSpeed;

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

        public SpellList Spells
        {
            get;
            protected set;
        }

        //public Inventory TokenInventory
        //{
        //    get;
        //    protected set;
        //}

        //TODO: Add ranges for each stat as parameters to be passed in.
        public Player(int level)
        {
            Level = level;

            MaxHP = 10;
            for (int i = 0; i < level; ++i)
            {
                MaxHP += r.Next(6);
            }
            CurHP = MaxHP;

            MaxMana = 2 + r.Next(2);

            for (int i = 0; i < level; ++i)
            {
                MaxMana += r.Next(2) + 1;
            }
            CurMana = MaxMana;

            AttackPts = 3;
            for (int i = 0; i < level; ++i)
            {
                AttackPts += r.Next(3) + 2;
            }

            DefPts = 3;
            for (int i = 0; i < level; ++i)
            {
                DefPts += r.Next(3) + 1;
            }

            SpeeedPts = 0;
            for (int i = 0; i < level; ++i)
            {
                SpeeedPts += r.Next(2);
            }
        }

        public void LevelUp()
        {
            ++Level;

            int temp = r.Next(6);
            MaxHP += temp;
            CurHP += temp;

            temp = r.Next(2) + 1;
            MaxMana += temp;
            CurMana += temp;

            AttackPts += 2 + r.Next(3);
            DefPts += 1 + r.Next(3);
            SpeeedPts += 1 + r.Next(2);
        }

        public void Print()
        {
            Console.WriteLine("level: {0}", Level);
            Console.WriteLine("Health: {0}/{1}", CurHP, MaxHP);
            Console.WriteLine("Mana: {0}/{1}", CurMana, MaxMana);
            Console.WriteLine("attack: {0}", AttackPts);
            Console.WriteLine("defense: {0}", DefPts);
            Console.WriteLine("speed: {0}", SpeeedPts);
        }
    }
}
