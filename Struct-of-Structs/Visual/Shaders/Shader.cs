using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;

namespace Struct_of_Structs.Visual.Shaders
{
	public abstract class Shader : Cleanup
	{
		private VertexShader vertShader;
		private PixelShader pixShader;
		private InputLayout layout;

	}
}
