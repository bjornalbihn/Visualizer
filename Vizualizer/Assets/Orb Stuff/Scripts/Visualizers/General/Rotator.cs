using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{
	public Vector3 m_rotation;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(m_rotation * Time.deltaTime);
	}
}
