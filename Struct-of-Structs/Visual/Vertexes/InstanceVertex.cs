using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Struct_of_Structs.Visual.Vertexes;

namespace Struct_of_Structs.Visual.Models
{
	[StructLayout(LayoutKind.Sequential)]
	public struct InstanceVertex : IVertex
	{
		public Vector3 Position;
		public float Size;
		public Color4 Color;

		public InstanceVertex(Vector3 pos, float size, Color4 color)
		{
			Position = pos;
			Size = size;
			Color = color;
		}

		public InputElement[] Layout()
		{
			return new InputElement[]
			{
				new InputElement("POSITION", 1, Format.R32G32B32_Float, 0, 1, InputClassification.PerInstanceData, 1), 
				new InputElement("PSIZE", 0, Format.R32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1),
 				new InputElement("COLOR", 0, Format.R32G32B32A32_Float, InputElement.AppendAligned, 1, InputClassification.PerInstanceData, 1), 
			};
		}
	}
}
