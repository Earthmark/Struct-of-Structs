cbuffer matrixBuffer
{
	matrix world;
	matrix view;
	matrix projection;
}

struct VertexInput
{
	float3 localPos : POSITION0;
	float3 globalPos : POSITION1;
	float4 color : COLOR;
	float size : PSIZE;
};

struct PixelInput
{
	float4 position : SV_POSITION;
	float4 color : COLOR;
};

PixelInput VertexShaderMethod(VertexInput input)
{
	PixelInput output;

	input.localPos *= input.size;

	output.position = mul(mul(mul(float4(input.localPos + input.globalPos, 1.0f), world), view), projection);

	output.color = input.color;

	return output;
}

float4 PixelShaderMethod(PixelInput input) : SV_TARGET
{
	return input.color;
}