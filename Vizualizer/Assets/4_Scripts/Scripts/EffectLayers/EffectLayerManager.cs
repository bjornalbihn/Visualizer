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
		List<string> backgroundKeys = new List<string>() {"1","2","3","4","5","6","7","8","9","0"};
		List<string> midGroundKeys = new List<string>() {"q","w","e","r","t","y","u","i","o","p"};
		List<string> foregroundKeys = new List<string>() {"a","s","d","f","g","h","j","k","l","ö"};

		_foreground.Setup(foregroundKeys);
		_midground.Setup(midGroundKeys);
		_background.Setup(backgroundKeys);
	}

	private void Update()
	{
		InputDevice activeDevice = InputManager.ActiveDevice;

		if (activeDevice != null)
		{
			CheckEffectToggles(activeDevice);
			CheckEffectsFired(activeDevice);
            SendAnalogValues(activeDevice);
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
			FireEffectsOnAllLayers(1);
		if (Input.GetKeyDown(KeyCode.UpArrow))
			FireEffectsOnAllLayers(2);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			FireEffectsOnAllLayers(3);
		if (Input.GetKeyDown(KeyCode.DownArrow))
			FireEffectsOnAllLayers(4);
	}

	private void CheckEffectToggles(InputDevice activeDevice)
	{
		if (activeDevice.DPadLeft.WasPressed)
			_foreground.ToggleEffectsActive();
		if (activeDevice.DPadUp.WasPressed)
			_midground.ToggleEffectsActive();
		if (activeDevice.DPadUp.WasPressed)
			_background.ToggleEffectsActive();
	}

	private void CheckEffectsFired(InputDevice activeDevice)
	{
		if (activeDevice.Action1.WasPressed)
			FireEffectsOnAllLayers(1);
		if (activeDevice.Action2.WasPressed)
			FireEffectsOnAllLayers(2);
		if (activeDevice.Action3.WasPressed)
			FireEffectsOnAllLayers(3);
		if (activeDevice.Action4.WasPressed)
			FireEffectsOnAllLayers(4);
	}
		
	private void FireEffectsOnAllLayers(int effect)
	{
		_foreground.FireEffectOnActiveLayer(effect);
		_midground.FireEffectOnActiveLayer(effect);
		_background.FireEffectOnActiveLayer(effect);
	}

    private void SendAnalogValues(InputDevice activeDevice)
    {
        float leftX = (activeDevice.LeftStick.X + 1) / 2;
        float leftY = (activeDevice.LeftStick.Y + 1) / 2;
        float rightX = (activeDevice.RightStick.X + 1) / 2;
        float rightY = (activeDevice.RightStick.Y + 1) / 2;
        float leftTrigger = activeDevice.LeftTrigger.RawValue;
        float rightTrigger = activeDevice.RightTrigger.RawValue;

        Vector2 leftStick = new Vector2(leftX, leftY);
        Vector2 rightStick = new Vector2(rightX, rightY);

        _foreground.SendAnalogValues(leftStick, rightStick, leftTrigger, rightTrigger);
        _midground.SendAnalogValues(leftStick, rightStick, leftTrigger, rightTrigger);
        _background.SendAnalogValues(leftStick, rightStick, leftTrigger, rightTrigger);
    }
}
