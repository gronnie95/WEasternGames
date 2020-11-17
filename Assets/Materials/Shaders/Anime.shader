Shader "Anime/Anime"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
            };

            float AnimeLighting(float3 normal, float3 lightDirection) {
                float dotProduct = dot(normalize(normal), normalize(lightDirection));

                if (dotProduct < 0.2) {
                    dotProduct = 0.6;
                } else {
                    dotProduct = 1;
                }

                return dotProduct;
            }

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture i.e get the color info from the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                col = col * AnimeLighting(i.worldNormal, _WorldSpaceLightPos0.xyz);

                return col;
            }
            ENDCG
        }
    }
}
