﻿using System;
using SharpDX;

namespace Struct_of_Structs.Visual
{
	public sealed class Graphics : Cleanup
	{
		private readonly DX directX;
		private readonly Camera camera;
		private readonly StdShaderBuffer stdBuffer;
		private readonly IntPtr hwnd;

		public Graphics(int width, int height, bool vSync, IntPtr hwnd)
		{
			this.hwnd = hwnd;
			directX = new DX(width, height, 1000f, 0.1f, vSync, false, hwnd);
			camera = new Camera(new Vector3(0f, 0f, -10f), new Vector3());
			stdBuffer = new StdShaderBuffer(directX.DXDevice);

			OnCleanup += directX.Dispose;
			OnCleanup += stdBuffer.Dispose;
		}

		public void Frame()
		{
			stdBuffer.BindBuffer(directX.DXContext, directX.World, camera.View, directX.Projection);

		}
	}
}