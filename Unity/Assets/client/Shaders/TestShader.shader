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

            // pixel shader, no inputs needed
            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 tex = tex2D(_MainTex, i.uv);
				fixed4 base = _BaseColor;

				float total = tex.r + tex.g + tex.b;
				float brightness = total / 3;
				//float saturation = 

				float rColorSaturation = tex.r / total;
				float gColorSaturation = tex.g / total;
				float bColorSaturation = tex.b / total;

				tex.r = _RKeyColor.r * rColorSaturation;
				tex.r += _GKeyColor.r * gColorSaturation;
				tex.r += _BKeyColor.r * bColorSaturation;



				tex.r = _RKeyColor.r * rColorSaturation + _GKeyColor.r * gColorSaturation + _BKeyColor.r * bColorSaturation;
				tex.g = _RKeyColor.g * rColorSaturation + _GKeyColor.g * gColorSaturation + _BKeyColor.g * bColorSaturation;
				tex.b = _RKeyColor.b * rColorSaturation + _GKeyColor.b * gColorSaturation + _BKeyColor.b * bColorSaturation;

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