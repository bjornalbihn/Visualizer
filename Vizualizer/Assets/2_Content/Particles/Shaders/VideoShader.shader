Shader "Orb/Video Masked" 
{
    Properties 
    {
     	_TintColor ("Color (RGBA)", Color) = (0.5,0.5,0.5,0.5)
        _MainTex ("Base (RGB)", 2D) = "grey" {}
        _Mask ("Alpha (A)", 2D) = "white" {}
    }

    SubShader 
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
       
        ZWrite Off
        Lighting Off

        Blend SrcAlpha OneMinusSrcAlpha
       
        Pass {
            SetTexture [_MainTex] 
            {
                 Combine texture

                 constantColor [_TintColor]
                 Combine constant * texture
            }

            SetTexture[_Mask] 
            {
                Combine previous *  texture
            }
        }
    }
}
