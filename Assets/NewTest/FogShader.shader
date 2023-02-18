Shader "Custom/FogShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
       Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        blend SrcAlpha OneMinusSrcAlpha
        Zwrite off
        Cull off
            
        CGPROGRAM
        #pragma surface surf Lambert keepalpha
        // Physically based Standard lighting model, and enable shadows on all light types
        //#pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
