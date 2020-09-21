// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_Cloud"
{
	Properties
	{
		_Position_Rotate("Position_Rotate", Vector) = (1,0,0,0)
		_Noise_Scale("Noise_Scale", Float) = 10
		_Panner_Speed_X_Y("Panner_Speed_X_Y", Vector) = (1,1,0,0)
		_Vertex_Multiplier("Vertex_Multiplier", Float) = 1
		_Remap_InMin_InMax_OutMin_OutMax("Remap_InMin_InMax_OutMin_OutMax", Vector) = (0,1,-1,1)
		_Color_W("Color_W", Color) = (1,1,1,0)
		_Color_B("Color_B", Color) = (0,0,0,0)
		_Smoothstep_Min_Max("Smoothstep_Min_Max", Vector) = (0,1,0,0)
		_Power("Power", Float) = 2
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		uniform float2 _Smoothstep_Min_Max;
		uniform float2 _Panner_Speed_X_Y;
		uniform float4 _Position_Rotate;
		uniform float _Noise_Scale;
		uniform float _Power;
		uniform float4 _Remap_InMin_InMax_OutMin_OutMax;
		uniform float _Vertex_Multiplier;
		uniform float4 _Color_B;
		uniform float4 _Color_W;


		float3 RotateAroundAxis( float3 center, float3 original, float3 u, float angle )
		{
			original -= center;
			float C = cos( angle );
			float S = sin( angle );
			float t = 1 - C;
			float m00 = t * u.x * u.x + C;
			float m01 = t * u.x * u.y - S * u.z;
			float m02 = t * u.x * u.z + S * u.y;
			float m10 = t * u.x * u.y + S * u.z;
			float m11 = t * u.y * u.y + C;
			float m12 = t * u.y * u.z - S * u.x;
			float m20 = t * u.x * u.z - S * u.y;
			float m21 = t * u.y * u.z + S * u.x;
			float m22 = t * u.z * u.z + C;
			float3x3 finalMatrix = float3x3( m00, m01, m02, m10, m11, m12, m20, m21, m22 );
			return mul( finalMatrix, original ) + center;
		}


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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 rotatedValue4 = RotateAroundAxis( float3( 0,0,0 ), ase_worldPos, _Position_Rotate.xyz, _Position_Rotate.w );
			float2 panner8 = ( _Time.y * _Panner_Speed_X_Y + rotatedValue4.xy);
			float simplePerlin2D1 = snoise( panner8*_Noise_Scale );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float simplePerlin2D29 = snoise( rotatedValue4.xy*_Noise_Scale );
			simplePerlin2D29 = simplePerlin2D29*0.5 + 0.5;
			float smoothstepResult44 = smoothstep( _Smoothstep_Min_Max.x , _Smoothstep_Min_Max.y , abs( (_Remap_InMin_InMax_OutMin_OutMax.z + (pow( saturate( ( ( simplePerlin2D1 + simplePerlin2D29 ) / 2.0 ) ) , _Power ) - _Remap_InMin_InMax_OutMin_OutMax.x) * (_Remap_InMin_InMax_OutMin_OutMax.w - _Remap_InMin_InMax_OutMin_OutMax.z) / (_Remap_InMin_InMax_OutMin_OutMax.y - _Remap_InMin_InMax_OutMin_OutMax.x)) ));
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( smoothstepResult44 * ase_vertexNormal * _Vertex_Multiplier );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 rotatedValue4 = RotateAroundAxis( float3( 0,0,0 ), ase_worldPos, _Position_Rotate.xyz, _Position_Rotate.w );
			float2 panner8 = ( _Time.y * _Panner_Speed_X_Y + rotatedValue4.xy);
			float simplePerlin2D1 = snoise( panner8*_Noise_Scale );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float simplePerlin2D29 = snoise( rotatedValue4.xy*_Noise_Scale );
			simplePerlin2D29 = simplePerlin2D29*0.5 + 0.5;
			float smoothstepResult44 = smoothstep( _Smoothstep_Min_Max.x , _Smoothstep_Min_Max.y , abs( (_Remap_InMin_InMax_OutMin_OutMax.z + (pow( saturate( ( ( simplePerlin2D1 + simplePerlin2D29 ) / 2.0 ) ) , _Power ) - _Remap_InMin_InMax_OutMin_OutMax.x) * (_Remap_InMin_InMax_OutMin_OutMax.w - _Remap_InMin_InMax_OutMin_OutMax.z) / (_Remap_InMin_InMax_OutMin_OutMax.y - _Remap_InMin_InMax_OutMin_OutMax.x)) ));
			float4 lerpResult38 = lerp( _Color_B , _Color_W , smoothstepResult44);
			o.Albedo = lerpResult38.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
594;73;840;575;3392.121;1549.895;4.373677;True;False
Node;AmplifyShaderEditor.CommentaryNode;43;-2525.306,-511.6212;Inherit;False;646.2236;467.3049;;3;3;5;4;Projection;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;51;-2358.692,76.80971;Inherit;False;471.6182;302.738;;3;8;11;10;Time;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;5;-2429.258,-461.6213;Inherit;False;Property;_Position_Rotate;Position_Rotate;0;0;Create;True;0;0;False;0;1,0,0,0;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-2475.306,-269.3163;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;49;-1618.747,-502.6582;Inherit;False;839.6193;302.2827;;6;6;1;29;31;32;48;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-2273.946,268.5477;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;11;-2308.692,133.2321;Inherit;False;Property;_Panner_Speed_X_Y;Panner_Speed_X_Y;2;0;Create;True;0;0;False;0;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RotateAboutAxisNode;4;-2199.082,-342.154;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1568.747,-317.6528;Inherit;False;Property;_Noise_Scale;Noise_Scale;1;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;8;-2092.074,136.225;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;29;-1380.558,-340.3754;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;1;-1378.594,-452.6582;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1179.679,-408.4187;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;32;-1057.987,-406.6478;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;50;-1648.055,-75.80556;Inherit;False;869.1106;392.2214;;7;44;35;33;34;45;46;47;Mod;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-1574.809,-5.296188;Inherit;False;Property;_Power;Power;8;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;48;-944.128,-405.5839;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;46;-1437.989,-24.23583;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;35;-1598.055,104.4158;Inherit;False;Property;_Remap_InMin_InMax_OutMin_OutMax;Remap_InMin_InMax_OutMin_OutMax;4;0;Create;True;0;0;False;0;0,1,-1,1;0,1,-1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;33;-1268.502,-25.80557;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;45;-1260.749,150.3895;Inherit;False;Property;_Smoothstep_Min_Max;Smoothstep_Min_Max;7;0;Create;True;0;0;False;0;0,1;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;42;-604.0435,128.7182;Inherit;False;451.8254;323.6268;;3;12;21;13;Vertex;1,1,1,1;0;0
Node;AmplifyShaderEditor.AbsOpNode;34;-1093.874,-22.66131;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;41;-612.5074,-514.8682;Inherit;False;488.0097;443.8108;;3;39;40;38;Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;40;-562.507,-283.0573;Inherit;False;Property;_Color_B;Color_B;6;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;44;-967.9449,-19.90853;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;12;-539.583,178.7182;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;39;-562.5072,-464.8681;Inherit;False;Property;_Color_W;Color_W;5;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-554.0435,336.345;Inherit;False;Property;_Vertex_Multiplier;Vertex_Multiplier;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-314.218,230.0276;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;38;-306.4974,-281.1172;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;20;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Shader_Cloud;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;5;0
WireConnection;4;1;5;4
WireConnection;4;3;3;0
WireConnection;8;0;4;0
WireConnection;8;2;11;0
WireConnection;8;1;10;0
WireConnection;29;0;4;0
WireConnection;29;1;6;0
WireConnection;1;0;8;0
WireConnection;1;1;6;0
WireConnection;31;0;1;0
WireConnection;31;1;29;0
WireConnection;32;0;31;0
WireConnection;48;0;32;0
WireConnection;46;0;48;0
WireConnection;46;1;47;0
WireConnection;33;0;46;0
WireConnection;33;1;35;1
WireConnection;33;2;35;2
WireConnection;33;3;35;3
WireConnection;33;4;35;4
WireConnection;34;0;33;0
WireConnection;44;0;34;0
WireConnection;44;1;45;1
WireConnection;44;2;45;2
WireConnection;13;0;44;0
WireConnection;13;1;12;0
WireConnection;13;2;21;0
WireConnection;38;0;40;0
WireConnection;38;1;39;0
WireConnection;38;2;44;0
WireConnection;20;0;38;0
WireConnection;20;11;13;0
ASEEND*/
//CHKSM=2EAFB3AE9493DEAB118EA5E10A14391A5B7A79B7