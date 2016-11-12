using UnityEngine;
using System.Collections;

public class ScaledNetVisualizer : SpectrumDrawer, AudioProcessor.AudioCallbacks
{
	[SerializeField] private float m_radius = 1;  
	[SerializeField] private Color[] m_colors;
	[SerializeField] private bool m_changeColorOnBeat;
	[SerializeField] private bool m_changePositionsOnBeat;
	[SerializeField] private float _changeduration;

	private Vector3[] _currentPositions;

	protected override void OnEnable()
	{
		Core.AudioProcessor.addAudioCallback(this);

		samples = new float[samplesAmount];    
		lRenderer.SetVertexCount(samples.Length);  

		AdjustLineRenderer();
		_currentPositions = GetRandomPositions();
		for(int i = 0; i<samplesAmount; i++)
			lRenderer.SetPosition(i, _currentPositions[i]);  
	} 

	void Update ()  
	{  
		AdjustLineRenderer();
	}  

	public void SetRandomColors()
	{
		if (m_colors.Length>0)
		{
			Color startColor = m_colors[Random.Range(0,m_colors.Length)];
			Color endColor = m_colors[Random.Range(0,m_colors.Length)];
			lRenderer.SetColors(startColor, endColor);
		}
	}

	private Vector3[] GetRandomPositions()
	{
		Vector3[] positions = new Vector3[samplesAmount];
		for (int i = 0; i<samplesAmount; i++)
		{
			//Vector3 pos = transform.TransformPoint(Random.onUnitSphere * m_radius);
			positions[i] = Random.onUnitSphere * m_radius;
		}

		return positions;
	}

	public void SetNewPositions()
	{
		StartCoroutine(ChangePositions());
	}

	private IEnumerator ChangePositions()
	{
		Vector3[] newPositions = GetRandomPositions();
		float time = 0;
		while (time<1)
		{
			time += Time.deltaTime /_changeduration;
			time = Mathf.Min(time, 1);
			for (int i = 0; i<samplesAmount; i++)
			{
				Vector3 pos = Vector3.Lerp (_currentPositions[i], newPositions[i], time);
				lRenderer.SetPosition(i, pos);  
			}
			yield return 0;
		}
		_currentPositions = newPositions;
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