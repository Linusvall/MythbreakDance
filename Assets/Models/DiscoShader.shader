Shader "Unlit/DiscoShader"
{
    Properties
    {
        _Speed ("Animation Speed", Float) = 1.0
        _Color1 ("Color 1", Color) = (10, 0, 0, 1)    // Red
        _Color2 ("Color 2", Color) = (0, 0, 10, 1)
       
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

            float _Speed;
            float4 _Color1;
            float4 _Color2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 gridUV = floor(i.uv * 6);
                float checker = fmod(gridUV.x + gridUV.y, 2);
                float secondsPerBeat = 60.0 / _Speed; // Convert BPM to seconds per beat
                float currentBeat = floor(_Time.y / secondsPerBeat) % 2;
                checker = abs(checker - currentBeat);
                float3 color = lerp(_Color1.rgb, _Color2.rgb, checker);
                return fixed4(color * 2, 1.0);
            
            }
            ENDCG
        }
    }
}
