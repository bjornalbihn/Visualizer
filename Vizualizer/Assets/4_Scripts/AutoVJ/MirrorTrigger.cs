using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MirrorTrigger : AutoVJTrigger 
{
	[SerializeField] private Mirror _mirrorEffect;
	[SerializeField] private int[] _mirrorModes;

	private float _timer;

	protected override void Fire ()
	{
		base.Fire ();

		int effect = _mirrorModes[Random.Range(0,_mirrorModes.Length-1)];
		_mirrorEffect.SetSetting(effect);
	}
		
}
