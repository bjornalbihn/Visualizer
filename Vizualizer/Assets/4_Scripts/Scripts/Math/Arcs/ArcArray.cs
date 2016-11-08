using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ArcArray : MonoBehaviour 
{
	public List<Transform> m_transforms;  
	public float m_radius = 10;  

	[Range(1,40)] public int widthAmount = 5;  
	[Range(-180,180)] public float m_widthFrom = 0;  
	[Range(-180,180)] public float m_widthTo = 90; 

	[Range(1,40)] public int heightAmount = 5;  
	[Range(-90,90)] public float m_heightFrom = -30;  
	[Range(-90,90)]public float m_heightTo = 30;  
	
	[SerializeField] private bool m_runOnUpdate;

	[ContextMenu("Update")]
	public void Update()
	{
		if (Application.isPlaying && !m_runOnUpdate)
			return;

		Vector3[] array = GetArcArray();
		for (int i = 0; i<m_transforms.Count; i++)
		{
			if (i<m_transforms.Count)
			{
				m_transforms[i].transform.position = transform.TransformPoint(array[i]);
				//m_transforms[i].LookAt(transform.position);
				m_transforms[i].rotation = Quaternion.LookRotation(m_transforms[i].transform.position - transform.position);
			}
		}
	}
	
	private Vector3[] GetArcArray()
	{
		Vector3[] array = new Vector3[widthAmount*heightAmount];
		int i = 0;

		float widthStep =  (m_widthTo-m_widthFrom) / widthAmount;
		float heightStep =  (m_heightTo-m_heightFrom) / heightAmount;

		for(int h=0; h<heightAmount; h++)  
		{  
			float pitch = m_heightTo - (h * heightStep);

			for (int w = 0; w<widthAmount; w++)
			{
				float yaw = m_widthFrom + (w * widthStep);
				Vector3 pos = ArcMath.Vector(yaw,pitch) * m_radius;
				array[i++] = pos;
			}
		}

		return array;
	}

	private void OnDrawGizmos()
	{
		Vector3[] array = GetArcArray();
		for (int i = 0; i<array.Length; i++)
		{
			Gizmos.color = (i == 0) ? Color.blue : Color.green;
			Gizmos.DrawWireSphere(transform.TransformPoint(array[i]), 0.05f);
		}
	}
}
