// Upgrade NOTE: upgraded instancing buffer 'ZianTornado' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Zian/Tornado"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		[HDR]_Green("Green", Color) = (0.3212634,0.9448925,0.06299283,1)
		[HDR]_Flame("Flame", Color) = (3.85,0.26,0,1)
		[HDR]_Icecolor("Icecolor", Color) = (0.12,0.58,0.75,1)
		_Ice("Ice", 2D) = "white" {}
		_FrozenLevel("Frozen Level", Range( 0 , 1)) = 0
		[HDR]_FreezeBorderColor("FreezeBorderColor", Color) = (1,1,1,0)
		_Height("Height", Range( 0 , 40)) = 25
		_OpacityMultiplier("OpacityMultiplier", Range( 0 , 1)) = 1
		_DisplacementMultiplier("Displacement Multiplier", Range( 0 , 10)) = 10
		_Cutoff( "Mask Clip Value", Float ) = 0.18
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Fire("Fire", 2D) = "white" {}
		_OpacityMask("OpacityMask", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		#pragma multi_compile_instancing
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample1;
		uniform sampler2D _TextureSample0;
		uniform float _Height;
		uniform sampler2D _TextureSample2;
		uniform float _DisplacementMultiplier;
		uniform sampler2D _Ice;
		uniform sampler2D _Fire;
		uniform sampler2D _OpacityMask;
		uniform float _OpacityMultiplier;
		uniform float _Cutoff = 0.18;
		uniform float _EdgeLength;

		UNITY_INSTANCING_BUFFER_START(ZianTornado)
			UNITY_DEFINE_INSTANCED_PROP(float4, _TextureSample0_ST)
#define _TextureSample0_ST_arr ZianTornado
			UNITY_DEFINE_INSTANCED_PROP(float4, _Icecolor)
#define _Icecolor_arr ZianTornado
			UNITY_DEFINE_INSTANCED_PROP(float4, _Green)
#define _Green_arr ZianTornado
			UNITY_DEFINE_INSTANCED_PROP(float4, _Flame)
#define _Flame_arr ZianTornado
			UNITY_DEFINE_INSTANCED_PROP(float4, _FreezeBorderColor)
#define _FreezeBorderColor_arr ZianTornado
			UNITY_DEFINE_INSTANCED_PROP(float, _FrozenLevel)
#define _FrozenLevel_arr ZianTornado
		UNITY_INSTANCING_BUFFER_END(ZianTornado)

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float _FrozenLevel_Instance = UNITY_ACCESS_INSTANCED_PROP(_FrozenLevel_arr, _FrozenLevel);
			float4 _TextureSample0_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureSample0_ST_arr, _TextureSample0_ST);
			float2 uv_TextureSample0 = v.texcoord * _TextureSample0_ST_Instance.xy + _TextureSample0_ST_Instance.zw;
			float lerpResult43 = lerp( 0.0 , 1.0 , _FrozenLevel_Instance);
			float temp_output_42_0 = (-0.1 + (( _FrozenLevel_Instance + tex2Dlod( _TextureSample0, float4( uv_TextureSample0, 0, 0.0) ).r ) - 0.0) * (lerpResult43 - -0.1) / (1.0 - 0.0));
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float temp_output_45_0 = ( ase_worldPos.y / _Height );
			float temp_output_48_0 = step( temp_output_42_0 , temp_output_45_0 );
			float Freeze50 = temp_output_48_0;
			float2 panner78 = ( 1.0 * _Time.y * ( float2( -0.15,0.3 ) * Freeze50 ) + v.texcoord.xy);
			float4 tex2DNode81 = tex2Dlod( _TextureSample1, float4( panner78, 0, 0.0) );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 Displacement86 = ( tex2DNode81.r * tex2DNode81.r * tex2Dlod( _TextureSample2, float4( panner78, 0, 0.0) ).r * ase_vertexNormal * _DisplacementMultiplier );
			v.vertex.xyz += Displacement86;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner29 = ( 1.0 * _Time.y * float2( -0.01,-0.03 ) + i.uv_texcoord);
			float4 _Icecolor_Instance = UNITY_ACCESS_INSTANCED_PROP(_Icecolor_arr, _Icecolor);
			float4 IceAlbedo33 = ( tex2D( _Ice, panner29 ) * _Icecolor_Instance );
			float2 panner5 = ( 1.0 * _Time.y * float2( -0.6,-0.6 ) + i.uv_texcoord);
			float4 tex2DNode4 = tex2D( _Fire, panner5 );
			float4 _Green_Instance = UNITY_ACCESS_INSTANCED_PROP(_Green_arr, _Green);
			float4 _Flame_Instance = UNITY_ACCESS_INSTANCED_PROP(_Flame_arr, _Flame);
			float4 FireAlbedo13 = ( ( step( tex2DNode4.r , 0.25 ) * _Green_Instance ) + ( step( tex2DNode4.r , 0.325 ) * _Flame_Instance ) );
			float _FrozenLevel_Instance = UNITY_ACCESS_INSTANCED_PROP(_FrozenLevel_arr, _FrozenLevel);
			float4 _TextureSample0_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureSample0_ST_arr, _TextureSample0_ST);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST_Instance.xy + _TextureSample0_ST_Instance.zw;
			float lerpResult43 = lerp( 0.0 , 1.0 , _FrozenLevel_Instance);
			float temp_output_42_0 = (-0.1 + (( _FrozenLevel_Instance + tex2D( _TextureSample0, uv_TextureSample0 ).r ) - 0.0) * (lerpResult43 - -0.1) / (1.0 - 0.0));
			float3 ase_worldPos = i.worldPos;
			float temp_output_45_0 = ( ase_worldPos.y / _Height );
			float temp_output_48_0 = step( temp_output_42_0 , temp_output_45_0 );
			float Freeze50 = temp_output_48_0;
			float4 lerpResult27 = lerp( IceAlbedo33 , FireAlbedo13 , Freeze50);
			o.Albedo = lerpResult27.rgb;
			float4 _FreezeBorderColor_Instance = UNITY_ACCESS_INSTANCED_PROP(_FreezeBorderColor_arr, _FreezeBorderColor);
			float4 FreezeBorder54 = ( ( step( temp_output_42_0 , ( temp_output_45_0 + 0.1 ) ) - temp_output_48_0 ) * _FreezeBorderColor_Instance );
			o.Emission = ( ( lerpResult27 * i.vertexColor ) + FreezeBorder54 ).rgb;
			float smoothstepResult91 = smoothstep( 0.0 , 1.0 , i.uv_texcoord.y);
			float4 temp_cast_2 = (smoothstepResult91).xxxx;
			float4 lerpResult94 = lerp( ( IceAlbedo33 * smoothstepResult91 ) , temp_cast_2 , Freeze50);
			float4 Opacity95 = lerpResult94;
			o.Alpha = Opacity95.r;
			float2 panner59 = ( 1.0 * _Time.y * ( float2( 0.6,0.6 ) * Freeze50 ) + i.uv_texcoord);
			float4 tex2DNode62 = tex2D( _OpacityMask, panner59 );
			float smoothstepResult74 = smoothstep( 0.0 , ( 1.0 - ( i.uv_texcoord.y * ( 1.0 - i.uv_texcoord.y ) ) ) , ( ( tex2DNode62.r * _OpacityMultiplier ) + 0.0 ));
			float OpacityMask75 = ( smoothstepResult74 - ( 0.0 * tex2DNode62.r ) );
			clip( OpacityMask75 - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
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
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
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
				vertexDataFunc( v );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
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
				surfIN.vertexColor = IN.color;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
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
0;73;860;794;3538.97;1087.755;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;55;-4412.075,-879.2681;Inherit;False;2057.158;684.6563;Frozen Level;16;41;39;42;40;43;44;46;47;48;49;51;53;52;50;54;45;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-4317.997,-785.5783;Float;False;InstancedProperty;_FrozenLevel;Frozen Level;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;-4362.075,-597.3986;Inherit;True;Property;_TextureSample0;Texture Sample 0;15;0;Create;True;0;0;False;0;-1;6cd2478afd4c55348a107e1685ae161c;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;44;-3697.349,-584.7862;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;43;-3893.938,-571.9145;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-3807.366,-310.6122;Float;False;Property;_Height;Height;11;0;Create;True;0;0;False;0;25;25;0;40;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-3887.763,-788.4039;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;45;-3399.083,-538.1976;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;42;-3558.395,-829.2682;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;48;-3169.821,-803.122;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;28;-1999.387,-858.4112;Inherit;False;1758.115;718.0544;Fire Emissive;12;3;5;4;8;6;11;10;7;12;2;13;9;;1,0.6096754,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1949.387,-625.8867;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;3;-1917.698,-500.6064;Inherit;False;Constant;_Vector0;Vector 0;0;0;Create;True;0;0;False;0;-0.6,-0.6;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;34;-2002.511,-1.710583;Inherit;False;1671.062;565.8754;Ice Emissive;7;20;21;29;30;31;33;32;;0.08116765,0.9056604,0.8552058,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;-2825.283,-807.7256;Inherit;False;Freeze;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;76;-4441.058,-93.71121;Inherit;False;2114.69;714.6002;OpacityMask;17;57;58;59;60;62;63;65;56;67;69;68;70;64;66;74;71;75;;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;58;-4389.137,319.6333;Inherit;False;50;Freeze;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;21;-1952.511,244.0996;Inherit;False;Constant;_Vector2;Vector 2;3;0;Create;True;0;0;False;0;-0.01,-0.03;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;5;-1640.207,-619.6072;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;57;-4391.058,143.0488;Inherit;False;Constant;_Vector1;Vector 1;7;0;Create;True;0;0;False;0;0.6,0.6;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1952.013,48.28942;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-1410.516,-677.3118;Inherit;True;Property;_Fire;Fire;16;0;Create;True;0;0;False;0;-1;94aefe2e99e52354991113994ab93560;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;56;-4369.887,-41.43594;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;29;-1601.397,109.9777;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-4154.757,237.8887;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;11;-935.2988,-564.1917;Inherit;False;InstancedProperty;_Green;Green;5;1;[HDR];Create;True;0;0;False;0;0.3212634,0.9448925,0.06299283,1;0.3212634,0.9448925,0.06299283,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;30;-1277.76,84.52886;Inherit;True;Property;_Ice;Ice;8;0;Create;True;0;0;False;0;-1;6cd2478afd4c55348a107e1685ae161c;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;87;-2007.705,668.3848;Inherit;False;1922.927;807.4216;Displacement;11;77;78;22;79;23;81;82;83;84;85;86;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StepOpNode;6;-1096.309,-808.4112;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-1494.327,-352.3568;Inherit;False;InstancedProperty;_Flame;Flame;6;1;[HDR];Create;True;0;0;False;0;3.85,0.26,0,1;3.85,0.26,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;32;-1252.45,352.1648;Inherit;False;InstancedProperty;_Icecolor;Icecolor;7;1;[HDR];Create;True;0;0;False;0;0.12,0.58,0.75,1;0.12,0.58,0.75,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;59;-3964.357,17.08875;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;8;-1187.327,-450.3568;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.325;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;67;-4092.359,401.0888;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;62;-3705.158,-43.71122;Inherit;True;Property;_OpacityMask;OpacityMask;17;0;Create;True;0;0;False;0;-1;6cd2478afd4c55348a107e1685ae161c;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;23;-1957.705,1157.616;Inherit;False;50;Freeze;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-854.4497,194.1648;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1041.327,-332.7568;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-755.2542,-714.2231;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector2Node;79;-1944.088,991.3848;Inherit;False;Constant;_Vector3;Vector 3;9;0;Create;True;0;0;False;0;-0.15,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;65;-3860.359,269.8889;Float;False;Property;_OpacityMultiplier;OpacityMultiplier;12;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;96;-3775.9,808.486;Inherit;False;1527.886;438.0248;Opacity;7;89;91;90;92;93;94;95;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;68;-3781.959,509.8889;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-3269.164,-527.0452;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;77;-1901.088,800.3848;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-555.4497,195.1648;Inherit;False;IceAlbedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1688.72,1078.247;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-3561.159,450.6888;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-3725.9,930.9637;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-663.2064,-424.8484;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;49;-3100.079,-547.8209;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-3409.156,215.4888;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;90;-3380.808,858.486;Inherit;False;33;IceAlbedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-465.272,-426.7656;Inherit;False;FireAlbedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;66;-3205.958,218.6888;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;51;-2964.711,-652.0538;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;70;-3394.759,418.6888;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;91;-3336.944,1058.015;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;78;-1528.088,911.3848;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;53;-2938.991,-473.3693;Inherit;False;InstancedProperty;_FreezeBorderColor;FreezeBorderColor;10;1;[HDR];Create;True;0;0;False;0;1,1,1,0;2.492147,2.492147,2.492147,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;26;212.7389,330.0037;Inherit;False;50;Freeze;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;93;-3056.014,1130.511;Inherit;False;50;Freeze;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;-3067.014,906.5108;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-1173.266,1359.806;Float;False;Property;_DisplacementMultiplier;Displacement Multiplier;13;0;Create;True;0;0;False;0;10;10;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;81;-1270.088,718.3848;Inherit;True;Property;_TextureSample1;Texture Sample 1;18;0;Create;True;0;0;False;0;-1;94aefe2e99e52354991113994ab93560;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;82;-1269.088,926.3848;Inherit;True;Property;_TextureSample2;Texture Sample 2;19;0;Create;True;0;0;False;0;-1;94aefe2e99e52354991113994ab93560;94aefe2e99e52354991113994ab93560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;24;184.1389,91.50371;Inherit;False;33;IceAlbedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-3140.358,-5.311218;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;83;-1187.65,1164.459;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;74;-3047.159,274.0887;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-2726.465,-654.761;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;25;209.7388,231.0037;Inherit;False;13;FireAlbedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;54;-2578.916,-653.407;Inherit;False;FreezeBorder;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;98;404.0524,413.9648;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;27;495.5792,214.5376;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;71;-2799.359,145.2888;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;94;-2821.014,953.5108;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-588.6345,1007.331;Inherit;True;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;86;-308.7784,1044.154;Inherit;False;Displacement;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;104;554.6166,528.3959;Inherit;False;54;FreezeBorder;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;75;-2550.367,200.7736;Inherit;False;OpacityMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;95;-2472.014,958.5108;Inherit;False;Opacity;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;673.0524,301.9648;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;102;410.0524,799.9648;Inherit;False;75;OpacityMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;99;409.0524,684.9648;Inherit;False;95;Opacity;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;100;401.0524,917.9648;Inherit;False;86;Displacement;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;103;792.6166,417.3959;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;976.8978,286.2818;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;Zian/Tornado;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.18;True;True;0;True;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;14;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;2;39;0
WireConnection;40;0;39;0
WireConnection;40;1;41;1
WireConnection;45;0;44;2
WireConnection;45;1;46;0
WireConnection;42;0;40;0
WireConnection;42;4;43;0
WireConnection;48;0;42;0
WireConnection;48;1;45;0
WireConnection;50;0;48;0
WireConnection;5;0;2;0
WireConnection;5;2;3;0
WireConnection;4;1;5;0
WireConnection;29;0;20;0
WireConnection;29;2;21;0
WireConnection;60;0;57;0
WireConnection;60;1;58;0
WireConnection;30;1;29;0
WireConnection;6;0;4;1
WireConnection;59;0;56;0
WireConnection;59;2;60;0
WireConnection;8;0;4;1
WireConnection;62;1;59;0
WireConnection;31;0;30;0
WireConnection;31;1;32;0
WireConnection;10;0;8;0
WireConnection;10;1;9;0
WireConnection;7;0;6;0
WireConnection;7;1;11;0
WireConnection;68;0;67;2
WireConnection;47;0;45;0
WireConnection;33;0;31;0
WireConnection;22;0;79;0
WireConnection;22;1;23;0
WireConnection;69;0;67;2
WireConnection;69;1;68;0
WireConnection;12;0;7;0
WireConnection;12;1;10;0
WireConnection;49;0;42;0
WireConnection;49;1;47;0
WireConnection;64;0;62;1
WireConnection;64;1;65;0
WireConnection;13;0;12;0
WireConnection;66;0;64;0
WireConnection;51;0;49;0
WireConnection;51;1;48;0
WireConnection;70;0;69;0
WireConnection;91;0;89;2
WireConnection;78;0;77;0
WireConnection;78;2;22;0
WireConnection;92;0;90;0
WireConnection;92;1;91;0
WireConnection;81;1;78;0
WireConnection;82;1;78;0
WireConnection;63;1;62;1
WireConnection;74;0;66;0
WireConnection;74;2;70;0
WireConnection;52;0;51;0
WireConnection;52;1;53;0
WireConnection;54;0;52;0
WireConnection;27;0;24;0
WireConnection;27;1;25;0
WireConnection;27;2;26;0
WireConnection;71;0;74;0
WireConnection;71;1;63;0
WireConnection;94;0;92;0
WireConnection;94;1;91;0
WireConnection;94;2;93;0
WireConnection;85;0;81;1
WireConnection;85;1;81;1
WireConnection;85;2;82;1
WireConnection;85;3;83;0
WireConnection;85;4;84;0
WireConnection;86;0;85;0
WireConnection;75;0;71;0
WireConnection;95;0;94;0
WireConnection;97;0;27;0
WireConnection;97;1;98;0
WireConnection;103;0;97;0
WireConnection;103;1;104;0
WireConnection;0;0;27;0
WireConnection;0;2;103;0
WireConnection;0;9;99;0
WireConnection;0;10;102;0
WireConnection;0;11;100;0
ASEEND*/
//CHKSM=225360612468588234DCEF4DF5FA34B859B3BA19