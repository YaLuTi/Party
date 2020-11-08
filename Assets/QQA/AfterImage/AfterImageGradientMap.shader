// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Citadel Deep/After Image Gradient Map" {
	Properties{
		_MainTex("Base (RGBA)", 2D) = "white" {}
	_Color1("Hi Color", color) = (1.0,1.0,1.0,1.0)
		_Step1("Color Cutoff", range(0.0,2.0)) = 0.5
		_Color2("Lo Color", color) = (1.0,0.95,0.8,1.0)
		
		
		_Intensity("Intensity", range(-1,10)) = 1

		_MKGlowPower("Emission Power", range(0,10)) = 0

		_MKGlowColor("Emission Color", color) = (1.0,0.95,0.8,1.0)
	
		_StencilMask("Mask Layer", Range(0, 255)) = 1
		[Enum(CompareFunction)] _StencilComp("Mask Mode", Int) = 6
	}

		SubShader{
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
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "RenderType" = "MKGlow" }
		LOD 100

		//ZWrite Off
		Blend One OneMinusSrcAlpha//SrcAlpha OneMinusSrcAlpha
								  //AlphaToMask On

		Pass{
		Cull Back
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
	float4 _Color1;
	float4 _Color2;
	float _Step1;
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



	fixed4 frag(v2f i) : SV_Target
	{
	fixed4 col = tex2D(_MainTex, i.texcoord);

	if (col.r > _Intensity * 0.1f) discard;

	float blendV = col.r;
	float texA = col.a;

	
	col = lerp(_Color2,_Color1,smoothstep(0,_Step1,blendV));

	
	col *= _Intensity;

	

	col.xyz *= texA;
	col.a *= texA;
	col *= i.color;

	
	
	return fixed4(col.r, col.g, col.b, col.a);
	}
		ENDCG
	}


		
	}

}