using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerTrigger : MonoBehaviour 
{
	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private EffectLayerControl _layerControl;
	[SerializeField] private List<EffectLayer> _layers;

	private EffectLayer _currentLayer;
	private List<EffectLayer> _layerList = new List<EffectLayer>();
	private int _currentLayerNumber;
	private float _timer;

	private void Awake()
	{
		FillList();
	}

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;

		if (_timer <= 0)
		{
			_timer = _coolDown.Random();
			SetNewLayer();
		}
	}

	private void SetNewLayer()
	{
		_currentLayerNumber++;

		if (_currentLayerNumber >= _layers.Count)
			FillList();

		_currentLayer = _layers[_currentLayerNumber];
		_layerControl.SetActiveLayer(_currentLayer);
	}

	private void FillList()
	{
		_layerList.Clear();
		foreach (EffectLayer layer in _layers)
		{
			_layerList.Add(layer);
			_layerList = _layerList.OrderBy( x => Random.value ).ToList( );
		}
		_currentLayerNumber = 0;
	}

	[ContextMenu("GetLayers")]
	private void GetLayers()
	{
		_layers = _layerControl.Layers;
	}
}
