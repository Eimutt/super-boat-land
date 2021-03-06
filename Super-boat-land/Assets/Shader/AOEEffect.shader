﻿Shader "Unlit/AOEEffect"
{
   Properties{
        _Color ("Tint", Color) = (0, 0, 0, 1.0)
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", float) = 1.0
        _TimeFraction("Input radius", float) = 0.2
    }

    SubShader{
        Tags{ 
            "RenderType"="Opaque" 
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        
        ZWrite On
        ZTest Off

        Pass{

            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Radius;
            float _TimeFraction;
            fixed4 _Color;

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f{
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v){
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET{
                //fixed4 col = tex2D(_MainTex, i.uv);
                float d = distance(float2(0.5,0.5), i.uv);
                fixed4 col = (1-step(_Radius, d))*step(_Radius-0.01f, d);
                
                fixed4 insideCol = 1-step(_Radius*_TimeFraction, d);
                col += insideCol;
                
                return (col)*_Color;
            }

            ENDCG
        }
    }
}
