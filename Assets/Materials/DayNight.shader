Shader "Custom/DayNight"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _MetallicGlossMap ("Metallic Map", 2D) = "black" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _HeightMap ("Height Map", 2D) = "white" {}
        _EmissionMap ("Emission Map", 2D) = "black" {}
        _DayTint ("Day Tint", Color) = (1, 1, 1, 1)
        _NightTint ("Night Tint", Color) = (0.1, 0.1, 0.3, 1)
        _BlendFactor ("Day/Night Blend", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            sampler2D _MetallicGlossMap;
            float _Smoothness;
            sampler2D _NormalMap;
            sampler2D _HeightMap;
            sampler2D _EmissionMap;
            fixed4 _DayTint;
            fixed4 _NightTint;
            float _BlendFactor; // Day/Night blend factor

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                return o;
            }

            // Fragment shader
            half4 frag(v2f i) : SV_Target
            {
                // Sample textures
                half4 baseColor = tex2D(_MainTex, i.uv);
                half metallic = tex2D(_MetallicGlossMap, i.uv).r;
                half smoothness = _Smoothness;
                half3 normal = tex2D(_NormalMap, i.uv).rgb;
                half height = tex2D(_HeightMap, i.uv).r;
                half4 emission = tex2D(_EmissionMap, i.uv);

                // Blend between Day and Night
                half4 tint = lerp(_NightTint, _DayTint, _BlendFactor);

                // Calculate final color with tint and textures
                half4 color = baseColor * tint;
                color.rgb += emission.rgb; // Add emission effect

                // Use smoothness and metallic for the final material
                half metallicSpec = metallic * smoothness;
                color.rgb = color.rgb * (1 - metallicSpec) + metallicSpec * baseColor.rgb;

                return color;
            }
            ENDCG
        }
    }

    FallBack "Standard"
}
