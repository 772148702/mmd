Shader "Unlit/pressure"
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
            sampler2D _DivergenceTex;
            sampler2D _PressureTex;
            float4 _MainTex_ST;
            float2 _PressureTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float Top =    tex2D(_PressureTex,    i.uv + float2(0.0f, _PressureTex_TexelSize.y )).y;
                float Bottom = tex2D(_PressureTex, i.uv - float2(0.0f, _PressureTex_TexelSize.y)).y;
                float Right =  tex2D(_PressureTex,  i.uv + float2(_PressureTex_TexelSize.x,0.0f)).x;
                float Left = tex2D(_PressureTex,   i.uv - float2(_PressureTex_TexelSize.x,0.0f)).x;
                float Divergence = tex2D(_DivergenceTex, i.uv).x;
                float temp = (Top + Bottom + Left + Right - 2 * Divergence)*0.25;
          
                float4 col = float4(temp, 0.0f, 0.0f, 1.0f);
           
                return col;
            }
            ENDCG
        }
    }
}
