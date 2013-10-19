using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;

namespace Struct_of_Structs.Visual.Vertexes
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorVertex : IVertex
	{
		public Vector3 Position;

		public ColorVertex(Vector3 position)
		{
			Position = position;
		}

		public InputElement[] Layout()
		{
			return new[]
			{
				new InputElement("POSITION", 0, Format.R32G32B32_Float, 0)
			};
		}
	}
}
