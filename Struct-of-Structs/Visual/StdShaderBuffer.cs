using System.Diagnostics.Contracts;
using SharpDX;
using SharpDX.Direct3D11;

namespace Struct_of_Structs.Visual
{
	public sealed class StdShaderBuffer : Cleanup
	{
		private readonly int bufferSlot;
		private readonly int matrixCount;
		private readonly Buffer matrixBuffer;

		public StdShaderBuffer(Device device, int bufferSlot = 0, int matrixCount = 3)
		{
			Contract.Assert(device != null);
			Contract.Assert(bufferSlot >= 0);
			Contract.Assert(matrixCount >= 0);

			this.matrixCount = matrixCount;
			this.bufferSlot = bufferSlot;
			matrixBuffer = new Buffer(device, Matrix.SizeInBytes * matrixCount, ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);

			OnCleanup += matrixBuffer.Dispose;
		}

		public void BindBuffer(DeviceContext context, params Matrix[] matricies)
		{
			Contract.Assert(matricies.Length == matrixCount);

			DataStream data;
			context.MapSubresource(matrixBuffer, 0, MapMode.WriteDiscard, MapFlags.None, out data);

			foreach(var matrix in matricies)
			{
				data.Write(matrix);
			}

			context.UnmapSubresource(matrixBuffer, 0);
			context.VertexShader.SetConstantBuffer(bufferSlot, matrixBuffer);
		}
	}
}
