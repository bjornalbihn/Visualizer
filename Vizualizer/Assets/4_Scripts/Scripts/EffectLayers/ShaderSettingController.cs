using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShaderSettingController : MonoBehaviour 
{
	[SerializeField] private int _reactOnEffectNumber;

	[SerializeField] private Material _material;
	[SerializeField] private List<ShaderFloat> _floats;
	[SerializeField] private List<ShaderVec4> _vectors;
	[SerializeField] private List<ShaderColor> _colors;

	[SerializeField] CurveAnimatedFloat _changeTiming;

    [SerializeField] private AnalogAxis _reactOnAnalogAxis;

	private List<ShaderValue> _values;

    enum AnalogAxis
    {
        None,
        leftX,
        leftY,
        rightX,
        rightY,
        leftTrigger,
        rightTrigger
    }

	private void Awake()
	{
		EffectLayer layer = GetComponent<EffectLayer>();
		if (layer)
        {
			layer.OnEffectFired += EvaluateEffect;
            layer.OnAnalogValuesChanged += EvaluateAnalogInput;
        }

		_values = new List<ShaderValue>();
		foreach (ShaderValue shaderValue in _floats) _values.Add(shaderValue);
		foreach (ShaderValue shaderValue in _vectors) _values.Add(shaderValue);
		foreach (ShaderValue shaderValue in _colors) _values.Add(shaderValue);

		foreach (ShaderValue shaderValue in _values) 
			shaderValue.Init(_material);

		_changeTiming.onNewValue += OnChangeTimingUpdated;
	}

	private void EvaluateEffect(int effect)
	{
		if (_reactOnEffectNumber == effect)
			Cycle(1);
	}

    private void EvaluateAnalogInput(Vector2 leftStick, Vector2 rightStick, float leftTrigger, float rightTrigger)
    {
        float result = 0;

        switch (_reactOnAnalogAxis)
        {
            case AnalogAxis.None:
                return;
            case AnalogAxis.leftX:
                result = leftStick.x;
                break;
            case AnalogAxis.leftY:
                result = leftStick.y;
                break;
            case AnalogAxis.rightX:
                result = rightStick.x;
                break;
            case AnalogAxis.rightY:
                result = rightStick.y;
                break;
            case AnalogAxis.leftTrigger:
                result = leftTrigger;
                break;
            case AnalogAxis.rightTrigger:
                result = rightTrigger;
                break;
        }

        foreach (ShaderValue shaderValue in _values)
            shaderValue.SetDirect(result);
    }

	public void Cycle(int dir)
	{
		foreach (ShaderValue shaderValue in _values) 
			shaderValue.Cycle(dir);

		_changeTiming.SetValue(0);
		StartCoroutine(_changeTiming.SetValueOverTime(1));
	}

	private void OnChangeTimingUpdated(float amount)
	{
		foreach (ShaderValue shaderValue in _values) 
			shaderValue.Update(amount);
	}
}

[Serializable]
public class ShaderValue 
{
	[SerializeField] protected string _property;

	protected int _current;
	protected Material _material;

	public void Init(Material material)
	{
		_material = material;
		GetCurrentValue();
		SetTargetValue();
	}

	public virtual void Cycle(int dir)
	{
		_current += dir;
	}

	protected virtual void GetCurrentValue()
	{
	}

	protected virtual void SetTargetValue()
	{
	}

	public virtual void Update(float amount)
	{
	}

    public virtual void SetDirect(float amount)
    {
    }
}

[Serializable]
public class ShaderFloat : ShaderValue 
{
	[SerializeField] private float[] _values;

	private float _materialValue;
	private float _targetValue;

	public override void Cycle(int dir)
	{
		base.Cycle(dir);
		if (_current >= _values.Length)
			_current = 0;
		if (_current < 0)
			_current = _values.Length-1;

		GetCurrentValue();
		SetTargetValue();
	}

	protected override void SetTargetValue ()
	{
		_targetValue = (_values[_current]);
	}

	protected override void GetCurrentValue ()
	{
		_materialValue = _material.GetFloat(_property);
	} 

	public override void Update(float amount)
	{
		_material.SetFloat(_property, Mathf.Lerp(_materialValue, _targetValue, amount));
	}

    public override void SetDirect(float amount)
    {
        if (_values.Length > 1)
            _material.SetFloat(_property, Mathf.Lerp(_values[0], _values[1], amount));
    }
}

[Serializable]
public class ShaderVec4 : ShaderValue 
{
	[SerializeField] private Vector4[] _values;

	private Vector4 _materialValue;
	private Vector4 _targetValue;

	public override void Cycle(int dir)
	{
		base.Cycle(dir);
		if (_current >= _values.Length)
			_current = 0;
		if (_current < 0)
			_current = _values.Length-1;

		GetCurrentValue();
		SetTargetValue();
	}

	protected override void SetTargetValue ()
	{
		_targetValue = (_values[_current]);
	}

	protected override void GetCurrentValue ()
	{
		_materialValue = _material.GetVector(_property);
	} 

	public override void Update(float amount)
	{
		_material.SetVector(_property, Vector4.Lerp(_materialValue, _targetValue, amount));
	}
}

[Serializable]
public class ShaderColor : ShaderValue 
{
	[SerializeField] private Color[] _values;

	private Color _materialValue;
	private Color _targetValue;

	public override void Cycle(int dir)
	{
		base.Cycle(dir);
		if (_current >= _values.Length)
			_current = 0;
		if (_current < 0)
			_current = _values.Length-1;

		GetCurrentValue();
		SetTargetValue();
	}

	protected override void SetTargetValue ()
	{
		_targetValue = (_values[_current]);
	}

	protected override void GetCurrentValue ()
	{
		_materialValue = _material.GetColor(_property);
	} 

	public override void Update(float amount)
	{
		_material.SetColor(_property, Color.Lerp(_materialValue, _targetValue, amount));
	}
}