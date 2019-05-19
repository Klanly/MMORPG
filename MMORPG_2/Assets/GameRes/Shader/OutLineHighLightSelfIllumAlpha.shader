Shader "Outline_/OutLineHighLightSelfIllumAlpha" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,1)
	//_Color ("Main Color", Color) = (1,1,1,1)
    _HighLightColor ("High Light", Color) = (0.6,0.6,0.6,1)
    _PowValue("Pow Value" , float) = 0
    //_MainTex ("Texture", 2D) = "white" {}
    _Illum ("Illumin (A)", 2D) = "white" {}
    _LightColor("Light Color" , Color) = (1,1,1,1)
    _LightDir("Light Direction" , Vector) = (0,0,0,0)
	_Cutoff("Cut Alpha" , Range(0,1)) = 0.6
	}
	SubShader {	
		Tags { "Queue" = "Transparent-10" }
	


		 CGPROGRAM
    #pragma surface surf SimpleLambert noforwardadd alphatest:_Cutoff
	sampler2D _MainTex; 
	fixed _Outline;
	fixed4 _OutlineColor;
	fixed4 _Color;
	//fixed4 _Color;
	fixed4 _HighLightColor;
	half _PowValue;
	fixed4 _LightColor;
	half4 _LightDir;
	
    half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
    {
	    //half NdotL = max(dot (normalize(s.Normal), normalize(lightDir)),0);
	    half NdotL = s.Specular;
	    half4 c;
	    half3 temp = s.Albedo * (_HighLightColor.rgb + _LightColor.rgb) * (NdotL * pow(NdotL , _PowValue)  *2) + s.Albedo * min(_PowValue/10,0.3);
	    c.rgb = temp;
	    c.a = s.Alpha;
	
	         
	    return c;		
    }

     struct Input
     {
         half2 uv_MainTex;
         //float3 worldPos;
     };
     //sampler2D _MainTex;
     sampler2D _Illum;
     void surf (Input IN, inout SurfaceOutput o) 
     {
     	 fixed4 texColor = tex2D(_MainTex , IN.uv_MainTex) * _Color;
         o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color;
         o.Emission = texColor.rgb * tex2D(_Illum , IN.uv_MainTex).a;
         o.Alpha = texColor.a;
         o.Specular = max(dot(normalize(o.Normal) , normalize(_LightDir.xyz)),0.001);
     }
      ENDCG
    }
	//} 
	FallBack "Diffuse"
}

