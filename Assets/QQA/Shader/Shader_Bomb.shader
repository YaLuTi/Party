// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_Bomb"
{
	Properties
	{
		_Color_Base("Color_Base", Color) = (0.3584906,0.3584906,0.3584906,0)
		[HDR]_Color_Glow("Color_Glow", Color) = (10.68063,2.513089,0,0)
		_Noise_Scale("Noise_Scale", Float) = 10
		_Noise_Power("Noise_Power", Float) = 5
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
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color_Base;
		uniform float4 _Color_Glow;
		uniform float2 _Panner_Speed_X_Y;
		uniform float _Noise_Scale;
		uniform float _Noise_Power;


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
			o.Albedo = _Color_Base.rgb;
			float2 panner7 = ( _Time.y * _Panner_Speed_X_Y + i.uv_texcoord);
			float simplePerlin2D3 = snoise( panner7*_Noise_Scale );
			simplePerlin2D3 = simplePerlin2D3*0.5 + 0.5;
			o.Emission = ( _Color_Glow * pow( simplePerlin2D3 , _Noise_Power ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
666;73;768;578;1544.028;687.2022;2.561818;True;False
Node;AmplifyShaderEditor.Vector2Node;8;-1442.727,152.6122;Inherit;False;Property;_Panner_Speed_X_Y;Panner_Speed_X_Y;4;0;Create;True;0;0;False;0;0.1,0.05;0.1,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-1443.718,4.412519;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1424.527,294.3122;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;7;-1195.727,70.7122;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1187.8,-30.91669;Inherit;False;Property;_Noise_Scale;Noise_Scale;2;0;Create;True;0;0;False;0;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;3;-956.3625,43.09928;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-862.9847,-42.4266;Inherit;False;Property;_Noise_Power;Noise_Power;3;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;11;-663.6164,51.12746;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-666.2857,-153.8305;Inherit;False;Property;_Color_Glow;Color_Glow;1;1;[HDR];Create;True;0;0;False;0;10.68063,2.513089,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-405.1211,-41.17329;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;13;-413.7216,-333.7156;Inherit;False;Property;_Color_Base;Color_Base;0;0;Create;True;0;0;False;0;0.3584906,0.3584906,0.3584906,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;17;-311.7931,411.8178;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Shader_Bomb;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;1;0
WireConnection;7;2;8;0
WireConnection;7;1;6;0
WireConnection;3;0;7;0
WireConnection;3;1;10;0
WireConnection;11;0;3;0
WireConnection;11;1;12;0
WireConnection;15;0;14;0
WireConnection;15;1;11;0
WireConnection;0;0;13;0
WireConnection;0;2;15;0
ASEEND*/
//CHKSM=3A377DBEFC87975CEC633F1DE91F0C84C5E4AFCB