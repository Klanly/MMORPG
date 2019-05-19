// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/BlackScreen" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	CGINCLUDE

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		
		uniform fixed _blackCount;
		
		struct v2f_b
		{
			half4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
		};	
		
		
		v2f_b vertBlackScreen( appdata_img  v )
		{
			v2f_b o;
			o.pos = UnityObjectToClipPos (v.vertex);
			o.uv = v.texcoord;
			return o;
		}
		
		fixed4 fragBlackScreen( v2f_b i ) : COLOR
		{
			fixed4 color = tex2D(_MainTex , i.uv);
			return _blackCount * color;
		}
		
	ENDCG
		
		
	SubShader {
		ZTest Off Cull Off ZWrite Off Blend Off
	  Fog { Mode off }  
	  
	  pass
	  	{
	  		CGPROGRAM
				
			#pragma vertex vertBlackScreen
			#pragma fragment fragBlackScreen
			#pragma fragmentoption ARB_precision_hint_fastest 
		
			ENDCG
		}
		
	} 
	FallBack off
}
