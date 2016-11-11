using UnityEngine;
using System.Collections;

public class KeepInFrontOfCameraBehaviour : MonoBehaviour 
{
	[SerializeField]
	private Transform m_centerObject;
	[SerializeField]
	private float m_distance;
	[SerializeField]
	private bool m_lookAtCenter;

	private Transform m_camera;


	// Use this for initialization
	void Start () 
	{
		m_camera = Core.CameraController.CameraTransform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction =  (m_centerObject.position - m_camera.position).normalized;
		transform.position = m_centerObject.position + direction * m_distance;

		if (m_lookAtCenter)
			transform.LookAt(m_centerObject.position);
	}
}
