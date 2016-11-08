using UnityEngine;
using System.Collections;

public class CameraPositioner : MonoBehaviour 
{
	[SerializeField] private Transform transformAffected;
	[SerializeField] private bool isEnabled;					// remove later?
	[SerializeField] private CameraValues currentValues;		

	public Transform TransformAffected {get{return transformAffected;}}
	public CameraValues CurrentValues {get{return currentValues;} set{currentValues = value;}}
	
	void Update()
	{
		SetCameraAngle();
	}
	
	public void SetCameraAngle()
	{
		if (isEnabled)
		{
			Vector3 position = transform.position;

			Quaternion rotation = Quaternion.Euler(-currentValues.Pitch, currentValues.Yaw, 0);
			transformAffected.transform.position = position + (rotation * Vector3.forward * currentValues.Zoom);

			//transformAffected.LookAt(position,Vector3.up);
			rotation = Quaternion.Euler(currentValues.Pitch, currentValues.Yaw+180, 0);
			transformAffected.rotation = rotation;

		}
	}
}
