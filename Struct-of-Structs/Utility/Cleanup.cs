using System;

namespace Struct_of_Structs
{
	public abstract class Cleanup : IDisposable
	{
		protected event Action OnCleanup;

		~Cleanup()
		{
			Console.WriteLine("Object {0} not disposed.", this);
			Shutdown();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Shutdown();
		}

		private void Shutdown()
		{
			if (OnCleanup != null) OnCleanup();
		}
	}
}
