// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// colored vertex lighting
Shader "Test Shader"
{
	

    // a single color property
    Properties {
		_MainTex ("Main Texture", 2D) = "defaulttexture" {}
        _Color ("Main Color", Color) = (1,.5,.5,1)
		_RKeyColor ("Red Key Color", Color) = (1,.5,.5,1)
		_GKeyColor ("Green Key Color", Color) = (1,.5,.5,1)
		_BKeyColor ("Blue Key Color", Color) = (1,.5,.5,1)
		_BaseColor ("Base Color", Color) = (1,1,1,1)
    }
    // define one subshader
    SubShader {
	Tags { "CanUseSpriteAtlas" = "true" }
		Pass {
			Lighting Off

			CGPROGRAM

			#pragma vertex vert
            #pragma fragment frag
            
            // vertex shader
            // this time instead of using "appdata" struct, just spell inputs manually,
            // and instead of returning v2f struct, also just return a single output
            // float4 clip position
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

			// vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
            };
            
            // color from the material
            fixed4 _Color;
			fixed4 _RKeyColor;
			fixed4 _GKeyColor;
			fixed4 _BKeyColor;
			fixed4 _BaseColor;

			// texture we will sample
            sampler2D _MainTex;

			float mid = 0.5f;//* 255 / 2*// 

			float calculateOuterValue(float colorValue)
			{
				return min(colorValue, 1 - colorValue);
			}

			void applyColorTemplate(fixed4 sourceTex, fixed4 tex, fixed4 replacement, float inputBrightness, float sourceColorValue)
			{
				tex.r = replacement.r + calculateOuterValue(replacement.r) * inputBrightness;
				tex.g = replacement.g + calculateOuterValue(replacement.g) * inputBrightness;
				tex.b = replacement.b + calculateOuterValue(replacement.b) * inputBrightness;
			}

            // pixel shader, no inputs needed
            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 sourceTex = tex2D(_MainTex, i.uv);
				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 base = _BaseColor;

				float total = tex.r + tex.g + tex.b;
				//float brightness = total / 3;

				float cMax = 0;
				cMax = max(cMax, tex.r);
				cMax = max(cMax, tex.g);
				cMax = max(cMax, tex.b);

				float cMin = cMax;
				cMin = min(cMin, tex.r);
				cMin = min(cMin, tex.g);
				cMin = min(cMin, tex.b);

				float chroma = cMax - cMin;
				float lightness = 0.333333 * (tex.r + tex.g + tex.b);
				float lightnessOffset = (lightness * 2) - 1;

				float cMid = (cMin + cMax)/ 2;

				float range = cMax - cMin;

				float rangeTotal = 0;
				rangeTotal += tex.r - cMin;
				rangeTotal += tex.g - cMin;
				rangeTotal += tex.b - cMin;

				float brightness = cMax;
				float saturation = cMin + range / 2;
				float brightnessOffset = (brightness * 2) - 1;
				float saturationOffset = (saturation * 2) - 1;

				//avoid deviding by 0 in later calculations
				float safeRangeTotal = max(rangeTotal, 1);

				/*applyColorTemplate(sourceTex, tex, _RKeyColor, brightness, sourceTex.r);
				applyColorTemplate(sourceTex, tex, _GKeyColor, brightness, sourceTex.g);
				applyColorTemplate(sourceTex, tex, _BKeyColor, brightness, sourceTex.b);*/

				//tex.r = _RKeyColor.r + (calculateOuterValue(_RKeyColor.r) * lightnessOffset);
				//tex.g = _RKeyColor.g + (calculateOuterValue(_RKeyColor.g) * lightnessOffset);
				//tex.b = _RKeyColor.b + (calculateOuterValue(_RKeyColor.b) * lightnessOffset);

				float red = tex.r;

				tex.r = _RKeyColor.r * red;
					tex.g = _RKeyColor.g  * red;
					tex.b = _RKeyColor.b * red;

				if(lightness < 0.5)
				{
					float mod = (0.5 - lightness) * 2;
					float smallMod = (0.5 - lightness);

					

					tex.r = tex.r * mod;
					tex.g = tex.g  * mod;
					tex.b = tex.b * mod;
				}
				else
				{
					float mod = (lightness - 0.5) * 2;
					tex.r = tex.r + (0.5 - tex.r) * mod;
					tex.g = tex.g + (0.5 - tex.g) * mod;
					tex.b = tex.b + (0.5 - tex.b) * mod;
				}
				
	

				/*tex.r = _GKeyColor.r + calculateOuterValue(_GKeyColor.r) * brightnessOffset;
				tex.g = _GKeyColor.g + calculateOuterValue(_GKeyColor.g) * brightnessOffset;
				tex.b = _GKeyColor.b + calculateOuterValue(_GKeyColor.b) * brightnessOffset;

				tex.r = _BKeyColor.r + calculateOuterValue(_BKeyColor.r) * brightnessOffset;
				tex.g = _BKeyColor.g + calculateOuterValue(_BKeyColor.g) * brightnessOffset;
				tex.b = _BKeyColor.b + calculateOuterValue(_BKeyColor.b) * brightnessOffset;*/

				//float rBrightnessMod = (mid - abs((tex.r / 2) - mid)) / mid;
				/*

				float rColorSaturation = (tex.r - cMin) / safeRangeTotal;
				float gColorSaturation = (tex.g - cMin) / safeRangeTotal;
				float bColorSaturation = (tex.b - cMin) / safeRangeTotal;

				tex.r = _RKeyColor.r * rColorSaturation;
				tex.r += _GKeyColor.r * gColorSaturation;
				tex.r += _BKeyColor.r * bColorSaturation;*/



				//tex.r = _RKeyColor.r * rColorSaturation + _GKeyColor.r * gColorSaturation + _BKeyColor.r * bColorSaturation;
				//tex.g = _RKeyColor.g * rColorSaturation + _GKeyColor.g * gColorSaturation + _BKeyColor.g * bColorSaturation;
				//tex.b = _RKeyColor.b * rColorSaturation + _GKeyColor.b * gColorSaturation + _BKeyColor.b * bColorSaturation;

				/*tex.r = brightness;
				tex.g = brightness;
				tex.b = brightness;*/


                return tex; // just return it
				//return fixed4(_RKeyColor * col.r, _GKeyColor * col.g, _BKeyColor * col.b, col.a)
            }

				ENDCG
		}
	}
}