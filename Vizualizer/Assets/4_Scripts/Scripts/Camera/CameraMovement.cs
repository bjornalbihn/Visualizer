using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	[SerializeField] private float _speed;

	private void Update()
	{
		Vector3 input = Vector3.zero;
		input.x = Input.GetAxis("Horizontal");
		input.z = Input.GetAxis("Vertical");

		//Vector3 dir = input * Quaternion.Euler(transform.forward);
		Vector3 targetPos = transform.position + input * _speed;
			
		transform.position = Vector3.Lerp(transform.position, targetPos , Time.deltaTime);
		//transform.position += input * _speed;

	}
}
