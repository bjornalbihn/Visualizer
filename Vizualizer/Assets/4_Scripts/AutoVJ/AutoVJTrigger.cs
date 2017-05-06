using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoVJTrigger : MonoBehaviour 
{
	[SerializeField] private BeatSync.BeatBehaviour _syncOn;
	[SerializeField] private bool _useCooldownDuringBeatsync;
	[SerializeField] private MinMaxValue _coolDown;

	private float _timer;

	protected virtual void Awake()
	{
		SignUp();
	}

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;
		if (BeatSync.Active == false)
		{
			if (CheckCoolDown())
				Fire();
		}
	}

	private void BeatSyncCheck()
	{
		if (!gameObject.activeSelf)
			return;

		if (!_useCooldownDuringBeatsync || CheckCoolDown())
			Fire();
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


	protected virtual void Fire()
	{
		
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
