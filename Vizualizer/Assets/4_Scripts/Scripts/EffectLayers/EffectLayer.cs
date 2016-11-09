using UnityEngine;
using System.Collections;
using System;

public class EffectLayer : MonoBehaviour 
{
	public bool Active {private set; get;}
	public bool AffectedByInputEffects{protected get; set;}

	public Action<int> OnEffectFired;

	public virtual void SetActive(bool state)
	{
		Active = state;
	}

	public void FireEffect(int effect)
	{
		if (OnEffectFired != null)
			OnEffectFired(effect);
	}
}
