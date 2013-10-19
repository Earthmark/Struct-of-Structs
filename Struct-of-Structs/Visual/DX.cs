using System;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;
using Resource = SharpDX.Direct3D11.Resource;

namespace Struct_of_Structs.Visual
{
	public sealed class DX : Cleanup
	{
		private SwapChain swapChain;
		private RasterizerState rasterState;
		private Texture2D depthStenBuffer;
		private RenderTargetView renderTarget;
		private DepthStencilState depthStenState;
		private DepthStencilView depthStenView;

		public Device DXDevice { get; private set; }
		public DeviceContext DXContext { get; private set; }

		public Matrix Projection { get; private set; }
		public Matrix World { get; private set; }
		public Matrix Ortho { get; private set; }

		public DX(int width, int height, float screenDepth, float screenNear, bool vSync, bool fullScreen, IntPtr hwnd)
		{
			var factory = new Factory();
			var adapter = factory.GetAdapter(0);
			var monitor = adapter.Outputs[0];
			var modes = monitor.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);
			
			var rational = new Rational(0, 1);
			if(vSync)
			{
				foreach(var mode in modes)
				{
					if(mode.Width == width && mode.Height == height)
					{
						rational = new Rational(mode.RefreshRate.Numerator, mode.RefreshRate.Denominator);
						break;
					}
				}
			}

			monitor.Dispose();
			adapter.Dispose();
			factory.Dispose();

			var swapChainDesc = new SwapChainDescription
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(width, height, rational, Format.R8G8B8A8_UNorm),
				Usage = Usage.RenderTargetOutput,
				OutputHandle = hwnd,
				SampleDescription = new SampleDescription(1, 0),
				IsWindowed = !fullScreen,
				Flags = SwapChainFlags.None,
				SwapEffect = SwapEffect.Discard
			};

			Device device;
			Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, new[] {FeatureLevel.Level_10_0}, swapChainDesc, out device, out swapChain);

			DXDevice = device;
			DXContext = device.ImmediateContext;

			var backBuffer = Resource.FromSwapChain<Texture2D>(swapChain, 0);
			renderTarget = new RenderTargetView(device, backBuffer);
			backBuffer.Dispose();

			var depthBufferDesc = new Texture2DDescription
			{
				Width = width,
				Height = height,
				MipLevels = 1,
				ArraySize = 1,
				Format = Format.D24_UNorm_S8_UInt,
				SampleDescription = new SampleDescription(1, 0),
				Usage = ResourceUsage.Default,
				BindFlags = BindFlags.DepthStencil,
				CpuAccessFlags = CpuAccessFlags.None,
				OptionFlags = ResourceOptionFlags.None
			};
			depthStenBuffer = new Texture2D(device, depthBufferDesc);

			var depthStencilDesc = new DepthStencilStateDescription
			{
				IsDepthEnabled = true,
				DepthWriteMask = DepthWriteMask.All,
				DepthComparison = Comparison.Less,
				IsStencilEnabled = true,
				StencilReadMask = 0xFF,
				StencilWriteMask = 0xFF,
				FrontFace = new DepthStencilOperationDescription
				{
					FailOperation = StencilOperation.Keep,
					DepthFailOperation = StencilOperation.Increment,
					PassOperation = StencilOperation.Keep,
					Comparison = Comparison.Always
				},
				BackFace = new DepthStencilOperationDescription
				{
					FailOperation = StencilOperation.Keep,
					DepthFailOperation = StencilOperation.Decrement,
					PassOperation = StencilOperation.Keep,
					Comparison = Comparison.Always
				}
			};

			depthStenState = new DepthStencilState(DXDevice, depthStencilDesc);

			DXContext.OutputMerger.SetDepthStencilState(depthStenState, 1);

			var depthStencilViewDesc = new DepthStencilViewDescription
			{
				Format = Format.D24_UNorm_S8_UInt,
				Dimension = DepthStencilViewDimension.Texture2D,
				Texture2D = new DepthStencilViewDescription.Texture2DResource
				{
					MipSlice = 0
				}
			};

			depthStenView = new DepthStencilView(DXDevice, depthStenBuffer, depthStencilViewDesc);
			DXContext.OutputMerger.SetTargets(depthStenView, renderTarget);

			var rasterDesc = new RasterizerStateDescription
			{
				IsAntialiasedLineEnabled = false,
				CullMode = CullMode.Back,
				DepthBias = 0,
				DepthBiasClamp = .0f,
				IsDepthClipEnabled = true,
				FillMode = FillMode.Solid,
				IsFrontCounterClockwise = false,
				IsMultisampleEnabled = false,
				IsScissorEnabled = false,
				SlopeScaledDepthBias = .0f
			};

			rasterState = new RasterizerState(DXDevice, rasterDesc);
			DXContext.Rasterizer.State = rasterState;
			DXContext.Rasterizer.SetViewport(0, 0, width, height);

			Projection = Matrix.PerspectiveFovLH((float) (Math.PI / 4), ((float) width) / height, screenNear, screenDepth);
			World = Matrix.Identity;
			Ortho = Matrix.OrthoLH(width, height, screenNear, screenDepth);

			OnCleanup += swapChain.Dispose;
			OnCleanup += rasterState.Dispose;
			OnCleanup += depthStenBuffer.Dispose;
			OnCleanup += depthStenState.Dispose;
			OnCleanup += depthStenView.Dispose;
			OnCleanup += renderTarget.Dispose;
			OnCleanup += DXContext.Dispose;
			OnCleanup += DXDevice.Dispose;
		}
	}
}
