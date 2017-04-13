using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using UnityEngine;
using System;

public class Mirror : ImageEffectBase
{
	[SerializeField] private MirrorValues _values;
	private MirrorSetting _currentSetting;

	[Header("Lerp Values")]
    [SerializeField] private LerpValue _horizontalValue;
    [SerializeField] private LerpValue _verticalValue;

    private void Awake()
    {
        _horizontalValue.Setup(this, 0);
        _verticalValue.Setup(this, 0);
        SetSetting(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) SetSetting(0);
        if (Input.GetKeyDown(KeyCode.X)) SetSetting(1);
        if (Input.GetKeyDown(KeyCode.C)) SetSetting(2);
        if (Input.GetKeyDown(KeyCode.V)) SetSetting(3);
        if (Input.GetKeyDown(KeyCode.B)) SetSetting(4);
        if (Input.GetKeyDown(KeyCode.N)) SetSetting(5);
        if (Input.GetKeyDown(KeyCode.M)) SetSetting(6);
        if (Input.GetKeyDown(KeyCode.Comma)) SetSetting(7);
    }

    public void SetSetting(int setting)
    {
		_currentSetting = _values.SetSetting(setting);

		_horizontalValue.SetValue(_currentSetting.Horizontal, false);
		_verticalValue.SetValue(_currentSetting.Vertical, false);
    }

// Called by the camera to apply the image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
		if (!_values.Debug)
        {
            material.SetFloat("_XMirror", _horizontalValue.CurrentValue);
			material.SetFloat("_YMirror", _verticalValue.CurrentValue);
        }
        else
        {
			material.SetFloat("_XMirror", _values.DebugSetting.Horizontal);
			material.SetFloat("_YMirror", _values.DebugSetting.Vertical);
        }
			
        //mat is the material containing your shader
		Graphics.Blit(source,destination,material);
    }
}

