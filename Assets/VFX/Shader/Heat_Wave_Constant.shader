// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Heat_wave_dissolves"
{
	Properties
	{
		_Mask("Mask", 2D) = "white" {}
		_DistorsionMap("DistorsionMap", 2D) = "white" {}
		_DistortionAmount("DistortionAmount", Range( 0 , 1)) = 1
		_ScrollSpeed("ScrollSpeed", Range( 0 , 1)) = 0
		_Warn("Warn", Color) = (0.8588235,0.3958808,0,0)
		_Hot("Hot", Color) = (1,0.8696957,0,0)
		_Saturation("Saturation", Range( 0 , 2)) = 2
		_Burn("Burn", Range( 0 , 1.6)) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_AmbientOcclusion("Ambient Occlusion", 2D) = "white" {}
		_Heatwave("Heat wave", Range( 0 , 1)) = 0.3056416
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		AlphaToMask On
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _Warn;
		uniform float4 _Hot;
		uniform sampler2D _Mask;
		uniform sampler2D _DistorsionMap;
		uniform float4 _DistorsionMap_ST;
		uniform float _DistortionAmount;
		uniform float _ScrollSpeed;
		uniform float _Saturation;
		uniform float _Heatwave;
		uniform float _Burn;
		uniform sampler2D _AmbientOcclusion;
		uniform float4 _AmbientOcclusion_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = tex2D( _Normal, uv_Normal ).rgb;
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = tex2D( _Albedo, uv_Albedo ).rgb;
			float2 uv_DistorsionMap = i.uv_texcoord * _DistorsionMap_ST.xy + _DistorsionMap_ST.zw;
			float2 panner29 = ( ( _Time.y * _ScrollSpeed ) * float2( 1,1 ) + float2( 0,0 ));
			float2 uv_TexCoord26 = i.uv_texcoord + panner29;
			float4 lerpResult36 = lerp( _Warn , _Hot , tex2D( _Mask, ( ( (UnpackNormal( tex2D( _DistorsionMap, uv_DistorsionMap ) )).xyz * _DistortionAmount ) + float3( uv_TexCoord26 ,  0.0 ) ).xy ).r);
			float4 temp_cast_4 = (_Saturation).xxxx;
			float2 panner59 = ( 1.0 * _Time.y * float2( 0,1 ) + i.uv_texcoord);
			float2 uv_TexCoord65 = i.uv_texcoord + float2( 1,0 );
			float4 tex2DNode44 = tex2D( _Mask, ( ( (UnpackNormal( tex2D( _DistorsionMap, panner59 ) )).xyz * _Heatwave ) + float3( uv_TexCoord65 ,  0.0 ) ).xy );
			float temp_output_45_0 = step( tex2DNode44.r , _Burn );
			o.Emission = ( ( pow( lerpResult36 , temp_cast_4 ) * _Saturation ) * ( temp_output_45_0 + ( temp_output_45_0 - step( tex2DNode44.r , ( _Burn / 2.0 ) ) ) ) ).rgb;
			float2 uv_AmbientOcclusion = i.uv_texcoord * _AmbientOcclusion_ST.xy + _AmbientOcclusion_ST.zw;
			o.Occlusion = tex2D( _AmbientOcclusion, uv_AmbientOcclusion ).r;
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
			AlphaToMask Off
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
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
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
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
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
Version=18900
925;7;920;1012;-236.0472;999.3568;1.740219;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;58;-2250.874,903.8184;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;22;-2168.942,-15.60514;Inherit;True;Property;_DistorsionMap;DistorsionMap;2;0;Create;True;0;0;0;False;0;False;ef4bcd0e1f0aedd4d80a248a5c2879e7;ef4bcd0e1f0aedd4d80a248a5c2879e7;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleTimeNode;33;-2129.879,335.9719;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-2174.246,608.0464;Inherit;False;Property;_ScrollSpeed;ScrollSpeed;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;59;-1940.069,974.6263;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;20;-1628.588,-27.91916;Inherit;True;Property;;;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1851.244,481.0355;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;60;-1606.757,978.3849;Inherit;True;Property;_TextureSample2;Texture Sample 2;10;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;62;-1265.066,1312.013;Inherit;True;Property;_Heatwave;Heat wave;12;0;Create;True;0;0;0;False;0;False;0.3056416;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;61;-1235.776,992.6581;Inherit;True;True;True;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-1410.978,347.216;Inherit;False;Property;_DistortionAmount;DistortionAmount;3;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;29;-1538.286,495.0443;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;1,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;23;-1291.789,156.3313;Inherit;False;True;True;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1038.678,317.4414;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;65;-921.3859,1313.403;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;1,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-927.9434,1013.926;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-1072.502,662.4503;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-176.2483,853.0198;Inherit;False;Property;_Burn;Burn;8;0;Create;True;0;0;0;False;0;False;1;0;0;1.6;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;66;-640.5768,1136.188;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-203.0415,945.5748;Inherit;False;Constant;_DivedeAmount;DivedeAmount;10;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;17;-759.883,248.9823;Inherit;True;Property;_Mask;Mask;0;0;Create;True;0;0;0;False;0;False;5d66be8e14e4d904383185a5f5b9d88b;5d66be8e14e4d904383185a5f5b9d88b;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-761.6395,527.3735;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;35;-650.3973,0.958941;Inherit;False;Property;_Hot;Hot;6;0;Create;True;0;0;0;False;0;False;1,0.8696957,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-504.8403,271.6452;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;34;-658.7828,-203.6471;Inherit;False;Property;_Warn;Warn;5;0;Create;True;0;0;0;False;0;False;0.8588235,0.3958808,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;44;-208.0161,650.7762;Inherit;True;Property;_TextureSample1;Texture Sample 1;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;50;180.0448,797.434;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;51;418.3171,592.6662;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;36;-222.7913,-153.9404;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;45;291.6561,323.076;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-207.5348,67.99529;Inherit;False;Property;_Saturation;Saturation;7;0;Create;True;0;0;0;False;0;False;2;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;38;48.04163,-156.4361;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;53;677.7368,499.4691;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;594.9689,225.206;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;294.3237,-157.1749;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;67;1013.925,-388.5398;Inherit;True;Property;_Normal;Normal;10;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;1080.479,135.0354;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;49;1039.524,-619.9713;Inherit;True;Property;_Albedo;Albedo;9;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;68;1017.405,-169.2724;Inherit;True;Property;_AmbientOcclusion;Ambient Occlusion;11;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;1;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1495.494,238.2545;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Heat_wave_dissolves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;59;0;58;0
WireConnection;20;0;22;0
WireConnection;31;0;33;0
WireConnection;31;1;32;0
WireConnection;60;0;22;0
WireConnection;60;1;59;0
WireConnection;61;0;60;0
WireConnection;29;1;31;0
WireConnection;23;0;20;0
WireConnection;24;0;23;0
WireConnection;24;1;25;0
WireConnection;63;0;61;0
WireConnection;63;1;62;0
WireConnection;26;1;29;0
WireConnection;66;0;63;0
WireConnection;66;1;65;0
WireConnection;28;0;24;0
WireConnection;28;1;26;0
WireConnection;18;0;17;0
WireConnection;18;1;28;0
WireConnection;44;0;17;0
WireConnection;44;1;66;0
WireConnection;50;0;46;0
WireConnection;50;1;52;0
WireConnection;51;0;44;1
WireConnection;51;1;50;0
WireConnection;36;0;34;0
WireConnection;36;1;35;0
WireConnection;36;2;18;1
WireConnection;45;0;44;1
WireConnection;45;1;46;0
WireConnection;38;0;36;0
WireConnection;38;1;41;0
WireConnection;53;0;45;0
WireConnection;53;1;51;0
WireConnection;54;0;45;0
WireConnection;54;1;53;0
WireConnection;39;0;38;0
WireConnection;39;1;41;0
WireConnection;47;0;39;0
WireConnection;47;1;54;0
WireConnection;0;0;49;0
WireConnection;0;1;67;0
WireConnection;0;2;47;0
WireConnection;0;5;68;0
ASEEND*/
//CHKSM=79DF44DDCE138A2FE0AB32E7B6BD87E06E7F6CAD