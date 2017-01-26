using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScaleWithAudio : MonoBehaviour 
{
	[System.Serializable]
	public class ObjectToScale
	{
		public GameObject _gameObject;
		public int _band;
	}

	[SerializeField] private AudioBridge _audioBridge;
	private List<ObjectToScale> _objectsToScale = new List<ObjectToScale>();
	[SerializeField] private float _smoothing;
	[SerializeField] private float _minExpectedVolume = -55f;
	[SerializeField] private float _maxExpectedVolume = -35f;

	private float[] smoothLevels = new float[10];

	void Update () 
	{
		float[] newLevels = _audioBridge.Levels;

		for (int i = 0; i < newLevels.Length; i++)
		{
			if (smoothLevels[i] == 0)
				smoothLevels[i] = newLevels[i];
			else
				smoothLevels[i] = Mathf.Lerp(smoothLevels[i], newLevels[i], Time.deltaTime * _smoothing);
		}

		if (smoothLevels.Length > 0)
		{
			foreach (ObjectToScale o in _objectsToScale)
			{
				float scaleValue = smoothLevels [o._band];
				scaleValue = Mathf.Clamp01 ((scaleValue - _minExpectedVolume) / (_maxExpectedVolume - _minExpectedVolume)) + 0.5f;
				o._gameObject.transform.localScale = new Vector3 (scaleValue, scaleValue, scaleValue);
			}
		}
	}

    public void AddObjectToScale(ObjectToScale objectToScale)
    {
        _objectsToScale.Add(objectToScale);
    }
}
