using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour 
{
	[SerializeField] private BeatSync.BeatBehaviour _syncOn;
	[SerializeField] private bool _useCooldownDuringBeatsync;

	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private Mirror _mirrorEffect;
	[SerializeField] private int[] _mirrorModes;

	private float _timer;

	private void Awake()
	{
		SignUp();
	}

	private void SetNewMirrorMode()
	{
		int effect = _mirrorModes[Random.Range(0,_mirrorModes.Length-1)];
		_mirrorEffect.SetSetting(effect);
	}

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;
		if (BeatSync.Active == false)
		{
			if (CheckCoolDown())
				SetNewMirrorMode();
		}
	}

	private void BeatSyncCheck()
	{
		if (!enabled)
			return;

		if (!_useCooldownDuringBeatsync || CheckCoolDown())
			SetNewMirrorMode();
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
