using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Win32;
using Struct_of_Structs.Items;

namespace Struct_of_Structs
{
	class Program
	{
		static void Main(string[] args)
		{
			var newPath = Path.Combine(Environment.CurrentDirectory, Environment.Is64BitProcess ? "x64" : "x86") + ";" + Environment.GetEnvironmentVariable("PATH");
			Environment.SetEnvironmentVariable("PATH", newPath);
		}

        //private static void TestInventory()
        //{
        //    Inventory inv = new Inventory()
        //    {
        //        new Tuple<Item, int>(new Sword("small sword", 1), 20),
        //        new Tuple<Item, int>(new Sword("medium sword", 10), 10),
        //        new Tuple<Item, int>(new Sword("big sword", 50), 3),
        //        new Tuple<Item, int>(new Shield("wooden shield", 3), 20),
        //        new Tuple<Item, int>(new Shield("rubber shield", 10), 10),
        //        new Tuple<Item, int>(new Shield("adamantium shield", 1000), 3),
        //        new Tuple<Item, int>(new Potion("green potion", Potion.PotionType.DefenseBoost), 10),
        //        new Tuple<Item, int>(new Potion("red potion", Potion.PotionType.AttackBoost), 10),
        //        new Tuple<Item, int>(new Potion("blue potion", Potion.PotionType.SpeedBoost), 10),
        //        new Tuple<Item, int>(new Item("Monkey Butts"), 50)
        //    };

        //    Console.WriteLine("The unfiltered inventory:");
        //    inv.Print();
        //    Console.ReadKey(true);
        //    Console.WriteLine("Just the swords:");
        //    inv.GetItemsByType<Sword>().Print();
        //    Console.ReadKey(true);
        //    Console.WriteLine("Just the shields:");
        //    inv.GetItemsByType<Shield>().Print();
        //    Console.ReadKey(true);
        //    Console.WriteLine("Just the potions:");
        //    inv.GetItemsByType<Potion>().Print();
        //    Console.ReadKey(true);
        //}
	}


}
