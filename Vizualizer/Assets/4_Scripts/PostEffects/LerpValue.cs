using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LerpValue
{
    [SerializeField] private CurveAnimatedValue _value;
    public float CurrentValue { private set; get; }

    private float _startValue;
    private float _targetValue;
    private MonoBehaviour _owner;
    private Coroutine _coroutine;

    public void Setup(MonoBehaviour owner, float value)
    {
        _owner = owner;
        SetValue(value,true);
        _value.OnValue += NewValue;
    }

    public void SetValue(float value, bool immediate)
    {
        if (immediate)
            CurrentValue = _startValue = _targetValue = value;
        else
        {
            _startValue = CurrentValue;
            _targetValue = value;

            if(_coroutine != null)
                _owner.StopCoroutine(_coroutine);

            _coroutine =  _owner.StartCoroutine(_value.SetNewValue(true));
        }

    }

    private void NewValue(float value)
    {
        CurrentValue = Mathf.Lerp(_startValue, _targetValue, value);
    }
}