using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Win32;
using Struct_of_Structs.Items;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Windows;
using Struct_of_Structs.Control;
using Struct_of_Structs.Visual;

namespace Struct_of_Structs
{
	class Program : Cleanup
	{
		private readonly RenderForm form;
		private readonly Graphics graphics;
		private readonly Input input;
		public TimerTick Timer { get; private set; }
		private bool closed;

		public bool Closed
		{
			get { return closed; }
			set
			{
				closed = value;
				if (!closed && value)
					form.Close();
			}
		}

		static void Main(string[] args)
		{
			var newPath = Path.Combine(Environment.CurrentDirectory, Environment.Is64BitProcess ? "x64" : "x86") + ";" + Environment.GetEnvironmentVariable("PATH");
			Environment.SetEnvironmentVariable("PATH", newPath);

            TestInventory();

            //using(var program = new Program())
            //{
            //    program.Run();
            //}
		}

		private Program()
		{
			form = new RenderForm("Struct-of-Structs");
			graphics = new Graphics(800, 600, false, form.Handle);
			input = new Input(form);
			Timer = new TimerTick();

			OnCleanup += graphics.Dispose;
			OnCleanup += input.Dispose;
			OnCleanup += form.Dispose;
		}

		private void Run()
		{
			RenderLoop.Run(form, Loop);
		}

		private void Loop()
		{
			if (Closed)
				return;

			Timer.Tick();

			if (input[Keys.Escape])
			{
				form.Close();
			}
			else
			{
				graphics.Frame();
			}
		}

        private static void TestInventory()
        {
            Inventory<Item> inv = new Inventory<Item>()
            {
                new Tuple<Item, int>(new Sword("small sword", 1), 20),
                new Tuple<Item, int>(new Sword("medium sword", 10), 10),
                new Tuple<Item, int>(new Sword("big sword", 50), 3),
                new Tuple<Item, int>(new Shield("wooden shield", 3), 20),
                new Tuple<Item, int>(new Shield("rubber shield", 10), 10),
                new Tuple<Item, int>(new Shield("adamantium shield", 1000), 3),
                new Tuple<Item, int>(new Potion("green potion", 5, Potion.PotionType.DefenseBoost), 10),
                new Tuple<Item, int>(new Potion("red potion", 5, Potion.PotionType.AttackBoost), 10),
                new Tuple<Item, int>(new Potion("blue potion", 5, Potion.PotionType.SpeedBoost), 10),
                new Tuple<Item, int>(new Item("Monkey Butts"), 50)
            };

            Inventory<Sword> swords = (Inventory<Sword>)inv.GetByType<Sword>();
            Inventory<Shield> shields = (Inventory<Shield>)inv.GetByType<Shield>();
            Inventory<Potion> potions = (Inventory<Potion>)inv.GetByType<Potion>();

            Console.WriteLine("The unfiltered inventory:");
            inv.Print();
            Console.ReadKey(true);

            Console.WriteLine("Just the swords:");
            swords.Print();
            Console.ReadKey(true);
            
            Console.WriteLine("Just the shields:");
            shields.Print();
            Console.ReadKey(true);
            Console.WriteLine("Just the potions:");
            potions.Print();
            Console.ReadKey(true);
        }
	}


}
