using UnityEngine;
using System.Collections;

public class PinchToZoom : MonoBehaviour
{
	[SerializeField] private CameraSmoothAdjust m_smoothAdjust;
	[SerializeField] private float m_zoomSpeed = 0.5f;

	[SerializeField] private float m_minZoom = 3f;
	[SerializeField] private float m_maxZoom = 10f;
	
	void Update()
	{
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			float currentTargetZoom = m_smoothAdjust.Target.Zoom;
			currentTargetZoom = deltaMagnitudeDiff * m_zoomSpeed;
			currentTargetZoom = Mathf.Clamp(m_minZoom, m_maxZoom, currentTargetZoom);
			m_smoothAdjust.Target.Zoom = currentTargetZoom;
		}
	}
}