Shader "Mapa/cropp"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Img ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        //_StencilComp ("Stencil Comparison", Float) = 8
       // _Stencil ("Stencil ID", Float) = 0


        _OffsetX ("OffsetX", Float) = 0
        _OffsetY ("OffsetY", Float) = 0
        _Zoom ("Zoom", Float) = 0

        _point ("Point Texture", 2D) = "white" {}
        _pointX ("_pointX", Float) = 0
        _pointY ("_pointY", Float) = 0
        _pointLarge ("point large", Float) = 10

        //_StencilOp ("Stencil Operation", Float) = 0
        //_StencilWriteMask ("Stencil Write Mask", Float) = 255
        //_StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _Img;
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;

            float _Zoom;
            float _OffsetX;
            float _OffsetY;
            
            sampler2D _point;
            float _pointX;
            float _pointY;
            float _pointLarge;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                _OffsetX-=1.0f;
                float2 offset=float2(_OffsetX,-_OffsetY);
                float center=(0.5)-((1.0f/_Zoom)*0.5f);

                half4 image = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                half4 color= tex2D(_Img, (IN.texcoord/_Zoom+offset)+center+0.5f) * IN.color;
                color.a=image.a;


                /*if(((IN.texcoord/_Zoom+offset)+center+0.5f).x>_pointX)
                    if(((IN.texcoord/_Zoom+offset)+center+0.5f).x<_pointX+0.01f)
                        if(((IN.texcoord/_Zoom+offset)+center+0.5f).y>_pointY)
                            if(((IN.texcoord/_Zoom+offset)+center+0.5f).y<_pointY+0.01f)
                                {
                                    color.r=1.0f;
                                    color.g=0.0f;
                                    color.b=0.0f;
                                }*/


                _pointX-=_pointLarge/15;
                _pointY-=_pointLarge/15;
                float2 offpoint=float2(_pointX,-_pointY);

                center=(0.5)-((_pointLarge/(_Zoom))*0.5f);
                half4 alt= tex2D(_point, ((IN.texcoord*_pointLarge)/_Zoom+(offset+offpoint)*_pointLarge)+center+0.5f);
                if(alt.a>0.0f)
                    color=alt;
                //color.a+=alt.a;

                float black=color.rgba.r+color.rgba.g+color.rgba;
                if(black==0)
                    color.rgba.a=0;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
                //float alx=((IN.texcoord/_Zoom+offset)+center+0.5f).x;
                //float aly=((IN.texcoord/_Zoom+offset)+center+0.5f).y;
                /*if((2*alx.x+0.2)/2.0>_pointX)
                    if((2*aly.x+0.2)/2.0>_pointY)
                            color.r=1.0f;*/

                return color;
            }
        ENDCG
        }
    }
}
