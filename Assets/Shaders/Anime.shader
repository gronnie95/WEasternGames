// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

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
                float3 vertexNormal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 fragmentNormal : NORMAL;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.fragmentNormal = mul( unity_ObjectToWorld, float4( v.vertexNormal, 0.0 ) ).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float dotProduct = dot(normalize(i.fragmentNormal), normalize(_WorldSpaceLightPos0.xyz));
                float lightValue = 1;

                //dotProduct = pow(dotProduct, 3);

                if (dotProduct < 0.2) {
                    lightValue = 0.2;
                } else {
                    //lightValue = 0.2 + (pow(pow(((dotProduct-0.2)/0.8),0.1),3) * 0.8);
                    lightValue = 0.2 + (pow(((dotProduct-0.2)/0.8),0.14) * 0.8);
                }

                return col * lightValue;
            }
            ENDCG
        }
    }
}
