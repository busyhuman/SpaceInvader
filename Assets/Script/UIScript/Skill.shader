Shader "Unlit/Skil"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TimeSpeed("time", Float) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            Blend SrcAlpha OneMinusSrcAlpha
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
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float   _TimeSpeed;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = float4(0,0,0,0.5f);
                     float2 v2 = float2(0.5, 0.5) - i.uv;
                    float degree = degrees(atan2(v2.y, v2.x));
                    if (degree < 0) degree += 360;
                    if (degree < _TimeSpeed)
                        col.a = 0;
                    return col;
                }
                ENDCG
            }
        }
}
