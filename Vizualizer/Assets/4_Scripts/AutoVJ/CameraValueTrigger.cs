using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraValueTrigger : MonoBehaviour 
{
	[SerializeField] private MinMaxValue _coolDown;
	[SerializeField] private CameraController _cameraController;
	[SerializeField] private ControllerRotation _controllerRotation;
	[SerializeField] private List<CameraValues> _values;

	private List<CameraValues> _valuesList = new List<CameraValues>();
	private int _currentValueNumber;
	private float _timer;

	private void Awake()
	{
		FillList();
	}

	private void OnEnable()
	{
		_controllerRotation.enabled = false;
	}
		
	private void OnDisable()
	{
		_controllerRotation.enabled = true;
	}

	private void Update()
	{
		_timer -= Time.unscaledDeltaTime;

		if (_timer <= 0)
		{
			_timer = _coolDown.Random();
			SetNewValue();
		}
	}

	private void SetNewValue()
	{
		_currentValueNumber++;

		if (_currentValueNumber >= _values.Count)
			FillList();

		_cameraController.CameraSmoothAdjust.Target.Pitch =_values[_currentValueNumber].Pitch;
		_cameraController.CameraSmoothAdjust.Target.Yaw =_values[_currentValueNumber].Yaw;
		_cameraController.CameraSmoothAdjust.Target.Zoom =_values[_currentValueNumber].Zoom;
	}

	private void FillList()
	{
		_valuesList.Clear();
		foreach (CameraValues layer in _values)
		{
			_valuesList.Add(layer);
			_valuesList = _valuesList.OrderBy( x => Random.value ).ToList( );
		}
		_currentValueNumber = 0;
	}
}
