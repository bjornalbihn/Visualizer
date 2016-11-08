using UnityEngine;
using System.Collections;

public class ArcVisualizer : SpectrumDrawer 
{
	[SerializeField]
	private bool m_updateArcEveryFrame = true;

	public float m_radius = 1;  
	[Range(-360,360)] public float m_to = 90;  
	public float y_scale = 1;  
	public float cutOff = 75;  

	public ParticleSystem m_particleSystem ;  
	public float m_particleSpawnIntervals = 0.15f ;  
	public float m_particlethreshold = 0.15f ;  

	private Vector3[] arcPositions;

	protected override void OnEnable()
	{
		AdjustLineRenderer();

		samples = new float[samplesAmount];    
		lRenderer.SetVertexCount(samples.Length);  

		arcPositions = GetArc();

		if (m_particleSystem)
			StartCoroutine(SpawnParticles());
	} 
		
	void Update ()  
	{  
		AdjustLineRenderer();
		if (m_updateArcEveryFrame)
			arcPositions = GetArc();

		//Obtain the samples from the frequency bands of the attached AudioSource  
		m_audioSource.GetSpectrumData(this.samples,0,FFTWindow.BlackmanHarris);  

		//For each sample  
		for(int i=0; i<samples.Length;i++)  
		{  
			float height = Mathf.Clamp(samples[i]*(cutOff+i*i),0,cutOff)*y_scale;
			Vector3 pos = arcPositions[i];
			pos.z = height;
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
				float height = Mathf.Clamp(samples[i]*(cutOff+i*i),0,cutOff)*y_scale;
				Vector3 pos = arcPositions[i];
				pos.z = height;

				if (Mathf.Abs(pos.z*5)>m_particlethreshold)
				{
					pos = transform.TransformPoint(pos);
					m_particleSystem.Emit(pos, Vector3.zero, m_particleSystem.startSize, m_particleSystem.startLifetime, m_particleSystem.startColor);
				}  

			}
			
			yield return new WaitForSeconds(m_particleSpawnIntervals);
		}
	}

	private Vector3[] GetArc()
	{
		Vector3[] arc = new Vector3[samplesAmount];    //generates a lot of gc
		float widthStep = m_to / samples.Length;
		for (int w = 0; w<samples.Length; w++)
		{
			float yaw = w * widthStep;
			Vector3 pos = ArcMath.Vector(yaw,0) * m_radius;
			arc[w] = pos;
		}
		return arc;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		Vector3[] arc = GetArc();

		Vector3 lastPos = transform.TransformPoint(arc[0]);
		for(int i=1; i<samples.Length;i++)  
		{  
			Vector3 pos = transform.TransformPoint(arc[i]);
			Gizmos.DrawLine(pos,lastPos);
			lastPos = pos;
		}
	}
}  