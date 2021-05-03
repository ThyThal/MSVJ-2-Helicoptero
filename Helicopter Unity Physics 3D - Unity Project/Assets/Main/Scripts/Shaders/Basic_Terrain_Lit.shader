// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Indie-Pixel/Basic/Terrain_Lit"
{
	Properties
	{
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Roughness("Roughness", Range( 0 , 1)) = 0.5
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Albedo("Albedo", 2D) = "white" {}
		_AO("AO", 2D) = "white" {}
		_DirtB("Dirt B", 2D) = "white" {}
		_DirtA("Dirt A", 2D) = "white" {}
		_DirtANormal("Dirt A Normal", 2D) = "bump" {}
		_Masks("Masks", 2D) = "white" {}
		_Grass("Grass", 2D) = "white" {}
		_RockNormal("Rock Normal", 2D) = "bump" {}
		_RockAlbedo("Rock Albedo", 2D) = "white" {}
		_RockTiling("Rock Tiling", Range( 0 , 10)) = 2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _DirtANormal;
		uniform float4 _DirtANormal_ST;
		uniform sampler2D _RockNormal;
		uniform float _RockTiling;
		uniform sampler2D _Masks;
		uniform float4 _Masks_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Grass;
		uniform float4 _Grass_ST;
		uniform sampler2D _DirtA;
		uniform float4 _DirtA_ST;
		uniform sampler2D _DirtB;
		uniform float4 _DirtB_ST;
		uniform sampler2D _RockAlbedo;
		uniform float _Metallic;
		uniform float _Roughness;
		uniform sampler2D _AO;
		uniform float4 _AO_ST;


		inline float4 TriplanarSamplingSF( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float tilling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= projNormal.x + projNormal.y + projNormal.z;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = ( tex2D( topTexMap, tilling * worldPos.zy * float2( nsign.x, 1.0 ) ) );
			yNorm = ( tex2D( topTexMap, tilling * worldPos.xz * float2( nsign.y, 1.0 ) ) );
			zNorm = ( tex2D( topTexMap, tilling * worldPos.xy * float2( -nsign.z, 1.0 ) ) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float2 uv_DirtANormal = i.uv_texcoord * _DirtANormal_ST.xy + _DirtANormal_ST.zw;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float4 triplanar23 = TriplanarSamplingSF( _RockNormal, ase_worldPos, ase_worldNormal, 1.0, _RockTiling, 1.0, 0 );
			float2 uv_Masks = i.uv_texcoord * _Masks_ST.xy + _Masks_ST.zw;
			float4 tex2DNode15 = tex2D( _Masks, uv_Masks );
			float3 lerpResult28 = lerp( UnpackNormal( tex2D( _DirtANormal, uv_DirtANormal ) ) , UnpackScaleNormal( triplanar23, 0.5 ) , tex2DNode15.b);
			float3 normal13 = BlendNormals( UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) ) , lerpResult28 );
			o.Normal = normal13;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv_Grass = i.uv_texcoord * _Grass_ST.xy + _Grass_ST.zw;
			float2 uv_DirtA = i.uv_texcoord * _DirtA_ST.xy + _DirtA_ST.zw;
			float2 uv_DirtB = i.uv_texcoord * _DirtB_ST.xy + _DirtB_ST.zw;
			float4 lerpResult30 = lerp( tex2D( _DirtA, uv_DirtA ) , tex2D( _DirtB, uv_DirtB ) , tex2DNode15.g);
			float4 lerpResult16 = lerp( tex2D( _Grass, uv_Grass ) , lerpResult30 , tex2DNode15.r);
			float4 triplanar20 = TriplanarSamplingSF( _RockAlbedo, ase_worldPos, ase_worldNormal, 1.0, _RockTiling, 1.0, 0 );
			float4 lerpResult19 = lerp( lerpResult16 , triplanar20 , tex2DNode15.b);
			half4 color11 = ( tex2D( _Albedo, uv_Albedo ) * lerpResult19 );
			o.Albedo = color11.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Roughness;
			float2 uv_AO = i.uv_texcoord * _AO_ST.xy + _AO_ST.zw;
			o.Occlusion = tex2D( _AO, uv_AO ).r;
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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
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
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
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
Version=15500
60;133;1906;1044;4498.013;2633.723;3.406307;True;True
Node;AmplifyShaderEditor.SamplerNode;7;-2501.921,-2044.342;Float;True;Property;_DirtA;Dirt A;6;0;Create;True;0;0;False;0;None;f6605a6d90c197c4d99424a12dffb35c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;-3332.444,-854.9565;Float;False;Property;_RockTiling;Rock Tiling;12;0;Create;True;0;0;False;0;2;0.09;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-2330.438,-963.2187;Float;True;Property;_Masks;Masks;8;0;Create;True;0;0;False;0;None;5cab31679ea72984aaa4aa3ddaadaeda;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-2508.04,-1814.706;Float;True;Property;_DirtB;Dirt B;5;0;Create;True;0;0;False;0;None;f6605a6d90c197c4d99424a12dffb35c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;24;-3161.371,-437.6985;Float;True;Property;_RockNormal;Rock Normal;10;0;Create;True;0;0;False;0;None;0bebe40e9ebbecc48b8e9cfea982da7e;True;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TriplanarNode;23;-2766.685,-439.4146;Float;True;Spherical;World;False;Top Texture 1;_TopTexture1;white;1;None;Mid Texture 1;_MidTexture1;white;-1;None;Bot Texture 1;_BotTexture1;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;30;-2064.039,-1865.706;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-2736.332,-78.2196;Float;False;Constant;_Float0;Float 0;12;0;Create;True;0;0;False;0;0.5;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-2013.802,-2248.28;Float;True;Property;_Grass;Grass;9;0;Create;True;0;0;False;0;None;42a91a090222e8948a229c10e2b60a81;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;21;-3105.75,-1680.987;Float;True;Property;_RockAlbedo;Rock Albedo;11;0;Create;True;0;0;False;0;None;b297077dae62c1944ba14cad801cddf5;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TriplanarNode;20;-2552.263,-1485.222;Float;True;Spherical;World;False;Top Texture 0;_TopTexture0;white;0;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;False;9;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;8;FLOAT;1;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.UnpackScaleNormalNode;26;-2277.524,-271.2392;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;10;-2357.161,-2.01543;Float;True;Property;_DirtANormal;Dirt A Normal;7;0;Create;True;0;0;False;0;None;ad2dd91cdc11f8f43a926c7820f96c8a;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;16;-1735.899,-1786.537;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;5;-1533.734,-2083.039;Float;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;False;0;None;59c1d40f064691f41bda5dd4ec7694fc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;28;-1855.69,-102.7024;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;4;-1683.685,-346.1039;Float;True;Property;_NormalMap;Normal Map;2;0;Create;True;0;0;False;0;None;58a1173eac464e74ca4d08225869bf9e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;19;-1505.581,-1716.932;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.BlendNormalsNode;9;-1300.716,-128.701;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1237.528,-1786.189;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-177.7985,-429.0166;Float;False;Property;_Roughness;Roughness;1;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;100.9249,-1244.533;Float;False;13;0;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;2;-164.1076,-546.8621;Float;False;Property;_Metallic;Metallic;0;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;12;122.1269,-1467.99;Float;False;11;0;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-1048.758,-120.3655;Float;False;normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-1050.358,-1802.079;Half;False;color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;6;-205.4555,-821.2048;Float;True;Property;_AO;AO;4;0;Create;True;0;0;False;0;None;ea24bbc3f9034cc43a514eafeb7ba56e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;375.9706,-1271.066;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Indie-Pixel/Basic/Terrain_Lit;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;24;0
WireConnection;23;3;22;0
WireConnection;30;0;7;0
WireConnection;30;1;29;0
WireConnection;30;2;15;2
WireConnection;20;0;21;0
WireConnection;20;3;22;0
WireConnection;26;0;23;0
WireConnection;26;1;27;0
WireConnection;16;0;17;0
WireConnection;16;1;30;0
WireConnection;16;2;15;1
WireConnection;28;0;10;0
WireConnection;28;1;26;0
WireConnection;28;2;15;3
WireConnection;19;0;16;0
WireConnection;19;1;20;0
WireConnection;19;2;15;3
WireConnection;9;0;4;0
WireConnection;9;1;28;0
WireConnection;8;0;5;0
WireConnection;8;1;19;0
WireConnection;13;0;9;0
WireConnection;11;0;8;0
WireConnection;0;0;12;0
WireConnection;0;1;14;0
WireConnection;0;3;2;0
WireConnection;0;4;3;0
WireConnection;0;5;6;0
ASEEND*/
//CHKSM=BBBFA402D43E795D39AE4B0E58765BE12004111A