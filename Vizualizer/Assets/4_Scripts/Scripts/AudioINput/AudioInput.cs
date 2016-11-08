using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioInput : MonoBehaviour 
{
	[SerializeField] private AudioBridge _audioBridge;
	[SerializeField] private Material _material;

	[SerializeField] private float _smoothing;
	[SerializeField] private Vector4 cloud;
	[SerializeField] private bool _affectCloud;

	[SerializeField] private Vector4 _scale;
	[SerializeField] private bool _affectScale;

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
			if (_affectCloud)
			{
				float cloudX = Mathf.InverseLerp(-100,-150,smoothLevels[0]);
				cloudX =Mathf.Lerp(cloud.x, cloud.y, cloudX);

				float cloudY = Mathf.InverseLerp(-100,-150,smoothLevels[9]);
				cloudY =Mathf.Lerp(cloud.z, cloud.w, cloudY);

				_material.SetVector("_CloudDensity", new Vector4(cloudX, cloudY, 0,0));
			}

			if (_affectScale)
			{
				float x = Mathf.InverseLerp(-100,-150,smoothLevels[5]);
				x =Mathf.Lerp(_scale.x, _scale.y, x);

				float y = Mathf.InverseLerp(-100,-150,smoothLevels[0]);
				y =Mathf.Lerp(_scale.z, _scale.w, y);

				_material.SetVector("_NoiseSize", new Vector4(x, y, 1,1));
			}
				
		}
	}

	private void OnDrawGizmos()
	{
		float[] levels = _audioBridge.Levels;
		for(int i = 0; i<levels.Length; i++)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(new Vector3(i,levels[i],0), .25f);
		}

		Gizmos.DrawSphere(new Vector3(0,0,10), .25f);
	}
}
