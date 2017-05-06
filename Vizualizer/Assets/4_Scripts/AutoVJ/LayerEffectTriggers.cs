using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEffectTriggers : MonoBehaviour 
{
	[SerializeField] private BeatSync.BeatBehaviour _syncOn;
	[SerializeField] private bool _useCooldownDuringBeatsync;

	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private EffectLayerControl _layerControl;
	[SerializeField] private int _trigger;

	private EffectLayer _currentLayer;
	private float _timer;

	private void Awake()
	{
		SignUp();
	}

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;
		if (BeatSync.Active == false)
		{
			if (CheckCoolDown())
				FireTrigger();
		}
	}

	private void BeatSyncCheck()
	{
		if (!enabled)
			return;

		if (!_useCooldownDuringBeatsync || CheckCoolDown())
			FireTrigger();
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


	private void FireTrigger()
	{
		_layerControl.FireEffectOnActiveLayer(_trigger);
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
