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
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _PaintTex;
            float4 _BrushColor;
            float _BrushSize;

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

                // Mélanger la couleur de base avec la couleur de peinture
                half4 finalColor = lerp(baseColor, paintColor, paintColor.a);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
