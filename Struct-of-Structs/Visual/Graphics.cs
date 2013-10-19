using System;
using SharpDX;
using Struct_of_Structs.Visual.Models;
using Struct_of_Structs.Visual.Shaders;

namespace Struct_of_Structs.Visual
{
	public sealed class Graphics : Cleanup
	{
		private readonly DX directX;
		private readonly Camera camera;
		private readonly Shader shader;
		private readonly StdShaderBuffer stdBuffer;
		private readonly IntPtr hwnd;

		private readonly Model<ColorVertex> model;

		public Graphics(int width, int height, bool vSync, IntPtr hwnd)
		{
			this.hwnd = hwnd;
			directX = new DX(width, height, 1000f, 0.1f, vSync, false, hwnd);
			camera = new Camera(new Vector3(0f, 0f, -10f), new Vector3());
			stdBuffer = new StdShaderBuffer(directX.DXDevice);
			shader = new Shader(directX.DXDevice, "Shaders/Color.fx", "VertexShaderMethod", "PixelShaderMethod", new ColorVertex().Layout());
			model = new Model<ColorVertex>(directX.DXDevice, new []{0, 1, 2}, new []{new ColorVertex(), });

			OnCleanup += directX.Dispose;
			OnCleanup += stdBuffer.Dispose;
		}

		public void Frame()
		{
			directX.Begin(Color4.Black);
			stdBuffer.BindBuffer(directX.DXContext, directX.World, camera.View, directX.Projection);
			shader.Bind(directX.DXContext);
			model.Draw(directX.DXContext);
			directX.End();
		}
	}
}
