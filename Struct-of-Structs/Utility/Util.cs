using System;

namespace Struct_of_Structs
{
	public static class Util
	{
		public static bool SafeDispose(this IDisposable disposable)
		{
			if(disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			return false;
		}
	}
}
