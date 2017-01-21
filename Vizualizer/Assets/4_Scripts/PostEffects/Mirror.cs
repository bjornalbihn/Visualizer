using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mirror : MonoBehaviour
{
    [SerializeField] private bool _debug;
    public Material mat;
    [Range(0, 1)] public float _horizontal;
    [Range(0, 1)] public float _vertical;

    [SerializeField] private MirrorSetting[] _settings;
    private int _currentSetting;

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

    void SetSetting(int setting)
    {
        _currentSetting = (setting) % _settings.Length;
        MirrorSetting newSetting = _settings[_currentSetting];

        _horizontalValue.SetValue(newSetting.Horizontal, false);
        _verticalValue.SetValue(newSetting.Vertical, false);
    }

// Called by the camera to apply the image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination)
    {
        if (!_debug)
        {
            mat.SetFloat("_XMirror", _horizontalValue.CurrentValue);
            mat.SetFloat("_YMirror", _verticalValue.CurrentValue);
        }
        else
        {
            mat.SetFloat("_XMirror", _horizontal);
            mat.SetFloat("_YMirror", _vertical);
        }


        //mat is the material containing your shader
        Graphics.Blit(source,destination,mat);
    }
}

[Serializable]
public class MirrorSetting
{
    [Range(0, 1)] public float Horizontal;
    [Range(0, 1)] public float Vertical;
}

