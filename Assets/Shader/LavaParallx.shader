// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "LavaParallx"
{
	Properties
	{
		_FlowMap("FlowMap", 2D) = "white" {}
		_Speed("Speed", Range( 0 , 1)) = 0.23
		_Diffuse("Diffuse", 2D) = "white" {}
		_Emissive("Emissive", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_Tilling("Tilling", Range( 1 , 1000)) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Strengh("Strengh", Range( 0 , 1)) = 0
		[HDR]_EmissiveColor("EmissiveColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _FlowMap;
		uniform float4 _FlowMap_ST;
		uniform float _Speed;
		uniform float _Strengh;
		uniform float _Tilling;
		uniform sampler2D _Diffuse;
		uniform sampler2D _Emissive;
		uniform float4 _EmissiveColor;
		uniform float _Metallic;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_FlowMap = i.uv_texcoord * _FlowMap_ST.xy + _FlowMap_ST.zw;
			float2 blendOpSrc16 = i.uv_texcoord;
			float2 blendOpDest16 = (tex2D( _FlowMap, uv_FlowMap )).rg;
			float2 temp_output_16_0 = ( saturate( (( blendOpDest16 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest16 ) * ( 1.0 - blendOpSrc16 ) ) : ( 2.0 * blendOpDest16 * blendOpSrc16 ) ) ));
			float temp_output_10_0 = ( _Time.y * _Speed );
			float temp_output_1_0_g3 = temp_output_10_0;
			float temp_output_13_0 = (0.0 + (( ( temp_output_1_0_g3 - floor( ( temp_output_1_0_g3 + 0.5 ) ) ) * 2 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
			float TimeA20 = ( -temp_output_13_0 * _Strengh );
			float2 lerpResult17 = lerp( i.uv_texcoord , temp_output_16_0 , TimeA20);
			float2 temp_cast_0 = (_Tilling).xx;
			float2 uv_TexCoord28 = i.uv_texcoord * temp_cast_0;
			float2 DiffuseTilling29 = uv_TexCoord28;
			float2 FlowA21 = ( lerpResult17 + DiffuseTilling29 );
			float temp_output_1_0_g2 = (temp_output_10_0*1.0 + 0.5);
			float TimeB70 = ( -(0.0 + (( ( temp_output_1_0_g2 - floor( ( temp_output_1_0_g2 + 0.5 ) ) ) * 2 ) - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) * _Strengh );
			float2 lerpResult58 = lerp( i.uv_texcoord , temp_output_16_0 , TimeB70);
			float2 FlowB62 = ( DiffuseTilling29 + lerpResult58 );
			float BleedTime83 = saturate( abs( ( 1.0 - ( temp_output_13_0 / 0.5 ) ) ) );
			float3 lerpResult91 = lerp( UnpackNormal( tex2D( _Normal, FlowA21 ) ) , UnpackNormal( tex2D( _Normal, FlowB62 ) ) , BleedTime83);
			float3 NormalsM92 = lerpResult91;
			o.Normal = NormalsM92;
			float4 lerpResult48 = lerp( tex2D( _Diffuse, FlowA21 ) , tex2D( _Diffuse, FlowB62 ) , BleedTime83);
			float4 Diffuse22 = lerpResult48;
			o.Albedo = Diffuse22.rgb;
			float4 lerpResult103 = lerp( ( tex2D( _Emissive, FlowA21 ) * _EmissiveColor ) , ( tex2D( _Emissive, FlowB62 ) * _EmissiveColor ) , BleedTime83);
			float4 Emissive104 = lerpResult103;
			o.Emission = Emissive104.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
807;19;920;1000;-1012.636;1653.458;1.315788;True;False
Node;AmplifyShaderEditor.CommentaryNode;30;-2862.433,-619.5272;Inherit;False;2356.168;939.8728;Comment;20;70;69;65;64;20;14;13;12;68;10;11;9;79;80;81;82;83;96;97;98;Time;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-2809.233,-300.6614;Inherit;True;Property;_Speed;Speed;1;0;Create;True;0;0;0;False;0;False;0.23;0.12;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;9;-2773.779,-560.4999;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-2490.101,-460.5637;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;68;-2204.233,-223.1586;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;64;-1942.167,-223.4215;Inherit;True;Sawtooth Wave;-1;;2;289adb816c3ac6d489f255fc3caf5016;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;12;-2201.194,-521.9612;Inherit;True;Sawtooth Wave;-1;;3;289adb816c3ac6d489f255fc3caf5016;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-1725.464,-225.8723;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;13;-1968.677,-539.1461;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;33;-1593.491,-2483.672;Inherit;False;881.5667;349;Comment;3;27;28;29;Diffuse Tilling;1,1,1,1;0;0
Node;AmplifyShaderEditor.NegateNode;69;-1448.454,-232.5218;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-1584.873,-345.7879;Inherit;False;Property;_Strengh;Strengh;8;0;Create;True;0;0;0;False;0;False;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;31;-2767.621,-2037.311;Inherit;False;2288.63;1262.559;Comment;13;21;34;17;18;16;3;15;2;58;59;60;61;62;FlowMap UV;1,1,1,1;0;0
Node;AmplifyShaderEditor.NegateNode;14;-1664.912,-564.0908;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-2725.469,-1721.064;Inherit;True;Property;_FlowMap;FlowMap;0;0;Create;True;0;0;0;False;0;False;-1;None;5b39914406c74104ead53eaa03f468ae;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-1543.491,-2430.918;Inherit;True;Property;_Tilling;Tilling;5;0;Create;True;0;0;0;False;0;False;1;1;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;-1266.822,-570.8304;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-1163.469,-255.7709;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-899.2396,-562.8592;Inherit;True;TimeA;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;3;-2398.031,-1484.881;Inherit;True;True;True;False;False;1;0;COLOR;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-1219.056,-2433.672;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-2379.309,-1824.293;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;70;-844.5629,-240.4478;Inherit;True;TimeB;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;60;-1989.821,-1288.29;Inherit;True;70;TimeB;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-954.924,-2417.238;Inherit;True;DiffuseTilling;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BlendOpsNode;16;-2106.838,-1533.477;Inherit;True;Overlay;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;18;-2022.267,-1989.285;Inherit;True;20;TimeA;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;59;-1431.517,-1722.878;Inherit;True;29;DiffuseTilling;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;58;-1710.259,-1535.148;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;17;-1727.536,-1825.673;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;79;-1744.149,40.05125;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1154.599,-1876.969;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-1146.211,-1444.537;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;80;-1512.35,47.21214;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;81;-1332.491,36.82762;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-876.428,-1883.879;Inherit;True;FlowA;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;62;-856.8479,-1443.965;Inherit;True;FlowB;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;99;-256.4402,-1462.384;Inherit;False;1734.596;821.3462;Comment;11;104;102;105;106;107;103;100;101;108;109;110;Emissive;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;105;-218.1256,-1194.601;Inherit;True;21;FlowA;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;106;-206.4402,-1412.384;Inherit;True;Property;_Emissive;Emissive;3;0;Create;True;0;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;100;-210.6027,-996.3057;Inherit;True;62;FlowB;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;32;-251.1426,-2370.222;Inherit;False;1384.782;793.1354;Comment;8;8;23;24;22;48;46;76;63;Diffuse;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;84;1251.019,-2373.564;Inherit;False;1443.738;775.4296;Comment;8;92;91;89;90;88;86;87;85;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;82;-1140.491,50.82762;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;102;121.6881,-952.4293;Inherit;True;Property;_TextureSample5;Texture Sample 5;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;110;153.2962,-1157.11;Inherit;False;Property;_EmissiveColor;EmissiveColor;9;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;107;113.3298,-1379.341;Inherit;True;Property;_TextureSample6;Texture Sample 6;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;86;1328.353,-2323.564;Inherit;True;Property;_Normal;Normal;4;0;Create;True;0;0;0;False;0;False;None;5b5062a1f7721224c87d657254c2cd70;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RegisterLocalVarNode;83;-938.83,40.881;Inherit;True;BleedTime;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;85;1324.191,-1907.486;Inherit;True;62;FlowB;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;87;1316.668,-2105.781;Inherit;True;21;FlowA;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;63;-205.3051,-1904.144;Inherit;True;62;FlowB;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;23;-201.1426,-2320.222;Inherit;True;Property;_Diffuse;Diffuse;2;0;Create;True;0;0;0;False;0;False;None;c0b0db8d91ee13e43a57181e25205ef5;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.GetLocalVarNode;24;-212.828,-2102.439;Inherit;True;21;FlowA;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;118.6274,-2287.179;Inherit;True;Property;_diff;diff;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;46;140.7662,-2042.352;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;76;182.9976,-1810.17;Inherit;True;83;BleedTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;101;585.2227,-896.5513;Inherit;True;83;BleedTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;90;1670.262,-2045.694;Inherit;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;88;1712.493,-1813.512;Inherit;True;83;BleedTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;89;1648.123,-2290.521;Inherit;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;548.8658,-1418.616;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;594.9534,-1174.788;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;91;2027.128,-2165.131;Inherit;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;48;497.6324,-2161.789;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;103;844.9699,-1270.544;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;104;1131.419,-1272.565;Inherit;True;Emissive;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;92;2313.577,-2293.769;Inherit;True;NormalsM;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;784.0816,-2290.426;Inherit;True;Diffuse;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;93;1747.792,-1199.721;Inherit;True;92;NormalsM;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;95;1713.266,-563.4888;Inherit;False;Property;_Metallic;Metallic;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;1714.548,-657.5637;Inherit;False;Property;_Smoothness;Smoothness;6;0;Create;True;0;0;0;False;0;False;0;0.6;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;25;1742.062,-1400.434;Inherit;True;22;Diffuse;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;111;1743.34,-1000.311;Inherit;True;104;Emissive;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2064.055,-1366.32;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;LavaParallx;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;68;0;10;0
WireConnection;64;1;68;0
WireConnection;12;1;10;0
WireConnection;65;0;64;0
WireConnection;13;0;12;0
WireConnection;69;0;65;0
WireConnection;14;0;13;0
WireConnection;96;0;14;0
WireConnection;96;1;98;0
WireConnection;97;0;69;0
WireConnection;97;1;98;0
WireConnection;20;0;96;0
WireConnection;3;0;2;0
WireConnection;28;0;27;0
WireConnection;70;0;97;0
WireConnection;29;0;28;0
WireConnection;16;0;15;0
WireConnection;16;1;3;0
WireConnection;58;0;15;0
WireConnection;58;1;16;0
WireConnection;58;2;60;0
WireConnection;17;0;15;0
WireConnection;17;1;16;0
WireConnection;17;2;18;0
WireConnection;79;0;13;0
WireConnection;34;0;17;0
WireConnection;34;1;59;0
WireConnection;61;0;59;0
WireConnection;61;1;58;0
WireConnection;80;0;79;0
WireConnection;81;0;80;0
WireConnection;21;0;34;0
WireConnection;62;0;61;0
WireConnection;82;0;81;0
WireConnection;102;0;106;0
WireConnection;102;1;100;0
WireConnection;107;0;106;0
WireConnection;107;1;105;0
WireConnection;83;0;82;0
WireConnection;8;0;23;0
WireConnection;8;1;24;0
WireConnection;46;0;23;0
WireConnection;46;1;63;0
WireConnection;90;0;86;0
WireConnection;90;1;85;0
WireConnection;89;0;86;0
WireConnection;89;1;87;0
WireConnection;108;0;107;0
WireConnection;108;1;110;0
WireConnection;109;0;102;0
WireConnection;109;1;110;0
WireConnection;91;0;89;0
WireConnection;91;1;90;0
WireConnection;91;2;88;0
WireConnection;48;0;8;0
WireConnection;48;1;46;0
WireConnection;48;2;76;0
WireConnection;103;0;108;0
WireConnection;103;1;109;0
WireConnection;103;2;101;0
WireConnection;104;0;103;0
WireConnection;92;0;91;0
WireConnection;22;0;48;0
WireConnection;0;0;25;0
WireConnection;0;1;93;0
WireConnection;0;2;111;0
WireConnection;0;3;95;0
WireConnection;0;4;94;0
ASEEND*/
//CHKSM=A6D3F56A468E8458263C2B08F7F405B1B0BB3014