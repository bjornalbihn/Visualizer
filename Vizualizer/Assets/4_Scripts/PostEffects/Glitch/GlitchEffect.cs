using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using System;

public class GlitchEffect : ImageEffectBase
{
	[Header("Lerp Values")]
    [SerializeField] private LerpValue _verticalJerkValue;
    [SerializeField] private LerpValue _verticalMovementValue;
    [SerializeField] private LerpValue _bottomStaticValue;
    [SerializeField] private LerpValue _scalinesValue;
    [SerializeField] private LerpValue _rgbOffsetValue;
    [SerializeField] private LerpValue _horzFuzzValue;

    private void Awake()
    {
        _verticalJerkValue.Setup(this, 0);
        _verticalMovementValue.Setup(this, 0);
        _bottomStaticValue.Setup(this, 0);
        _scalinesValue.Setup(this, 0);
        _rgbOffsetValue.Setup(this, 0);
        _horzFuzzValue.Setup(this, 0);

		SetSetting(_settings[1]);
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			Cycle();
		}
    }

	[SerializeField] private List<GlitchSetting> _settings;
	private int _currentSetting;

	private void Cycle()
	{
		_currentSetting = (_currentSetting + 1) % _settings.Count;
		SetSetting(_settings[_currentSetting]);
	}

	void SetSetting(GlitchSetting setting)
    {
        _verticalJerkValue.SetValue(setting.VerticalJerk, false);
        _verticalMovementValue.SetValue(setting.VerticalMovement, false);
        _bottomStaticValue.SetValue(setting.BottomStatic, false);
        _scalinesValue.SetValue(setting.Scanlines, false);
        _rgbOffsetValue.SetValue(setting.RgbOffset, false);
        _horzFuzzValue.SetValue(setting.HorziontalFuzz, false);
    }

    // Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{

        material.SetFloat("_vertJerkOpt", _verticalJerkValue.CurrentValue);
        material.SetFloat("_vertMovementOpt", _verticalMovementValue.CurrentValue);
        material.SetFloat("_bottomStaticOpt", _bottomStaticValue.CurrentValue);
        material.SetFloat("_scalinesOpt", _scalinesValue.CurrentValue);
        material.SetFloat("_rgbOffsetOpt", _rgbOffsetValue.CurrentValue);
        material.SetFloat("_horzFuzzOpt", _horzFuzzValue.CurrentValue);

	    Graphics.Blit (source, destination, material);
	}
}




