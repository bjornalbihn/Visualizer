using UnityEngine;
using System.Collections;
using System;

public class EffectLayer : MonoBehaviour 
{
	public bool Active {private set; get;}
	public bool AffectedByInputEffects{protected get; set;}

	public Action<int> OnEffectFired;
    public Action<Vector2, Vector2, float, float> OnAnalogValuesChanged;

	public virtual void SetActive(bool state)
	{
		Active = state;
	}

	public void FireEffect(int effect)
	{
		if (OnEffectFired != null)
			OnEffectFired(effect);
	}

    public void SetAnalogValues(Vector2 leftStick, Vector2 rightStick, float leftTrigger, float rightTrigger)
    {
        if (OnAnalogValuesChanged != null)
            OnAnalogValuesChanged(leftStick, rightStick, leftTrigger, rightTrigger);
    }
}
