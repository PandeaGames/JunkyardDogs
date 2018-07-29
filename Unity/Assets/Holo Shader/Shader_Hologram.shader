// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:1,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:33909,y:32304,varname:node_2865,prsc:2|normal-1309-RGB,emission-9085-OUT,alpha-6749-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:5460,x:31532,y:32621,varname:node_5460,prsc:2;n:type:ShaderForge.SFN_Append,id:1165,x:31735,y:32733,cmnt:Y Axis,varname:node_1165,prsc:2|A-5460-X,B-5460-Y;n:type:ShaderForge.SFN_Multiply,id:262,x:32153,y:32632,varname:node_262,prsc:2|A-6096-OUT,B-6716-OUT;n:type:ShaderForge.SFN_Append,id:6716,x:32153,y:32767,varname:node_6716,prsc:2|A-4375-OUT,B-6390-OUT;n:type:ShaderForge.SFN_Vector1,id:4375,x:31954,y:32767,varname:node_4375,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:6390,x:31954,y:32882,ptovrint:False,ptlb:Scanline Density,ptin:_ScanlineDensity,cmnt:Number of Scanlines,varname:node_6390,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:3373,x:31954,y:32541,ptovrint:False,ptlb:Scanline Speed,ptin:_ScanlineSpeed,varname:_ScanlineSpeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Append,id:6832,x:32160,y:32477,varname:node_6832,prsc:2|A-7244-OUT,B-3373-OUT;n:type:ShaderForge.SFN_Time,id:1332,x:32160,y:32341,varname:node_1332,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9949,x:32391,y:32415,varname:node_9949,prsc:2|A-1332-TSL,B-6832-OUT;n:type:ShaderForge.SFN_Add,id:8512,x:32391,y:32615,varname:node_8512,prsc:2|A-9949-OUT,B-262-OUT;n:type:ShaderForge.SFN_OneMinus,id:1068,x:32577,y:32615,varname:node_1068,prsc:2|IN-8512-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5167,x:32757,y:32615,varname:node_5167,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-1068-OUT;n:type:ShaderForge.SFN_Frac,id:6163,x:32926,y:32615,varname:node_6163,prsc:2|IN-5167-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3094,x:32926,y:32789,ptovrint:False,ptlb:Scanline Exp,ptin:_ScanlineExp,cmnt:Exposure of the scanlines,varname:node_3094,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Power,id:1178,x:33102,y:32615,varname:node_1178,prsc:2|VAL-6163-OUT,EXP-3094-OUT;n:type:ShaderForge.SFN_Slider,id:3393,x:32424,y:32125,ptovrint:False,ptlb:Desat. Amount,ptin:_DesatAmount,varname:node_3393,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Desaturate,id:9831,x:32804,y:32089,varname:node_9831,prsc:2|COL-8279-RGB,DES-3393-OUT;n:type:ShaderForge.SFN_Multiply,id:9251,x:33294,y:32197,varname:node_9251,prsc:2|A-8153-OUT,B-2765-OUT,C-6420-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8153,x:33294,y:32116,ptovrint:False,ptlb:Brightness,ptin:_Brightness,cmnt:Diffuse Brightness,varname:node_8153,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:526,x:33489,y:32279,varname:node_526,prsc:2|A-9251-OUT,B-5793-OUT;n:type:ShaderForge.SFN_Multiply,id:5793,x:33297,y:32615,varname:node_5793,prsc:2|A-1178-OUT,B-2581-OUT,C-995-RGB;n:type:ShaderForge.SFN_Slider,id:2581,x:32769,y:32882,ptovrint:False,ptlb:Scanline Opacity,ptin:_ScanlineOpacity,varname:node_2581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Add,id:4358,x:33489,y:32449,varname:node_4358,prsc:2|A-5793-OUT,B-1903-OUT,C-5658-OUT;n:type:ShaderForge.SFN_Slider,id:1903,x:32769,y:32985,ptovrint:False,ptlb:Object Opacity,ptin:_ObjectOpacity,varname:node_1903,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Multiply,id:9085,x:33688,y:32357,varname:node_9085,prsc:2|A-526-OUT,B-4358-OUT;n:type:ShaderForge.SFN_Tex2d,id:8279,x:32581,y:31931,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:_Diffuse_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:995,x:32581,y:32228,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ValueProperty,id:8707,x:32581,y:32415,ptovrint:False,ptlb:Color Intensity,ptin:_ColorIntensity,varname:node_492,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:6786,x:32804,y:32306,varname:node_6786,prsc:2|A-995-RGB,B-8707-OUT;n:type:ShaderForge.SFN_Multiply,id:2765,x:33017,y:32189,varname:node_2765,prsc:2|A-9831-OUT,B-6786-OUT,C-995-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:6096,x:31954,y:32632,ptovrint:False,ptlb:Y Axis,ptin:_YAxis,varname:node_6096,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-8043-OUT,B-1165-OUT;n:type:ShaderForge.SFN_Append,id:8043,x:31735,y:32541,cmnt:X Axis,varname:node_8043,prsc:2|A-5460-Y,B-5460-X;n:type:ShaderForge.SFN_Multiply,id:6749,x:33297,y:32757,varname:node_6749,prsc:2|A-8279-A,B-1903-OUT;n:type:ShaderForge.SFN_Fresnel,id:5787,x:33495,y:33055,varname:node_5787,prsc:2|EXP-6604-OUT;n:type:ShaderForge.SFN_Slider,id:9435,x:32769,y:33200,ptovrint:False,ptlb:Fres. Intensity,ptin:_FresIntensity,cmnt:Amount of edge glow,varname:node_9435,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_OneMinus,id:6604,x:33297,y:33039,varname:node_6604,prsc:2|IN-9435-OUT;n:type:ShaderForge.SFN_DepthBlend,id:4705,x:33297,y:32901,varname:node_4705,prsc:2|DIST-2485-OUT;n:type:ShaderForge.SFN_OneMinus,id:1954,x:33495,y:32913,varname:node_1954,prsc:2|IN-4705-OUT;n:type:ShaderForge.SFN_Slider,id:2485,x:32769,y:33091,ptovrint:False,ptlb:Intersect Attenuation,ptin:_IntersectAttenuation,cmnt:Glow area where objects intersect,varname:node_2485,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:4543,x:33710,y:33071,ptovrint:False,ptlb:Intersect Brightness,ptin:_IntersectBrightness,varname:node_4543,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:6420,x:33710,y:32913,varname:node_6420,prsc:2|A-1954-OUT,B-4543-OUT,C-5787-OUT;n:type:ShaderForge.SFN_Tex2d,id:1309,x:33688,y:32136,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_1309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Vector1,id:7244,x:31954,y:32433,varname:node_7244,prsc:2,v1:0;n:type:ShaderForge.SFN_ScreenPos,id:6128,x:31726,y:32982,varname:node_6128,prsc:2,sctp:0;n:type:ShaderForge.SFN_Multiply,id:2691,x:31954,y:32982,varname:node_2691,prsc:2|A-6128-UVOUT,B-587-OUT;n:type:ShaderForge.SFN_Relay,id:587,x:31726,y:33130,varname:node_587,prsc:2|IN-1332-TSL;n:type:ShaderForge.SFN_Noise,id:8683,x:32153,y:32982,cmnt:Static effect,varname:node_8683,prsc:2|XY-2691-OUT;n:type:ShaderForge.SFN_RemapRange,id:9230,x:32336,y:32982,varname:node_9230,prsc:2,frmn:0,frmx:1,tomn:0.5,tomx:1|IN-8683-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7751,x:32153,y:33142,ptovrint:False,ptlb:Static Intensity,ptin:_StaticIntensity,varname:node_7751,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9006,x:32519,y:32982,varname:node_9006,prsc:2|A-9230-OUT,B-7751-OUT;n:type:ShaderForge.SFN_Lerp,id:5658,x:32519,y:32823,varname:node_5658,prsc:2|A-8799-OUT,B-9006-OUT,T-7751-OUT;n:type:ShaderForge.SFN_Vector1,id:8799,x:32336,y:32904,varname:node_8799,prsc:2,v1:0;proporder:8279-1309-6096-3373-6390-3094-2581-1903-3393-8153-995-8707-9435-2485-4543-7751;pass:END;sub:END;*/

Shader "Shader Forge/Shader_Hologram" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        [MaterialToggle] _YAxis ("Y Axis", Float ) = 0
        _ScanlineSpeed ("Scanline Speed", Float ) = 3
        _ScanlineDensity ("Scanline Density", Float ) = 1
        _ScanlineExp ("Scanline Exp", Float ) = 10
        _ScanlineOpacity ("Scanline Opacity", Range(0, 1)) = 0.5
        _ObjectOpacity ("Object Opacity", Range(0, 1)) = 0.5
        _DesatAmount ("Desat. Amount", Range(0, 1)) = 1
        _Brightness ("Brightness", Float ) = 0.5
        _Color ("Color", Color) = (0,1,1,1)
        _ColorIntensity ("Color Intensity", Float ) = 1
        _FresIntensity ("Fres. Intensity", Range(0, 1)) = 1
        _IntersectAttenuation ("Intersect Attenuation", Range(0, 1)) = 0
        _IntersectBrightness ("Intersect Brightness", Float ) = 0.5
        _StaticIntensity ("Static Intensity", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float _ScanlineDensity;
            uniform float _ScanlineSpeed;
            uniform float _ScanlineExp;
            uniform float _DesatAmount;
            uniform float _Brightness;
            uniform float _ScanlineOpacity;
            uniform float _ObjectOpacity;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _ColorIntensity;
            uniform fixed _YAxis;
            uniform float _FresIntensity;
            uniform float _IntersectAttenuation;
            uniform float _IntersectBrightness;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _StaticIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                float4 screenPos : TEXCOORD5;
                float4 projPos : TEXCOORD6;
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 node_1332 = _Time + _TimeEditor;
                float3 node_5793 = (pow(frac((1.0 - ((node_1332.r*float2(0.0,_ScanlineSpeed))+(lerp( float2(i.posWorld.g,i.posWorld.r), float2(i.posWorld.r,i.posWorld.g), _YAxis )*float2(1.0,_ScanlineDensity)))).g),_ScanlineExp)*_ScanlineOpacity*_Color.rgb);
                float2 node_2691 = (i.screenPos.rg*node_1332.r);
                float2 node_8683_skew = node_2691 + 0.2127+node_2691.x*0.3713*node_2691.y;
                float2 node_8683_rnd = 4.789*sin(489.123*(node_8683_skew));
                float node_8683 = frac(node_8683_rnd.x*node_8683_rnd.y*(1+node_8683_skew.x)); // Static effect
                float3 emissive = (((_Brightness*(lerp(_Diffuse_var.rgb,dot(_Diffuse_var.rgb,float3(0.3,0.59,0.11)),_DesatAmount)*(_Color.rgb*_ColorIntensity)*_Color.rgb)*((1.0 - saturate((sceneZ-partZ)/_IntersectAttenuation))+_IntersectBrightness+pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 - _FresIntensity))))+node_5793)*(node_5793+_ObjectOpacity+lerp(0.0,((node_8683*0.5+0.5)*_StaticIntensity),_StaticIntensity)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Diffuse_var.a*_ObjectOpacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform float _ScanlineDensity;
            uniform float _ScanlineSpeed;
            uniform float _ScanlineExp;
            uniform float _DesatAmount;
            uniform float _Brightness;
            uniform float _ScanlineOpacity;
            uniform float _ObjectOpacity;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _Color;
            uniform float _ColorIntensity;
            uniform fixed _YAxis;
            uniform float _FresIntensity;
            uniform float _IntersectAttenuation;
            uniform float _IntersectBrightness;
            uniform float _StaticIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                float4 projPos : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float4 node_1332 = _Time + _TimeEditor;
                float3 node_5793 = (pow(frac((1.0 - ((node_1332.r*float2(0.0,_ScanlineSpeed))+(lerp( float2(i.posWorld.g,i.posWorld.r), float2(i.posWorld.r,i.posWorld.g), _YAxis )*float2(1.0,_ScanlineDensity)))).g),_ScanlineExp)*_ScanlineOpacity*_Color.rgb);
                float2 node_2691 = (i.screenPos.rg*node_1332.r);
                float2 node_8683_skew = node_2691 + 0.2127+node_2691.x*0.3713*node_2691.y;
                float2 node_8683_rnd = 4.789*sin(489.123*(node_8683_skew));
                float node_8683 = frac(node_8683_rnd.x*node_8683_rnd.y*(1+node_8683_skew.x)); // Static effect
                o.Emission = (((_Brightness*(lerp(_Diffuse_var.rgb,dot(_Diffuse_var.rgb,float3(0.3,0.59,0.11)),_DesatAmount)*(_Color.rgb*_ColorIntensity)*_Color.rgb)*((1.0 - saturate((sceneZ-partZ)/_IntersectAttenuation))+_IntersectBrightness+pow(1.0-max(0,dot(normalDirection, viewDirection)),(1.0 - _FresIntensity))))+node_5793)*(node_5793+_ObjectOpacity+lerp(0.0,((node_8683*0.5+0.5)*_StaticIntensity),_StaticIntensity)));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
