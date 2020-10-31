// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_Decal_IceGround"
{
	Properties
	{
		_Position_Scale("Position_Scale", Float) = 1
		[HDR]_Color0("Color 0", Color) = (1,0,0,0)
		_TexturesCom_Fireworks0005_6_M_1("TexturesCom_Fireworks0005_6_M_1", 2D) = "white" {}
		_Position_Multiplier("Position_Multiplier", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
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


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexturesCom_Fireworks0005_6_M_1 = i.uv_texcoord * _TexturesCom_Fireworks0005_6_M_1_ST.xy + _TexturesCom_Fireworks0005_6_M_1_ST.zw;
			float4 temp_output_26_0 = ( _Color0 * tex2D( _TexturesCom_Fireworks0005_6_M_1, uv_TexturesCom_Fireworks0005_6_M_1 ) );
			o.Albedo = temp_output_26_0.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
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
			o.Alpha = temp_output_5_0;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
696;73;738;575;846.1035;412.7916;1.424461;True;False
Node;AmplifyShaderEditor.CommentaryNode;14;-1507.936,5.344671;Inherit;False;1210.536;327.8786;Decal;9;1;2;10;13;3;4;5;22;23;;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;1;-1457.936,59.6452;Inherit;False;Reconstruct World Position From Depth;-1;;1;e7094bcbcc80eb140b2a3dbe6a861de8;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TransformPositionNode;2;-1107.907,55.34467;Inherit;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;13;-1077.153,217.2233;Inherit;False;Property;_Position_Scale;Position_Scale;0;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-886.2266,60.51736;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LengthOpNode;3;-746.0616,62.79821;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-730.6333,186.05;Inherit;False;Property;_Position_Multiplier;Position_Multiplier;3;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;4;-615.1065,61.31404;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-524.4456,168.4085;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-904.5695,-291.6635;Inherit;True;Property;_TexturesCom_Fireworks0005_6_M_1;TexturesCom_Fireworks0005_6_M_1;2;0;Create;True;0;0;False;0;False;-1;f5838d06cf436164885d5479f4c54f3b;f5838d06cf436164885d5479f4c54f3b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-832.6135,-498.833;Inherit;False;Property;_Color0;Color 0;1;1;[HDR];Create;True;0;0;False;0;False;1,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-516.6989,-368.5996;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;5;-462.4005,61.5029;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-392.2269,-155.9679;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;9;-260.6996,-58.88941;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;27;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;QQA/Shader_Decal_IceGround;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;2;False;-1;7;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;4;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;10;0;2;0
WireConnection;10;1;13;0
WireConnection;3;0;10;0
WireConnection;4;0;3;0
WireConnection;22;0;4;0
WireConnection;22;1;23;0
WireConnection;26;0;7;0
WireConnection;26;1;18;0
WireConnection;5;0;22;0
WireConnection;6;0;26;0
WireConnection;6;1;5;0
WireConnection;9;0;6;0
WireConnection;27;0;26;0
WireConnection;27;9;5;0
ASEEND*/
//CHKSM=E2F897334891054834E656AAEA70C29F36324BB5