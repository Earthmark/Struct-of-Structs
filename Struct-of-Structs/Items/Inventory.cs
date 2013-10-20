using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs.Items
{
    class Inventory<T> : QuantList<T> where T: Item
    {
        public Inventory() {}

        public Inventory(IEnumerable<Tuple<T,int>> collection) : base(collection)
        {}

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

        new public Inventory<U> GetByType<U>() where U : class, T
        {
            return new Inventory<U>(from v in this where v.Item1 is U select new Tuple<U, int>((U)v.Item1, v.Item2));
        }
    }
}
