using UnityEngine;
using System.Collections;

public class EmitParticlesBasedOnVolume : MonoBehaviour 
{
	public AudioSource aSource;  
	public int samplesAmount = 8;  

	public float m_lowerThreshold = 0.15f ;  

	public ParticleSystem m_particleSystem ;  
	private float[] samples = new float[64];  
	
	void Awake ()  
	{  
		samples = new float[samplesAmount];  
	}  
	
	void AdjustSamples()
	{
		if (samples.Length != samplesAmount)
			samples = new float[samplesAmount];    
	} 
	
	void Update ()  
	{  
		AdjustSamples();
		aSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);  

		int amount = 0;

		//For each sample  
		for(int i=0; i<samples.Length;i++)  
		{  
			if (samples[i] * 100 > m_lowerThreshold)
				amount++;
		}  

		m_particleSystem.Emit(amount);
	}  
}  