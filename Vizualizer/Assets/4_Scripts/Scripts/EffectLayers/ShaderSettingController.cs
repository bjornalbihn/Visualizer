using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShaderSettingController : MonoBehaviour 
{
	[SerializeField] private Material _material;
	[SerializeField] private List<ShaderFloat> _floats;
	[SerializeField] private List<ShaderVec4> _vectors;
	[SerializeField] private List<ShaderColor> _colors;

	[SerializeField] CurveAnimatedFloat _changeTiming;

	private List<ShaderValue> _values;

	private void Awake()
	{
		_values = new List<ShaderValue>();
		foreach (ShaderValue shaderValue in _floats) _values.Add(shaderValue);
		foreach (ShaderValue shaderValue in _vectors) _values.Add(shaderValue);
		foreach (ShaderValue shaderValue in _colors) _values.Add(shaderValue);

		foreach (ShaderValue shaderValue in _values) 
			shaderValue.Init(_material);

		_changeTiming.onNewValue += OnChangeTimingUpdated;
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