// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "OmniDecal"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Size("Size", Range( 0 , 1)) = 1
		_Rampfalloff("Ramp falloff", Range( 0.01 , 5)) = 5
		_Ramp("Ramp", 2D) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Front
		ZWrite Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float3 data101;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			fixed3 Albedo;
			fixed3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			fixed Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _Ramp;
		uniform sampler2D _CameraDepthTexture;
		uniform float _Size;
		uniform float _Rampfalloff;
		uniform float _Cutoff = 0.5;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 ase_vertex4Pos = v.vertex;
			float3 componentMask139 = mul( UNITY_MATRIX_MV, ase_vertex4Pos ).xyz;
			o.data101 = ( componentMask139 * float3( -1,-1,1 ) );
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float4 transform114 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float clampDepth106 = Linear01Depth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float componentMask103 = i.data101.z;
			float4 appendResult141 = (float4(( clampDepth106 * ( i.data101 * ( _ProjectionParams.z / componentMask103 ) ) ) , 1.0));
			float3 componentMask160 = ( transform114 - mul( unity_CameraToWorld, appendResult141 ) ).xyz;
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );;
			float temp_output_184_0 = ( 1.0 - saturate( pow( ( length( abs( componentMask160 ) ) / ( max( max( ase_objectScale.x , ase_objectScale.y ) , ase_objectScale.z ) * 0.5 * _Size ) ) , _Rampfalloff ) ) );
			float2 temp_cast_0 = (temp_output_184_0).xx;
			c.rgb = tex2D( _Ramp, temp_cast_0, float2( 0,0 ), float2( 0,0 ) ).rgb;
			c.a = temp_output_184_0;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13201
487;595;979;423;752.3486;710.6234;4.851588;False;False
Node;AmplifyShaderEditor.CommentaryNode;217;-655.5935,-243.2917;Float;False;2107;533;Comment;15;99;137;138;139;100;101;103;97;102;106;104;107;109;141;108;Reconstructed world position from depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.PosVertexDataNode;99;-605.5934,14.70838;Float;False;1;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.MVMatrixNode;137;-605.5934,-81.29168;Float;False;0;1;FLOAT4x4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;-365.5931,-81.29168;Float;False;2;2;0;FLOAT4x4;0.0;False;1;FLOAT4;0.0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ComponentMaskNode;139;-205.5936,-50.29168;Float;False;True;True;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;34.40618,-65.29168;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;-1,-1,1;False;1;FLOAT3
Node;AmplifyShaderEditor.VertexToFragmentNode;101;194.4066,-65.29168;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.ComponentMaskNode;103;418.4063,174.7078;Float;False;False;False;True;False;1;0;FLOAT3;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.ProjectionParams;97;418.4063,14.70838;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;102;642.4059,46.70885;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ScreenDepthNode;106;722.406,-145.2917;Float;False;1;1;0;FLOAT4;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;786.4056,-65.29168;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;962.4068,-113.2917;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT3;0;False;1;FLOAT3
Node;AmplifyShaderEditor.CameraToWorldMatrix;109;1042.407,-193.2917;Float;False;0;1;FLOAT4x4
Node;AmplifyShaderEditor.CommentaryNode;219;1378.052,401.4491;Float;False;300.3615;234.4789;Object pivot position in world space;1;114;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;141;1122.407,-113.2917;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;1.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;114;1428.052,451.4491;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;220;1675.14,720.5161;Float;False;527.0399;229;Make the effect scale with the sphere;3;152;154;155;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;1282.407,-193.2917;Float;False;2;2;0;FLOAT4x4;0.0;False;1;FLOAT4;0.0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ObjectScaleNode;152;1725.14,770.5161;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;113;1830.928,475.2047;Float;False;2;0;FLOAT4;0.0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.ComponentMaskNode;160;2016,496;Float;False;True;True;True;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMaxOp;154;1924.767,773.5155;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.AbsOpNode;158;2240,496;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMaxOp;155;2048.176,805.9933;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;216;1922.201,1004.83;Float;False;Property;_Size;Size;1;0;1;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.LengthOpNode;159;2400,496;Float;False;1;0;FLOAT3;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;2254.979,824.6212;Float;False;3;3;0;FLOAT;0.0;False;1;FLOAT;0.5;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;164;2512,672;Float;False;Property;_Rampfalloff;Ramp falloff;2;0;5;0.01;5;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;148;2560,496;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.PowerNode;163;2736,496;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SaturateNode;187;2944,496;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;184;3120,496;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;185;3344,288;Float;True;Property;_Ramp;Ramp;3;0;None;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3696.048,315.2336;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;OmniDecal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;Front;2;7;False;0;0;Custom;0.5;True;False;0;True;Transparent;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;False;2;SrcAlpha;OneMinusSrcAlpha;0;Zero;Zero;OFF;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;138;0;137;0
WireConnection;138;1;99;0
WireConnection;139;0;138;0
WireConnection;100;0;139;0
WireConnection;101;0;100;0
WireConnection;103;0;101;0
WireConnection;102;0;97;3
WireConnection;102;1;103;0
WireConnection;104;0;101;0
WireConnection;104;1;102;0
WireConnection;107;0;106;0
WireConnection;107;1;104;0
WireConnection;141;0;107;0
WireConnection;108;0;109;0
WireConnection;108;1;141;0
WireConnection;113;0;114;0
WireConnection;113;1;108;0
WireConnection;160;0;113;0
WireConnection;154;0;152;1
WireConnection;154;1;152;2
WireConnection;158;0;160;0
WireConnection;155;0;154;0
WireConnection;155;1;152;3
WireConnection;159;0;158;0
WireConnection;156;0;155;0
WireConnection;156;2;216;0
WireConnection;148;0;159;0
WireConnection;148;1;156;0
WireConnection;163;0;148;0
WireConnection;163;1;164;0
WireConnection;187;0;163;0
WireConnection;184;0;187;0
WireConnection;185;1;184;0
WireConnection;0;2;185;0
WireConnection;0;9;184;0
ASEEND*/
//CHKSM=38357DE697536638CD2F2EC20CB0E434C99350E1