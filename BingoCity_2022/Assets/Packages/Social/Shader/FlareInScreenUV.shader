// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "BingoBash/UI-Default-BlikInScreenUV"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _ReflTex ("Reflection Texture", 2D) = "white" {}
        _ReflColor ("Reflection Tint", Color) = (1,1,1,1)
        _ReflScale ("Reflection scale", Float) = 1
        _ReflRotation ("Reflection rotation", Float) = 0
        _U_Speed ("Refl U speed", Float) = 0.0
        _V_Speed ("Refl V speed", Float) = 0.0

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(REFLECTION_MULT_SPRITE)] _ReflMultSprite ("Reflection multiply by sprite color", Float) = 0
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

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
            #pragma multi_compile __ REFLECTION_MULT_SPRITE

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
                float4 reflcoord  : TEXCOORD1;
                float4 worldPosition : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _ReflTex;
            
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            
            float4 _ReflColor;
            float _ReflScale;
            
            float _U_Speed;
            float _V_Speed;
            
            float _ReflRotation;
            float _ReflMultSprite;


            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                
                float4 refluv = ComputeGrabScreenPos(v.vertex);
                float s = -sin ( _ReflRotation  );
                float c = cos ( _ReflRotation  );
                float2x2 rotationMatrix = float2x2( c, -s, s, c);
                refluv.xy = mul ( refluv.xy, rotationMatrix );
                
                OUT.reflcoord.xy = refluv.xy * _ReflScale + frac(_Time.y * float2(_U_Speed, _V_Speed));
				OUT.reflcoord.zw = refluv.zw;
                
                

                OUT.color = v.color * _Color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                half4 refl = tex2Dproj(_ReflTex, IN.reflcoord) * color.a * _ReflColor;
                refl.a = 0;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                half4 res = clamp( 0,1, color + refl );
                
                #ifdef REFLECTION_MULT_SPRITE
                res = clamp( 0,1, color + refl * color);
                #endif
             
                return res;
            }
        ENDCG
        }
    }
}
