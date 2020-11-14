// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QQA/Shader_Portal"
{
	Properties
	{
		_Polar_Radial_Length("Polar_Radial_Length", Vector) = (1,0,0,0)
		_Polar_Panner_Speed_X_Y("Polar_Panner_Speed_X_Y", Vector) = (1,0,0,0)
		_Polar_Noise_Scale("Polar_Noise_Scale", Float) = 1
		_Polar_Noise_Power("Polar_Noise_Power", Float) = 1
		_Polar_Noise_Multiplier("Polar_Noise_Multiplier", Float) = 1
		[HDR]_Emission_Ring_Color("Emission_Ring_Color", Color) = (2,0.2886244,0,1)
		[HDR]_Emission_Color("Emission_Color", Color) = (2,0.6588235,0,1)
		_Twirl_Strength("Twirl_Strength", Float) = 1
		_Twirl_Panner_Speed_X_Y("Twirl_Panner_Speed_X_Y", Vector) = (0,0,0,0)
		_Twirl_Noise_Scale("Twirl_Noise_Scale", Float) = 1
		_Twirl_Noise_Power("Twirl_Noise_Power", Float) = 1
		_Twirl_Noise_Multiplier("Twirl_Noise_Multiplier", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Overlap_Sub("Overlap_Sub", Range( -1 , 1)) = 0.5
		_Overlap_Add("Overlap_Add", Range( -1 , 1)) = 0.5
		_Overlap_Power("Overlap_Power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Overlap_Add;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float4 _Emission_Color;
		uniform float2 _Twirl_Panner_Speed_X_Y;
		uniform float _Twirl_Strength;
		uniform float _Twirl_Noise_Scale;
		uniform float _Twirl_Noise_Power;
		uniform float _Twirl_Noise_Multiplier;
		SamplerState sampler_TextureSample0;
		uniform float _Overlap_Sub;
		uniform float2 _Polar_Panner_Speed_X_Y;
		uniform float2 _Polar_Radial_Length;
		uniform float _Polar_Noise_Scale;
		uniform float _Polar_Noise_Power;
		uniform float _Polar_Noise_Multiplier;
		uniform float4 _Emission_Ring_Color;
		uniform float _Overlap_Power;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
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


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor38 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_grabScreenPosNorm.xy);
			float2 center45_g2 = float2( 0.5,0.5 );
			float2 delta6_g2 = ( i.uv_texcoord - center45_g2 );
			float angle10_g2 = ( length( delta6_g2 ) * _Twirl_Strength );
			float x23_g2 = ( ( cos( angle10_g2 ) * delta6_g2.x ) - ( sin( angle10_g2 ) * delta6_g2.y ) );
			float2 break40_g2 = center45_g2;
			float2 break41_g2 = float2( 0,0 );
			float y35_g2 = ( ( sin( angle10_g2 ) * delta6_g2.x ) + ( cos( angle10_g2 ) * delta6_g2.y ) );
			float2 appendResult44_g2 = (float2(( x23_g2 + break40_g2.x + break41_g2.x ) , ( break40_g2.y + break41_g2.y + y35_g2 )));
			float2 panner29 = ( 1.0 * _Time.y * _Twirl_Panner_Speed_X_Y + appendResult44_g2);
			float simplePerlin2D28 = snoise( panner29*_Twirl_Noise_Scale );
			simplePerlin2D28 = simplePerlin2D28*0.5 + 0.5;
			float2 CenteredUV15_g1 = ( i.uv_texcoord - float2( 0.5,0.5 ) );
			float2 break17_g1 = CenteredUV15_g1;
			float2 appendResult23_g1 = (float2(( length( CenteredUV15_g1 ) * _Polar_Radial_Length.x * 2.0 ) , ( atan2( break17_g1.x , break17_g1.y ) * ( 1.0 / 6.28318548202515 ) * _Polar_Radial_Length.y )));
			float2 panner6 = ( 1.0 * _Time.y * _Polar_Panner_Speed_X_Y + appendResult23_g1);
			float simplePerlin2D2 = snoise( panner6*_Polar_Noise_Scale );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			o.Emission = ( ( saturate( ( ( 1.0 - tex2D( _TextureSample0, uv_TextureSample0 ).r ) + _Overlap_Add ) ) * screenColor38 ) + ( _Emission_Color * ( saturate( ( pow( simplePerlin2D28 , _Twirl_Noise_Power ) * _Twirl_Noise_Multiplier ) ) * saturate( ( ( 1.0 - tex2D( _TextureSample0, uv_TextureSample0 ).r ) + _Overlap_Sub ) ) * saturate( ( pow( simplePerlin2D2 , _Polar_Noise_Power ) * _Polar_Noise_Multiplier ) ) ) ) + ( _Emission_Ring_Color * pow( ( 1.0 - tex2D( _TextureSample0, uv_TextureSample0 ).r ) , _Overlap_Power ) ) ).rgb;
			o.Alpha = saturate( round( ( tex2D( _TextureSample0, uv_TextureSample0 ).r + 0.5 ) ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18600
605;73;795;522;4108.708;1772.506;6.472541;False;False
Node;AmplifyShaderEditor.CommentaryNode;34;-2526.596,221.0416;Inherit;False;1482.576;438.7016;;11;15;20;18;19;6;2;22;21;25;24;26;Polar;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;48;-2326.351,-1141.419;Inherit;False;1335.421;385.6935;;11;27;1;30;29;32;28;43;44;46;47;45;Twirl;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;15;-2476.596,290.3245;Inherit;False;Property;_Polar_Radial_Length;Polar_Radial_Length;0;0;Create;True;0;0;False;0;False;1,0;0.1,0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;27;-2276.35,-1048.83;Inherit;False;Property;_Twirl_Strength;Twirl_Strength;7;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;20;-2197.225,495.743;Inherit;False;Property;_Polar_Panner_Speed_X_Y;Polar_Panner_Speed_X_Y;1;0;Create;True;0;0;False;0;False;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;1;-2078.838,-1091.419;Inherit;False;Twirl;-1;;2;90936742ac32db8449cd21ab6dd337c8;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;30;-2122.22,-919.7256;Inherit;False;Property;_Twirl_Panner_Speed_X_Y;Twirl_Panner_Speed_X_Y;8;0;Create;True;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;18;-2245.876,271.0414;Inherit;True;Polar Coordinates;-1;;1;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;6;-1937.56,300.5412;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;85;-1996.795,-152.9989;Inherit;False;916.7123;280;;5;49;53;50;52;54;Overlap_InnerRing_Sub;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;29;-1855.755,-1031.059;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1851.559,-896.8611;Inherit;False;Property;_Twirl_Noise_Scale;Twirl_Noise_Scale;9;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-1916.57,435.9481;Inherit;False;Property;_Polar_Noise_Scale;Polar_Noise_Scale;2;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1650.993,-897.0493;Inherit;False;Property;_Twirl_Noise_Power;Twirl_Noise_Power;10;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-1703.113,434.1004;Inherit;False;Property;_Polar_Noise_Power;Polar_Noise_Power;3;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;49;-1946.795,-102.9989;Inherit;True;Property;_TextureSample0;Texture Sample 0;17;0;Create;True;0;0;False;0;False;-1;2ab42dd1be19f16448bd1feab4b61388;2ab42dd1be19f16448bd1feab4b61388;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-1730.942,301.4155;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;92;-895.1971,-837.7977;Inherit;False;916.7123;280;;5;97;96;94;93;98;Overlap_InnerRing_Add;1,1,1,1;0;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;28;-1659.901,-1021.659;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;50;-1592.264,-88.27774;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-1654.49,6.958363;Inherit;False;Property;_Overlap_Sub;Overlap_Sub;19;0;Create;True;0;0;False;0;False;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;21;-1538.301,305.4357;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1485.561,433.6148;Inherit;False;Property;_Polar_Noise_Multiplier;Polar_Noise_Multiplier;4;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;86;-1005.538,369.9192;Inherit;False;732.9142;280;;4;61;82;60;83;Overlap_OuterRing_Add;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1444.568,-895.8823;Inherit;False;Property;_Twirl_Noise_Multiplier;Twirl_Noise_Multiplier;11;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;43;-1457.294,-1019.519;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;93;-845.1971,-787.7974;Inherit;True;Property;_TextureSample3;Texture Sample 3;17;0;Create;True;0;0;False;0;False;-1;2ab42dd1be19f16448bd1feab4b61388;2ab42dd1be19f16448bd1feab4b61388;True;0;False;white;Auto;False;Instance;49;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;-552.8918,-677.8401;Inherit;False;Property;_Overlap_Add;Overlap_Add;20;0;Create;True;0;0;False;0;False;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-1378.715,-90.09951;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;83;-955.538,419.9191;Inherit;True;Property;_TextureSample1;Texture Sample 1;17;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;49;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1351.866,307.5223;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;98;-486.7963,-760.1611;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1290.753,-1017.918;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;87;-1024.052,771.1531;Inherit;False;802.7476;280;Comment;4;56;57;58;84;Overlap_CircleAlpha;1,1,1,1;0;0
Node;AmplifyShaderEditor.GrabScreenPosition;40;-794.8046,-412.7349;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;82;-636.1367,438.2686;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;47;-1155.929,-1016.221;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;96;-277.1167,-774.898;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;54;-1245.083,-87.68631;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;84;-974.0517,821.1532;Inherit;True;Property;_TextureSample2;Texture Sample 2;17;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Instance;49;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;61;-647.4006,527.095;Inherit;False;Property;_Overlap_Power;Overlap_Power;21;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;26;-1209.019,307.483;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;60;-449.6238,446.6363;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-657.6975,850.4307;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;38;-544.7958,-411.5222;Inherit;False;Global;_GrabScreen0;Grab Screen 0;8;0;Create;True;0;0;False;0;False;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-982.6103,15.18838;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;89;-814.9952,87.43272;Inherit;False;Property;_Emission_Ring_Color;Emission_Ring_Color;5;1;[HDR];Create;True;0;0;False;0;False;2,0.2886244,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;97;-143.4848,-772.4849;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;41;-812.5769,-122.7079;Inherit;False;Property;_Emission_Color;Emission_Color;6;1;[HDR];Create;True;0;0;False;0;False;2,0.6588235,0,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-341.3658,-419.5708;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;67;-2331.37,-660.0839;Inherit;False;1335.421;385.6935;;11;78;77;76;75;74;73;72;71;70;69;68;Twirl2;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-570.9978,160.5849;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-574.1622,-17.10709;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RoundOpNode;57;-525.9725,850.4307;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1295.772,-536.582;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-1856.578,-415.525;Inherit;False;Property;_Twirl2_Noise_Scale;Twirl2_Noise_Scale;14;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;75;-1664.92,-540.324;Inherit;False;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;58;-386.3047,849.3597;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;77;-1462.313,-538.183;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;76;-1449.587,-414.547;Inherit;False;Property;_Twirl2_Noise_Multiplier;Twirl2_Noise_Multiplier;16;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;68;-1160.948,-534.886;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;70;-2127.24,-438.39;Inherit;False;Property;_Twirl2_Panner_Speed_X_Y;Twirl2_Panner_Speed_X_Y;13;0;Create;True;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;90;-402.6909,100.3755;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-1656.012,-415.714;Inherit;False;Property;_Twirl2_Noise_Power;Twirl2_Noise_Power;15;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-2281.369,-567.4949;Inherit;False;Property;_Twirl2_Strength;Twirl2_Strength;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;71;-2083.857,-610.0839;Inherit;False;Twirl;-1;;3;90936742ac32db8449cd21ab6dd337c8;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;4;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;73;-1860.774,-549.723;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;QQA/Shader_Portal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;3;27;0
WireConnection;18;3;15;1
WireConnection;18;4;15;2
WireConnection;6;0;18;0
WireConnection;6;2;20;0
WireConnection;29;0;1;0
WireConnection;29;2;30;0
WireConnection;2;0;6;0
WireConnection;2;1;19;0
WireConnection;28;0;29;0
WireConnection;28;1;32;0
WireConnection;50;0;49;1
WireConnection;21;0;2;0
WireConnection;21;1;22;0
WireConnection;43;0;28;0
WireConnection;43;1;44;0
WireConnection;52;0;50;0
WireConnection;52;1;53;0
WireConnection;24;0;21;0
WireConnection;24;1;25;0
WireConnection;98;0;93;1
WireConnection;45;0;43;0
WireConnection;45;1;46;0
WireConnection;82;0;83;1
WireConnection;47;0;45;0
WireConnection;96;0;98;0
WireConnection;96;1;94;0
WireConnection;54;0;52;0
WireConnection;26;0;24;0
WireConnection;60;0;82;0
WireConnection;60;1;61;0
WireConnection;56;0;84;1
WireConnection;38;0;40;0
WireConnection;31;0;47;0
WireConnection;31;1;54;0
WireConnection;31;2;26;0
WireConnection;97;0;96;0
WireConnection;99;0;97;0
WireConnection;99;1;38;0
WireConnection;88;0;89;0
WireConnection;88;1;60;0
WireConnection;42;0;41;0
WireConnection;42;1;31;0
WireConnection;57;0;56;0
WireConnection;78;0;77;0
WireConnection;78;1;76;0
WireConnection;75;0;73;0
WireConnection;75;1;72;0
WireConnection;58;0;57;0
WireConnection;77;0;75;0
WireConnection;77;1;74;0
WireConnection;68;0;78;0
WireConnection;90;0;99;0
WireConnection;90;1;42;0
WireConnection;90;2;88;0
WireConnection;71;3;69;0
WireConnection;73;0;71;0
WireConnection;73;2;70;0
WireConnection;0;2;90;0
WireConnection;0;9;58;0
ASEEND*/
//CHKSM=8B08E2CA52147AEFB3A634EEAB32528165C8C387