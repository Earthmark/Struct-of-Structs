using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct_of_Structs
{
	class Program
	{
		static void Main(string[] args)
		{
			var newPath = Path.Combine(Environment.CurrentDirectory, Environment.Is64BitProcess ? "x64" : "x86") + ";" + Environment.GetEnvironmentVariable("PATH");
			Environment.SetEnvironmentVariable("PATH", newPath);
		}
	}


}
