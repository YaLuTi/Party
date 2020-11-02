// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_OmniDecal"
{
	Properties
	{
		_Position_Scale("Position_Scale", Float) = 1
		_Position_Multiplier("Position_Multiplier", Float) = 1
		_Alpha_Bias("Alpha_Bias", Range( 0 , 1)) = 1
		[HDR]_Low_Color("Low_Color", Color) = (1,1,1,1)
		[HDR]_High_Color("High_Color", Color) = (4,0,0,1)
		_High_Power("High_Power", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Tiling_X_Y("Tiling_X_Y", Vector) = (1,1,0,0)
		_Noise_Scale("Noise_Scale", Float) = 1
		_Noise_Threshold("Noise_Threshold", Range( -1 , 1)) = 0.5
		[HDR]_Emission_Color("Emission_Color", Color) = (4,0,0,0)
		_Rim_Radius("Rim_Radius", Range( 0 , 1)) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One OneMinusSrcAlpha
		AlphaToMask Off
		Cull Front
		ColorMask RGBA
		ZWrite Off
		ZTest Always
		
		
		
		Pass
		{
			Name "Unlit"
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
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _Low_Color;
			uniform sampler2D _TextureSample0;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float2 _Tiling_X_Y;
			uniform float _High_Power;
			uniform float4 _High_Color;
			uniform float _Position_Scale;
			uniform float _Position_Multiplier;
			uniform float _Alpha_Bias;
			uniform float _Noise_Scale;
			uniform float _Noise_Threshold;
			uniform float _Rim_Radius;
			uniform float4 _Emission_Color;
			float2 UnStereo( float2 UV )
			{
				#if UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
				UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
				#endif
				return UV;
			}
			
			float3 InvertDepthDir72_g6( float3 In )
			{
				float3 result = In;
				#if !defined(ASE_SRP_VERSION) || ASE_SRP_VERSION <= 70301
				result *= float3(1,1,-1);
				#endif
				return result;
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
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
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
				float4 screenPos = i.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 UV22_g7 = ase_screenPosNorm.xy;
				float2 localUnStereo22_g7 = UnStereo( UV22_g7 );
				float2 break64_g6 = localUnStereo22_g7;
				float clampDepth69_g6 = SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy );
				#ifdef UNITY_REVERSED_Z
				float staticSwitch38_g6 = ( 1.0 - clampDepth69_g6 );
				#else
				float staticSwitch38_g6 = clampDepth69_g6;
				#endif
				float3 appendResult39_g6 = (float3(break64_g6.x , break64_g6.y , staticSwitch38_g6));
				float4 appendResult42_g6 = (float4((appendResult39_g6*2.0 + -1.0) , 1.0));
				float4 temp_output_43_0_g6 = mul( unity_CameraInvProjection, appendResult42_g6 );
				float3 In72_g6 = ( (temp_output_43_0_g6).xyz / (temp_output_43_0_g6).w );
				float3 localInvertDepthDir72_g6 = InvertDepthDir72_g6( In72_g6 );
				float4 appendResult49_g6 = (float4(localInvertDepthDir72_g6 , 1.0));
				float4 temp_output_32_0 = mul( unity_CameraToWorld, appendResult49_g6 );
				float3 worldToObjDir41 = mul( unity_WorldToObject, float4( temp_output_32_0.xyz, 0 ) ).xyz;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float3 appendResult82 = (float3(( ase_objectScale.x * _Tiling_X_Y.x ) , ase_objectScale.y , ( ase_objectScale.z * _Tiling_X_Y.y )));
				float2 temp_output_42_0 = (( worldToObjDir41 * appendResult82 )).xz;
				float4 tex2DNode61 = tex2D( _TextureSample0, temp_output_42_0 );
				float4 temp_cast_2 = (_High_Power).xxxx;
				float3 worldToObj33 = mul( unity_WorldToObject, float4( temp_output_32_0.xyz, 1 ) ).xyz;
				float temp_output_40_0 = saturate( ( ( 1.0 - length( ( worldToObj33 * _Position_Scale ) ) ) * _Position_Multiplier ) );
				float simplePerlin2D56 = snoise( temp_output_42_0*_Noise_Scale );
				simplePerlin2D56 = simplePerlin2D56*0.5 + 0.5;
				float temp_output_58_0 = step( simplePerlin2D56 , _Noise_Threshold );
				float4 temp_output_71_0 = ( ( ( _Low_Color * tex2DNode61 ) + ( pow( tex2DNode61 , temp_cast_2 ) * _High_Color ) ) * ( temp_output_40_0 * _Alpha_Bias ) * ( temp_output_58_0 + ( ( 1.0 - temp_output_58_0 ) * step( simplePerlin2D56 , ( _Noise_Threshold + _Rim_Radius ) ) * _Emission_Color ) ) );
				
				
				finalColor = temp_output_71_0;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18600
625;73;809;575;3825.131;1442.373;4.322775;True;False
Node;AmplifyShaderEditor.CommentaryNode;81;-2339.411,-719.0645;Inherit;False;800.8707;471.2303;;8;41;72;42;73;75;78;80;82;DisableY&LockScale;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;80;-2102.398,-475.055;Inherit;False;Property;_Tiling_X_Y;Tiling_X_Y;7;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ObjectScaleNode;73;-2289.411,-491.2188;Inherit;False;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;31;-2284.274,-57.50466;Inherit;False;1210.536;327.8786;;9;40;39;38;37;36;35;34;33;32;Decal;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;32;-2234.274,-3.204134;Inherit;False;Reconstruct World Position From Depth;-1;;6;e7094bcbcc80eb140b2a3dbe6a861de8;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1931.885,-519.3919;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1928.898,-372.6546;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;82;-1784.96,-461.12;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TransformDirectionNode;41;-2146.644,-669.0645;Inherit;False;World;Object;False;Fast;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-1916.882,-661.5325;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TransformPositionNode;33;-1884.245,-7.504663;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;34;-1853.491,154.374;Inherit;False;Property;_Position_Scale;Position_Scale;0;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;47;-1627.189,504.0071;Inherit;False;1279.294;798.6465;;11;59;58;57;56;55;53;52;51;50;49;48;Dissolve;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1662.564,-2.331972;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1477.65,771.9163;Inherit;False;Property;_Noise_Scale;Noise_Scale;8;0;Create;True;0;0;False;0;False;1;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;42;-1761.54,-661.014;Inherit;False;True;False;True;False;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;56;-1258.898,633.4077;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-1569.803,881.7517;Inherit;False;Property;_Noise_Threshold;Noise_Threshold;9;0;Create;True;0;0;False;0;False;0.5;1;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-1568.222,984.5311;Inherit;False;Property;_Rim_Radius;Rim_Radius;11;0;Create;True;0;0;False;0;False;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;60;-1334.305,-978.2056;Inherit;False;843.3665;826.319;;8;68;67;66;65;64;63;62;61;Base;1,1,1,1;0;0
Node;AmplifyShaderEditor.LengthOpNode;36;-1522.399,-0.05112219;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-1251.491,946.3468;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;38;-1391.444,-1.535291;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;58;-1021.516,627.2314;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-1149.796,-474.4946;Inherit;False;Property;_High_Power;High_Power;5;0;Create;True;0;0;False;0;False;1;3.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1506.971,123.2007;Inherit;False;Property;_Position_Multiplier;Position_Multiplier;1;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;61;-1284.305,-701.8359;Inherit;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;False;-1;f5838d06cf436164885d5479f4c54f3b;0ba5127c1d8e3f84c8c1f0acbf14cd07;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1300.783,105.5592;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;50;-816.0972,778.8254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-1023.596,1090.654;Inherit;False;Property;_Emission_Color;Emission_Color;10;1;[HDR];Create;True;0;0;False;0;False;4,0,0,0;63.99999,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;83;-948.6611,-38.78229;Inherit;False;456.7253;283.9344;;2;70;69;Alpha;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;65;-1220.85,-363.8865;Inherit;False;Property;_High_Color;High_Color;4;1;[HDR];Create;True;0;0;False;0;False;4,0,0,1;23.96863,23.96863,23.96863,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;64;-1218.75,-928.2056;Inherit;False;Property;_Low_Color;Low_Color;3;1;[HDR];Create;True;0;0;False;0;False;1,1,1,1;5.742706,6.61523,7.037304,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;63;-962.7358,-564.2047;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;59;-1026.362,861.2576;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-898.6611,129.1521;Inherit;False;Property;_Alpha_Bias;Alpha_Bias;2;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-679.9375,853.8528;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-795.5208,-660.0848;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;40;-1238.738,-1.346433;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-797.067,-495.454;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-499.8951,629.0975;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-642.9391,-552.1256;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-653.9358,11.21771;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;-154.9976,11.3702;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-358.5167,-96.5869;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;20;0,0;Float;False;True;-1;2;ASEMaterialInspector;100;1;QQA/Shader_OmniDecal;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;10;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;1;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;7;False;-1;True;False;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;75;0;73;1
WireConnection;75;1;80;1
WireConnection;78;0;73;3
WireConnection;78;1;80;2
WireConnection;82;0;75;0
WireConnection;82;1;73;2
WireConnection;82;2;78;0
WireConnection;41;0;32;0
WireConnection;72;0;41;0
WireConnection;72;1;82;0
WireConnection;33;0;32;0
WireConnection;35;0;33;0
WireConnection;35;1;34;0
WireConnection;42;0;72;0
WireConnection;56;0;42;0
WireConnection;56;1;55;0
WireConnection;36;0;35;0
WireConnection;49;0;57;0
WireConnection;49;1;48;0
WireConnection;38;0;36;0
WireConnection;58;0;56;0
WireConnection;58;1;57;0
WireConnection;61;1;42;0
WireConnection;39;0;38;0
WireConnection;39;1;37;0
WireConnection;50;0;58;0
WireConnection;63;0;61;0
WireConnection;63;1;62;0
WireConnection;59;0;56;0
WireConnection;59;1;49;0
WireConnection;52;0;50;0
WireConnection;52;1;59;0
WireConnection;52;2;51;0
WireConnection;67;0;64;0
WireConnection;67;1;61;0
WireConnection;40;0;39;0
WireConnection;66;0;63;0
WireConnection;66;1;65;0
WireConnection;53;0;58;0
WireConnection;53;1;52;0
WireConnection;68;0;67;0
WireConnection;68;1;66;0
WireConnection;70;0;40;0
WireConnection;70;1;69;0
WireConnection;45;0;71;0
WireConnection;45;3;40;0
WireConnection;71;0;68;0
WireConnection;71;1;70;0
WireConnection;71;2;53;0
WireConnection;20;0;71;0
ASEEND*/
//CHKSM=D76951C5BC43771481BFFFD7023E03E3B52528EB