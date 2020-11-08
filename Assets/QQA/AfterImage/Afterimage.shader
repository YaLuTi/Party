// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Citadel Deep/After Image"
{
	Properties
	{
		// Color property for material inspector, default to white
		_Color("Main Color", Color) = (1,1,1,1)
		_Intensity("Intensity", range(0,10)) = 1

		_MKGlowPower("Emission Power", range(0,10)) = 0

		_MKGlowColor("Emission Color", color) = (1.0,0.95,0.8,1.0)

		_StencilMask("Mask Layer", Range(0, 255)) = 1
		[Enum(CompareFunction)] _StencilComp("Mask Mode", Int) = 6

	}
		SubShader
	{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "False" "RenderType" = "Transparent" "RenderType" = "MKGlow" }
		Stencil
		{
			Ref 255
			ReadMask[_StencilMask]
			Comp[_StencilComp]
		}

	Pass
	{
		ZWrite On
		//ZTest LEqual
		ColorMask 0
	}
	
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "False" "RenderType" = "Transparent" "RenderType" = "MKGlow" }
		Pass
	{
		ZTest LEqual
		ZWrite Off
		Blend One OneMinusSrcAlpha
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		// vertex shader
		// this time instead of using "appdata" struct, just spell inputs manually,
		// and instead of returning v2f struct, also just return a single output
		// float4 clip position
		float4 vert(float4 vertex : POSITION) : SV_POSITION
	{
		return UnityObjectToClipPos(vertex);
	}

		// color from the material
	fixed4 _Color;
	float _Intensity;
	fixed4 _MKGlowColor;
	float	_MKGlowPower;
	// pixel shader, no inputs needed
	fixed4 frag() : SV_Target
	{ 
		//_Color.rgb *= _Intensity;
		//_Color.rgb *= _Color.a;
		
		_Color *= _Intensity;
		//_MKGlowColor = _Color;
		//_MKGlowPower = _Intensity;



		return _Color; // just return it
	}
		ENDCG
	}
	}
}