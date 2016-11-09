using UnityEngine;
using System.Collections;

public class EffectLayer : MonoBehaviour 
{
	public bool Active {private set; get;}
	public bool AffectedByInputEffects{protected get; set;}

	public virtual void SetActive(bool state)
	{
		Active = state;
	}

	public virtual void FireEffect(int effect)
	{
		
	}
}
