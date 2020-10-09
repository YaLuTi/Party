// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_IceSpear"
{
	Properties
	{
		[HDR]_Color_Base1("Color_Base", Color) = (0.8,0.8,0.8,0)
		[HDR]_Fresnel_Color1("Fresnel_Color", Color) = (1,1,1,0)
		_Metallic1("Metallic", Range( 0 , 1)) = 0
		_Smoothness1("Smoothness", Range( 0 , 1)) = 0.5
		_Fresnel_Bias1("Fresnel_Bias", Float) = 0
		_Fresnel_Scale1("Fresnel_Scale", Float) = 1
		_Fresnel_Power1("Fresnel_Power", Float) = 5
		_Emission_Map("Emission_Map", 2D) = "white" {}
		[HDR]_Emission_Color("Emission_Color", Color) = (1,1,1,0)
		_Panner_Speed_X_Y("Panner_Speed_X_Y", Vector) = (0.1,0.2,0,0)
		_Fresnel_Emission_Map("Fresnel_Emission_Map", 2D) = "white" {}
		_Texture_Power("Texture_Power", Float) = 2
		[HDR]_Fresnel_Emission("Fresnel_Emission", Color) = (4,4,4,0)
		_Panner2_Speed_X_Y("Panner2_Speed_X_Y", Vector) = (1,1,0,0)
		_Fresnel2_Bias("Fresnel2_Bias", Float) = 0
		_Fresnel2_Scale("Fresnel2_Scale", Float) = 1
		_Fresnel2_Power("Fresnel2_Power", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float2 uv_texcoord;
		};

		uniform float4 _Color_Base1;
		uniform float4 _Fresnel_Color1;
		uniform float _Fresnel_Bias1;
		uniform float _Fresnel_Scale1;
		uniform float _Fresnel_Power1;
		uniform sampler2D _Emission_Map;
		uniform float2 _Panner_Speed_X_Y;
		uniform float4 _Emission_Color;
		uniform sampler2D _Fresnel_Emission_Map;
		uniform float2 _Panner2_Speed_X_Y;
		uniform float _Texture_Power;
		uniform float4 _Fresnel_Emission;
		uniform float _Fresnel2_Bias;
		uniform float _Fresnel2_Scale;
		uniform float _Fresnel2_Power;
		uniform float _Metallic1;
		uniform float _Smoothness1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Color_Base1.rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV4 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode4 = ( _Fresnel_Bias1 + _Fresnel_Scale1 * pow( 1.0 - fresnelNdotV4, _Fresnel_Power1 ) );
			float2 panner31 = ( _Time.y * _Panner_Speed_X_Y + i.uv_texcoord);
			float2 panner16 = ( _Time.y * _Panner2_Speed_X_Y + i.uv_texcoord);
			float4 temp_cast_1 = (_Texture_Power).xxxx;
			float4 clampResult24 = clamp( pow( tex2D( _Fresnel_Emission_Map, panner16 ) , temp_cast_1 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float fresnelNdotV30 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode30 = ( _Fresnel2_Bias + _Fresnel2_Scale * pow( 1.0 - fresnelNdotV30, _Fresnel2_Power ) );
			o.Emission = ( ( _Fresnel_Color1 * fresnelNode4 ) + ( tex2D( _Emission_Map, panner31 ) * _Emission_Color ) + ( clampResult24 * _Fresnel_Emission * fresnelNode30 ) ).rgb;
			o.Metallic = _Metallic1;
			o.Smoothness = _Smoothness1;
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
813;73;621;575;1955.058;-194.8935;1.782746;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;19;-1791.707,1043.831;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1839.387,768.6554;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;20;-1829.851,900.7942;Inherit;False;Property;_Panner2_Speed_X_Y;Panner2_Speed_X_Y;13;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;16;-1585.618,771.2042;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;32;-1418.451,644.1456;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-1447.835,358.7437;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;33;-1432.713,489.0467;Inherit;False;Property;_Panner_Speed_X_Y;Panner_Speed_X_Y;9;0;Create;True;0;0;False;0;0.1,0.2;0.1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SamplerNode;15;-1380.653,762.5015;Inherit;True;Property;_Fresnel_Emission_Map;Fresnel_Emission_Map;10;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-1258.252,966.6203;Inherit;False;Property;_Texture_Power;Texture_Power;11;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-1253.106,246.6012;Inherit;False;Property;_Fresnel_Power1;Fresnel_Power;6;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;31;-1227.697,360.689;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;21;-1068.48,766.4194;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1235.11,1106.577;Inherit;False;Property;_Fresnel2_Bias;Fresnel2_Bias;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1233.11,1174.577;Inherit;False;Property;_Fresnel2_Scale;Fresnel2_Scale;15;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-1255.106,107.6012;Inherit;False;Property;_Fresnel_Bias1;Fresnel_Bias;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-1253.106,175.6012;Inherit;False;Property;_Fresnel_Scale1;Fresnel_Scale;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1233.11,1245.577;Inherit;False;Property;_Fresnel2_Power;Fresnel2_Power;16;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;30;-1047.274,1125.545;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;-1007.538,563.4274;Inherit;False;Property;_Emission_Color;Emission_Color;8;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;4;-1067.271,126.5692;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-1040.29,-82.8005;Inherit;False;Property;_Fresnel_Color1;Fresnel_Color;1;1;[HDR];Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1049.174,348.9392;Inherit;True;Property;_Emission_Map;Emission_Map;7;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;24;-910.4716,768.3073;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;25;-1014.51,917.8881;Inherit;False;Property;_Fresnel_Emission;Fresnel_Emission;12;1;[HDR];Create;True;0;0;False;0;4,4,4,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-790.2715,51.18357;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-717.3175,356.391;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-743.0703,833.847;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-551.4691,134.6744;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;9;-348.1013,-225.8151;Inherit;False;Property;_Color_Base1;Color_Base;0;1;[HDR];Create;True;0;0;False;0;0.8,0.8,0.8,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-375.7719,815.0862;Inherit;False;Property;_Metallic1;Metallic;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-377.7914,947.8911;Inherit;False;Property;_Smoothness1;Smoothness;3;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;QQA/Shader_IceSpear;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;18;0
WireConnection;16;2;20;0
WireConnection;16;1;19;0
WireConnection;15;1;16;0
WireConnection;31;0;11;0
WireConnection;31;2;33;0
WireConnection;31;1;32;0
WireConnection;21;0;15;0
WireConnection;21;1;22;0
WireConnection;30;1;27;0
WireConnection;30;2;28;0
WireConnection;30;3;29;0
WireConnection;4;1;1;0
WireConnection;4;2;2;0
WireConnection;4;3;3;0
WireConnection;10;1;31;0
WireConnection;24;0;21;0
WireConnection;8;0;5;0
WireConnection;8;1;4;0
WireConnection;13;0;10;0
WireConnection;13;1;12;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;26;2;30;0
WireConnection;14;0;8;0
WireConnection;14;1;13;0
WireConnection;14;2;26;0
WireConnection;0;0;9;0
WireConnection;0;2;14;0
WireConnection;0;3;6;0
WireConnection;0;4;7;0
ASEEND*/
//CHKSM=936668B9839D710D641973DDE55560624080DEB8