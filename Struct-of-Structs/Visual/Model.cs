using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;

namespace Struct_of_Structs.Visual
{
	public class Model : Cleanup
	{
		private readonly int stride;
		private readonly int indexCount;
		private readonly Buffer indexBuffer;
		private readonly Buffer vertexBuffer;

		public Model(Device device)
		{
			var indicies = new[] {0, 1, 2};
			var verticies = new[] {new ColorVertex(), new ColorVertex(), new ColorVertex()};

			stride = new ColorVertex().Size();
			indexCount = indicies.Length;

			vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, verticies);
			indexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indicies);

			OnCleanup += indexBuffer.Dispose;
			OnCleanup += vertexBuffer.Dispose;
		}

		public void Draw(DeviceContext context)
		{
			context.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBuffer, stride, 0));
			context.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);
			context.DrawIndexed(indexCount, 0, 0);
		}
	}
}
