struct SpritesVertexToPixel
{
    float4 Position   	: POSITION;
    float1 Size 		: PSIZE;
    float4 Color    	: COLOR0;
};


struct PixelToFrame
{
    float4 Color : COLOR0;
};

//------- Constants --------
float4x4 xView;
float4x4 xProjection;
float4x4 xWorld;
float3 xLightDirection;
bool xEnableLighting;
float xAmbient;

//------- Texture Samplers --------
Texture xTexture;
sampler TextureSampler = sampler_state { texture = <xTexture>; magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR; AddressU = mirror; AddressV = mirror;};

//------- Technique: PointSprites --------

// Vertex shader. Prepares the square that holds the pointsprite
SpritesVertexToPixel PointSpritesVS (float4 Position : POSITION, float4 Color : COLOR0, float1 Size : PSIZE)
{
    SpritesVertexToPixel Output = (SpritesVertexToPixel)0;
     
    float4x4 preViewProjection = mul (xView, xProjection);
	float4x4 preWorldViewProjection = mul (xWorld, preViewProjection); 
    Output.Position = mul(Position, preWorldViewProjection);    
    Output.Color = Color;
    Output.Size = 1/(pow(Output.Position.z,2)+1	) * Size;
    
    return Output;    
}

// Pixel shader. Sets the texture and color of the pointsprite
PixelToFrame PointSpritesPS(SpritesVertexToPixel PSIn, float2 TexCoords  : TEXCOORD0)
{ 
    PixelToFrame Output = (PixelToFrame)0;
    Output.Color = tex2D(TextureSampler, TexCoords);    
    
    Output.Color = float4(
		Output.Color.r * PSIn.Color.r,
		Output.Color.g * PSIn.Color.g,
		Output.Color.b * PSIn.Color.b,
		Output.Color.a
		); 
    return Output;
}

// Technique
technique PointSprites
{
	pass Pass0
    {   
    	PointSpriteEnable = true;
    	VertexShader = compile vs_1_1 PointSpritesVS();
        PixelShader  = compile ps_1_1 PointSpritesPS();
    }
}

