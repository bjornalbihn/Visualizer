using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class CameraSmoothAdjust 
{
	[SerializeField] private float turnSpeed = 0.5f;					// Camera rotation speed / sensitivity
	[SerializeField] private float zoomSpeed = 0.3f;					// Zoom distance change speed

	private CameraValues current = new CameraValues(0,0,0);				// Current CameraSetting
	private CameraValues target = new CameraValues(0,0,0);				// Target CameraSetting that we are tweening towards
	
	public CameraValues Target { set { target = value; } get { return target; } }
	public CameraValues Current { set { current = value; } get { return current; } }

	public void Update()
	{
		AdjustPitchAngle();
		AdjustYawAngle();
		AdjustZoom();
	}

	// Lerp the pitch
	public void AdjustPitchAngle()
	{
		if (current.Pitch != target.Pitch)
		{
			current.Pitch = Mathf.Lerp (current.Pitch, target.Pitch, Time.unscaledDeltaTime/turnSpeed);

			// Snap the last part
			if (Mathf.Abs(current.Pitch - target.Pitch) < 0.05)
				current.Pitch = target.Pitch;
		}					
	}
	
	// Lerps the yaw
	public void AdjustYawAngle()
	{
		if (current.Yaw != target.Yaw)
		{
			current.Yaw = Mathf.Lerp (current.Yaw, target.Yaw, Time.unscaledDeltaTime/turnSpeed);

			
			// Snap the last part
			if (Mathf.Abs(current.Yaw - target.Yaw) < 0.05)
				current.Yaw = target.Yaw;
		}
	}
	
	// Lerps the zoom
	public void AdjustZoom()
	{
		if(current.Zoom != target.Zoom)
		{			
			current.Zoom = Mathf.Lerp (current.Zoom, target.Zoom, Time.unscaledDeltaTime/zoomSpeed);
		}
	}
	
	// Stops the camera instantly
	public void FreezeCamera()
	{
		target.CopyValuesFromOther(current);	// lets target assume values from current, making all animations stop
	}
}
