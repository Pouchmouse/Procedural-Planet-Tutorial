Shader "Custom/TransparentVertexColoredShader"
 {
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _Alpha("Alpha", Range(0,1)) = 0.5
    }
    SubShader{
        Tags{ "RenderType" = "Opaque"}
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows addshadow alpha

        // Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float4 color : Color;
        };

        half   _Glossiness;
        half   _Metallic;
        fixed4 _Color;
		half   _Alpha;

        void surf(Input IN, inout SurfaceOutputStandard o) 
        {                        
            fixed4 c = IN.color;
			c.a = _Alpha;

			float  waveStrength = IN.uv_MainTex.y * (sin(_Time.y * 2.0) * 0.5f + 0.5f);
			fixed4 waveColor    = fixed4(1.0,1.0,1.0,1.0);

            o.Albedo = lerp(c, waveColor, waveStrength);
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
        }
        FallBack "Diffuse"
}