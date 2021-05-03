// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Indie-Pixel/Water/Basic_Water"
{
	Properties
	{
		_SmallWavesNormal("Small Waves Normal", 2D) = "bump" {}
		_SmallWaveSpeed("Small Wave Speed", Range( 0 , 10)) = 0.02
		_DeepColor("Deep Color", Color) = (0.1350124,0.536874,0.6981132,1)
		_Roughness("Roughness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_NormalScale("Normal Scale", Range( 0 , 1)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalScale;
		uniform float _SmallWaveSpeed;
		uniform sampler2D _SmallWavesNormal;
		uniform float4 _SmallWavesNormal_ST;
		uniform float4 _DeepColor;
		uniform float _Metallic;
		uniform float _Roughness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float temp_output_15_0 = ( _Time.y * _SmallWaveSpeed );
			float2 uv_SmallWavesNormal = i.uv_texcoord * _SmallWavesNormal_ST.xy + _SmallWavesNormal_ST.zw;
			float2 panner4 = ( temp_output_15_0 * float2( 0.4,0.3 ) + uv_SmallWavesNormal);
			float cos21 = cos( ( temp_output_15_0 * 0.001 ) );
			float sin21 = sin( ( temp_output_15_0 * 0.001 ) );
			float2 rotator21 = mul( uv_SmallWavesNormal - float2( 0.5,0.5 ) , float2x2( cos21 , -sin21 , sin21 , cos21 )) + float2( 0.5,0.5 );
			float2 panner6 = ( temp_output_15_0 * float2( -0.2,0.3 ) + rotator21);
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _SmallWavesNormal, panner4 ), _NormalScale ) , UnpackScaleNormal( tex2D( _SmallWavesNormal, panner6 ), _NormalScale ) );
			o.Albedo = _DeepColor.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15500
7;65;1906;986;2918.883;855.3884;1.691418;True;True
Node;AmplifyShaderEditor.CommentaryNode;18;-2288,-608;Float;False;1411.363;664;Small Wave Normals;13;14;8;15;7;4;6;2;1;3;21;23;22;24;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2263,-209;Float;False;Property;_SmallWaveSpeed;Small Wave Speed;1;0;Create;True;0;0;False;0;0.02;0.2;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;14;-2183,-305;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-2194.06,-38.9205;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;0.001;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1959,-241;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1934.06,-50.92053;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-2171,-533;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;21;-1749.559,-92.92621;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;4;-1744,-560;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.4,0.3;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1856.15,-412.6541;Float;False;Property;_NormalScale;Normal Scale;5;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;6;-1746,-292;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.2,0.3;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1504,-496;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;dd2fd2df93418444c8e280f1d34deeb5;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Instance;2;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1489,-175;Float;True;Property;_SmallWavesNormal;Small Waves Normal;0;0;Create;True;0;0;False;0;dd2fd2df93418444c8e280f1d34deeb5;dd2fd2df93418444c8e280f1d34deeb5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendNormalsNode;3;-1122,-310;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WireNode;19;-545.3871,-242.5988;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;12;-260.6428,-362.5073;Float;False;Property;_DeepColor;Deep Color;2;0;Create;True;0;0;False;0;0.1350124,0.536874,0.6981132,1;0.1217515,0.3585646,0.4528301,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-300.8737,87.68047;Float;False;Property;_Metallic;Metallic;4;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;20;-490.2354,-84.54639;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-294.4776,212.5246;Float;False;Property;_Roughness;Roughness;3;0;Create;True;0;0;False;0;0;0.93;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;41.706,-39.09937;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Indie-Pixel/Water/Basic_Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;14;0
WireConnection;15;1;8;0
WireConnection;22;0;15;0
WireConnection;22;1;23;0
WireConnection;21;0;7;0
WireConnection;21;2;22;0
WireConnection;4;0;7;0
WireConnection;4;1;15;0
WireConnection;6;0;21;0
WireConnection;6;1;15;0
WireConnection;1;1;4;0
WireConnection;1;5;24;0
WireConnection;2;1;6;0
WireConnection;2;5;24;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;19;0;3;0
WireConnection;20;0;19;0
WireConnection;0;0;12;0
WireConnection;0;1;20;0
WireConnection;0;3;16;0
WireConnection;0;4;17;0
ASEEND*/
//CHKSM=94B52417296FE712F5EA2E2617D0BDFFD50C0401