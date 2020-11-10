// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Citadel Deep/After Image Textured"
{
	Properties
	{
		// Color property for material inspector, default to white
		_MainTex("Texture", 2D) = "white" {}
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
		ZWrite Off
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
#include "UnityCG.cginc"

	struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		fixed4 color : COLOR;
		float2 maskcoord : TEXCOORD1;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		fixed4 color : COLOR;
		half2 maskcoord : TEXCOORD1;
		//fixed4 color : COLOR;
	};

	//sampler2D _MainTex;

	float4 _MainTex_ST;
	float4 _Color;

	float _Intensity;

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = v.color;

		return o;
	}

	sampler2D _MainTex;
	// color from the material

// pixel shader, no inputs needed
	fixed4 frag(v2f i) : SV_Target
	{
	fixed4 col = tex2D(_MainTex,i.texcoord);
	return col * _Color * _Intensity;
}
	ENDCG
}
	}
}