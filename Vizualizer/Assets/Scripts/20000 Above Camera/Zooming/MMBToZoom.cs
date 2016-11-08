using UnityEngine;
using System.Collections;

public class MMBToZoom : MonoBehaviour
{
	[SerializeField] private CameraSmoothAdjust m_smoothAdjust;
	[SerializeField] private float m_zoomSpeed = 0.5f;

	[SerializeField] private float m_minZoom = 3f;
	[SerializeField] private float m_maxZoom = 10f;

	public bool IsZooming;

	void Update()
	{
		// If there are two touches on the device...
		if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButton(1))
		{
			if (!IsZooming)
				StartCoroutine(ChangeCameraZoom());
		}
	}

	// Changes Pitch and Yaw
	public IEnumerator ChangeCameraZoom ()
	{
		IsZooming = true;

		// Get initial position of the mouse and yaw/pitchAngle from mainCameraController
		Vector3 startPosition = Input.mousePosition;
		
		float startZoom = m_smoothAdjust.Current.Zoom;
		float targetZoom = m_smoothAdjust.Current.Zoom;
		
		while (Input.GetMouseButton(2) || Input.GetKey(KeyCode.Z) || Input.GetMouseButton(1))
		{
			// Update mouse location
			Vector3 newMousePosition = Input.mousePosition;
			
			targetZoom = ((newMousePosition.y - startPosition.y) * m_zoomSpeed) + startZoom;
			m_smoothAdjust.Target.Zoom = Mathf.Clamp(targetZoom, m_minZoom, m_maxZoom);
			
			yield return 0;
		}

		IsZooming = false;
	}
}