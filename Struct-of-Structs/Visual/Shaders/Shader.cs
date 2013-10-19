using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;

namespace Struct_of_Structs.Visual.Shaders
{
	public class Shader : Cleanup
	{
		private readonly VertexShader vertShader;
		private readonly PixelShader pixShader;
		private readonly InputLayout layout;

		public Shader(Device device, string shaderFile, string vertexTarget, string pixelTraget, InputElement[] layouts)
		{
			var shaderString = ShaderBytecode.PreprocessFromFile(shaderFile);

			using (var bytecode = ShaderBytecode.Compile(shaderString, vertexTarget, "vs_4_0"))
			{
				layout = new InputLayout(device, ShaderSignature.GetInputSignature(bytecode), layouts);
				vertShader = new VertexShader(device, bytecode);
			}

			using (var bytecode = ShaderBytecode.Compile(shaderString, pixelTraget, "vs_4_0"))
			{
				pixShader = new PixelShader(device, bytecode);
			}

			OnCleanup += vertShader.Dispose;
			OnCleanup += pixShader.Dispose;
			OnCleanup += layout.Dispose;
		}

		public void Bind(DeviceContext context)
		{
			context.VertexShader.Set(vertShader);
			context.PixelShader.Set(pixShader);
			context.InputAssembler.InputLayout = layout;
		}
	}
}
