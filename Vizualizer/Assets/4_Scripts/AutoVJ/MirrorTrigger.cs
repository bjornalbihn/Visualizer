using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour 
{
	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private Mirror _mirrorEffect;
	[SerializeField] private int[] _mirrorModes;

	private float _timer;

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;

		if (_timer <= 0)
		{
			_timer = _coolDown.Random();
			SetNewMirrorMode();
		}
	}

	private void SetNewMirrorMode()
	{
		int effect = _mirrorModes[Random.Range(0,_mirrorModes.Length-1)];
		_mirrorEffect.SetSetting(effect);
	}
}
