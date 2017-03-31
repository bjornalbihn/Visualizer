using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotionGraphicsUI
{
	[RequireComponent (typeof (MeshFilter))]
	[RequireComponent (typeof (MeshRenderer))]

	public class MeshMaker : MonoBehaviour 
	{
		private Mesh m_mesh;
		private MeshFilter m_meshFilter;
		private MeshRenderer m_meshRenderer;

		private List<LineData> DATA;

		protected virtual void Awake()
		{
			m_meshFilter = GetComponent<MeshFilter>();
			m_meshRenderer = GetComponent<MeshRenderer>();

			m_mesh = new Mesh();
			m_mesh.name = "Procedural Mesh";
			m_meshFilter.mesh = m_mesh;

			m_mesh.MarkDynamic();
		}

		public void MakeLine(List<LineData> data, bool looping = false)
		{
			DATA = data;

			if (data.Count < 2)
				return;

			int xSegments = data.Count-1;
			int ySegments = 1;

			Vector3[] verts = new Vector3[(xSegments + 1) * (ySegments + 1)];
			Vector2[] uv = new Vector2[verts.Length];
			Color32[] colors = new Color32[verts.Length];

			for (int i = 0, y = 0; y <= ySegments; y++) 
			{
				for (int x = 0; x <= xSegments; x++, i++) 
				{
					//Vector3 normal = (x>0 ? data[x].Position - data[x-1].Position : (data[x+1].Position - data[x].Position)).normalized;

					float normalOffset =  data[x].Width * y - data[x].Pivot;

					verts[i] = data[x].Position + data[x].Normal * normalOffset; 
					//verts[i] = new Vector3(x * m_length /xSegments, y * m_minHeight);
					uv[i] = new Vector2((float) x / xSegments, (float) y / ySegments);
					colors[i] =  data[x].Color;

				}
			}

			m_mesh.Clear();
			m_mesh.vertices = verts;
			m_mesh.colors32 = colors;
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

		[System.Serializable]
		public class LineData
		{
			public Vector3 Position;
			public Vector3 Normal;
			public Color Color;
			public float Width;
			public float Pivot;
		}
	}
}
