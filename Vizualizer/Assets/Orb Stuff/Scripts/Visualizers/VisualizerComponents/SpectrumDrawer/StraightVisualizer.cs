using UnityEngine;
using System.Collections;

public class StraightVisualizer : SpectrumDrawer 
{
	public int samplesAmount = 256;  
	public float x_scale = 1;  
	public float y_scale = 1;  

	public ParticleSystem m_particleSystem ;  
	public float m_particleSpawnIntervals = 0.15f ;  
	public float m_particlethreshold = 0.15f ;  

	private float[] samples = new float[64];  
	private LineRenderer lRenderer;  
	private AudioSource m_audioSource;  

	void Awake ()  
	{  
		m_audioSource = Core.AudioSource;

		samples = new float[samplesAmount];    
		lRenderer = GetComponent<LineRenderer>();  
		lRenderer.SetVertexCount(samples.Length);  
		if (m_particleSystem)
			StartCoroutine(SpawnParticles());
	}  

	void Update ()  
	{  
		AdjustLineRenderer();

		//Obtain the samples from the frequency bands of the attached AudioSource  
		m_audioSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);  


		//For each sample  
		for(int i=0; i<samples.Length;i++)  
		{  
			float height = Mathf.Clamp(samples[i]*(50+i*i),0,50)*y_scale;
			Vector3 pos = new Vector3(i*x_scale, height, 0);
			pos = transform.TransformPoint(pos);
			lRenderer.SetPosition(i, pos);  
		}  
	}  

	private IEnumerator SpawnParticles()
	{
		while(true)
		{
			//For each sample  
			for(int i=0; i<samples.Length;i++)  
			{  
				Vector3 pos = new Vector3(i*x_scale, Mathf.Clamp(samples[i]*(50+i*i),0,50)*y_scale, 0);
				if (Mathf.Abs(pos.y*5)>m_particlethreshold)
				{
					pos = transform.TransformPoint(pos);
					m_particleSystem.Emit(pos, Vector3.zero, m_particleSystem.startSize, m_particleSystem.startLifetime, m_particleSystem.startColor);
				}  

			}
			
			yield return new WaitForSeconds(m_particleSpawnIntervals);
		}
	}
}  