using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class EffectLayerControl : MonoBehaviour 
{
	[SerializeField] private int _startingLayer = -1;
	[SerializeField] private bool _inputEffectsActive;
	[SerializeField] private bool _canBeToggledOff;
	[SerializeField] private List<EffectLayer> _layers;

	private Dictionary<string, EffectLayer> _library = new Dictionary<string, EffectLayer>();
	private EffectLayer _activeLayer;

	public void Setup(List<string> keys)
	{
		for(int i = 0; i<_layers.Count; i++)
		{
			if (i<keys.Count)
			{
				_library.Add(keys[i],_layers[i]);
			}

			_layers[i].SetActive(false);
		}

		if (_startingLayer > 0 && _startingLayer <_layers.Count)
			SetActiveLayer(_layers[_startingLayer]);
		
	}

	private void Update()
	{
		// Keyboard layer selection
		foreach (KeyValuePair<string, EffectLayer> kvp in _library)
		{
			if (Input.GetKeyDown(kvp.Key))
			{
				SetActiveLayer(kvp.Value);
				return;
			}
		}
	}
		
	private void SetActiveLayer(EffectLayer layer)
	{
		EffectLayer previousActive = _activeLayer;

		if (previousActive != layer)
		{
			_activeLayer = layer;
			_activeLayer.SetActive(true);
			if (previousActive)
				previousActive.SetActive(false);
		}
		else if (_canBeToggledOff)
		{
			_activeLayer = null;
			if (previousActive)
				previousActive.SetActive(false);
		}
		
	}

	public void ToggleEffectsActive()
	{
		_inputEffectsActive = !_inputEffectsActive;
	}

	public void FireEffectOnActiveLayer(int effect)
	{
		if (_activeLayer && _inputEffectsActive)
			_activeLayer.FireEffect(effect);
	}

    public void SendAnalogValues(Vector2 leftStick, Vector2 rightStick, float leftTrigger, float rightTrigger)
    {
        if (_activeLayer && _inputEffectsActive)
            _activeLayer.SetAnalogValues(leftStick, rightStick, leftTrigger, rightTrigger);
    }
}
