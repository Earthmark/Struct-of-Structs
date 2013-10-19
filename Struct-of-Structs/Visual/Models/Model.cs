using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using Struct_of_Structs.Visual.Vertexes;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Struct_of_Structs.Visual.Models
{
	public class Model<TVertex, TInstance> : Cleanup
		where TVertex : struct, IVertex
		where TInstance : struct, IVertex
	{
		private readonly int vertexStride;
		private readonly int indexCount;
		private readonly Buffer indexBuffer;
		private readonly Buffer vertexBuffer;

		private bool remakeBuffer;
		private int instanceCount;
		private readonly int instanceStride;
		private readonly Buffer instanceBuffer;
		private readonly IDictionary<int, TInstance> instances;
		private readonly BitArray freeInstances;


		public Model(Device device, int instCap, int[] indicies, TVertex[] verticies)
		{
			var generics = GetType().GenericTypeArguments;
			vertexStride = Marshal.SizeOf(generics[0]);
			instanceStride = Marshal.SizeOf(generics[1]);

			indexCount = indicies.Length;
			instanceCount = 0;

			instanceBuffer = new Buffer(device, instanceStride * instCap, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, instanceStride);
			vertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, verticies);
			indexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indicies);

			instances = new Dictionary<int, TInstance>(instCap);
			freeInstances = new BitArray(instCap, true);

			OnCleanup += indexBuffer.Dispose;
			OnCleanup += vertexBuffer.Dispose;
			OnCleanup += instanceBuffer.Dispose;
		}

		public void Draw(DeviceContext context)
		{
			if (remakeBuffer)
				UpdateInstanceBuffer(context);

			context.InputAssembler.SetVertexBuffers(0,
				new VertexBufferBinding(vertexBuffer, vertexStride, 0),
				new VertexBufferBinding(instanceBuffer, instanceStride, 0));
			context.InputAssembler.SetIndexBuffer(indexBuffer, Format.R32_UInt, 0);
			context.DrawIndexedInstanced(indexCount, instanceCount, 0, 0, 0);
		}

		public int AddInstance(DeviceContext context, TInstance instance)
		{
			int pos;
			for(pos = 0; pos < freeInstances.Length; pos++)
			{
				if(freeInstances[pos])
					break;
			}

			if(pos == freeInstances.Length)
				return -1;

			freeInstances[pos] = false;
			instances.Add(pos, instance);
			instanceCount++;
			remakeBuffer = true;
			return pos;
		}

		public void RemoveInstance(DeviceContext context, int index)
		{
			if(instances.ContainsKey(index))
			{
				freeInstances[index] = true;
				instances.Remove(index);
				instanceCount--;
				remakeBuffer = true;
			}
		}

		private void UpdateInstanceBuffer(DeviceContext context)
		{
			DataStream data;
			context.MapSubresource(instanceBuffer, 0, MapMode.WriteDiscard, MapFlags.None, out data);
			foreach(var instance in instances.Values)
			{
				data.Write(instance);
			}
			context.UnmapSubresource(instanceBuffer, 0);
			remakeBuffer = false;
		}
	}
}
