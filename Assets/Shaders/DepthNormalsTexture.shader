Shader "Anime/DepthNormalsTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CameraColorTexture ("Camera Color Texture", 2D) = "white" {}
        _CameraDepthTexture ("Camera Depth Texture", 2D) = "white" {}
        _CameraDepthNormalsTexture ("Camera Depth Normals Texture", 2D) = "white" {}
        _OutlineThickness("Outline Thickness", float) = 1
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _DepthSensitivity("Depth Sensitivity", float) = 1
        _ColorSensitivity("Color Sensitivity", float) = 1
        _NormalsSensitivity("Normals Sensitivity", float) = 1
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Texture2D _CameraColorTexture;
            SamplerState sampler_CameraColorTexture;
            float4 _CameraColorTexture_TexelSize;

            Texture2D _CameraDepthTexture;
            SamplerState sampler_CameraDepthTexture;

            Texture2D _CameraDepthNormalsTexture;
            SamplerState sampler_CameraDepthNormalsTexture;

            float _OutlineThickness;
            fixed4 _OutlineColor;
            float _DepthSensitivity;
            float _ColorSensitivity;
            float _NormalsSensitivity;

            float3 DecodeNormal(float4 enc)
            {
                float kScale = 1.7777;
                float3 nn = enc.xyz*float3(2*kScale,2*kScale,0) + float3(-kScale,-kScale,1);
                float g = 2.0 / dot(nn.xyz,nn.xyz);
                float3 n;
                n.xy = g*nn.xy;
                n.z = g-1;
                return n;
            }

            float4 GenerateOutline(float2 UV) {

                float halfScaleFloor = floor(_OutlineThickness * 0.5);
                float halfScaleCeil = ceil(_OutlineThickness * 0.5);
                float2 Texel = (1.0) / float2(_CameraColorTexture_TexelSize.z, _CameraColorTexture_TexelSize.w);
        
                float2 uvSamples[4];
                float depthSamples[4];
                float3 normalSamples[4], colorSamples[4];

                uvSamples[0] = UV - float2(Texel.x, Texel.y) * halfScaleFloor;
                uvSamples[1] = UV + float2(Texel.x, Texel.y) * halfScaleCeil;
                uvSamples[2] = UV + float2(Texel.x * halfScaleCeil, -Texel.y * halfScaleFloor);
                uvSamples[3] = UV + float2(-Texel.x * halfScaleFloor, Texel.y * halfScaleCeil);

                for(int i = 0; i < 4 ; i++)
                {
                    depthSamples[i] = _CameraDepthTexture.Sample(sampler_CameraDepthTexture, uvSamples[i]).r;
                    normalSamples[i] = DecodeNormal(_CameraDepthNormalsTexture.Sample(sampler_CameraDepthNormalsTexture, uvSamples[i]));
                    colorSamples[i] = _CameraColorTexture.Sample(sampler_CameraColorTexture, uvSamples[i]);
                }
        
                // Depth
                float depthFiniteDifference0 = depthSamples[1] - depthSamples[0];
                float depthFiniteDifference1 = depthSamples[3] - depthSamples[2];
                float edgeDepth = sqrt(pow(depthFiniteDifference0, 2) + pow(depthFiniteDifference1, 2)) * 100;
                float depthThreshold = (1/_DepthSensitivity) * depthSamples[0];
                edgeDepth = edgeDepth > depthThreshold ? 1 : 0;
        
                // Normals
                float3 normalFiniteDifference0 = normalSamples[1] - normalSamples[0];
                float3 normalFiniteDifference1 = normalSamples[3] - normalSamples[2];
                float edgeNormal = sqrt(dot(normalFiniteDifference0, normalFiniteDifference0) + dot(normalFiniteDifference1, normalFiniteDifference1));
                edgeNormal = edgeNormal > (1/_NormalsSensitivity) ? 1 : 0;

                  // Color
                float3 colorFiniteDifference0 = colorSamples[1] - colorSamples[0];
                float3 colorFiniteDifference1 = colorSamples[3] - colorSamples[2];
                float edgeColor = sqrt(dot(colorFiniteDifference0, colorFiniteDifference0) + dot(colorFiniteDifference1, colorFiniteDifference1));
        	    edgeColor = edgeColor > (1/_ColorSensitivity) ? 1 : 0;
        
                float edge = max(edgeDepth, max(edgeNormal, edgeColor));
        
                float4 original = _CameraColorTexture.Sample(sampler_CameraColorTexture, uvSamples[0]);	
                return ((1 - edge) * original) + (edge * lerp(original, _OutlineColor,  _OutlineColor.a));
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = GenerateOutline(i.uv);

                return col;
            }
            ENDCG
        }
    }
}
