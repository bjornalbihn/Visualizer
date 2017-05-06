using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayerTrigger : AutoVJTrigger 
{
	[SerializeField] private EffectLayerControl _layerControl;
	[SerializeField] private List<EffectLayer> _layers;

	private EffectLayer _currentLayer;
	private List<EffectLayer> _layerList = new List<EffectLayer>();
	private int _currentLayerNumber;

	protected override void Awake()
	{
		base.Awake();
		FillList(); 
	}
		
	protected override void Fire ()
	{
		base.Fire ();

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
			_layerList = _layerList.OrderBy( x => UnityEngine.Random.value ).ToList( );
		}
		_currentLayerNumber = 0;
	}

	[ContextMenu("GetLayers")]
	private void GetLayers()
	{
		_layers = _layerControl.Layers;
	}
}
