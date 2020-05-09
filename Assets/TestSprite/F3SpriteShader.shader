// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/F3SpriteShader"
{
    Properties
    {
        _MainTex("Base (RGB), Alpha (A)", 2D) = "black" {}
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "IgnoreProjector" = "true"
            "RenderType" = "Transparent"
        }

        LOD 200

        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Offset -1, -1
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            float4 _MainTex_ST;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            v2f o;
            v2f vert(appdata_t v)
            {
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;

                //o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;

                return o;
            }

            fixed4 frag(v2f IN) : COLOR
            {
                fixed4 color = tex2D(_MainTex, IN.uv) * IN.color;
                return color;

            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
