using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ArcLine : MonoBehaviour
{ 
	[SerializeField] private LineRenderer lRenderer;  

	public int m_points = 10;  
	public float m_radius = 1;  
	[Range(-360,360)] public float m_from = 90;  
	[Range(-360,360)] public float m_to = 90;  
 
	private Vector3[] arcPositions = new Vector3[10];
	
	void Awake ()  
	{  
		AdjustLineRenderer();
		arcPositions = GetArc(m_points);
	}  
	
	void AdjustLineRenderer()
	{
		if (arcPositions.Length != m_points)
		{
			lRenderer.SetVertexCount(m_points); 
		}
	} 
	
	void Update ()  
	{  
		AdjustLineRenderer();
		arcPositions = GetArc(m_points);

		for(int i=0; i<m_points;i++)  
		{  
			Vector3 pos = arcPositions[i];
			pos = transform.TransformPoint(pos);
			lRenderer.SetPosition(i, pos);  
		}  
	}  

	private Vector3[] GetArc(int amount)
	{
		Vector3[] arc = new Vector3[amount];
		float widthStep = (m_to-m_from) / amount;
		for (int w = 0; w<amount; w++)
		{
			float yaw = m_to + (w * widthStep);
			Vector3 pos = ArcMath.Vector(yaw,0) * m_radius;
			arc[w] = pos;
		}
		return arc;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		if (m_points>1)
		{
			Vector3[] arc = GetArc(m_points);
			
			Vector3 lastPos = transform.TransformPoint(arc[0]);
			for(int i=1; i<m_points;i++)  
			{  
				Vector3 pos = transform.TransformPoint(arc[i]);
				Gizmos.DrawLine(pos,lastPos);
				lastPos = pos;
			}
		}

	}
}
