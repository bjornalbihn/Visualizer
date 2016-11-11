using UnityEngine;
using System.Collections;

public class ConstrainCamera : MonoBehaviour 
{
	[SerializeField] private Transform _position;
	[SerializeField] private Transform _lookAtTarget;

	// Update is called once per frame
	void LateUpdate () 
	{	
		if (_position)
		{
			transform.position = _position.position;
			transform.rotation = _position.rotation;
		}
		if (_lookAtTarget)
			transform.LookAt(_lookAtTarget);
	}
}
