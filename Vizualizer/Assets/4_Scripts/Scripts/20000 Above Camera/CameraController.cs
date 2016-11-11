using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform CameraTransform {get{return m_camera;}}
	public CameraPositioner CameraPositioner {get{return positioner;}}
	public CameraSmoothAdjust CameraSmoothAdjust {get{return smoothAdjust;}}

	[SerializeField] private Transform m_camera;
	[SerializeField] private float maximumPitch = 88;
	[SerializeField] private float mouseAffectTurnSpeed = 0.2f;	
	[SerializeField] private CameraValues firstValues;

	[Header("Movement")]
	[SerializeField] private MMBToZoom m_zooming;
	[SerializeField] private CameraPositioner positioner;
	[SerializeField] private CameraSmoothAdjust smoothAdjust;

	private IEnumerator Start()
	{
		yield return 0;
		positioner.CurrentValues.CopyValuesFromOther(firstValues);

		smoothAdjust.Current.CopyValuesFromOther(firstValues);
		smoothAdjust.Target.CopyValuesFromOther(firstValues);
	}

	private void Update()
	{
		smoothAdjust.Update();
		positioner.Update(this);

		positioner.CurrentValues = smoothAdjust.Current;

		// Change Angle
		if (Input.GetMouseButtonDown(0) && !m_zooming.IsZooming)
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
			if (m_zooming.IsZooming)
			{
				// wait if player is zooming
				while (m_zooming.IsZooming)
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
