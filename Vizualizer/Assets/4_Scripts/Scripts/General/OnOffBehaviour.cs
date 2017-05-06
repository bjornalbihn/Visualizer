using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffBehaviour : MonoBehaviour 
{
	[SerializeField] private KeyCode _key;
	[SerializeField] private GameObject _target;

	// Update is called once per frame
	void Update () 
	{
		if (_target != null)
		{
			if (Input.GetKeyDown(_key))
				_target.SetActive( !_target.activeSelf);
		}	
	}
}
