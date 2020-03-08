Shader "Unlit/BG_sin"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GameTime ("GameTime", float) = 0
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
            float _GameTime;

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
                

                float osciSpeed = _GameTime;
                float osciHeight = 0.05;
                float horizScrollSpeed = _GameTime/30;

                float2 newUV = float2( i.uv[0] + horizScrollSpeed , i.uv[1] * osciHeight * (sin(osciSpeed) + 20));
                fixed4 mainTex = tex2D(_MainTex, newUV);
                // sample the texture

                float4 colorSin = float4(1, 1, 1 ,1);

				fixed4 col = mainTex;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
