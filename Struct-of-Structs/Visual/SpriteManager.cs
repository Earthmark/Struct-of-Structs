using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct3D11;
using Struct_of_Structs.Visual.Models;
using Struct_of_Structs.Visual.Shaders;
using Struct_of_Structs.Visual.Vertexes;

namespace Struct_of_Structs.Visual
{
	public class SpriteManager : Cleanup
	{
		private readonly Model<ColorVertex, InstanceVertex> spriteModel;
		private readonly Shader shader;

		public SpriteManager(Device device)
		{
			spriteModel = new Model<ColorVertex, InstanceVertex>(device, 255,
				new[] {0, 1, 2, 1, 2, 3},
				new[] {
					new ColorVertex(new Vector3(-0.5f, -0.5f, 0f)),
					new ColorVertex(new Vector3(-0.5f, 0.5f, 0f)),
					new ColorVertex(new Vector3(0.5f, -0.5f, 0f)),
					new ColorVertex(new Vector3(0.5f, 0.5f, 0f))}
					);
			shader = new Shader(device, "Shaders/Color.fx", "VertexShaderMethod", "PixelShaderMethod",
							new ColorVertex().Layout().Union(new InstanceVertex().Layout()).ToArray());

			spriteModel.AddInstance(device.ImmediateContext, new InstanceVertex(Vector3.Zero, 3, new Color4(1f, 1f, 1f, 1f)));

			OnCleanup += shader.Dispose;
			OnCleanup += spriteModel.Dispose;
		}

		public void Draw(DeviceContext context)
		{
			shader.Bind(context);
			spriteModel.Draw(context);
		}
	}
}
