using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public static class General
{
	static float[] samples;

	public static T CreateUnderObject<T>(T prefab, Transform parent) where T : MonoBehaviour
	{
		T copy = GameObject.Instantiate<T>(prefab);

		copy.transform.parent = parent;
		copy.transform.localScale = Vector3.one;
		copy.transform.localPosition = Vector3.zero;
		copy.transform.localRotation = Quaternion.identity;

		return copy;
	}

	public static List<T> CreateUnderObjects<T>(T prefab, List<Transform> parents) where T : MonoBehaviour
	{
		List<T> list = new List<T>();

		foreach (Transform parent in parents)
		{
			T copy = CreateUnderObject<T>(prefab, parent);
			list.Add(copy);
		}

		return list;
	}

	public static void KillAllChildren (Transform m_parent)
	{
		for (int i = 0; i<m_parent.childCount; i++)
		{
			GameObject.Destroy (m_parent.GetChild(i).gameObject);
		}
	}

	// not working yet
	/*
	public static float[] GetAudioSpectrum(int inputSamples, int finalCount, AudioListener listener, FFTWindow window)
	{
		float[] samples = new float[inputSamples];
		float[] output = new float[finalCount];

		int count=0;
		float diff = 0;

		listener.GetSpectrumData(samples, 0, window);

		for (int i=0;i<8;++i) {
			float average = 0;

			int sampleCount = (int) Mathf.Pow(2,i)*2;
			for (int j=0;j<sampleCount ;++j) 
			{
				average += samples[count] * (count+1);
				++count;        
			}
			average/=sampleCount;
			diff = Mathf.Clamp(average * 10 - output[i],0,4);
			output[i] = average * 10;
		}

		return output;
	}
	*/
}
