// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_Bomb"
{
	Properties
	{
		_Base_Albedo("Base_Albedo", 2D) = "white" {}
		_Base_Normal("Base_Normal", 2D) = "bump" {}
		[Toggle(_USETEXTURE_ON)] _UseTexture("UseTexture?", Float) = 0
		[Toggle(_USEEMISSION_ON)] _UseEmission("UseEmission?", Float) = 0
		[HDR]_Color_Base("Color_Base", Color) = (0.362,0.362,0.362,0)
		[HDR]_Color_Glow("Color_Glow", Color) = (10.68063,2.513089,0,0)
		_Metallice("Metallice", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Noise_Scale("Noise_Scale", Float) = 10
		_Noise_Power("Noise_Power", Float) = 5
		_Panner_UV_X_Y("Panner_UV_X_Y", Vector) = (1,1,0,0)
		_Panner_Speed_X_Y("Panner_Speed_X_Y", Vector) = (0.1,0.05,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _USETEXTURE_ON
		#pragma shader_feature_local _USEEMISSION_ON
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Base_Normal;
		uniform float4 _Base_Normal_ST;
		uniform float4 _Color_Base;
		uniform sampler2D _Base_Albedo;
		uniform float4 _Base_Albedo_ST;
		uniform float4 _Color_Glow;
		uniform float2 _Panner_Speed_X_Y;
		uniform float2 _Panner_UV_X_Y;
		uniform float _Noise_Scale;
		uniform float _Noise_Power;
		uniform float _Metallice;
		uniform float _Smoothness;


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
			float2 uv_Base_Normal = i.uv_texcoord * _Base_Normal_ST.xy + _Base_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Base_Normal, uv_Base_Normal ) );
			float2 uv_Base_Albedo = i.uv_texcoord * _Base_Albedo_ST.xy + _Base_Albedo_ST.zw;
			#ifdef _USETEXTURE_ON
				float4 staticSwitch24 = tex2D( _Base_Albedo, uv_Base_Albedo );
			#else
				float4 staticSwitch24 = _Color_Base;
			#endif
			o.Albedo = staticSwitch24.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float4 appendResult34 = (float4(( ase_screenPosNorm.x * _Panner_UV_X_Y.x ) , ( ase_screenPosNorm.y * _Panner_UV_X_Y.y ) , 0.0 , 0.0));
			float2 panner7 = ( _Time.y * _Panner_Speed_X_Y + appendResult34.xy);
			float simplePerlin2D3 = snoise( panner7*_Noise_Scale );
			simplePerlin2D3 = simplePerlin2D3*0.5 + 0.5;
			#ifdef _USEEMISSION_ON
				float4 staticSwitch26 = ( _Color_Glow * pow( simplePerlin2D3 , _Noise_Power ) );
			#else
				float4 staticSwitch26 = float4( 0,0,0,0 );
			#endif
			o.Emission = staticSwitch26.rgb;
			o.Metallic = _Metallice;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
614;73;893;521;2242.079;160.2298;1.294641;True;False
Node;AmplifyShaderEditor.Vector2Node;37;-1909.357,190.6179;Inherit;False;Property;_Panner_UV_X_Y;Panner_UV_X_Y;10;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScreenPosInputsNode;32;-1902.36,10.09445;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1683.661,-18.77224;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-1684.961,89.12775;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;34;-1559.858,39.08836;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1424.527,294.3122;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-1442.727,152.6122;Inherit;False;Property;_Panner_Speed_X_Y;Panner_Speed_X_Y;11;0;Create;True;0;0;False;0;False;0.1,0.05;0.1,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;7;-1195.727,70.7122;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1187.8,-30.91669;Inherit;False;Property;_Noise_Scale;Noise_Scale;8;0;Create;True;0;0;False;0;False;10;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-862.9847,-42.4266;Inherit;False;Property;_Noise_Power;Noise_Power;9;0;Create;True;0;0;False;0;False;5;300;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;3;-956.3625,43.09928;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-666.2857,-153.8305;Inherit;False;Property;_Color_Glow;Color_Glow;5;1;[HDR];Create;True;0;0;False;0;False;10.68063,2.513089,0,0;26.70181,6.291003,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;11;-663.6164,51.12746;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-405.1211,-41.17329;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;25;-403.5604,-498.5688;Inherit;False;Property;_Color_Base;Color_Base;4;1;[HDR];Create;True;0;0;False;0;False;0.362,0.362,0.362,0;26.70181,6.291003,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-418.9163,-714.727;Inherit;True;Property;_Base_Albedo;Base_Albedo;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-423.2923,-301.7584;Inherit;True;Property;_Base_Normal;Base_Normal;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-418.5951,193.0594;Inherit;False;Property;_Metallice;Metallice;6;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;24;-104.7948,-494.5626;Inherit;False;Property;_UseTexture;UseTexture?;2;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;26;-209.0219,-1.908121;Inherit;False;Property;_UseEmission;UseEmission?;3;0;Create;True;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-414.583,299.3251;Inherit;False;Property;_Smoothness;Smoothness;7;0;Create;True;0;0;False;0;False;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;QQA/Shader_Bomb;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;35;0;32;1
WireConnection;35;1;37;1
WireConnection;36;0;32;2
WireConnection;36;1;37;2
WireConnection;34;0;35;0
WireConnection;34;1;36;0
WireConnection;7;0;34;0
WireConnection;7;2;8;0
WireConnection;7;1;6;0
WireConnection;3;0;7;0
WireConnection;3;1;10;0
WireConnection;11;0;3;0
WireConnection;11;1;12;0
WireConnection;15;0;14;0
WireConnection;15;1;11;0
WireConnection;24;1;25;0
WireConnection;24;0;18;0
WireConnection;26;0;15;0
WireConnection;0;0;24;0
WireConnection;0;1;19;0
WireConnection;0;2;26;0
WireConnection;0;3;17;0
WireConnection;0;4;16;0
ASEEND*/
//CHKSM=B6EC2EC3F7DDBB11405C6FB11208D4AA0DF9B3F7