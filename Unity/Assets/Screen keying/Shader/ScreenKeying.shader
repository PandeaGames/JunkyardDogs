Shader "Unlit/ScreenKeying"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VideoTex ("Video (RGB)", 2D) = "white" {}
		_SynType ("Type 1-blue 2-green", Range(1,2)) = 1						// 1,2		###类型	
		_Multiple ("Multiple", Range(0.0,1.0)) = 0.285				// 0-1.0f	###范围
		_Radix ("Radix", Range(0.0,1.0)) = 0.898					// 0-1.0f	###强度
		_RefColor("Main Color",Color) = (0.10156,0.3125,0.6289,1.0)	// RGBA		###拾取
		_Turn ("Turn", Range(0,3)) = 0			
		
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _VideoTex;
			float4 _MainTex_ST;
			float4 _VideoTex_ST;

			float _SynType;
			float _Multiple;
			float _Radix;
			float4 _RefColor;
			int _Turn;



			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			float3 Synthesis( float3 SColor,float3 DColor)
			{
			    float3 color;
			    float alpha = 0;
			    if (_SynType == 1){//抠蓝 Mask blue
			        alpha = (SColor.r+SColor.g)/2 - SColor.b;
			    }else if(_SynType == 2){//抠绿  Mask green
			        alpha = (SColor.r+SColor.b)/2 - SColor.g;
			    }
			    float fMultiple = _Multiple / 0.5f + 0.00392f;// 1/255=0.003921568627451
			    alpha =alpha+ 1.0f;
			    if(alpha < _Radix){
			        alpha = 0;
			    }else{
			        alpha = (alpha - _Radix) * 2.0f / (1.0f - _Radix + 0.00392f);
			        alpha = min(1.0f, (float)(alpha * fMultiple));
			    }

			    if (alpha == 0){
			        color=DColor;
			    }else if (alpha == 1.0f){
			        color=SColor;
			    }else if (alpha > 0 && alpha < 1.0f){
			        SColor.r = max(0, min(1.0f, (float)((SColor.r - _RefColor.r) * 1.0f / alpha) + _RefColor.r));
			        SColor.g = max(0, min(1.0f, (float)((SColor.g - _RefColor.g) * 1.0f / alpha) + _RefColor.g));
			        SColor.b = max(0, min(1.0f, (float)((SColor.b - _RefColor.b) * 1.0f / alpha) + _RefColor.b));
			        color.r = ((SColor.r - DColor.r) * alpha / 1.0f) + DColor.r;
			        color.g = ((SColor.g - DColor.g) * alpha / 1.0f) + DColor.g;
			        color.b = ((SColor.b - DColor.b) * alpha / 1.0f) + DColor.b;
			    }
			    return color;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uvA=i.uv;
				float4 colorV=0;
				if(_Turn==0){
					colorV= tex2D(_VideoTex, i.uv);
				}
				else if(_Turn==1){
					uvA.x=i.uv.x;
					uvA.y=1-i.uv.y;
					colorV = tex2D(_VideoTex, uvA);
				}
				else if(_Turn==2){
					uvA.x=1-i.uv.x;
					uvA.y=i.uv.y;
					colorV = tex2D(_VideoTex, uvA);
				}
				else{
					uvA.x=1-i.uv.x;
					uvA.y=1-i.uv.y;
					colorV = tex2D(_VideoTex, uvA);
				}

				float4 colorB = tex2D(_MainTex, i.uv);

				colorB.rgb = Synthesis( colorV.rgb,colorB.rgb);
				colorB.a=1.0f;
				return colorB;
			}
			ENDCG
		}
	}
}
