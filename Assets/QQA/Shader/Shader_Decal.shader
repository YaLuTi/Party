// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_Decal"
{
	Properties
	{
		_Position_Scale("Position_Scale", Float) = 1
		[HDR]_Color0("Color 0", Color) = (1,0,0,0)
		_TexturesCom_Fireworks0005_6_M_1("TexturesCom_Fireworks0005_6_M_1", 2D) = "white" {}
		_Position_Multiplier("Position_Multiplier", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

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
				float4 ase_texcoord : TEXCOORD0;
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

			uniform float4 _Color0;
			uniform sampler2D _TexturesCom_Fireworks0005_6_M_1;
			uniform float4 _TexturesCom_Fireworks0005_6_M_1_ST;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float _Position_Scale;
			uniform float _Position_Multiplier;
			float2 UnStereo( float2 UV )
			{
				#if UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
				UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
				#endif
				return UV;
			}
			
			float3 InvertDepthDir72_g1( float3 In )
			{
				float3 result = In;
				#if !defined(ASE_SRP_VERSION) || ASE_SRP_VERSION <= 70301
				result *= float3(1,1,-1);
				#endif
				return result;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord2 = screenPos;
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
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
				float2 uv_TexturesCom_Fireworks0005_6_M_1 = i.ase_texcoord1.xy * _TexturesCom_Fireworks0005_6_M_1_ST.xy + _TexturesCom_Fireworks0005_6_M_1_ST.zw;
				float4 screenPos = i.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 UV22_g3 = ase_screenPosNorm.xy;
				float2 localUnStereo22_g3 = UnStereo( UV22_g3 );
				float2 break64_g1 = localUnStereo22_g3;
				float clampDepth69_g1 = SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy );
				#ifdef UNITY_REVERSED_Z
				float staticSwitch38_g1 = ( 1.0 - clampDepth69_g1 );
				#else
				float staticSwitch38_g1 = clampDepth69_g1;
				#endif
				float3 appendResult39_g1 = (float3(break64_g1.x , break64_g1.y , staticSwitch38_g1));
				float4 appendResult42_g1 = (float4((appendResult39_g1*2.0 + -1.0) , 1.0));
				float4 temp_output_43_0_g1 = mul( unity_CameraInvProjection, appendResult42_g1 );
				float3 In72_g1 = ( (temp_output_43_0_g1).xyz / (temp_output_43_0_g1).w );
				float3 localInvertDepthDir72_g1 = InvertDepthDir72_g1( In72_g1 );
				float4 appendResult49_g1 = (float4(localInvertDepthDir72_g1 , 1.0));
				float3 worldToObj2 = mul( unity_WorldToObject, float4( mul( unity_CameraToWorld, appendResult49_g1 ).xyz, 1 ) ).xyz;
				float temp_output_5_0 = saturate( ( ( 1.0 - length( ( worldToObj2 * _Position_Scale ) ) ) * _Position_Multiplier ) );
				float4 appendResult9 = (float4(( ( _Color0 * tex2D( _TexturesCom_Fireworks0005_6_M_1, uv_TexturesCom_Fireworks0005_6_M_1 ) ) * temp_output_5_0 ).rgb , temp_output_5_0));
				
				
				finalColor = appendResult9;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18600
696;73;738;575;1419.901;730.9756;1.917528;True;False
Node;AmplifyShaderEditor.CommentaryNode;14;-1507.936,5.344671;Inherit;False;1210.536;327.8786;Decal;9;1;2;10;13;3;4;5;22;23;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;1;-1457.936,59.6452;Inherit;False;Reconstruct World Position From Depth;-1;;1;e7094bcbcc80eb140b2a3dbe6a861de8;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TransformPositionNode;2;-1107.907,55.34467;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;13;-1077.153,217.2233;Inherit;False;Property;_Position_Scale;Position_Scale;0;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-886.2266,60.51736;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LengthOpNode;3;-746.0616,62.79821;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-730.6333,186.05;Inherit;False;Property;_Position_Multiplier;Position_Multiplier;3;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;4;-615.1065,61.31404;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-524.4456,168.4085;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-793.0831,-589.9246;Inherit;False;Property;_Color0;Color 0;1;1;[HDR];Create;True;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-940.3909,-316.8679;Inherit;True;Property;_TexturesCom_Fireworks0005_6_M_1;TexturesCom_Fireworks0005_6_M_1;2;0;Create;True;0;0;False;0;False;-1;f5838d06cf436164885d5479f4c54f3b;f5838d06cf436164885d5479f4c54f3b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;5;-462.4005,61.5029;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-516.6989,-368.5996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-318.0751,-192.1816;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;9;-141.9319,-8.268536;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;20;0,0;Float;False;True;-1;2;ASEMaterialInspector;100;1;QQA/Shader_Decal;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;10;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;True;0;False;-1;True;1;False;-1;True;True;True;True;True;0;False;-1;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;7;False;-1;True;False;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;2;0;1;0
WireConnection;10;0;2;0
WireConnection;10;1;13;0
WireConnection;3;0;10;0
WireConnection;4;0;3;0
WireConnection;22;0;4;0
WireConnection;22;1;23;0
WireConnection;5;0;22;0
WireConnection;26;0;7;0
WireConnection;26;1;18;0
WireConnection;6;0;26;0
WireConnection;6;1;5;0
WireConnection;9;0;6;0
WireConnection;9;3;5;0
WireConnection;20;0;9;0
ASEEND*/
//CHKSM=56B9989812DEB392AC39CEB6C07705F160766D1F