using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshRenderer))]

public class ProceduralLine : MonoBehaviour 
{
	[SerializeField] protected SoundOutput m_soundOutput;

	[SerializeField] protected float m_length;
	[SerializeField] protected float m_height;
	[SerializeField] protected float m_minHeight;

	[SerializeField] private ColorByBeat m_colorByBeat;

	protected Mesh m_mesh;
	private MeshFilter m_meshFilter;
	private MeshRenderer m_meshRenderer;

	protected Vector3[] m_vertices;

	protected virtual void Awake()
	{
		m_soundOutput.OnNewData += SetVertices;

		m_meshFilter = gameObject.GetComponent<MeshFilter>();
		m_meshRenderer = gameObject.GetComponent<MeshRenderer>();

		m_mesh = new Mesh();
		m_mesh.name = "Procedural Line";
		m_meshFilter.mesh = m_mesh;

		m_mesh.MarkDynamic();
		Generate();

		m_colorByBeat.OnNewColor += SetColor;
		m_colorByBeat.Activate();
	}

	private void SetColor(Color newColor)
	{
		Color32[] colors = new Color32[m_vertices.Length];
		Color32 color = newColor;
		for (int i = 0; i < m_vertices.Length; i++) 
			colors[i] = newColor;

		m_mesh.colors32 = colors;
	}

	private void Generate () 
	{
		int xSegments = m_soundOutput.AmountOfSamples-1;
		int ySegments = 1;

		m_vertices = new Vector3[(xSegments + 1) * (ySegments + 1)];
		Vector2[] uv = new Vector2[m_vertices.Length];

		for (int i = 0, y = 0; y <= ySegments; y++) 
		{
			for (int x = 0; x <= xSegments; x++, i++) 
			{
				m_vertices[i] = new Vector3(x * m_length /xSegments, y * m_minHeight);
				uv[i] = new Vector2((float) x / xSegments, (float) y / ySegments);
			}
		}

		m_mesh.vertices = m_vertices;
		m_mesh.uv = uv;

		CreateTriangles(xSegments, ySegments);
	}

	protected virtual void CreateTriangles(int xSegments, int ySegments)
	{
		int[] triangles = new int[xSegments * ySegments * 6];
		for (int ti = 0, vi = 0, y = 0; y < ySegments; y++, vi++) {
			for (int x = 0; x < xSegments; x++, ti += 6, vi++) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + xSegments + 1;
				triangles[ti + 5] = vi + xSegments + 2;
			}
		}

		m_mesh.triangles = triangles;
		m_mesh.RecalculateNormals(); // Better to set up self
	}

	protected virtual void SetVertices(float[] heights)
	{
		for (int i = 0, y = 0; y <= 1; y++) 
		{
			for (int x = 0; x <= m_soundOutput.AmountOfSamples-1; x++, i++) 
			{
				m_vertices[i] = new Vector3(x * m_length /m_soundOutput.AmountOfSamples-1, y * ((heights[x] * m_height) + m_minHeight));
			}
		}

		m_mesh.vertices = m_vertices;
	}

	/*
	private void OnDrawGizmos () 
	{
		if (m_vertices != null) 
		{
			Gizmos.color = Color.black;
			for (int i = 0; i < m_vertices.Length; i++) 
			{
				Vector3 pos = transform.TransformPoint(m_vertices[i]);
				Gizmos.DrawSphere(pos, 0.1f);
			}
		}
	}
	*/
}
