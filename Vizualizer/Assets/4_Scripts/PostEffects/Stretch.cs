using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch : MonoBehaviour
{
    public Material mat;
    [Range(1, 2)]
    public float _width = 1;
    [Range(1, 2)]
    public float _height = 1;

    // Called by the camera to apply the image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Width", _width);
        mat.SetFloat("_Height", _height);

        //mat is the material containing your shader
        Graphics.Blit(source,destination,mat);
    }
}
