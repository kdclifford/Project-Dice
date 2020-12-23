Shader "Unlit/PlantShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
       _Tint("Tint", Color) = (1,1,1,1)
    _Head("Head", 2D) = "white" {}
          _HeadColour("HeadColour", Color) = (1,1,1,1)
    _Mouth("Mouth", 2D) = "white" {}
                _MouthColour("MouthColour", Color) = (1,1,1,1)
    _Body("Body", 2D) = "white" {}
             _BodyColour("BodyColour", Color) = (1,1,1,1)
    _Other("Other", 2D) = "white" {}
                   _OtherColour("OtherColour", Color) = (1,1,1,1)
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
           sampler2D _Head     ;
           sampler2D _Body     ;
           sampler2D _Mouth    ;
           sampler2D _Other    ;
           float4 _HeadColour ;
           float4 _BodyColour ;
           float4 _MouthColour  ;
           float4 _OtherColour;
           float4 _Tint;


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
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
            fixed4 col1 = (tex2D(_Head, i.uv) * _HeadColour ) ;
            fixed4 col2 = (tex2D(_Mouth, i.uv) * _MouthColour);
            fixed4 col3 = (tex2D(_Body, i.uv) * _BodyColour) ;
            fixed4 col4 = (tex2D(_Other, i.uv) * _OtherColour);
  

            if (col1.a > 0.5f)
            {
                col = col1;
            }

            if (col2.a > 0.5f)
            {
                col = col2;
            }

            if (col3.a > 0.5f)
            {
                col = col3;
            }

            if (col4.a > 0.5f)
            {
                col = col4;
            }



                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
