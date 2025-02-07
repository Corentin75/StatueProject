Shader "Custom/PaintShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _PaintTex ("Paint Texture", 2D) = "black" { }
        _BrushColor ("Brush Color", Color) = (1, 0, 0, 1)
        _BrushSize ("Brush Size", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _PaintTex;
            float4 _BrushColor;
            float _BrushSize;
            float2 _PaintTex_TexelSize;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 baseColor = tex2D(_MainTex, i.uv);
                half4 paintColor = tex2D(_PaintTex, i.uv);
                half brushEffect = step(0.5, length(i.uv - _PaintTex_TexelSize));

                // Appliquer la peinture sur la texture
                baseColor = lerp(baseColor, _BrushColor, brushEffect * _BrushSize);
                
                return baseColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
