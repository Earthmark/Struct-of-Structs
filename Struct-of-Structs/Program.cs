using System;
using System.IO;
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

			using(var program = new Program())
			{
				program.Run();
			}
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
	}


}
