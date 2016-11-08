using UnityEngine;
using System.Collections;

public class NoiseMovement : MonoBehaviour 
{
	[SerializeField] private AudioBridge _audioBridge;
	[SerializeField] private Vector4 _movement;

	[SerializeField] private float _smoothing;

	float[] smoothLevels = new float[10];

	private void Update()
	{
		float[] newLevels = _audioBridge.Levels;

		for (int i = 0; i<newLevels.Length; i++)
		{
			if (smoothLevels[i] == 0)
				smoothLevels[i] = newLevels[i];
			else
				smoothLevels[i] = Mathf.Lerp(smoothLevels[i], newLevels[i], Time.deltaTime * _smoothing);
		}


		if (smoothLevels.Length > 0)
		{
			float X = Mathf.InverseLerp(-100,-150,smoothLevels[0]);
			X =Mathf.Lerp(_movement.x, _movement.y, X);

			float Y = Mathf.InverseLerp(-100,-150,smoothLevels[9]);
			Y =Mathf.Lerp(_movement.z, _movement.w, Y);
				
			transform.localPosition = new Vector3(X, Y, 0);

		}
	}
}
