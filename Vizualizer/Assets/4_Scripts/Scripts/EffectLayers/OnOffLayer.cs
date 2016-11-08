using UnityEngine;
using System.Collections;

public class OnOffLayer : EffectLayer 
{
	public override void SetActive (bool state)
	{
		base.SetActive (state);
		gameObject.SetActive(state);
	}
}
