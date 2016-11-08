// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1544,x:33229,y:32882,varname:node_1544,prsc:2|emission-3778-OUT,alpha-3770-OUT;n:type:ShaderForge.SFN_Tex2d,id:411,x:32782,y:32614,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_411,prsc:2,tex:e0160f53c14d243efad7c1436c7f70f3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1232,x:32757,y:33174,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_1232,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:8609,x:32782,y:32787,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8609,prsc:2,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:9665,x:32678,y:33057,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_9665,prsc:2,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:3770,x:32988,y:33163,varname:node_3770,prsc:2|A-9665-OUT,B-1232-A;n:type:ShaderForge.SFN_Lerp,id:3778,x:32978,y:32675,varname:node_3778,prsc:2|A-411-RGB,B-8609-RGB,T-8609-A;proporder:411-1232-9665-8609;pass:END;sub:END;*/

Shader "Shader Forge/MaskedUnlit" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Alpha ("Alpha", Range(0, 1)) = 1
        _Color ("Color", Color) = (0,0,0,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _Color;
            uniform float _Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 emissive = lerp(_Texture_var.rgb,_Color.rgb,_Color.a);
                float3 finalColor = emissive;
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                fixed4 finalRGBA = fixed4(finalColor,(_Alpha*_Mask_var.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
