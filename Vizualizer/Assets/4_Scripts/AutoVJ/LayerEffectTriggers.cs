using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LayerEffectTriggers : AutoVJTrigger 
{
	[SerializeField] private EffectLayerControl _layerControl;
	[SerializeField] private int _trigger;

	protected override void Fire ()
	{
		base.Fire ();
		_layerControl.FireEffectOnActiveLayer(_trigger);
	}
}
