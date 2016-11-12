using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserNetEffectChanger : MonoBehaviour 
{
	[SerializeField] private int _changeColorOnEffectNumber;
	[SerializeField] private int _changePositionsOnEffectNumber;

	private ScaledNetVisualizer[] _netVisualizers;

	private void Awake()
	{
		_netVisualizers = GetComponentsInChildren<ScaledNetVisualizer>();
		EffectLayer layer = GetComponent<EffectLayer>();
		if (layer)
			layer.OnEffectFired += EvaluateEffect;

	}

	void EvaluateEffect(int id)
	{
		if (id == _changeColorOnEffectNumber)
		{
			foreach (ScaledNetVisualizer net in _netVisualizers)
				net.SetRandomColors();
		}
		if (id == _changePositionsOnEffectNumber)
		{
			foreach (ScaledNetVisualizer net in _netVisualizers)
				net.SetNewPositions();
		}
	}

}
