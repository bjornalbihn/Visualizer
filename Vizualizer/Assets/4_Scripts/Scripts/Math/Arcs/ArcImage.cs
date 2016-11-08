using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ArcImage : MonoBehaviour 
{
	[SerializeField] private float radius = 30;
	[SerializeField] private int widthSegments = 24;
	[SerializeField] private int heightSegments = 16;

	[Range(0f,180f)][SerializeField] private float width = 90;
	[Range(0f,90f)][SerializeField] private float height = 30;

	[SerializeField] private bool m_invertFaces;
	[SerializeField] private bool m_invertNormals;
	[SerializeField] private bool m_bent;
	[SerializeField] private int m_gridSize;

	[SerializeField] private MeshFilter m_meshFilter;
	[SerializeField] private MeshRenderer m_meshRenderer;

	[SerializeField] private bool m_runInEditor;
	
	void Awake()
	{
		CreatePartialSphere();
	}

	void Update()
	{
		if (!Application.isPlaying && m_runInEditor)	
			CreatePartialSphere ();
	}

	[ContextMenu("Create")]
	void CreatePartialSphere()
	{
		Mesh mesh = m_meshFilter.mesh;
		mesh.Clear();
		
		#region Vertices
		Vector3[] vertices = (m_bent) ? Bent() : LongLat();
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = (m_invertNormals) ? -vertices[n].normalized: vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		for( int lat = 0; lat < heightSegments; lat++ )
			for( int lon = 0; lon <= widthSegments; lon++ )
				uvs[lon + lat * (widthSegments + 1)] = new Vector2( (float)lon / widthSegments, 1f - (float)(lat) / (heightSegments-1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];

		int i = 0;
		for( int lat = 0; lat < heightSegments - 1; lat++ )
		{
			for( int lon = 0; lon < widthSegments; lon++ )
			{
				int current = lon + lat * (widthSegments + 1);
				int next = current + widthSegments + 1;

				if (m_invertFaces)
				{
					triangles[i++] = current;
					triangles[i++] = next + 1;
					triangles[i++] = current + 1;
					
					triangles[i++] = current;
					triangles[i++] = next;
					triangles[i++] = next + 1;
				}
				else
				{
					triangles[i++] = current;
					triangles[i++] = current + 1;
					triangles[i++] = next + 1;
					
					triangles[i++] = current;
					triangles[i++] = next + 1;
					triangles[i++] = next;
				}

			}
		}
		#endregion
		
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		mesh.RecalculateBounds();
		mesh.Optimize();
	}

	private Vector3[] Bent()
	{
		Vector3[] vertices = new Vector3[(widthSegments+1) * heightSegments];



		float xStep = (float) (width*2) /(float)widthSegments ;
		float yStep = (float) (height*2) /(float)heightSegments ;
		
		for( int y = 0; y < heightSegments; y++ )
		{
			float yPos = (-height) + (y * yStep);
			
			for( int x = 0; x <= widthSegments; x++ )
			{
				float xPos = (-width) + (x * xStep);
				Vector3 vector = SetVertex(x, y, 0);
				
				vertices[ x + y * (widthSegments + 1)] = vector.normalized * radius;
			}
		}



		return vertices;
	}

	private Vector3 SetVertex (int x, int y, int z) {
		Vector3 v = new Vector3(x, y, z) * 2f / m_gridSize - Vector3.one;
		float x2 = v.x * v.x;
		float y2 = v.y * v.y;
		float z2 = v.z * v.z;
		Vector3 s;
		s.x = v.x * Mathf.Sqrt(1f - y2 / 2f - z2 / 2f + y2 * z2 / 3f);
		s.y = v.y * Mathf.Sqrt(1f - x2 / 2f - z2 / 2f + x2 * z2 / 3f);
		s.z = v.z * Mathf.Sqrt(1f - x2 / 2f - y2 / 2f + x2 * y2 / 3f);
		return s * radius;
	}


	private Vector3[] LongLat()
	{
		Vector3[] vertices = new Vector3[(widthSegments+1) * heightSegments];
		float xStep = (float) (width*2) /(float)widthSegments ;
		float yStep = (float) (height*2) /(float)heightSegments ;
		
		for( int lat = 0; lat < heightSegments; lat++ )
		{
			float y = (-height) + (lat * yStep);
			
			for( int lon = 0; lon <= widthSegments; lon++ )
			{
				float x = (-width) + (lon * xStep);
				Vector3 vector = ArcMath.Vector(x, y);
				
				vertices[ lon + lat * (widthSegments + 1)] = vector * radius;
			}
		}

		return vertices;
	}


}
