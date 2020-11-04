// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Projectors_IceGround"
{
	Properties
	{
		_LightTex("LightTex", 2D) = "white" {}
		_FalloffTex("FalloffTex", 2D) = "white" {}
		_Alpha_Bias("Alpha_Bias", Range( 0 , 1)) = 1
		[HDR]_Low_Color("Low_Color", Color) = (1,1,1,1)
		[HDR]_High_Color("High_Color", Color) = (4,0,0,1)
		_High_Power("High_Power", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Noise_Scale1("Noise_Scale", Float) = 1
		_Noise_Threshold1("Noise_Threshold", Range( -1 , 1)) = 0.5
		[HDR]_Emission_Color1("Emission_Color", Color) = (4,0,0,0)
		_Rim_Radius1("Rim_Radius", Range( 0 , 1)) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 0

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend DstColor One
		AlphaToMask Off
		Cull Back
		ColorMask RGB
		ZWrite Off
		ZTest LEqual
		Offset -1 , -1
		
		
		
		Pass
		{
			Name "SubShader 0 Pass 0"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_VERT_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _Low_Color;
			uniform sampler2D _TextureSample0;
			float4x4 unity_Projector;
			uniform float _High_Power;
			uniform float4 _High_Color;
			uniform sampler2D _LightTex;
			uniform float _Noise_Scale1;
			uniform float _Noise_Threshold1;
			uniform float _Rim_Radius1;
			uniform float4 _Emission_Color1;
			SamplerState sampler_LightTex;
			uniform sampler2D _FalloffTex;
			SamplerState sampler_FalloffTex;
			float4x4 unity_ProjectorClip;
			uniform float _Alpha_Bias;
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
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 vertexToFrag11 = mul( unity_Projector, v.vertex );
				o.ase_texcoord1 = vertexToFrag11;
				float4 vertexToFrag15 = mul( unity_ProjectorClip, v.vertex );
				o.ase_texcoord2 = vertexToFrag15;
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float4 vertexToFrag11 = i.ase_texcoord1;
				float2 temp_output_20_0 = ( (vertexToFrag11).xy / (vertexToFrag11).w );
				float4 tex2DNode50 = tex2D( _TextureSample0, temp_output_20_0 );
				float4 temp_cast_0 = (_High_Power).xxxx;
				float4 tex2DNode18 = tex2D( _LightTex, temp_output_20_0 );
				float simplePerlin2D64 = snoise( temp_output_20_0*_Noise_Scale1 );
				simplePerlin2D64 = simplePerlin2D64*0.5 + 0.5;
				float temp_output_68_0 = step( simplePerlin2D64 , _Noise_Threshold1 );
				float4 appendResult25 = (float4(( ( ( _Low_Color * tex2DNode50 ) + ( pow( tex2DNode50 , temp_cast_0 ) * _High_Color ) ) * tex2DNode18 * ( temp_output_68_0 + ( ( 1.0 - temp_output_68_0 ) * step( simplePerlin2D64 , ( _Noise_Threshold1 + _Rim_Radius1 ) ) * _Emission_Color1 ) ) ).rgb , ( 1.0 - tex2DNode18.a )));
				float4 vertexToFrag15 = i.ase_texcoord2;
				
				
				finalColor = ( appendResult25 * ( tex2D( _FalloffTex, ( (vertexToFrag15).xy / (vertexToFrag15).w ) ).a * _Alpha_Bias ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18600
625;73;809;575;1254.857;85.01828;2.042294;True;False
Node;AmplifyShaderEditor.CommentaryNode;57;-1458,-50;Inherit;False;1034;337;;7;10;8;12;11;22;21;20;LightUV;1,1,1,1;0;0
Node;AmplifyShaderEditor.UnityProjectorMatrixNode;8;-1408,0;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;10;-1408,80;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1200,0;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexToFragmentNode;11;-1056,0;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;21;-816,0;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;62;-1351.569,750.2849;Inherit;False;1279.294;798.6465;;11;73;72;71;70;69;68;67;66;65;64;63;Dissolve;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;22;-816,80;Inherit;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;58;-1458,334;Inherit;False;1034;337;;7;9;13;14;15;33;32;31;FalloffUV;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;20;-576,0;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-1202.03,1018.194;Inherit;False;Property;_Noise_Scale1;Noise_Scale;7;0;Create;True;0;0;False;0;False;1;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-1292.602,1230.809;Inherit;False;Property;_Rim_Radius1;Rim_Radius;10;0;Create;True;0;0;False;0;False;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;48;-855.9097,-926.483;Inherit;False;843.3665;826.319;;8;56;55;54;53;52;51;50;49;Base;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-1294.183,1128.029;Inherit;False;Property;_Noise_Threshold1;Noise_Threshold;8;0;Create;True;0;0;False;0;False;0.5;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;64;-983.2775,879.6855;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;13;-1408,464;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.UnityProjectorClipMatrixNode;9;-1408,384;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-1200,384;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-671.4006,-422.772;Inherit;False;Property;_High_Power;High_Power;5;0;Create;True;0;0;False;0;False;1;3.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;50;-817.9097,-650.1133;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;False;-1;None;0ba5127c1d8e3f84c8c1f0acbf14cd07;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;68;-745.8956,873.5091;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;67;-975.8705,1192.624;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;53;-484.3407,-512.4822;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;52;-740.3546,-876.483;Inherit;False;Property;_Low_Color;Low_Color;3;1;[HDR];Create;True;0;0;False;0;False;1,1,1,1;5.742706,6.61523,7.037304,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;51;-742.4546,-312.1639;Inherit;False;Property;_High_Color;High_Color;4;1;[HDR];Create;True;0;0;False;0;False;4,0,0,1;23.96863,23.96863,23.96863,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;69;-540.4769,1025.103;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;70;-747.9756,1336.932;Inherit;False;Property;_Emission_Color1;Emission_Color;9;1;[HDR];Create;True;0;0;False;0;False;4,0,0,0;63.99999,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;71;-750.7416,1107.535;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;15;-1056,384;Inherit;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-404.3171,1100.13;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;33;-816,464;Inherit;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;32;-816,384;Inherit;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-317.1257,-608.3622;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-318.6719,-443.7314;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;31;-576,384;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-224.2747,875.3753;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-164.5439,-500.403;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;18;-432,0;Inherit;True;Property;_LightTex;LightTex;0;0;Create;True;0;0;False;0;False;-1;None;84508b93f15f2b64386ec07486afc7a3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;59;-133.2733,386.1832;Inherit;False;456.7253;283.9344;;2;61;60;Alpha;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-96,-16;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;34;-432,384;Inherit;True;Property;_FalloffTex;FalloffTex;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;30;-96,96;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-83.2733,554.1174;Inherit;False;Property;_Alpha_Bias;Alpha_Bias;2;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;161.452,436.183;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;80,32;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;272,256;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;38;416,256;Float;False;True;-1;2;ASEMaterialInspector;0;1;QQA/Projectors_IceGround;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;SubShader 0 Pass 0;2;True;1;2;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;0;False;-1;True;True;True;True;False;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;False;-1;True;True;-1;False;-1;-1;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;12;0;8;0
WireConnection;12;1;10;0
WireConnection;11;0;12;0
WireConnection;21;0;11;0
WireConnection;22;0;11;0
WireConnection;20;0;21;0
WireConnection;20;1;22;0
WireConnection;64;0;20;0
WireConnection;64;1;63;0
WireConnection;14;0;9;0
WireConnection;14;1;13;0
WireConnection;50;1;20;0
WireConnection;68;0;64;0
WireConnection;68;1;65;0
WireConnection;67;0;65;0
WireConnection;67;1;66;0
WireConnection;53;0;50;0
WireConnection;53;1;49;0
WireConnection;69;0;68;0
WireConnection;71;0;64;0
WireConnection;71;1;67;0
WireConnection;15;0;14;0
WireConnection;72;0;69;0
WireConnection;72;1;71;0
WireConnection;72;2;70;0
WireConnection;33;0;15;0
WireConnection;32;0;15;0
WireConnection;54;0;52;0
WireConnection;54;1;50;0
WireConnection;55;0;53;0
WireConnection;55;1;51;0
WireConnection;31;0;32;0
WireConnection;31;1;33;0
WireConnection;73;0;68;0
WireConnection;73;1;72;0
WireConnection;56;0;54;0
WireConnection;56;1;55;0
WireConnection;18;1;20;0
WireConnection;24;0;56;0
WireConnection;24;1;18;0
WireConnection;24;2;73;0
WireConnection;34;1;31;0
WireConnection;30;0;18;4
WireConnection;61;0;34;4
WireConnection;61;1;60;0
WireConnection;25;0;24;0
WireConnection;25;3;30;0
WireConnection;35;0;25;0
WireConnection;35;1;61;0
WireConnection;38;0;35;0
ASEEND*/
//CHKSM=9CFCE0A560E9668272DAC32D79A12E78F9DB63B6