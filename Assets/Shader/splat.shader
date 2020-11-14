Shader "Unlit/splat"
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _VelocityTex;
            float4 _MainTex_ST;

            float PositionX;
            float PositionY;
            float PositionDx;
            float PositionDy;
            int MouseDown;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 pointuv = float2(PositionX,PositionY);
                float2 dis = i.uv - pointuv;
                float radius = 0.001f;
                float3 color = float3(PositionDx, PositionDy, 0.0f) * 50.0f;
                float3 splat = pow(2.1, -dot(dis, dis) / radius) * color;
                float3 base = tex2D(_VelocityTex, i.uv).xyz;
                if (MouseDown == 1) {
                    base += splat;
                }
                // sample the texture
                float4 col = float4(base, 1.0f);
                return col;
            }
            ENDCG
        }
    }
}
