using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rotator))]
public class RotatorValueChanger : MonoBehaviour 
{

	[SerializeField] private int _reactOnEffectNumber;

	[SerializeField] private List<Vector3> _rotations;
	[SerializeField] CurveAnimatedFloat _changeTiming;

	int _current;
	private Rotator _rotator;
	private Vector3 _currentRotation;
	private Vector3 _targetRotation;

	private void Awake()
	{
		_rotator = GetComponent<Rotator>();
		_currentRotation = _rotator.Rotation;

		EffectLayer layer = GetComponentInParent<EffectLayer>();
		if (layer)
			layer.OnEffectFired += EvaluateEffect;

		_changeTiming.onNewValue += OnChangeTimingUpdated;
	}

	private void EvaluateEffect(int effect)
	{
		if (_reactOnEffectNumber == effect)
			Cycle(1);
	}

	public void Cycle(int dir)
	{
		_current += dir;
		if (_current >= _rotations.Count)
			_current = 0;
		if (_current < 0)
			_current = _rotations.Count-1;

		_currentRotation = _rotator.Rotation;
		_targetRotation = _rotations[_current];

		_changeTiming.SetValue(0);
		StartCoroutine(_changeTiming.SetValueOverTime(1));
	}

	private void OnChangeTimingUpdated(float amount)
	{
		_rotator.SetRotation(Vector3.Lerp(_currentRotation, _targetRotation, amount));
	}
}
