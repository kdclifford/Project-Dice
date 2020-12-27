Shader "Custom/ForceField"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    _SecondTex("Force Field", 2D) = "white" {}
            _Scroll("TextureScroll", Range(0,1)) = 0.0
        _Color ("Color", Color) = (1,1,1,1)
             [Toggle]   _Force("Force", Range(0,1)) = 0
    }
SubShader
{
    Tags { "RenderType" = "Opaque" }
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
        sampler2D _SecondTex;
        float4 _Color;
        float4 _MainTex_ST;
        half _Scroll;
        float _Force;

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
            fixed4 col;
   

        // sample the texture

       col = tex2D(_MainTex, i.uv);

      // fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
       fixed4 newCol = (0, 0, 0, 0);
       fixed4 secondTex = tex2D(_SecondTex, i.uv);
       fixed2 scroll = (_Scroll, _Scroll);

       if (_Force == 1)
       {
           if (secondTex.r != 0 && secondTex.g != 0 && secondTex.b != 0)
           {
               if (secondTex.a == 1)
               {
                   col += tex2D(_SecondTex, ((i.uv * 4) + scroll) * 0.5f) * _Color;
               }
           }
       }

       
        // apply fog
        UNITY_APPLY_FOG(i.fogCoord, col);
        return col;
    }
    ENDCG
}
}
}
