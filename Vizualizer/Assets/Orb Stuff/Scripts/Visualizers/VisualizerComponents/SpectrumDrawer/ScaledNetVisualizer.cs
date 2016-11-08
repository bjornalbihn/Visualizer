using UnityEngine;
using System.Collections;

public class ScaledNetVisualizer : SpectrumDrawer, AudioProcessor.AudioCallbacks
{
	[SerializeField] private float m_radius = 1;  
	[SerializeField] private Color[] m_colors;
	[SerializeField] private bool m_changeColorOnBeat;
	[SerializeField] private bool m_changePositionsOnBeat;

	protected override void OnEnable()
	{
		Core.AudioProcessor.addAudioCallback(this);

		AdjustLineRenderer();
		GetRandomPositions();

		samples = new float[samplesAmount];    
		lRenderer.SetVertexCount(samples.Length);  
	} 

	void Update ()  
	{  
		AdjustLineRenderer();
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

	private void GetRandomPositions()
	{
		for (int i = 0; i<samplesAmount; i++)
		{
			//Vector3 pos = transform.TransformPoint(Random.onUnitSphere * m_radius);
			Vector3 pos = Random.onUnitSphere * m_radius;
			lRenderer.SetPosition(i, pos);  
		}
	}

		
	public void onBeatDetected()
	{
		if(m_changeColorOnBeat)
			SetRandomColors();
		if(m_changePositionsOnBeat)
			GetRandomPositions();
	}

	public void onSpectrum(float[] spectrum)
	{
		
	}


	private void OnDisable()
	{
		Core.AudioProcessor.removeAudioCallback(this);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position,m_radius);
	}
}  