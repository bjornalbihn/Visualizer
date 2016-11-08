using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]

public class ProceduralArcLine : ProceduralLine 
{
	[Header("Arc")]
	[SerializeField, Range(-180,180)] private float m_from;
	[SerializeField, Range(-180,180)] private float m_to;

	[SerializeField, Range(-180,180)] private float m_startRoll;
	[SerializeField] private float m_rollSpeed;

	[SerializeField] private float m_radius;

	private float m_roll;

	protected override void Awake ()
	{
		base.Awake ();
		m_roll = m_startRoll;
	}

	private void Update()
	{
		if (m_rollSpeed != 0)
			m_roll +=  m_rollSpeed * Time.deltaTime;
	}

	protected override void SetVertices(float[] heights)
	{
		int steps = m_soundOutput.AmountOfSamples-1;

		for (int i = 0, y = 0; y <= 1; y++) 
		{
			for (int x = 0; x <= steps; x++, i++) 
			{
				float height = y * ((heights[x] * m_height) + m_minHeight);
				float yaw = Mathf.Lerp(m_from, m_to, (float) x /(steps));
				Vector3 linePos = ArcMath.Vector(yaw, 0) * m_radius;
				linePos.z = height;

				if (y != 0)
				{
					Vector3 lineDirection = Vector3.Cross(linePos, Vector3.forward);
					Vector3 rollVector =  Quaternion.AngleAxis(m_roll, lineDirection.normalized) * Vector3.forward;
					linePos = linePos + rollVector.normalized * height;
				}

				m_vertices[i] = linePos;
			}
				
			if (m_from == -180 && m_to == 180)
			{
				m_vertices[i-1] = m_vertices[i-1-steps] = Vector3.Lerp(m_vertices[i-1], m_vertices[i-1-steps],0.5f);
			}
		}

		m_mesh.vertices = m_vertices;

		// tehse might be expensive
		m_mesh.RecalculateNormals();
		m_mesh.RecalculateBounds();
	}

	private void OnDrawGizmos () 
	{
		int steps = m_soundOutput.AmountOfSamples-1;
		Vector3 lastPos = Vector3.zero;
		for (int x = 0; x <= steps; x++) 
		{
			float yaw = Mathf.Lerp(m_from, m_to, (float) x / steps);

			Vector3 linePos = ArcMath.Vector(yaw, 0) * m_radius;
			Vector3 lineDirection = Vector3.Cross(linePos, Vector3.forward);
			Vector3 rollVector =  Quaternion.AngleAxis(m_startRoll, lineDirection.normalized) * Vector3.forward;

			Vector3 topPos = linePos + rollVector.normalized * m_height;

			topPos = transform.TransformPoint(topPos);
			linePos = transform.TransformPoint(linePos);

			lastPos = linePos;

			if (lastPos != Vector3.zero)
			{
				Gizmos.DrawLine(linePos, lastPos);
				Gizmos.DrawLine(linePos, topPos);
			}
		}
	}

}
