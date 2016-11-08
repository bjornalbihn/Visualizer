using UnityEngine;
using System.Collections;

public class SpectrumDrawer : MonoBehaviour 
{
	public int samplesAmount;  

	protected LineRenderer lRenderer;
	protected float[] samples = new float[64];

	protected AudioSource m_audioSource;

	private void Awake()
	{
		lRenderer = GetComponent<LineRenderer>();
		lRenderer.SetVertexCount(0); 
		m_audioSource = Core.AudioSource;
	}

	protected virtual void OnEnable()
	{
		AdjustLineRenderer();
	}

	protected void AdjustLineRenderer()
	{
		if (samples.Length != samplesAmount)
		{
			samples = new float[samplesAmount];    
			lRenderer.SetVertexCount(samples.Length); 
		}
	} 
}
