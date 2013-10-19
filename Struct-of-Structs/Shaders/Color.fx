matrix world;
matrix view;
matrix projection;

struct VertexInput
{
	float3 position : POSITION;
	float4 color : COLOR;
};

struct PixelInput
{
	float3 position : SV_POSITION;
	float4 color : COLOR;
};

PixelInput VertexShaderMethod(VertexInput input)
{
	PixelInput output;

	output.position = mul(mul(mul(input.position, world), view), projection);

	output.color = input.color;

	return output;
}

color4 PixelShaderMethod(PixelInput input) : SV_TARGET
{
	return input.color;
}