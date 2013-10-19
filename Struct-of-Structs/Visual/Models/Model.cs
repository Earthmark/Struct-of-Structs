using System.Runtime.InteropServices;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;

namespace Struct_of_Structs.Visual.Models
{
	public class Model<T> : Cleanup where T : struct, IVertex
	{
		private readonly int stride;
		private readonly int indexCount;
		private readonly Buffer indexBuffer;
		private readonly Buffer vertexBuffer;

		public Model(Device device, int[] indicies, T[] verticies)
		{
			stride = Marshal.SizeOf(GetType().GenericTypeArguments[0]);
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
