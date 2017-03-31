using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoVJ : MonoBehaviour 
{
	[SerializeField] private GameObject[] _effectTriggers;

	bool _state;

	// Use this for initialization
	void Awake () 
	{
		SetAllActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Backspace))
			SetAllActive(!_state);	
	}

	private void SetAllActive(bool state)
	{
		_state = state;
		foreach (GameObject go in _effectTriggers)
			go.SetActive(state);
	}
}
