// Upgrade NOTE: upgraded instancing buffer 'ZianFlipBookRain' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Zian/FlipBookRain"
{
	Properties
	{
		_Tint("Tint", Color) = (1,1,1,0)
		_Offset("Offset", Vector) = (0,0,0,0)
		_Tiling("Tiling", Vector) = (1,1,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_AO("AO", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		_Wetness("Wetness", Range( 0 , 1)) = 0.8
		_Speed("Speed", Float) = 1
		_RippleTiling("Ripple Tiling", Float) = 1
		_RippleNormals("Ripple Normals", 2D) = "white" {}
		_RippleMaskSpeed("Ripple Mask Speed", Float) = 0.35
		_RippleStrength("Ripple Strength", Float) = 0.5
		_PuddleTiling("Puddle Tiling", Float) = 0
		_PuddleStrength("Puddle Strength", Float) = 0.3
		_PuddleNormal("PuddleNormal", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform sampler2D _RippleNormals;
		uniform float _RippleStrength;
		uniform float _RippleMaskSpeed;
		uniform float _RippleTiling;
		uniform float _PuddleStrength;
		uniform sampler2D _PuddleNormal;
		uniform float _PuddleTiling;
		uniform sampler2D _Albedo;
		uniform sampler2D _AO;

		UNITY_INSTANCING_BUFFER_START(ZianFlipBookRain)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Tint)
#define _Tint_arr ZianFlipBookRain
			UNITY_DEFINE_INSTANCED_PROP(float2, _Tiling)
#define _Tiling_arr ZianFlipBookRain
			UNITY_DEFINE_INSTANCED_PROP(float2, _Offset)
#define _Offset_arr ZianFlipBookRain
			UNITY_DEFINE_INSTANCED_PROP(float, _Speed)
#define _Speed_arr ZianFlipBookRain
			UNITY_DEFINE_INSTANCED_PROP(float, _Wetness)
#define _Wetness_arr ZianFlipBookRain
		UNITY_INSTANCING_BUFFER_END(ZianFlipBookRain)


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 _Tiling_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tiling_arr, _Tiling);
			float2 _Offset_Instance = UNITY_ACCESS_INSTANCED_PROP(_Offset_arr, _Offset);
			float2 uv_TexCoord64 = i.uv_texcoord * _Tiling_Instance + _Offset_Instance;
			float3 BaseNormals60 = UnpackNormal( tex2D( _NormalMap, uv_TexCoord64 ) );
			float temp_output_35_0 = ( _Time.y * _RippleMaskSpeed );
			float RippleTile16 = _RippleTiling;
			float2 temp_cast_0 = (RippleTile16).xx;
			float2 uv_TexCoord31 = i.uv_texcoord * temp_cast_0;
			float2 panner32 = ( temp_output_35_0 * float2( 1,1 ) + uv_TexCoord31);
			float simplePerlin2D30 = snoise( panner32 );
			simplePerlin2D30 = simplePerlin2D30*0.5 + 0.5;
			float2 temp_cast_1 = (RippleTile16).xx;
			float2 uv_TexCoord43 = i.uv_texcoord * temp_cast_1;
			float2 panner42 = ( temp_output_35_0 * float2( -1,-1 ) + uv_TexCoord43);
			float simplePerlin2D41 = snoise( panner42 );
			simplePerlin2D41 = simplePerlin2D41*0.5 + 0.5;
			float RippleMask37 = ( _RippleStrength * ( simplePerlin2D30 + simplePerlin2D41 ) );
			float2 temp_cast_2 = (RippleTile16).xx;
			float2 uv_TexCoord4 = i.uv_texcoord * temp_cast_2;
			float4 appendResult12 = (float4(frac( uv_TexCoord4.x ) , frac( uv_TexCoord4.y ) , 0.0 , 0.0));
			float temp_output_4_0_g3 = 4.0;
			float temp_output_5_0_g3 = 4.0;
			float2 appendResult7_g3 = (float2(temp_output_4_0_g3 , temp_output_5_0_g3));
			float totalFrames39_g3 = ( temp_output_4_0_g3 * temp_output_5_0_g3 );
			float2 appendResult8_g3 = (float2(totalFrames39_g3 , temp_output_5_0_g3));
			float _Speed_Instance = UNITY_ACCESS_INSTANCED_PROP(_Speed_arr, _Speed);
			float temp_output_8_0 = ( _Time.y * _Speed_Instance );
			float clampResult42_g3 = clamp( 0.0 , 0.0001 , ( totalFrames39_g3 - 1.0 ) );
			float temp_output_35_0_g3 = frac( ( ( temp_output_8_0 + clampResult42_g3 ) / totalFrames39_g3 ) );
			float2 appendResult29_g3 = (float2(temp_output_35_0_g3 , ( 1.0 - temp_output_35_0_g3 )));
			float2 temp_output_15_0_g3 = ( ( appendResult12.xy / appendResult7_g3 ) + ( floor( ( appendResult8_g3 * appendResult29_g3 ) ) / appendResult7_g3 ) );
			float2 temp_cast_4 = (( RippleTile16 / 0.6 )).xx;
			float2 uv_TexCoord19 = i.uv_texcoord * temp_cast_4 + float2( 0.76,0.29 );
			float4 appendResult22 = (float4(frac( uv_TexCoord19.x ) , frac( uv_TexCoord19.y ) , 0.0 , 0.0));
			float temp_output_4_0_g4 = 4.0;
			float temp_output_5_0_g4 = 4.0;
			float2 appendResult7_g4 = (float2(temp_output_4_0_g4 , temp_output_5_0_g4));
			float totalFrames39_g4 = ( temp_output_4_0_g4 * temp_output_5_0_g4 );
			float2 appendResult8_g4 = (float2(totalFrames39_g4 , temp_output_5_0_g4));
			float clampResult42_g4 = clamp( 0.0 , 0.0001 , ( totalFrames39_g4 - 1.0 ) );
			float temp_output_35_0_g4 = frac( ( ( temp_output_8_0 + clampResult42_g4 ) / totalFrames39_g4 ) );
			float2 appendResult29_g4 = (float2(temp_output_35_0_g4 , ( 1.0 - temp_output_35_0_g4 )));
			float2 temp_output_15_0_g4 = ( ( appendResult22.xy / appendResult7_g4 ) + ( floor( ( appendResult8_g4 * appendResult29_g4 ) ) / appendResult7_g4 ) );
			float3 RippleNormals25 = BlendNormals( UnpackScaleNormal( tex2D( _RippleNormals, temp_output_15_0_g3 ), RippleMask37 ) , UnpackScaleNormal( tex2D( _RippleNormals, temp_output_15_0_g4 ), RippleMask37 ) );
			float2 temp_cast_6 = (_PuddleTiling).xx;
			float2 uv_TexCoord72 = i.uv_texcoord * temp_cast_6;
			float3 PuddleNormals71 = UnpackScaleNormal( tex2D( _PuddleNormal, uv_TexCoord72 ), _PuddleStrength );
			o.Normal = BlendNormals( BlendNormals( BaseNormals60 , RippleNormals25 ) , PuddleNormals71 );
			float4 _Tint_Instance = UNITY_ACCESS_INSTANCED_PROP(_Tint_arr, _Tint);
			float4 Albedo56 = ( ( tex2D( _Albedo, uv_TexCoord64 ) * tex2D( _AO, uv_TexCoord64 ) ) * _Tint_Instance );
			o.Albedo = Albedo56.rgb;
			float _Wetness_Instance = UNITY_ACCESS_INSTANCED_PROP(_Wetness_arr, _Wetness);
			o.Smoothness = _Wetness_Instance;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
0;73;1029;966;3205.784;321.2946;1.812502;True;False
Node;AmplifyShaderEditor.CommentaryNode;17;-4400.258,-893.2592;Inherit;False;530.001;286.2209;Comment;2;16;5;Ripple Tile;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-4350.258,-843.2591;Float;False;Property;_RippleTiling;Ripple Tiling;8;0;Create;True;0;0;False;0;1;10.23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;39;-3920.292,-3553.978;Inherit;False;2491.664;1341.809;Comment;15;36;45;37;42;34;41;43;44;31;32;30;35;33;46;47;Ripple Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;16;-4110.376,-842.3427;Float;False;RippleTile;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;-3811.373,-2557.015;Inherit;False;16;RippleTile;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-3843.261,-2911.702;Float;True;Property;_RippleMaskSpeed;Ripple Mask Speed;10;0;Create;True;0;0;False;0;0.35;0.38;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;34;-3855.195,-3192.736;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-3870.292,-3503.978;Inherit;False;16;RippleTile;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-3443.335,-3496.096;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;38;-3522.274,-1036.062;Inherit;False;2612.508;1282.958;Comment;22;1;24;12;2;23;18;4;28;19;21;20;6;7;22;8;11;10;15;14;3;25;40;Ripple Normals;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-3407.01,-2795.106;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-3424.814,-3172.649;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;23;-3472.274,-393.4473;Inherit;False;16;RippleTile;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;32;-3126.912,-3446.042;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;42;-3066.441,-2783.685;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;30;-2786.222,-3428.393;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-3446.247,-855.6519;Inherit;False;16;RippleTile;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-3225.163,-499.0976;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;41;-2737.404,-2928.397;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-3195.838,-871.9106;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-2317.404,-3360.11;Float;False;Property;_RippleStrength;Ripple Strength;11;0;Create;True;0;0;False;0;0.5;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-2364.589,-3067.969;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-3154.143,-228.4525;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0.76,0.29;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-2029.487,-3225.093;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;20;-2930.884,-218.7532;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;21;-2934.913,-7.104484;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-2756.618,-308.8037;Float;True;InstancedProperty;_Speed;Speed;7;0;Create;True;0;0;False;0;1;10.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;11;-2887.968,-744.5021;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;6;-2760.219,-536.0511;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;10;-2897.233,-986.0621;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;37;-1776.526,-3215.167;Float;False;RippleMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-2581.849,-337.1487;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-2574.448,-62.43694;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;-2640.057,-969.7407;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector2Node;69;-1745.362,-2186.519;Float;False;InstancedProperty;_Tiling;Tiling;2;0;Create;True;0;0;False;0;1,1;3,3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;68;-1728.951,-1909.173;Float;False;InstancedProperty;_Offset;Offset;1;0;Create;True;0;0;False;0;0,0;0.8,1.47;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;40;-2249.677,-422.9703;Inherit;False;37;RippleMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1;-2291.051,-694.9282;Inherit;True;Property;_RippleNormals;Ripple Normals;9;0;Create;True;0;0;False;0;None;89a10b0a1ce3db74c94ceb0fe24e563e;True;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.FunctionNode;2;-2384.749,-899.4248;Inherit;False;Flipbook;-1;;3;53c2488c220f6564ca6c90721ee16673;2,71,0,68,0;8;51;SAMPLER2D;0.0;False;13;FLOAT2;0,0;False;4;FLOAT;4;False;5;FLOAT;4;False;24;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;FLOAT;62
Node;AmplifyShaderEditor.FunctionNode;15;-2293.344,-166.6408;Inherit;False;Flipbook;-1;;4;53c2488c220f6564ca6c90721ee16673;2,71,0,68,0;8;51;SAMPLER2D;0.0;False;13;FLOAT2;0,0;False;4;FLOAT;4;False;5;FLOAT;4;False;24;FLOAT;0;False;2;FLOAT;0;False;55;FLOAT;0;False;70;FLOAT;0;False;5;COLOR;53;FLOAT2;0;FLOAT;47;FLOAT;48;FLOAT;62
Node;AmplifyShaderEditor.SamplerNode;3;-1866.563,-616.9789;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;64;-1453.838,-2128.335;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;77;-2826.67,698.5026;Float;True;Property;_PuddleTiling;Puddle Tiling;12;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1849.929,-295.0515;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;-1;89a10b0a1ce3db74c94ceb0fe24e563e;89a10b0a1ce3db74c94ceb0fe24e563e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;66;-885.361,-2403.182;Inherit;False;675.6459;309;Comment;2;59;60;Normal Map;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;58;-881.3,-1914.177;Inherit;False;1516.527;741.5977;Comment;6;52;51;53;50;55;56;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-2350.457,982.6047;Float;False;Property;_PuddleStrength;Puddle Strength;13;0;Create;True;0;0;False;0;0.3;0.16;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;24;-1431.891,-419.3203;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;53;-827.5492,-1633.121;Inherit;True;Property;_AO;AO;4;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;52;-831.3,-1864.177;Inherit;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;-1;None;98cca8deac0b1894989f3a2e7dc4c107;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;72;-2329.335,634.3263;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;59;-835.361,-2343.104;Inherit;True;Property;_NormalMap;NormalMap;5;0;Create;True;0;0;False;0;-1;None;60b29ceb82cfd1c4bae1a09884a1fa60;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-407.4865,-1760.749;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;83;-1918.01,634.9913;Inherit;True;Property;_PuddleNormal;PuddleNormal;14;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;51;-738.6068,-1384.579;Inherit;False;InstancedProperty;_Tint;Tint;0;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;60;-433.7151,-2353.182;Float;False;BaseNormals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;25;-1133.766,-427.3191;Float;False;RippleNormals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;71;-1564.079,644.1635;Float;True;PuddleNormals;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;61;-285.9043,-728.5772;Inherit;False;60;BaseNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;26;-273.0754,-451.5354;Inherit;False;25;RippleNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-34.70799,-1706.128;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;75;56.66683,-495.3503;Inherit;False;71;PuddleNormals;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BlendNormalsNode;63;-46.63089,-672.4681;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;56;411.2272,-1590.571;Float;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;57;308.3293,-784.5155;Inherit;False;56;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;74;349.6282,-659.4499;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;9;333.4827,-389.4476;Float;False;InstancedProperty;_Wetness;Wetness;6;0;Create;True;0;0;False;0;0.8;0.83;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;880.7529,-594.3741;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Zian/FlipBookRain;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;5;0
WireConnection;31;0;33;0
WireConnection;43;0;44;0
WireConnection;35;0;34;0
WireConnection;35;1;36;0
WireConnection;32;0;31;0
WireConnection;32;1;35;0
WireConnection;42;0;43;0
WireConnection;42;1;35;0
WireConnection;30;0;32;0
WireConnection;28;0;23;0
WireConnection;41;0;42;0
WireConnection;4;0;18;0
WireConnection;45;0;30;0
WireConnection;45;1;41;0
WireConnection;19;0;28;0
WireConnection;47;0;46;0
WireConnection;47;1;45;0
WireConnection;20;0;19;1
WireConnection;21;0;19;2
WireConnection;11;0;4;2
WireConnection;10;0;4;1
WireConnection;37;0;47;0
WireConnection;8;0;6;0
WireConnection;8;1;7;0
WireConnection;22;0;20;0
WireConnection;22;1;21;0
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;2;13;12;0
WireConnection;2;2;8;0
WireConnection;15;13;22;0
WireConnection;15;2;8;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;3;5;40;0
WireConnection;64;0;69;0
WireConnection;64;1;68;0
WireConnection;14;0;1;0
WireConnection;14;1;15;0
WireConnection;14;5;40;0
WireConnection;24;0;3;0
WireConnection;24;1;14;0
WireConnection;53;1;64;0
WireConnection;52;1;64;0
WireConnection;72;0;77;0
WireConnection;59;1;64;0
WireConnection;50;0;52;0
WireConnection;50;1;53;0
WireConnection;83;1;72;0
WireConnection;83;5;78;0
WireConnection;60;0;59;0
WireConnection;25;0;24;0
WireConnection;71;0;83;0
WireConnection;55;0;50;0
WireConnection;55;1;51;0
WireConnection;63;0;61;0
WireConnection;63;1;26;0
WireConnection;56;0;55;0
WireConnection;74;0;63;0
WireConnection;74;1;75;0
WireConnection;0;0;57;0
WireConnection;0;1;74;0
WireConnection;0;4;9;0
ASEEND*/
//CHKSM=7E86013EC2C5596E2E980246E5C979347FC2DE73