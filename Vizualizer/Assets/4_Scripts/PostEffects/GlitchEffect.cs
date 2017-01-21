using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;

public class GlitchEffect : ImageEffectBase
{

    [SerializeField] private bool _debug;

    [Range(0, 10)] [SerializeField] private float _verticalJerk;
    [Range(0, 10)] [SerializeField] private float _verticalMovement;
    [Range(0, 10)] [SerializeField] private float _bottomStatic;
    [Range(0, 10)] [SerializeField] private float _scalines;
    [Range(0, 10)] [SerializeField] private float _rgbOffset;
    [Range(0, 10)] [SerializeField] private float _horzFuzz;

    [SerializeField] private GlitchSetting[] _settings;
    private int _currentSetting;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CycleSettings();
    }

    void CycleSettings()
    {
        _currentSetting = (_currentSetting + 1) % _settings.Length;
        GlitchSetting setting = _settings[_currentSetting];

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
	    if (!_debug)
	    {
            material.SetFloat("_vertJerkOpt", _verticalJerkValue.CurrentValue);
            material.SetFloat("_vertMovementOpt", _verticalMovementValue.CurrentValue);
            material.SetFloat("_bottomStaticOpt", _bottomStaticValue.CurrentValue);
            material.SetFloat("_scalinesOpt", _scalinesValue.CurrentValue);
            material.SetFloat("_rgbOffsetOpt", _rgbOffsetValue.CurrentValue);
            material.SetFloat("_horzFuzzOpt", _horzFuzzValue.CurrentValue);
	    }
	    else
	    {
            material.SetFloat("_vertJerkOpt", _verticalJerk);
            material.SetFloat("_vertMovementOpt", _verticalMovement);
            material.SetFloat("_bottomStaticOpt", _bottomStatic);
            material.SetFloat("_scalinesOpt", _scalines);
            material.SetFloat("_rgbOffsetOpt", _rgbOffset);
            material.SetFloat("_horzFuzzOpt", _horzFuzz);
	    }

	    Graphics.Blit (source, destination, material);
	}
}

[Serializable]
public class GlitchSetting
{
    [Range(0, 10)] public float VerticalJerk;
    [Range(0, 10)] public float VerticalMovement;
    [Range(0, 10)] public float BottomStatic;
    [Range(0, 10)] public float Scanlines;
    [Range(0, 10)] public float RgbOffset;
    [Range(0, 10)] public float HorziontalFuzz;
}



