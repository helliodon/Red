Shader "Unlit/ParalaxShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {} // Main texture
        _ScrollSpeedX ("Scroll Speed X", Float) = 0.1 // Horizontal scroll speed
        _ScrollSpeedY ("Scroll Speed Y", Float) = 0.1 // Vertical scroll speed
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // Proper transparency blending

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _ScrollSpeedX;
            float _ScrollSpeedY;
            
            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                
                // Scroll UVs dynamically
                o.uv = v.uv + float2(_ScrollSpeedX * _Time.y, _ScrollSpeedY * _Time.y);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv); // Sample texture with scrolled UV
            }
            ENDCG
        }
    }
}
