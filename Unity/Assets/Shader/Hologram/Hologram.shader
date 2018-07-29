// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Luciano/Hologram" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	_Color("Color", Color) = (0,0,0,0)
	_ColorB("ColorB", Color) = (0,0,0,0)
	_Listras("Stripes", Range(0,10)) = 0
	_Variacao("Variation", Range(0,8)) = 3.0
	_RimPower("RimPower", Range(0,1)) = 0.5
	_Interferencia("Interference", Range(0,1)) = 0.1
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		// Passo para eliminar faces sobrepostas
		Pass{
		ZWrite On
		ColorMask 0
	}

		
		CGPROGRAM

	#pragma surface surf Lambert alpha
	#pragma vertex vert
	#pragma target 3.0

	struct Input {
		float2 uv_MainTex;
		float3 viewDir;
		float4 screenPos;
	};
	sampler2D _MainTex;
	float4 _RimColor;
	float4 _Color;
	float4 _ColorB;
	half _Listras;
	half _Variacao;
	half _RimPower;
	float _Interferencia;

	void vert(inout appdata_full v, out Input o) {
		float posicao = sin(_Time.y) *0.5 + 0.5;
		o.uv_MainTex = float4(v.texcoord.xy, 0, 0);
		o.screenPos = ComputeScreenPos(UnityObjectToClipPos(v.vertex));
		float uv = o.screenPos.y;
		float condicao = ceil(uv - posicao) * ceil((posicao + _Interferencia / 5) - uv);
		float varx =saturate(_Interferencia- 0.2) * condicao;
		v.vertex.xyz = float3(v.vertex.x + varx, v.vertex.y, v.vertex.z);
		o.viewDir = _WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz;
	}

	void surf(Input IN, inout SurfaceOutput o) {
		float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

		fixed r = tex2D(_MainTex, float2(screenUV.x, (screenUV.y - _Time.x) * 3));

		fixed4 c = tex2D(_MainTex, float2(screenUV.x, screenUV.y))*_Color*saturate(r + _Listras)*pow((1 - screenUV.y), _Variacao);

		o.Albedo = c.rgb;

		half rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));

		o.Emission = _ColorB * pow(rim, _RimPower);

		o.Alpha =  pow(rim, _RimPower);

		clip(r - _Interferencia);
	}
	ENDCG
	}
		Fallback "Diffuse"
}