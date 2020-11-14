﻿Shader "Unlit/divergence"
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
            float4 _MainTex_ST;
            sampler2D _VelocityTex;
            float2 _MainTex_TexelSize;
            float2 _VelocityTex_TexelSize;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float Top =    tex2D(_VelocityTex, i.uv + float2(0.0f, _VelocityTex_TexelSize.y )).y;
                float Bottom = tex2D(_VelocityTex, i.uv - float2(0.0f, _VelocityTex_TexelSize.y )).y;
                float Right =  tex2D(_VelocityTex, i.uv + float2(_VelocityTex_TexelSize.x,  0.0f)).x;
                float Left =   tex2D(_VelocityTex, i.uv - float2(_VelocityTex_TexelSize.x, 0.0f)).x;
                
                float4 Divergence = 0.5f*float4(Top - Bottom + Right - Left, 0.0f, 0.0f, 0.0f);
                return Divergence;
            }
            ENDCG
        }
    }
}
