Shader "Orb/Video No Mask" 
{
    Properties 
    {
     	_TintColor ("Color (RGBA)", Color) = (0.5,0.5,0.5,0.5)
        _MainTex ("Base (RGB)", 2D) = "grey" {}
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
        }
    }
}
