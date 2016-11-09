using UnityEngine;
using System.Collections;
using System;

public class RayMarchingLayer : EffectLayer 
{
	[SerializeField] private ShaderSettingController _shaderSettingController;

	private int _currentSetting;

	public override void SetActive (bool state)
	{
		base.SetActive (state);
		gameObject.SetActive(state);
	}

	public override void FireEffect (int effect)
	{
		base.FireEffect (effect);

		if (effect == 1)
			CycleShaderSetting(1);

		if (effect == 2)
			CycleShaderSetting(-1);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
			CycleShaderSetting(1);
		if (Input.GetKeyDown(KeyCode.P))
			CycleShaderSetting(-1);
	}

	private void CycleShaderSetting(int dir)
	{
		_shaderSettingController.Cycle(dir);
	}
}

