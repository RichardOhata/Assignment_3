Shader "Custom/RevealingShader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _LightAngle("Light Angle", Range(0,180)) = 45
        _StrengthScalar("Strength", Float) = 50
    }
    SubShader {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal; // Needed for lighting calculations
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _LightAngle;
        float _StrengthScalar;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

            // Use Unity's lighting to calculate light effects
            float3 normal = normalize(IN.worldNormal);
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

            // Calculate custom "reveal" strength based on angle
            float scale = dot(normal, lightDir);
            float strength = scale - cos(_LightAngle * (3.14 / 360.0));
            strength = saturate(strength * _StrengthScalar);

            o.Albedo = c.rgb;
            o.Emission = c.rgb * strength; // Add light-based emission
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
