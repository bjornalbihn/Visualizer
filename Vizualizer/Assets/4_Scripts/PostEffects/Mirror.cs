using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Material mat;

// Called by the camera to apply the image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {

        //mat is the material containing your shader
        Graphics.Blit(source,destination,mat);
    }
}
