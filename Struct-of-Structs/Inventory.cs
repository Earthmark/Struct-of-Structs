using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs
{
    class Inventory<T> : List<Tuple<T, int>> where T : Item
    {
        public int GetQuant(T t)
        {
            foreach (var v in this)
            {
                if (v.Item1 == t)
                {
                    return v.Item2;
                }
            }
            return -1;
        }

        public Tuple<T, int> GetTuple(T t)
        {
            if (t == null)
            {
                return null;
            }
            foreach (var v in this)
            {
                if (v.Item1 == t)
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

        public void AddItem(T t, int quant = 1)
        {
            if (quant < 0)
            {
                return;
            }
            
            var tuple = GetTuple(t);

            if (tuple == null)
            {
                Add(new Tuple<T, int>(t, quant));
            }
            else
            {
                Remove(tuple);
                Add(new Tuple<T, int>(t, quant + tuple.Item2));
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

        public int RemoveItem(T t, int quant = 1)
        {
            if (quant < 0)
            {
                return -2;
            }

            var tuple = GetTuple(t);
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
                Add(new Tuple<T, int>(t, ret));
            }

            return ret;
        } //end of RemoveItem

        public Inventory<U> GetItemsByType<U>() where U: Item
        {
            var inv = new Inventory<U>();
            inv.AddRange(this.OfType<Tuple<U, int>>());
            return inv;
        }


    }
}
