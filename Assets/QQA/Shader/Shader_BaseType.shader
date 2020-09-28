// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_BaseType"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_R_Color("R_Color", Color) = (1,0,0,0)
		_R_Metallic("R_Metallic", Range( 0 , 1)) = 0
		_R_Smoothness("R_Smoothness", Range( 0 , 1)) = 0.5
		[HDR]_R_Fresnel_Color("R_Fresnel_Color", Color) = (1,1,1,0)
		_R_Fresnel_Bias("R_Fresnel_Bias", Float) = 0
		_R_Fresnel_Scale("R_Fresnel_Scale", Float) = 1
		_R_Fresnel_Power("R_Fresnel_Power", Float) = 5
		[HDR]_G_Color("G_Color", Color) = (0,1,0,0)
		_G_Metallic("G_Metallic", Range( 0 , 1)) = 0
		_G_Smoothness("G_Smoothness", Range( 0 , 1)) = 0.5
		[HDR]_G_Fresnel_Color("G_Fresnel_Color", Color) = (1,1,1,0)
		_G_Fresnel_Bias("G_Fresnel_Bias", Float) = 0
		_G_Fresnel_Scale("G_Fresnel_Scale", Float) = 1
		_G_Fresnel_Power("G_Fresnel_Power", Float) = 5
		[HDR]_B_Color("B_Color", Color) = (0,0,1,0)
		_B_Metallic("B_Metallic", Range( 0 , 1)) = 0
		_B_Smoothness("B_Smoothness", Range( 0 , 1)) = 0.5
		[HDR]_B_Fresnel_Color("B_Fresnel_Color", Color) = (1,1,1,0)
		_B_Fresnel_Bias("B_Fresnel_Bias", Float) = 0
		_B_Fresnel_Scale("B_Fresnel_Scale", Float) = 1
		_B_Fresnel_Power("B_Fresnel_Power", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _R_Color;
		uniform sampler2D _TextureSample0;
		uniform float4 _G_Color;
		uniform float4 _B_Color;
		uniform float _R_Fresnel_Bias;
		uniform float _R_Fresnel_Scale;
		uniform float _R_Fresnel_Power;
		uniform float4 _R_Fresnel_Color;
		uniform float _G_Fresnel_Bias;
		uniform float _G_Fresnel_Scale;
		uniform float _G_Fresnel_Power;
		uniform float4 _G_Fresnel_Color;
		uniform float _B_Fresnel_Bias;
		uniform float _B_Fresnel_Scale;
		uniform float _B_Fresnel_Power;
		uniform float4 _B_Fresnel_Color;
		uniform float _R_Metallic;
		uniform float _G_Metallic;
		uniform float _B_Metallic;
		uniform float _R_Smoothness;
		uniform float _G_Smoothness;
		uniform float _B_Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 tex2DNode3 = tex2D( _TextureSample0, i.uv_texcoord );
			float4 appendResult28 = (float4(tex2DNode3.r , tex2DNode3.g , tex2DNode3.b , 0.0));
			float4 break30 = appendResult28;
			o.Albedo = ( ( _R_Color * break30.x ) + ( _G_Color * break30.y ) + ( _B_Color * break30.z ) ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV50 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode50 = ( _R_Fresnel_Bias + _R_Fresnel_Scale * pow( 1.0 - fresnelNdotV50, _R_Fresnel_Power ) );
			float4 break45 = appendResult28;
			float fresnelNdotV51 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode51 = ( _G_Fresnel_Bias + _G_Fresnel_Scale * pow( 1.0 - fresnelNdotV51, _G_Fresnel_Power ) );
			float fresnelNdotV52 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode52 = ( _B_Fresnel_Bias + _B_Fresnel_Scale * pow( 1.0 - fresnelNdotV52, _B_Fresnel_Power ) );
			o.Emission = ( ( fresnelNode50 * break45.x * _R_Fresnel_Color ) + ( fresnelNode51 * break45.y * _G_Fresnel_Color ) + ( fresnelNode52 * break45.z * _B_Fresnel_Color ) ).rgb;
			float4 break81 = appendResult28;
			o.Metallic = ( ( _R_Metallic * break81.x ) + ( _G_Metallic * break81.y ) + ( _B_Metallic * break81.z ) );
			float4 break112 = appendResult28;
			o.Smoothness = ( ( _R_Smoothness * break112.x ) + ( _G_Smoothness * break112.y ) + ( _B_Smoothness * break112.z ) );
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18000
594;73;840;575;3023.595;806.8489;2.342188;True;False
Node;AmplifyShaderEditor.CommentaryNode;129;-3124.092,-28.09627;Inherit;False;706.5916;282.566;;3;1;3;28;UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1;-3074.092,21.90373;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;133;-1227.82,-960.2162;Inherit;False;1090.5;986.1759;;20;45;35;51;64;63;65;39;131;52;68;67;66;41;132;130;36;50;62;60;61;Fresnel;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;3;-2862.375,24.46972;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;63;-934.4402,-591.5574;Inherit;False;Property;_G_Fresnel_Bias;G_Fresnel_Bias;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;62;-936.9491,-720.7532;Inherit;False;Property;_R_Fresnel_Power;R_Fresnel_Power;7;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-925.7495,-283.5937;Inherit;False;Property;_B_Fresnel_Bias;B_Fresnel_Bias;19;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-923.7496,-215.5937;Inherit;False;Property;_B_Fresnel_Scale;B_Fresnel_Scale;20;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-932.4402,-452.5572;Inherit;False;Property;_G_Fresnel_Power;G_Fresnel_Power;14;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-936.9491,-791.7532;Inherit;False;Property;_R_Fresnel_Scale;R_Fresnel_Scale;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-923.7496,-144.5936;Inherit;False;Property;_B_Fresnel_Power;B_Fresnel_Power;21;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-938.9491,-859.7533;Inherit;False;Property;_R_Fresnel_Bias;R_Fresnel_Bias;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;128;-2169.147,-598.4673;Inherit;False;837.9404;634.7481;;8;6;17;11;12;18;10;19;30;Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;28;-2578.5,54.53991;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;127;-2175.746,229.2228;Inherit;False;893.9843;552.3;;8;94;93;95;90;91;87;92;81;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;126;-1174.689,224.4168;Inherit;False;893.985;552.3002;;8;114;113;118;106;119;109;110;112;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-932.4402,-523.5573;Inherit;False;Property;_G_Fresnel_Scale;G_Fresnel_Scale;13;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;50;-736.5999,-852.5944;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;52;-729.5663,-264.6841;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;45;-1177.82,-515.3012;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;30;-2119.147,-347.4926;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;81;-2125.746,413.2762;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;132;-521.0219,-186.0403;Inherit;False;Property;_B_Fresnel_Color;B_Fresnel_Color;18;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;51;-734.8156,-575.5555;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;-1875.392,463.3815;Inherit;False;Property;_G_Metallic;G_Metallic;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1880.447,642.3615;Inherit;False;Property;_B_Metallic;B_Metallic;16;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-1871.552,279.2228;Inherit;False;Property;_R_Metallic;R_Metallic;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;114;-878.0417,637.5557;Inherit;False;Property;_B_Smoothness;B_Smoothness;17;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;131;-519.0213,-488.9321;Inherit;False;Property;_G_Fresnel_Color;G_Fresnel_Color;11;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;113;-872.9857,458.5756;Inherit;False;Property;_G_Smoothness;G_Smoothness;10;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;118;-869.1457,274.4168;Inherit;False;Property;_R_Smoothness;R_Smoothness;3;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;112;-1124.689,400.5028;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;11;-1878.834,-357.6168;Inherit;False;Property;_G_Color;G_Color;8;1;[HDR];Create;True;0;0;False;0;0,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;6;-1876.034,-548.4673;Inherit;False;Property;_R_Color;R_Color;1;1;[HDR];Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;130;-513.2934,-778.8085;Inherit;False;Property;_R_Fresnel_Color;R_Fresnel_Color;4;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;17;-1877.229,-175.7192;Inherit;False;Property;_B_Color;B_Color;15;1;[HDR];Create;True;0;0;False;0;0,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-1616.143,646.5228;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1651.172,-494.0329;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-477.7259,-910.2162;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-487.4158,-605.4373;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-493.9109,-306.0277;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-1611.392,282.5997;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-1615.036,470.8387;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-613.7376,641.717;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-608.9866,277.7938;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-612.6307,466.0327;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1654.817,-307.7935;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1655.923,-132.1091;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-290.32,-572.8146;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;110;-433.704,446.2414;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;92;-1434.762,438.5858;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1484.206,-367.1791;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Shader_BaseType;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;1;0
WireConnection;28;0;3;1
WireConnection;28;1;3;2
WireConnection;28;2;3;3
WireConnection;50;1;60;0
WireConnection;50;2;61;0
WireConnection;50;3;62;0
WireConnection;52;1;66;0
WireConnection;52;2;67;0
WireConnection;52;3;68;0
WireConnection;45;0;28;0
WireConnection;30;0;28;0
WireConnection;81;0;28;0
WireConnection;51;1;63;0
WireConnection;51;2;64;0
WireConnection;51;3;65;0
WireConnection;112;0;28;0
WireConnection;91;0;95;0
WireConnection;91;1;81;2
WireConnection;10;0;6;0
WireConnection;10;1;30;0
WireConnection;36;0;50;0
WireConnection;36;1;45;0
WireConnection;36;2;130;0
WireConnection;39;0;51;0
WireConnection;39;1;45;1
WireConnection;39;2;131;0
WireConnection;41;0;52;0
WireConnection;41;1;45;2
WireConnection;41;2;132;0
WireConnection;90;0;93;0
WireConnection;90;1;81;0
WireConnection;87;0;94;0
WireConnection;87;1;81;1
WireConnection;109;0;114;0
WireConnection;109;1;112;2
WireConnection;119;0;118;0
WireConnection;119;1;112;0
WireConnection;106;0;113;0
WireConnection;106;1;112;1
WireConnection;12;0;11;0
WireConnection;12;1;30;1
WireConnection;18;0;17;0
WireConnection;18;1;30;2
WireConnection;35;0;36;0
WireConnection;35;1;39;0
WireConnection;35;2;41;0
WireConnection;110;0;119;0
WireConnection;110;1;106;0
WireConnection;110;2;109;0
WireConnection;92;0;90;0
WireConnection;92;1;87;0
WireConnection;92;2;91;0
WireConnection;19;0;10;0
WireConnection;19;1;12;0
WireConnection;19;2;18;0
WireConnection;0;0;19;0
WireConnection;0;2;35;0
WireConnection;0;3;92;0
WireConnection;0;4;110;0
ASEEND*/
//CHKSM=910ED7028AAD551EC3AD5ACFE5714552891A13E0