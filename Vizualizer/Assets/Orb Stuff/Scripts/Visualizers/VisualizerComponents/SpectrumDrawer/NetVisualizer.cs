using UnityEngine;
using System.Collections;

public class NetVisualizer : SpectrumDrawer 
{
	[SerializeField] private float m_radius = 1;  
	[SerializeField] private float m_neededForRedraw = 1;  
	[SerializeField] private Color[] m_colors;

	protected override void OnEnable()
	{
		GetRandomPositions();
		AdjustLineRenderer();

		samples = new float[samplesAmount];    
		lRenderer.SetVertexCount(samples.Length);  
	} 

	void Update ()  
	{  
		AdjustLineRenderer();

		//Obtain the samples from the frequency bands of the attached AudioSource  
		m_audioSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);  

		//For each sample  
		for(int i=0; i<samples.Length;i++)  
		{  
			if (samples[i]> m_neededForRedraw)
			{
				GetRandomPositions();
				SetRandomColors();
				return;
			}
		}
	}  

	private void GetRandomPositions()
	{
		for (int i = 0; i<samplesAmount; i++)
		{
			//Vector3 pos = transform.TransformPoint(Random.onUnitSphere * m_radius);
			Vector3 pos = Random.onUnitSphere * m_radius;
			lRenderer.SetPosition(i, pos);  
		}
	}

	private void SetRandomColors()
	{
		if (m_colors.Length>0)
		{
			Color startColor = m_colors[Random.Range(0,m_colors.Length)];
			Color endColor = m_colors[Random.Range(0,m_colors.Length)];
			lRenderer.SetColors(startColor, endColor);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,m_radius);
	}
}  