// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1101,x:33059,y:32704,varname:node_1101,prsc:2|diff-1023-OUT,emission-2916-OUT,amdfl-4184-OUT,alpha-865-OUT;n:type:ShaderForge.SFN_Cubemap,id:7286,x:32533,y:33019,ptovrint:False,ptlb:CubeMap,ptin:_CubeMap,varname:node_7286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:2502a9eff32216046b604bc89a5d0f30,pvfc:0;n:type:ShaderForge.SFN_Multiply,id:4184,x:32732,y:33019,varname:node_4184,prsc:2|A-7286-RGB,B-7286-A,C-4758-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4758,x:32527,y:33192,ptovrint:False,ptlb:CubePower,ptin:_CubePower,varname:node_4758,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Slider,id:7432,x:32694,y:33192,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_7432,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:7786,x:32442,y:32417,ptovrint:False,ptlb:Logo,ptin:_Logo,varname:node_7786,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:481f8be22665349ef870ee90c7ba95c0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:8648,x:32614,y:32706,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8648,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1023,x:32829,y:32622,varname:node_1023,prsc:2|A-5865-OUT,B-8648-RGB;n:type:ShaderForge.SFN_Color,id:3830,x:32442,y:32601,ptovrint:False,ptlb:BaseOrbColor,ptin:_BaseOrbColor,varname:node_3830,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:5865,x:32635,y:32475,varname:node_5865,prsc:2|A-3830-RGB,B-7786-RGB,T-7786-A;n:type:ShaderForge.SFN_Fresnel,id:4921,x:32410,y:32791,varname:node_4921,prsc:2|EXP-5879-OUT;n:type:ShaderForge.SFN_Multiply,id:2916,x:32345,y:32949,varname:node_2916,prsc:2|A-4921-OUT,B-9487-RGB,C-5850-OUT,D-9487-A;n:type:ShaderForge.SFN_Slider,id:5879,x:32039,y:32809,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:_Opacity_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:7.017094,max:16;n:type:ShaderForge.SFN_Color,id:9487,x:32118,y:32949,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:node_9487,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:0.5;n:type:ShaderForge.SFN_Vector1,id:5850,x:32168,y:33099,varname:node_5850,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:865,x:32874,y:32842,varname:node_865,prsc:2|A-7786-A,B-7432-OUT;proporder:7286-4758-7432-7786-8648-3830-5879-9487;pass:END;sub:END;*/

Shader "Shader Forge/OrbExteriorTransparent" {
    Properties {
        _CubeMap ("CubeMap", Cube) = "_Skybox" {}
        _CubePower ("CubePower", Float ) = 4
        _Opacity ("Opacity", Range(0, 1)) = 1
        _Logo ("Logo", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        [HDR]_BaseOrbColor ("BaseOrbColor", Color) = (0.5,0.5,0.5,1)
        _Fresnel ("Fresnel", Range(0, 16)) = 7.017094
        [HDR]_FresnelColor ("FresnelColor", Color) = (0.5,0.5,0.5,0.5)
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
            uniform float4 _LightColor0;
            uniform samplerCUBE _CubeMap;
            uniform float _CubePower;
            uniform float _Opacity;
            uniform sampler2D _Logo; uniform float4 _Logo_ST;
            uniform float4 _Color;
            uniform float4 _BaseOrbColor;
            uniform float _Fresnel;
            uniform float4 _FresnelColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _CubeMap_var = texCUBE(_CubeMap,viewReflectDirection);
                indirectDiffuse += (_CubeMap_var.rgb*_CubeMap_var.a*_CubePower); // Diffuse Ambient Light
                float4 _Logo_var = tex2D(_Logo,TRANSFORM_TEX(i.uv0, _Logo));
                float3 diffuseColor = (lerp(_BaseOrbColor.rgb,_Logo_var.rgb,_Logo_var.a)*_Color.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = (pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)*_FresnelColor.rgb*2.0*_FresnelColor.a);
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Logo_var.a*_Opacity));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Opacity;
            uniform sampler2D _Logo; uniform float4 _Logo_ST;
            uniform float4 _Color;
            uniform float4 _BaseOrbColor;
            uniform float _Fresnel;
            uniform float4 _FresnelColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Logo_var = tex2D(_Logo,TRANSFORM_TEX(i.uv0, _Logo));
                float3 diffuseColor = (lerp(_BaseOrbColor.rgb,_Logo_var.rgb,_Logo_var.a)*_Color.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * (_Logo_var.a*_Opacity),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
