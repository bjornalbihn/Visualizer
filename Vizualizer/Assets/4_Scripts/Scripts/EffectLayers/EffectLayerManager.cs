using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using System;

public class EffectLayerManager : MonoBehaviour 
{
	[SerializeField] private EffectLayerControl _foreground;
	[SerializeField] private EffectLayerControl _midground;
	[SerializeField] private EffectLayerControl _background;

	private void Awake()
	{
		List<string> foregroundKeys = new List<string>() {"1","2","3","4","5","6","7","8","9","0"};
		List<string> midgroundKeys = new List<string>() {"a","s","d","f","g","h","j","k","l","ö"};
		List<string> backgroundKeys = new List<string>() {"q","w","e","r","t","y","u","i","o","p"};

		_foreground.Setup(foregroundKeys);
		_midground.Setup(midgroundKeys);
		_background.Setup(backgroundKeys);
	}

	private void Update()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;

		if (activeDevice != null)
		{
			CheckEffectToggles(activeDevice);
			CheckEffectsFired(activeDevice);
		}
	}

	private void CheckEffectToggles(InputDevice activeDevice)
	{
		if (activeDevice.DPadLeft.WasReleased)
			_foreground.ToggleEffectsActive();
		if (activeDevice.DPadUp.WasReleased)
			_midground.ToggleEffectsActive();
		if (activeDevice.DPadUp.WasReleased)
			_background.ToggleEffectsActive();
	}

	private void CheckEffectsFired(InputDevice activeDevice)
	{
		if (activeDevice.Action1.WasReleased)
			FireEffectsOnAllLayers(1);
		if (activeDevice.Action2.WasReleased)
			FireEffectsOnAllLayers(2);
		if (activeDevice.Action3.WasReleased)
			FireEffectsOnAllLayers(3);
		if (activeDevice.Action4.WasReleased)
			FireEffectsOnAllLayers(4);
	}

	private void FireEffectsOnAllLayers(int effect)
	{
		_foreground.FireEffectOnActiveLayer(effect);
		_midground.FireEffectOnActiveLayer(effect);
		_background.FireEffectOnActiveLayer(effect);
	}
}
