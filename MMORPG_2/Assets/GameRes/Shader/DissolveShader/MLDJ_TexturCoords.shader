Shader "MLDJ/Dissolve_TexturCoords" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	//_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	//_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
	_Emission("Emission Scale" , Range(0,1)) = 0.2
	_Amount ("Amount", Range (0, 1)) = 0
	_StartAmount("StartAmount", float) = 0.1
	_Illuminate ("Illuminate", Range (0, 1)) = 0.5
	_Tile("Tile", float) = 1
	_DissColor ("DissColor", Color) = (1,1,1,1)
	//_ColorAnimate ("ColorAnimate", vector) = (1,1,1,1)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_DissolveSrc ("DissolveSrc", 2D) = "white" {}
	_DissolveSrcBump ("DissolveSrcBump", 2D) = "white" {}
    _Cutoff("Cut Alpha" , Range(0,1)) = 0.6
}
SubShader { 
	Tags { "RenderType"="Opaque" "Queue" = "Transparent-10" }
	LOD 400
	Cull Off
	
	
CGPROGRAM
//#pragma target 3.0
#pragma surface surf BlinnPhong nolightmap nodirlightmap noforwardadd alphatest:_Cutoff

 

sampler2D _MainTex;
sampler2D _DissolveSrc;
sampler2D _DissolveSrcBump;

fixed4 _Color;
half4 _DissColor;
//half _Shininess;
half _Emission;
half _Amount;
static half3 Color = float3(1,1,1);
//half4 _ColorAnimate;
half _Illuminate;
half _Tile;
half _StartAmount;

struct Input {
	half2 uv_MainTex;
};


void surf (Input IN, inout SurfaceOutput o) {

	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	
	o.Albedo = tex.rgb * _Color.rgb;
	
	float ClipTex = tex2D (_DissolveSrc, IN.uv_MainTex/_Tile).r ;
	
	float ClipAmount = ClipTex - _Amount;
	
	float Clip = 0;
	
	if (_Amount > 0)
	{
		Clip = (ClipAmount<0)? (-0.1) : 1;
		if (ClipAmount >= 0 && ClipAmount < _StartAmount)
		 {

			Color.x = _DissColor.x;
			  
			Color.y = ClipAmount/_StartAmount;
			  
			Color.z = _DissColor.z;

				o.Albedo  = (o.Albedo *((Color.x+Color.y+Color.z))* Color*((Color.x+Color.y+Color.z)))/(1 - _Illuminate);
				//o.Normal = UnpackNormal(tex2D(_DissolveSrcBump, IN.uv_MainTex));			
			//}
		 }
	 }
	clip(Clip);
		//////////////////////////////////
		//
		//o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		//o.Specular = _Shininess;
		o.Emission = o.Albedo * _Emission;
	}
ENDCG
}

FallBack "Self-Illumin/Diffuse"
}
