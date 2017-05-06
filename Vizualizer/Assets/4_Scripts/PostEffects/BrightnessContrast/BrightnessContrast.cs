using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;

public class BrightnessContrast : ImageEffectBase
{
	[SerializeField][Range(0, 1)] private float _brightness;
	[SerializeField][Range(0, 1)] private float _contrast;

	void Update()
	{
		if (Input.GetKeyDown(InputMapping.BrightnessUpKey))
			_brightness = Mathf.Clamp01( _brightness + .05f);
		if (Input.GetKeyDown(InputMapping.BrightnessDownKey))
			_brightness = Mathf.Clamp01( _brightness - .05f);
	}

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Brightness", _brightness);
		material.SetFloat("_Contrast", _contrast);
	
		Graphics.Blit (source, destination, material);
	}
}
