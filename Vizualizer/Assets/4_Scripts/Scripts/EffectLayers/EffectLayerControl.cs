using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class EffectLayerControl : MonoBehaviour 
{
	[SerializeField] private bool _inputEffectsActive;
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
		if(_activeLayer)
			_activeLayer.SetActive(false);

		if (_activeLayer != layer)
		{
			_activeLayer = layer;
			_activeLayer.SetActive(true);
		}
		else
			_activeLayer = null;
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
}
