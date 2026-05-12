Shader "Custom/SelectionGlow"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0.3,0.6,1,1)
        _GlowStrength ("Glow Strength", Float) = 2
        _PulseSpeed ("Pulse Speed", Float) = 3
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend SrcAlpha One
        ZWrite Off
        Cull Off

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _MainColor;
            float _GlowStrength;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // UV tengah
                float2 center = i.uv - 0.5;

                // Bentuk diamond
                float dist = abs(center.x) + abs(center.y);

                // Glow pinggir
                float glow = smoothstep(0.5, 0.2, dist);

                // Pulse animation
                float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;

                glow *= (_GlowStrength + pulse);

                return float4(_MainColor.rgb * glow, glow);
            }
            ENDCG
        }
    }
}