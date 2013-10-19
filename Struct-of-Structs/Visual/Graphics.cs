using System;
using System.Linq;
using SharpDX;
using SharpDX.Direct3D11;
using Struct_of_Structs.Visual.Models;
using Struct_of_Structs.Visual.Shaders;
using Struct_of_Structs.Visual.Vertexes;

namespace Struct_of_Structs.Visual
{
	public sealed class Graphics : Cleanup
	{
		private readonly DX directX;
		private readonly Camera camera;
		private readonly StdShaderBuffer stdBuffer;
		private readonly SpriteManager sprites;
		private readonly IntPtr hwnd;

		public Graphics(int width, int height, bool vSync, IntPtr hwnd)
		{
			this.hwnd = hwnd;
			directX = new DX(width, height, 1000f, 0.1f, vSync, false, hwnd);
			camera = new Camera(new Vector3(0f, 0f, -10f), new Vector3());
			stdBuffer = new StdShaderBuffer(directX.DXDevice);
			
			sprites = new SpriteManager(directX.DXDevice);

			OnCleanup += directX.Dispose;
			OnCleanup += stdBuffer.Dispose;
		}

		public void Frame()
		{
			directX.Begin(Color4.Black);
			stdBuffer.BindBuffer(directX.DXContext, directX.World, camera.View, directX.Projection);
			sprites.Draw(directX.DXContext);
			directX.End();
		}
	}
}
