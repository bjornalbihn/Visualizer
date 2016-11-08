using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraValues
{
	[SerializeField] private float pitch;		// up and down
	[SerializeField] private float yaw;			// Left and right
	[SerializeField] private float zoom;
	
	public float Pitch	
	{
		get{return pitch;} 
		set
		{
			pitch = Mathf.Clamp(value, -90,90); // Make sure yaw never leaves -90 to 90 space
		}
	}
	
	public float Yaw  	{get{return yaw;} 	set{yaw = value;}}
	public float Zoom 	{get{return zoom;} 	set{zoom = value;}}
	
	// Constructor
	public CameraValues(float pitch, float yaw, float zoom)
	{
		Pitch = pitch;
		Yaw = yaw;
		Zoom = zoom;
	}
	
	public Vector3 ToVector3()
	{
		return new Vector3(pitch, yaw, zoom);
	}
	
	public void CopyValuesFromOther(CameraValues other)
	{
		Pitch = other.Pitch;
		Yaw = other.Yaw;
		Zoom = other.Zoom;
	}
}
