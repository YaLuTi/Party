// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_AdvanceType"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_R_Color("R_Color", Color) = (1,0,0,0)
		_R_Metallic("R_Metallic", Range( 0 , 1)) = 0
		_R_Smoothness("R_Smoothness", Range( 0 , 1)) = 0.5
		_R_Fresnel_Bias("R_Fresnel_Bias", Float) = 0
		_R_Fresnel_Scale("R_Fresnel_Scale", Float) = 1
		_R_Fresnel_Power("R_Fresnel_Power", Float) = 5
		_G_Color("G_Color", Color) = (0,1,0,0)
		_G_Metallic("G_Metallic", Range( 0 , 1)) = 0
		_G_Smoothness("G_Smoothness", Range( 0 , 1)) = 0.5
		_G_Fresnel_Bias("G_Fresnel_Bias", Float) = 0
		_G_Fresnel_Scale("G_Fresnel_Scale", Float) = 1
		_G_Fresnel_Power("G_Fresnel_Power", Float) = 5
		_B_Color("B_Color", Color) = (0,0,1,0)
		_B_Metallic("B_Metallic", Range( 0 , 1)) = 0
		_B_Smoothness("B_Smoothness", Range( 0 , 1)) = 0.5
		_B_Fresnel_Bias("B_Fresnel_Bias", Float) = 0
		_B_Fresnel_Scale("B_Fresnel_Scale", Float) = 1
		_B_Fresnel_Power("B_Fresnel_Power", Float) = 5
		_Y_Color("Y_Color", Color) = (1,1,0,0)
		_Y_Metallic("Y_Metallic", Range( 0 , 1)) = 0
		_Y_Smoothness("Y_Smoothness", Range( 0 , 1)) = 0.5
		_Y_Fresnel_Bias("Y_Fresnel_Bias", Float) = 0
		_Y_Fresnel_Scale("Y_Fresnel_Scale", Float) = 1
		_Y_Fresnel_Power("Y_Fresnel_Power", Float) = 5
		_C_Color("C_Color", Color) = (0,1,1,0)
		_C_Metallic("C_Metallic", Range( 0 , 1)) = 0
		_C_Smoothness("C_Smoothness", Range( 0 , 1)) = 0.5
		_C_Fresnel_Bias("C_Fresnel_Bias", Float) = 0
		_C_Fresnel_Scale("C_Fresnel_Scale", Float) = 1
		_C_Fresnel_Power("C_Fresnel_Power", Float) = 5
		_P_Color("P_Color", Color) = (1,0,1,0)
		_P_Metallic("P_Metallic", Range( 0 , 1)) = 0
		_P_Smoothness("P_Smoothness", Range( 0 , 1)) = 0.5
		_P_Fresnel_Bias("P_Fresnel_Bias", Float) = 0
		_P_Fresnel_Scale("P_Fresnel_Scale", Float) = 1
		_P_Fresnel_Power("P_Fresnel_Power", Float) = 5
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
		uniform float4 _Y_Color;
		uniform float4 _C_Color;
		uniform float4 _P_Color;
		uniform float _R_Fresnel_Bias;
		uniform float _R_Fresnel_Scale;
		uniform float _R_Fresnel_Power;
		uniform float _G_Fresnel_Bias;
		uniform float _G_Fresnel_Scale;
		uniform float _G_Fresnel_Power;
		uniform float _B_Fresnel_Bias;
		uniform float _B_Fresnel_Scale;
		uniform float _B_Fresnel_Power;
		uniform float _Y_Fresnel_Bias;
		uniform float _Y_Fresnel_Scale;
		uniform float _Y_Fresnel_Power;
		uniform float _C_Fresnel_Bias;
		uniform float _C_Fresnel_Scale;
		uniform float _C_Fresnel_Power;
		uniform float _P_Fresnel_Bias;
		uniform float _P_Fresnel_Scale;
		uniform float _P_Fresnel_Power;
		uniform float _R_Metallic;
		uniform float _G_Metallic;
		uniform float _B_Metallic;
		uniform float _Y_Metallic;
		uniform float _C_Metallic;
		uniform float _P_Metallic;
		uniform float _R_Smoothness;
		uniform float _G_Smoothness;
		uniform float _B_Smoothness;
		uniform float _Y_Smoothness;
		uniform float _C_Smoothness;
		uniform float _P_Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 tex2DNode3 = tex2D( _TextureSample0, i.uv_texcoord );
			float4 appendResult29 = (float4(tex2DNode3.r , tex2DNode3.g , tex2DNode3.b , 0.0));
			float4 break47 = appendResult29;
			float4 appendResult11 = (float4(( 1.0 - tex2DNode3.r ) , ( 1.0 - tex2DNode3.g ) , ( 1.0 - tex2DNode3.b ) , 0.0));
			float4 break50 = appendResult11;
			o.Albedo = ( ( _R_Color * break47.x * break50.y * break50.z ) + ( _G_Color * break47.y * break50.x * break50.z ) + ( _B_Color * break47.z * break50.x * break50.y ) + ( _Y_Color * break47.x * break47.y * break50.z ) + ( _C_Color * break47.y * break47.z * break50.x ) + ( _P_Color * break47.x * break47.z * break50.y ) ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV62 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode62 = ( _R_Fresnel_Bias + _R_Fresnel_Scale * pow( 1.0 - fresnelNdotV62, _R_Fresnel_Power ) );
			float4 break49 = appendResult29;
			float4 break59 = appendResult11;
			float fresnelNdotV48 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode48 = ( _G_Fresnel_Bias + _G_Fresnel_Scale * pow( 1.0 - fresnelNdotV48, _G_Fresnel_Power ) );
			float fresnelNdotV58 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode58 = ( _B_Fresnel_Bias + _B_Fresnel_Scale * pow( 1.0 - fresnelNdotV58, _B_Fresnel_Power ) );
			float fresnelNdotV54 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode54 = ( _Y_Fresnel_Bias + _Y_Fresnel_Scale * pow( 1.0 - fresnelNdotV54, _Y_Fresnel_Power ) );
			float fresnelNdotV57 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode57 = ( _C_Fresnel_Bias + _C_Fresnel_Scale * pow( 1.0 - fresnelNdotV57, _C_Fresnel_Power ) );
			float fresnelNdotV56 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode56 = ( _P_Fresnel_Bias + _P_Fresnel_Scale * pow( 1.0 - fresnelNdotV56, _P_Fresnel_Power ) );
			float3 temp_cast_1 = (( ( fresnelNode62 * break49.x * break59.y * break59.z ) + ( fresnelNode48 * break49.y * break59.x * break59.z ) + ( fresnelNode58 * break49.z * break59.x * break59.y ) + ( fresnelNode54 * break49.x * break49.y * break59.z ) + ( fresnelNode57 * break49.y * break49.z * break59.x ) + ( fresnelNode56 * break49.x * break49.z * break59.y ) )).xxx;
			o.Emission = temp_cast_1;
			float4 break40 = appendResult29;
			float4 break52 = appendResult11;
			o.Metallic = ( ( _R_Metallic * break40.x * break52.y * break52.z ) + ( _G_Metallic * break40.y * break52.x * break52.z ) + ( _B_Metallic * break40.z * break52.x * break52.y ) + ( _Y_Metallic * break40.x * break40.y * break52.z ) + ( _C_Metallic * break40.y * break40.z * break52.x ) + ( _P_Metallic * break40.x * break40.z * break52.y ) );
			float4 break31 = appendResult29;
			float4 break36 = appendResult11;
			o.Smoothness = ( ( _R_Smoothness * break31.x * break36.y * break36.z ) + ( _G_Smoothness * break31.y * break36.x * break36.z ) + ( _B_Smoothness * break31.z * break36.x * break36.y ) + ( _Y_Smoothness * break31.x * break31.y * break36.z ) + ( _C_Smoothness * break31.y * break31.z * break36.x ) + ( _P_Smoothness * break31.x * break31.z * break36.y ) );
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
467;73;967;578;5612.063;2161.836;6.93874;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-3035.497,-9.741216;Inherit;False;706.5916;506.4701;;7;29;11;7;6;5;3;2;UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-2985.497,40.25878;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-2773.78,42.82478;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;4;-1193.184,-1075.539;Inherit;False;1078.722;1365.61;Comment;33;89;86;79;77;74;65;64;62;59;58;57;56;54;49;48;30;28;26;24;23;22;21;20;19;18;17;16;14;13;12;10;9;8;Fresnel;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;5;-2643.003,240.8477;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;6;-2642.537,314.1185;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-2647.745,385.7289;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-865.4871,174.0714;Inherit;False;Property;_P_Fresnel_Power;P_Fresnel_Power;36;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-2494.584,275.2745;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-872.196,-957.5389;Inherit;False;Property;_R_Fresnel_Scale;R_Fresnel_Scale;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-871.6215,-678.1154;Inherit;False;Property;_G_Fresnel_Power;G_Fresnel_Power;12;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-871.6215,-749.1154;Inherit;False;Property;_G_Fresnel_Scale;G_Fresnel_Scale;11;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;27;-2132.573,306.5241;Inherit;False;893.9846;1135.379;;15;90;83;78;73;69;68;67;53;52;40;37;35;34;33;32;Metallic;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-872.196,-886.5389;Inherit;False;Property;_R_Fresnel_Power;R_Fresnel_Power;6;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-873.6215,-817.1154;Inherit;False;Property;_G_Fresnel_Bias;G_Fresnel_Bias;10;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-874.196,-1025.539;Inherit;False;Property;_R_Fresnel_Bias;R_Fresnel_Bias;4;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-866.0304,-461.0391;Inherit;False;Property;_B_Fresnel_Power;B_Fresnel_Power;18;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;29;-2489.905,72.89497;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-865.4563,-323.6154;Inherit;False;Property;_Y_Fresnel_Scale;Y_Fresnel_Scale;23;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-867.4871,35.07144;Inherit;False;Property;_P_Fresnel_Bias;P_Fresnel_Bias;34;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-866.0621,-34.35264;Inherit;False;Property;_C_Fresnel_Power;C_Fresnel_Power;30;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-866.0304,-532.0392;Inherit;False;Property;_B_Fresnel_Scale;B_Fresnel_Scale;17;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-865.4563,-252.6154;Inherit;False;Property;_Y_Fresnel_Power;Y_Fresnel_Power;24;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-868.0303,-600.0392;Inherit;False;Property;_B_Fresnel_Bias;B_Fresnel_Bias;16;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-866.0621,-105.3523;Inherit;False;Property;_C_Fresnel_Scale;C_Fresnel_Scale;29;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-865.4871,103.0714;Inherit;False;Property;_P_Fresnel_Scale;P_Fresnel_Scale;35;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-867.4562,-391.6154;Inherit;False;Property;_Y_Fresnel_Bias;Y_Fresnel_Bias;22;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-868.0621,-173.3524;Inherit;False;Property;_C_Fresnel_Bias;C_Fresnel_Bias;28;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;25;-2107.805,-1043.393;Inherit;False;837.9405;1200.342;;15;88;85;82;72;71;66;63;61;60;55;50;47;46;44;42;Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;15;-1163.312,445.9019;Inherit;False;893.9846;1135.379;;15;87;84;81;80;76;75;70;51;45;43;41;39;38;36;31;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;42;-1829.089,-802.5422;Inherit;False;Property;_G_Color;G_Color;7;0;Create;True;0;0;False;0;0,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;40;-2082.573,692.1786;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;52;-2081.828,881.27;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;37;-1839.207,719.6627;Inherit;False;Property;_B_Metallic;B_Metallic;14;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;36;-1112.566,1020.648;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;35;-1830.311,356.5241;Inherit;False;Property;_R_Metallic;R_Metallic;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;31;-1113.312,831.5565;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;32;-1834.151,540.6829;Inherit;False;Property;_G_Metallic;G_Metallic;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;61;-1826.289,-993.3929;Inherit;False;Property;_R_Color;R_Color;1;0;Create;True;0;0;False;0;1,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;53;-1849.666,1249.388;Inherit;False;Property;_P_Metallic;P_Metallic;32;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;62;-671.8468,-938.8384;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-869.9441,859.0405;Inherit;False;Property;_B_Smoothness;B_Smoothness;15;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;59;-1142.438,-407.3745;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;38;-880.4032,1388.766;Inherit;False;Property;_P_Smoothness;P_Smoothness;33;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-871.5072,1027.625;Inherit;False;Property;_Y_Smoothness;Y_Smoothness;21;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-875.3472,1209.785;Inherit;False;Property;_C_Smoothness;C_Smoothness;27;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-861.0481,495.9019;Inherit;False;Property;_R_Smoothness;R_Smoothness;3;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;44;-1833.006,-427.7988;Inherit;False;Property;_Y_Color;Y_Color;19;0;Create;True;0;0;False;0;1,1,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;-864.8882,680.0607;Inherit;False;Property;_G_Smoothness;G_Smoothness;9;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;46;-1827.484,-620.6448;Inherit;False;Property;_B_Color;B_Color;13;0;Create;True;0;0;False;0;0,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;47;-2057.805,-610.3785;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FresnelNode;48;-671.847,-755.9681;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;49;-1143.184,-596.4659;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;60;-1834.202,-55.05042;Inherit;False;Property;_P_Color;P_Color;31;0;Create;True;0;0;False;0;1,0,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;34;-1844.61,1070.407;Inherit;False;Property;_C_Metallic;C_Metallic;26;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;50;-2057.06,-421.287;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;33;-1840.77,888.2476;Inherit;False;Property;_Y_Metallic;Y_Metallic;20;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;54;-673.7126,-399.5583;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;-1835.806,-236.948;Inherit;False;Property;_C_Color;C_Color;25;0;Create;True;0;0;False;0;0,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;56;-675.5789,-37.55014;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;57;-675.579,-211.0899;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;58;-671.8469,-576.8301;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-1583.415,1258.902;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-1573.796,548.14;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1574.903,723.8242;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-1570.152,359.901;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-1582.308,1083.218;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-1578.664,896.9788;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-1609.939,-403.8799;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-609.4014,1036.357;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-1614.69,-41.95594;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-446.0244,-923.3737;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-458.1811,-202.0555;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;-1605.072,-752.7191;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-614.1522,1398.28;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-1613.584,-217.6405;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-450.7755,-561.4496;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-613.0452,1222.596;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-1601.427,-938.9584;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-454.5365,-388.295;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-604.5332,687.5178;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-605.6403,863.202;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-1606.178,-577.0345;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-600.8892,499.2787;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-459.2876,-26.3712;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-449.669,-737.1343;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;90;-1391.589,761.2645;Inherit;False;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-422.3261,900.6422;Inherit;False;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-1422.865,-539.5942;Inherit;False;6;6;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-267.4621,-524.0093;Inherit;False;6;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Shader_AdvanceType;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;2;0
WireConnection;5;0;3;1
WireConnection;6;0;3;2
WireConnection;7;0;3;3
WireConnection;11;0;5;0
WireConnection;11;1;6;0
WireConnection;11;2;7;0
WireConnection;29;0;3;1
WireConnection;29;1;3;2
WireConnection;29;2;3;3
WireConnection;40;0;29;0
WireConnection;52;0;11;0
WireConnection;36;0;11;0
WireConnection;31;0;29;0
WireConnection;62;1;16;0
WireConnection;62;2;8;0
WireConnection;62;3;24;0
WireConnection;59;0;11;0
WireConnection;47;0;29;0
WireConnection;48;1;28;0
WireConnection;48;2;22;0
WireConnection;48;3;23;0
WireConnection;49;0;29;0
WireConnection;50;0;11;0
WireConnection;54;1;13;0
WireConnection;54;2;21;0
WireConnection;54;3;10;0
WireConnection;56;1;20;0
WireConnection;56;2;12;0
WireConnection;56;3;30;0
WireConnection;57;1;14;0
WireConnection;57;2;17;0
WireConnection;57;3;19;0
WireConnection;58;1;18;0
WireConnection;58;2;9;0
WireConnection;58;3;26;0
WireConnection;67;0;53;0
WireConnection;67;1;40;0
WireConnection;67;2;40;2
WireConnection;67;3;52;1
WireConnection;73;0;32;0
WireConnection;73;1;40;1
WireConnection;73;2;52;0
WireConnection;73;3;52;2
WireConnection;78;0;37;0
WireConnection;78;1;40;2
WireConnection;78;2;52;0
WireConnection;78;3;52;1
WireConnection;83;0;35;0
WireConnection;83;1;40;0
WireConnection;83;2;52;1
WireConnection;83;3;52;2
WireConnection;69;0;34;0
WireConnection;69;1;40;1
WireConnection;69;2;40;2
WireConnection;69;3;52;0
WireConnection;68;0;33;0
WireConnection;68;1;40;0
WireConnection;68;2;40;1
WireConnection;68;3;52;2
WireConnection;66;0;44;0
WireConnection;66;1;47;0
WireConnection;66;2;47;1
WireConnection;66;3;50;2
WireConnection;76;0;39;0
WireConnection;76;1;31;0
WireConnection;76;2;31;1
WireConnection;76;3;36;2
WireConnection;63;0;60;0
WireConnection;63;1;47;0
WireConnection;63;2;47;2
WireConnection;63;3;50;1
WireConnection;64;0;62;0
WireConnection;64;1;49;0
WireConnection;64;2;59;1
WireConnection;64;3;59;2
WireConnection;65;0;57;0
WireConnection;65;1;49;1
WireConnection;65;2;49;2
WireConnection;65;3;59;0
WireConnection;72;0;42;0
WireConnection;72;1;47;1
WireConnection;72;2;50;0
WireConnection;72;3;50;2
WireConnection;70;0;38;0
WireConnection;70;1;31;0
WireConnection;70;2;31;2
WireConnection;70;3;36;1
WireConnection;71;0;55;0
WireConnection;71;1;47;1
WireConnection;71;2;47;2
WireConnection;71;3;50;0
WireConnection;74;0;58;0
WireConnection;74;1;49;2
WireConnection;74;2;59;0
WireConnection;74;3;59;1
WireConnection;75;0;41;0
WireConnection;75;1;31;1
WireConnection;75;2;31;2
WireConnection;75;3;36;0
WireConnection;85;0;61;0
WireConnection;85;1;47;0
WireConnection;85;2;50;1
WireConnection;85;3;50;2
WireConnection;79;0;54;0
WireConnection;79;1;49;0
WireConnection;79;2;49;1
WireConnection;79;3;59;2
WireConnection;80;0;45;0
WireConnection;80;1;31;1
WireConnection;80;2;36;0
WireConnection;80;3;36;2
WireConnection;81;0;51;0
WireConnection;81;1;31;2
WireConnection;81;2;36;0
WireConnection;81;3;36;1
WireConnection;82;0;46;0
WireConnection;82;1;47;2
WireConnection;82;2;50;0
WireConnection;82;3;50;1
WireConnection;84;0;43;0
WireConnection;84;1;31;0
WireConnection;84;2;36;1
WireConnection;84;3;36;2
WireConnection;86;0;56;0
WireConnection;86;1;49;0
WireConnection;86;2;49;2
WireConnection;86;3;59;1
WireConnection;77;0;48;0
WireConnection;77;1;49;1
WireConnection;77;2;59;0
WireConnection;77;3;59;2
WireConnection;90;0;83;0
WireConnection;90;1;73;0
WireConnection;90;2;78;0
WireConnection;90;3;68;0
WireConnection;90;4;69;0
WireConnection;90;5;67;0
WireConnection;87;0;84;0
WireConnection;87;1;80;0
WireConnection;87;2;81;0
WireConnection;87;3;76;0
WireConnection;87;4;75;0
WireConnection;87;5;70;0
WireConnection;88;0;85;0
WireConnection;88;1;72;0
WireConnection;88;2;82;0
WireConnection;88;3;66;0
WireConnection;88;4;71;0
WireConnection;88;5;63;0
WireConnection;89;0;64;0
WireConnection;89;1;77;0
WireConnection;89;2;74;0
WireConnection;89;3;79;0
WireConnection;89;4;65;0
WireConnection;89;5;86;0
WireConnection;0;0;88;0
WireConnection;0;2;89;0
WireConnection;0;3;90;0
WireConnection;0;4;87;0
ASEEND*/
//CHKSM=A9D0B8EC89009FD3DADA4A26B9E43D07499AD859