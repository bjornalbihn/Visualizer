using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEffectTriggers : MonoBehaviour 
{
	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private EffectLayerControl _layerControl;
	[SerializeField] private int _trigger;

	private EffectLayer _currentLayer;
	private float _timer;


	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;

		if (_timer <= 0)
		{
			_timer = _coolDown.Random();
			FireTrigger();
		}
	}

	private void FireTrigger()
	{
		_layerControl.FireEffectOnActiveLayer(_trigger);
	}
}
