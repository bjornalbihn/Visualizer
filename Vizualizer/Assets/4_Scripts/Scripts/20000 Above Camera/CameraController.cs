using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public CameraPositioner CameraPositioner {get{return positioner;}}
	public Camera Camera {get{return m_camera;}}
	public CameraSmoothAdjust CameraSmoothAdjust {get{return smoothAdjust;}}

	[SerializeField] private Camera m_camera;
	[SerializeField] private float maximumPitch = 88;
	[SerializeField] private float mouseAffectTurnSpeed = 0.2f;	
	[SerializeField] private CameraPositioner positioner;
	[SerializeField] private CameraSmoothAdjust smoothAdjust;
	[SerializeField] private CameraValues startValues;
	[SerializeField] private CameraValues firstValues;

	[SerializeField] private MMBToZoom m_zoomScript;
	[SerializeField] private float m_moveInDelay = 1f;

	private IEnumerator Start()
	{
		yield return 0;
		positioner.CurrentValues.CopyValuesFromOther(firstValues);

		smoothAdjust.Current.CopyValuesFromOther(startValues);
		smoothAdjust.Target.CopyValuesFromOther(firstValues);

		#if UNITY_ANDROID && !UNITY_EDITOR
		enabled = false;
		#endif
	}

	private void Update()
	{
		positioner.CurrentValues = smoothAdjust.Current;

		// Change Angle
		if (Input.GetMouseButtonDown(0) && !m_zoomScript.IsZooming)
		{
			StartCoroutine(ChangeCameraAngle());
		}
	}
	
	// Changes Pitch and Yaw
	IEnumerator ChangeCameraAngle ()
	{
		// Get initial position of the mouse and yaw/pitchAngle from mainCameraController
		Vector3 startPosition = Input.mousePosition;
		
		float startPitchAngle = smoothAdjust.Current.Pitch;
		float startYawAngle = smoothAdjust.Current.Yaw;
		
		float targetPitchAngle = startPitchAngle;
		float targetYawAngle = startYawAngle;

		while (Input.GetMouseButton(0))
		{
			if (m_zoomScript.IsZooming)
			{
				// wait if player is zooming
				while (m_zoomScript.IsZooming)
				{
					yield return 0;
				}
				// restart the fucntion to get new start values
				StartCoroutine(ChangeCameraAngle());
				yield break;
			}


			// Update mouse location
			Vector3 newMousePosition = Input.mousePosition;
			
			// Update targetPitch(y) and targetYaw(x) to match mouse movement
			targetPitchAngle = ((newMousePosition.y - startPosition.y) * -mouseAffectTurnSpeed) + startPitchAngle;
			targetPitchAngle = Mathf.Clamp(targetPitchAngle, -maximumPitch, maximumPitch);
			targetYawAngle = ((newMousePosition.x - startPosition.x) * mouseAffectTurnSpeed) + startYawAngle;
			
			// Update target values
			smoothAdjust.Target.Pitch = targetPitchAngle;
			smoothAdjust.Target.Yaw = targetYawAngle;

			yield return 0;
		}
	}
}
