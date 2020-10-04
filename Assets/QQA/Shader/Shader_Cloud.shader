// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_Cloud"
{
	Properties
	{
		_Position_Rotate("Position_Rotate", Vector) = (1,0,0,1)
		_Noise_Scale("Noise_Scale", Float) = 0.02
		_Noise_Speed("Noise_Speed", Vector) = (30,30,0,0)
		_Noise_Height("Noise_Height", Float) = 30
		_Noise_Remap("Noise_Remap", Vector) = (0,1,-0.3,2.7)
		_Color_Peak("Color_Peak", Color) = (0.5019608,0.8726192,0.945098,0)
		_Color_Valley("Color_Valley", Color) = (0.4633766,0.7081341,0.8396226,0)
		_Noise_Edge("Noise_Edge", Vector) = (-1,2,0,0)
		_Noise_Power("Noise_Power", Float) = 2
		_Base_Scale("Base_Scale", Float) = 0.01
		_Base_Speed("Base_Speed", Vector) = (20,20,0,0)
		_Base_Strength("Base_Strength", Float) = 2
		_Emission_Strength("Emission_Strength", Float) = 2
		_Curvature_Radius("Curvature_Radius", Float) = 250
		_Fresnel_Bias("Fresnel_Bias", Float) = 0
		_Fresnel_Scale("Fresnel_Scale", Float) = 1
		_Fresnel_Power("Fresnel_Power", Float) = 8
		_Fresnel_Opacity("Fresnel_Opacity", Float) = 1
		_Fade_Depth("Fade_Depth", Float) = 100
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float4 screenPos;
		};

		uniform float2 _Noise_Edge;
		uniform float2 _Noise_Speed;
		uniform float4 _Position_Rotate;
		uniform float _Noise_Scale;
		uniform float _Noise_Power;
		uniform float4 _Noise_Remap;
		uniform float2 _Base_Speed;
		uniform float _Base_Scale;
		uniform float _Base_Strength;
		uniform float _Noise_Height;
		uniform float _Curvature_Radius;
		uniform float4 _Color_Valley;
		uniform float4 _Color_Peak;
		uniform float _Fresnel_Bias;
		uniform float _Fresnel_Scale;
		uniform float _Fresnel_Power;
		uniform float _Fresnel_Opacity;
		uniform float _Emission_Strength;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Fade_Depth;


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
			float2 panner8 = ( _Time.y * _Noise_Speed + rotatedValue4.xy);
			float simplePerlin2D1 = snoise( panner8*_Noise_Scale );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float simplePerlin2D29 = snoise( rotatedValue4.xy*_Noise_Scale );
			simplePerlin2D29 = simplePerlin2D29*0.5 + 0.5;
			float smoothstepResult44 = smoothstep( _Noise_Edge.x , _Noise_Edge.y , abs( (_Noise_Remap.z + (pow( saturate( ( ( simplePerlin2D1 + simplePerlin2D29 ) / 2.0 ) ) , _Noise_Power ) - _Noise_Remap.x) * (_Noise_Remap.w - _Noise_Remap.z) / (_Noise_Remap.y - _Noise_Remap.x)) ));
			float2 panner54 = ( _Time.y * _Base_Speed + rotatedValue4.xy);
			float simplePerlin2D52 = snoise( panner54*_Base_Scale );
			simplePerlin2D52 = simplePerlin2D52*0.5 + 0.5;
			float temp_output_59_0 = ( ( smoothstepResult44 + ( simplePerlin2D52 * _Base_Strength ) ) / ( 1.0 + _Base_Strength ) );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			v.vertex.xyz += ( ( temp_output_59_0 * ase_vertexNormal * _Noise_Height ) + ( ase_worldNormal * pow( ( distance( ase_objectScale , ase_worldPos ) / _Curvature_Radius ) , 3.0 ) ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 rotatedValue4 = RotateAroundAxis( float3( 0,0,0 ), ase_worldPos, _Position_Rotate.xyz, _Position_Rotate.w );
			float2 panner8 = ( _Time.y * _Noise_Speed + rotatedValue4.xy);
			float simplePerlin2D1 = snoise( panner8*_Noise_Scale );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float simplePerlin2D29 = snoise( rotatedValue4.xy*_Noise_Scale );
			simplePerlin2D29 = simplePerlin2D29*0.5 + 0.5;
			float smoothstepResult44 = smoothstep( _Noise_Edge.x , _Noise_Edge.y , abs( (_Noise_Remap.z + (pow( saturate( ( ( simplePerlin2D1 + simplePerlin2D29 ) / 2.0 ) ) , _Noise_Power ) - _Noise_Remap.x) * (_Noise_Remap.w - _Noise_Remap.z) / (_Noise_Remap.y - _Noise_Remap.x)) ));
			float2 panner54 = ( _Time.y * _Base_Speed + rotatedValue4.xy);
			float simplePerlin2D52 = snoise( panner54*_Base_Scale );
			simplePerlin2D52 = simplePerlin2D52*0.5 + 0.5;
			float temp_output_59_0 = ( ( smoothstepResult44 + ( simplePerlin2D52 * _Base_Strength ) ) / ( 1.0 + _Base_Strength ) );
			float4 lerpResult38 = lerp( _Color_Valley , _Color_Peak , temp_output_59_0);
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV82 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode82 = ( _Fresnel_Bias + _Fresnel_Scale * pow( 1.0 - fresnelNdotV82, _Fresnel_Power ) );
			float4 temp_output_88_0 = ( lerpResult38 + ( temp_output_59_0 * fresnelNode82 * _Fresnel_Opacity ) );
			o.Albedo = temp_output_88_0.rgb;
			o.Emission = ( temp_output_88_0 * _Emission_Strength ).rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth91 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float smoothstepResult99 = smoothstep( 0.0 , 1.0 , saturate( ( ( eyeDepth91 - ( ase_screenPosNorm.w - 1.0 ) ) / _Fade_Depth ) ));
			o.Alpha = smoothstepResult99;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

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
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
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
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				surfIN.screenPos = IN.screenPos;
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
637;148;480;546;1057.906;312.5442;1.580923;False;False
Node;AmplifyShaderEditor.CommentaryNode;43;-3042.609,-196.7177;Inherit;False;646.2236;467.3049;;3;3;5;4;Projection;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;3;-2943.209,51.28722;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;5;-2946.561,-146.7178;Inherit;False;Property;_Position_Rotate;Position_Rotate;0;0;Create;True;0;0;False;0;1,0,0,1;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;64;-2673.553,448.3514;Inherit;False;248;161;;1;10;Time;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;63;-2151.334,-521.8696;Inherit;False;1041.02;311.0311;;8;1;29;31;32;48;6;8;11;Noise;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;11;-2101.334,-471.8697;Inherit;False;Property;_Noise_Speed;Noise_Speed;2;0;Create;True;0;0;False;0;30,30;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RotateAboutAxisNode;4;-2716.385,-27.25046;Inherit;False;False;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;10;-2623.553,498.3514;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;8;-1894.316,-470.1089;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-1881.148,-326.8637;Inherit;False;Property;_Noise_Scale;Noise_Scale;1;0;Create;True;0;0;False;0;0.02;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;29;-1711.744,-350.8386;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;1;-1709.78,-463.1214;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1510.865,-418.8819;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;50;-2153.723,-104.8404;Inherit;False;869.1106;392.2214;;7;44;35;33;34;45;46;47;Mod;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;32;-1389.173,-417.111;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;62;-2137.706,383.4443;Inherit;False;849.4246;334.6626;;6;55;54;53;58;52;57;Base;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-2105.477,-35.33106;Inherit;False;Property;_Noise_Power;Noise_Power;8;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;48;-1275.314,-416.0471;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;35;-2103.723,75.38093;Inherit;False;Property;_Noise_Remap;Noise_Remap;4;0;Create;True;0;0;False;0;0,1,-0.3,2.7;0,1,-1,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;46;-1943.657,-53.2707;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;55;-2042.706,433.4443;Inherit;False;Property;_Base_Speed;Base_Speed;10;0;Create;True;0;0;False;0;20,20;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;53;-1819.817,575.363;Inherit;False;Property;_Base_Scale;Base_Scale;9;0;Create;True;0;0;False;0;0.01;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;54;-1834.836,434.0046;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;33;-1774.17,-54.84044;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;98;-195.1877,-72.98833;Inherit;False;854.6287;393.6296;;8;97;93;92;91;94;95;96;99;Fade;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;45;-1766.417,121.3546;Inherit;False;Property;_Noise_Edge;Noise_Edge;7;0;Create;True;0;0;False;0;-1,2;0,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.CommentaryNode;79;-804.713,701.7225;Inherit;False;836.8946;482.1881;;8;78;72;70;77;76;68;75;73;Bowl;1,1,1,1;0;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;52;-1650.597,481.3928;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;34;-1599.542,-51.69618;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-1642.973,602.1069;Inherit;False;Property;_Base_Strength;Base_Strength;11;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;92;-145.1877,108.6413;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;72;-754.713,1000.911;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SmoothstepOpNode;44;-1473.613,-48.9434;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectScaleNode;78;-751.8052,842.1334;Inherit;False;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;89;-880.5563,-377.3478;Inherit;False;645.8563;404.3084;Comment;6;86;84;85;82;87;81;Fresnel;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-1450.282,482.5053;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;61;-1241.334,176.3383;Inherit;False;343.2063;295.7594;;3;56;59;60;Combine;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;60;-1191.334,337.0976;Inherit;False;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-578.5532,1035.467;Inherit;False;Property;_Curvature_Radius;Curvature_Radius;13;0;Create;True;0;0;False;0;250;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-826.62,-241.2378;Inherit;False;Property;_Fresnel_Bias;Fresnel_Bias;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;91;-0.4549904,-22.98833;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;-830.5563,-89.03951;Inherit;False;Property;_Fresnel_Power;Fresnel_Power;16;0;Create;True;0;0;False;0;8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;93;46.36721,113.909;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;-826.6199,-165.1385;Inherit;False;Property;_Fresnel_Scale;Fresnel_Scale;15;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-1186.983,226.3382;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;70;-562.7461,923.9226;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;41;-879.4304,-927.8096;Inherit;False;488.0097;443.8108;;3;39;40;38;Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;42;-716.1866,203.7271;Inherit;False;451.8254;323.6268;;3;12;21;13;Vertex;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-605.9933,-327.3478;Inherit;False;Property;_Fresnel_Opacity;Fresnel_Opacity;17;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;40;-829.4301,-695.9988;Inherit;False;Property;_Color_Valley;Color_Valley;6;0;Create;True;0;0;False;0;0.4633766,0.7081341,0.8396226,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;39;-829.4302,-877.8094;Inherit;False;Property;_Color_Peak;Color_Peak;5;0;Create;True;0;0;False;0;0.5019608,0.8726192,0.945098,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;96;191.3933,166.0931;Inherit;False;Property;_Fade_Depth;Fade_Depth;18;0;Create;True;0;0;False;0;100;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;59;-1050.127,229.2006;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;76;-404.4589,925.4524;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;82;-646.8993,-237.1577;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;94;195.3367,41.91102;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;95;355.1915,47.30268;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;67;125.3937,-615.8102;Inherit;False;415.7444;189.6264;;2;66;65;Emission;1,1,1,1;0;0
Node;AmplifyShaderEditor.PowerNode;75;-281.451,925.0896;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;90;-327.5458,-723.4943;Inherit;False;202;185;;1;88;Final Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;38;-573.4204,-694.0587;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-396.6999,-263.9842;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;68;-395.8146,751.7225;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalVertexDataNode;12;-651.7261,253.7271;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;21;-666.1866,411.354;Inherit;False;Property;_Noise_Height;Noise_Height;3;0;Create;True;0;0;False;0;30;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;175.3936,-542.1838;Inherit;False;Property;_Emission_Strength;Emission_Strength;12;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;97;494.441,50.71785;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;80;-159.8872,442.6228;Inherit;False;202;185;;1;74;Position;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-426.3612,305.0366;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;-129.8185,838.485;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-277.5458,-673.4942;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;379.1382,-565.8102;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;99;472.6183,161.452;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;74;-109.8872,492.6227;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;20;763.6573,5.874287;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;QQA/Shader_Cloud;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
WireConnection;54;0;4;0
WireConnection;54;2;55;0
WireConnection;54;1;10;0
WireConnection;33;0;46;0
WireConnection;33;1;35;1
WireConnection;33;2;35;2
WireConnection;33;3;35;3
WireConnection;33;4;35;4
WireConnection;52;0;54;0
WireConnection;52;1;53;0
WireConnection;34;0;33;0
WireConnection;44;0;34;0
WireConnection;44;1;45;1
WireConnection;44;2;45;2
WireConnection;57;0;52;0
WireConnection;57;1;58;0
WireConnection;60;1;58;0
WireConnection;93;0;92;4
WireConnection;56;0;44;0
WireConnection;56;1;57;0
WireConnection;70;0;78;0
WireConnection;70;1;72;0
WireConnection;59;0;56;0
WireConnection;59;1;60;0
WireConnection;76;0;70;0
WireConnection;76;1;77;0
WireConnection;82;1;84;0
WireConnection;82;2;85;0
WireConnection;82;3;86;0
WireConnection;94;0;91;0
WireConnection;94;1;93;0
WireConnection;95;0;94;0
WireConnection;95;1;96;0
WireConnection;75;0;76;0
WireConnection;38;0;40;0
WireConnection;38;1;39;0
WireConnection;38;2;59;0
WireConnection;81;0;59;0
WireConnection;81;1;82;0
WireConnection;81;2;87;0
WireConnection;97;0;95;0
WireConnection;13;0;59;0
WireConnection;13;1;12;0
WireConnection;13;2;21;0
WireConnection;73;0;68;0
WireConnection;73;1;75;0
WireConnection;88;0;38;0
WireConnection;88;1;81;0
WireConnection;65;0;88;0
WireConnection;65;1;66;0
WireConnection;99;0;97;0
WireConnection;74;0;13;0
WireConnection;74;1;73;0
WireConnection;20;0;88;0
WireConnection;20;2;65;0
WireConnection;20;9;99;0
WireConnection;20;11;74;0
ASEEND*/
//CHKSM=7FC40F53E5C3B741B8AA7AAB9A769AC623947E50