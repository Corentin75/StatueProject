Shader "Custom/VertexColorShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Rend le matériau transparent
            ZWrite Off // Désactive l'écriture dans le depth buffer pour éviter les conflits de transparence
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR; // Couleur des sommets
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color; // Applique les couleurs des sommets (peinture)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Si l'alpha de la couleur est inférieur à un seuil (pratique pour éviter des artefacts), rendre transparent
                if (i.color.a < 0.1)
                    discard; // Le pixel est invisible s'il est trop transparent

                // Utilise la couleur avec son alpha pour le rendu
                return fixed4(i.color.rgb, i.color.a); // Peinture visible avec l'alpha d'origine
            }
            ENDCG
        }
    }
}