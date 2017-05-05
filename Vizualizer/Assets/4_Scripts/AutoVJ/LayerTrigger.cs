using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LayerTrigger : MonoBehaviour 
{
	[SerializeField] private BeatSync.BeatBehaviour _syncOn;
	[SerializeField] private bool _useCooldownDuringBeatsync;

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
		SignUp();
	}
		
	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;
		if (BeatSync.Active == false)
		{
			if (CheckCoolDown())
				SetNewLayer();
		}
	}

	private void BeatSyncCheck()
	{
		if (!_useCooldownDuringBeatsync || CheckCoolDown())
			SetNewLayer();
	}

	private bool CheckCoolDown()
	{
		if (_timer <= 0)
		{
			_timer = _coolDown.Random();
			return true;
		}
		return false;
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
			_layerList = _layerList.OrderBy( x => UnityEngine.Random.value ).ToList( );
		}
		_currentLayerNumber = 0;
	}

	[ContextMenu("GetLayers")]
	private void GetLayers()
	{
		_layers = _layerControl.Layers;
	}

	private void SignUp()
	{
		switch(_syncOn)
		{
			case BeatSync.BeatBehaviour.OnBeat : BeatSync.OnBeat += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnOne : BeatSync.OnOne += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnTwo : BeatSync.OnTwo += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnThree : BeatSync.OnThree += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnFour : BeatSync.OnFour += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnOneAndThree : BeatSync.OnOneAndThree += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnTwoAndFour : BeatSync.OnTwoAndFour += BeatSyncCheck; break;
			case BeatSync.BeatBehaviour.OnFourBar : BeatSync.OnFourBar += BeatSyncCheck; break;
		}
	}
}
