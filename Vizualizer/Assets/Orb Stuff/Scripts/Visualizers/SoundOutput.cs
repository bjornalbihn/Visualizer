using UnityEngine;
using System.Collections;
using System;

public class SoundOutput : MonoBehaviour 
{
	public int AmountOfSamples {get{return m_amountOfSamples;}}

	[SerializeField] private int m_amountOfSamples;
	[SerializeField] private float m_fallSpeed;
	[SerializeField] private float m_minHeight;

	private float[] m_samples;
	private AudioSource m_audioSource;

	public Action<float[]> OnNewData;

	// Use this for initialization
	private void Awake () 
	{
		m_samples = new float[m_amountOfSamples];
		for (int i = 0; i<m_amountOfSamples; i++)
		{
			m_samples[i] = m_minHeight;
		}

		m_audioSource = Core.AudioSource;
	}
	
	// Update is called once per frame
	private void Update () 
	{
		float[] newSamples = new float[m_amountOfSamples]; 
		m_audioSource.GetOutputData(newSamples, 0);
		ProcessData(newSamples);
		SendNewData();
	}

	private void ProcessData(float[] newSamples)
	{
		for (int i = 0; i<m_amountOfSamples; i++)
		{
			m_samples[i] -=   Time.deltaTime * m_fallSpeed;
			if (m_samples[i] < m_minHeight)
				m_samples[i] = m_minHeight;

			if (newSamples[i] > m_samples[i])
				m_samples[i] = newSamples[i];
		}
	}

	private void SendNewData()
	{
		if (OnNewData != null)
			OnNewData(m_samples);
	}

	private void OnDrawGizmos () 
	{
		if (m_samples != null) 
		{
			Gizmos.color = Color.black;
			for (int i = 0; i < m_samples.Length; i++) 
			{
				Vector3 pos = transform.TransformPoint(new Vector3(i*0.1f, m_samples[i] * m_amountOfSamples * .01f));
				Gizmos.DrawSphere(pos, 0.1f);
			}
		}
	}
}
