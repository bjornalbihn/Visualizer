using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotionGraphicsUI
{
	public class FaceCamera : MonoBehaviour 
	{
		void Update () 
		{
			Camera mainCamera = Camera.main;
			if (mainCamera)	
				transform.LookAt(mainCamera.transform.position);
		}
	}
}