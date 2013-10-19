using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;

namespace Struct_of_Structs.Visual
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorVertex : IVertex
	{
		public Vector4 Position;
		public Color4 Color;

		public int Size()
		{
			return Marshal.SizeOf(typeof(ColorVertex));
		}

		public InputElement[] Layout()
		{
			return new[]
			{
				new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0),
				new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 0)
			};
		}
	}
}
