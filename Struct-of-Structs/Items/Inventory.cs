using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Items
{
    class Inventory : List<Tuple<Item, int>>
    {
        /// <summary>
        /// add a list of items to the inventory, where the list is itself an inventory
        /// of items T
        /// </summary>
        /// <typeparam name="T">Must be of type item</typeparam>
        /// <param name="collection"></param>
        public void AddRange<T>(IEnumerable<Tuple<T, int>> collection) where T : Item
        {
            foreach (var v in collection)
            {
                Add(new Tuple<Item, int>(v.Item1, v.Item2));
            }
        }

        public int GetQuant(Item i)
        {
            foreach (var v in this)
            {
                if (v.Item1 == i)
                {
                    return v.Item2;
                }
            }
            return -1;
        }

        public Tuple<Item, int> GetTuple(Item i)
        {
            if (i == null)
            {
                return null;
            }
            foreach (var v in this)
            {
                if (v.Item1 == i)
                {
                    return v;
                }
            }
            return null;
        }

        //quant should never be negative. This function
        //will fail quietly (leave without changing anything)
        //if quant is negative
        //TODO: keep order of items even after remove-then-add

        public void AddItem(Item i, int quant = 1)
        {
            if (quant < 0)
            {
                return;
            }
            
            var tuple = GetTuple(i);

            if (tuple == null)
            {
                Add(new Tuple<Item, int>(i, quant));
            }
            else
            {
                Remove(tuple);
                Add(new Tuple<Item, int>(i, quant + tuple.Item2));
            }
        }


        //returns the current amount of item X. -1 means
        //there was nothing to be removed. -2 means quant was
        //negative.
        //quant should never be negative. This function
        //will fail quietly (leave without changing anything)
        //if quant is negative
        //TODO: keep order of items even after remove-then-add
        //TODO: better error handling for quant > current quantity

        public int RemoveItem(Item i, int quant = 1)
        {
            if (quant < 0)
            {
                return -2;
            }

            var tuple = GetTuple(i);
            int ret = 0;

            if (tuple == null)
            {
                return -1;
            }
            else
            {
                if (quant > tuple.Item2)
                {
                    ret = tuple.Item2 - quant;
                }
                else 
                {
                    ret = 0;
                }

                Remove(tuple);
                Add(new Tuple<Item, int>(i, ret));
            }

            return ret;
        } //end of RemoveItem

        public Inventory GetItemsByType<T>() where T: Item
        {
            var inv = new Inventory();
            
            inv.AddRange(this.Where(v => v.Item1 is T));

            return inv;
        }

        public void Print()
        {
            string fmt = "{0, -20}{1,-5}{2, -15}";
            Console.WriteLine(fmt, "name", "qty", "other");
            foreach (var v in this)
            {
                string temp = "";
                Item i = v.Item1;

                if (i is Sword)
                {
                    temp = ((Sword)i).Power.ToString();
                }
                else if (i is Shield)
                {
                    temp = ((Shield)i).Defense.ToString();
                }
                else if (i is Potion)
                {
                    temp = ((Potion)i).Type.ToString();
                }

                Console.WriteLine(fmt, v.Item1.Name, v.Item2, temp);
            }
        }
    }
}
