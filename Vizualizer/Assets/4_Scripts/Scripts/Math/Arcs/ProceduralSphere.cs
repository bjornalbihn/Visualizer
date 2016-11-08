using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ProceduralSphere : MonoBehaviour 
{
	[SerializeField] private Material m_material;

	[SerializeField] private float radius = 30;
	[SerializeField] private int longitudeSteps = 24;
	[SerializeField] private int latitudeSteps = 16;

	[Range(-180,0)][SerializeField] private int longitudeFrom = 0;
	[Range(0,180)][SerializeField] private int longitudeTo = 90;
	[Range(-90,0)][SerializeField] private int latitudeFrom = -30;
	[Range(0,90)][SerializeField] private int latitudeTo = 30;

	[SerializeField] private bool m_invertFaces;
	[SerializeField] private bool m_invertNormals;

	[SerializeField] private bool m_360Video;

	[SerializeField] private MeshFilter m_meshFilter;
	[SerializeField] private MeshRenderer m_meshRenderer;

	void Update()
	{
		if (m_360Video)
			Create360VideoSphere();
		else
			CreatePartialSphere();
	}

	void CreatePartialSphere()
	{
		Mesh mesh = m_meshFilter.mesh;
		mesh.Clear();
		
		#region Vertices
		Vector3[] vertices = new Vector3[(longitudeSteps+1) * latitudeSteps];
		float xStep = (float) (longitudeTo-longitudeFrom) /(float)longitudeSteps ;
		float yStep = (float) (latitudeTo-latitudeFrom) /(float)latitudeSteps ;

		for( int lat = 0; lat < latitudeSteps; lat++ )
		{
			float y = latitudeFrom + lat * yStep;
			
			for( int lon = 0; lon <= longitudeSteps; lon++ )
			{
				float x = longitudeFrom + lon * xStep;
				Vector3 vector = ArcMath.Vector(x, y);
				
				vertices[ lon + lat * (longitudeSteps + 1)] = vector * radius;
			}
		}
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = (m_invertNormals) ? -vertices[n].normalized: vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		for( int lat = 0; lat < latitudeSteps; lat++ )
			for( int lon = 0; lon <= longitudeSteps; lon++ )
				uvs[lon + lat * (longitudeSteps + 1)] = new Vector2( (float)lon / longitudeSteps, 1f - (float)(lat) / (latitudeSteps-1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];

		int i = 0;
		for( int lat = 0; lat < latitudeSteps - 1; lat++ )
		{
			for( int lon = 0; lon < longitudeSteps; lon++ )
			{
				int current = lon + lat * (longitudeSteps + 1);
				int next = current + longitudeSteps + 1;

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

	void Create360VideoSphere()
	{
		Mesh mesh = m_meshFilter.mesh;
		mesh.Clear();
		
		#region Vertices
		Vector3[] vertices = new Vector3[(longitudeSteps+1) * (latitudeSteps)];
		float xStep = (float) (longitudeTo-longitudeFrom) /(float)longitudeSteps ;
		float yStep = (float) (latitudeTo-latitudeFrom) /(float)latitudeSteps +1;
		
		for( int lat = 0; lat < latitudeSteps; lat++ )
		{
			float y = latitudeFrom + lat * yStep;
			if (lat == latitudeSteps-1)
				y = latitudeTo;
			
			for( int lon = 0; lon <= longitudeSteps; lon++ )
			{
				float x = longitudeFrom + lon * xStep;
				Vector3 vector = ArcMath.Vector(x, y);
				
				vertices[ lon + lat * (longitudeSteps + 1)] = vector * radius;
			}
		}
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = (m_invertNormals) ? -vertices[n].normalized: vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		for( int lat = 0; lat < latitudeSteps; lat++ )
			for( int lon = 0; lon <= longitudeSteps; lon++ )
				uvs[lon + lat * (longitudeSteps + 1)] = new Vector2( (float)lon / longitudeSteps, 1f - (float)(lat) / (latitudeSteps-1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];
		
		int i = 0;
		for( int lat = 0; lat < latitudeSteps - 1; lat++ )
		{
			for( int lon = 0; lon < longitudeSteps; lon++ )
			{
				int current = lon + lat * (longitudeSteps + 1);
				int next = current + longitudeSteps + 1;
				
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


	void CreateSphere()
	{
		Mesh mesh = m_meshFilter.mesh;
		mesh.Clear();

		#region Vertices
		Vector3[] vertices = new Vector3[(longitudeSteps+1) * latitudeSteps + 2];
		float _pi = Mathf.PI;
		float _2pi = _pi * 2f;
		
		vertices[0] = Vector3.up * radius;
		for( int lat = 0; lat < latitudeSteps; lat++ )
		{
			float a1 = _pi * (float)(lat+1) / (latitudeSteps+1);
			float sin1 = Mathf.Sin(a1);
			float cos1 = Mathf.Cos(a1);
			
			for( int lon = 0; lon <= longitudeSteps; lon++ )
			{
				float a2 = _2pi * (float)(lon == longitudeSteps ? 0 : lon) / longitudeSteps;
				float sin2 = Mathf.Sin(a2);
				float cos2 = Mathf.Cos(a2);
				
				vertices[ lon + lat * (longitudeSteps + 1) + 1] = new Vector3( sin1 * cos2, cos1, sin1 * sin2 ) * radius;
			}
		}
		vertices[vertices.Length-1] = Vector3.up * -radius;
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		uvs[0] = Vector2.up;
		uvs[uvs.Length-1] = Vector2.zero;
		for( int lat = 0; lat < latitudeSteps; lat++ )
			for( int lon = 0; lon <= longitudeSteps; lon++ )
				uvs[lon + lat * (longitudeSteps + 1) + 1] = new Vector2( (float)lon / longitudeSteps, 1f - (float)(lat+1) / (latitudeSteps+1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];
		
		//Top Cap
		int i = 0;
		for( int lon = 0; lon < longitudeSteps; lon++ )
		{
			triangles[i++] = lon+2;
			triangles[i++] = lon+1;
			triangles[i++] = 0;
		}
		
		//Middle
		for( int lat = 0; lat < latitudeSteps - 1; lat++ )
		{
			for( int lon = 0; lon < longitudeSteps; lon++ )
			{
				int current = lon + lat * (longitudeSteps + 1) + 1;
				int next = current + longitudeSteps + 1;
				
				triangles[i++] = current;
				triangles[i++] = current + 1;
				triangles[i++] = next + 1;
				
				triangles[i++] = current;
				triangles[i++] = next + 1;
				triangles[i++] = next;
			}
		}
		
		//Bottom Cap
		for( int lon = 0; lon < longitudeSteps; lon++ )
		{
			triangles[i++] = vertices.Length - 1;
			triangles[i++] = vertices.Length - (lon+2) - 1;
			triangles[i++] = vertices.Length - (lon+1) - 1;
		}
		#endregion
		
		mesh.vertices = vertices;
		mesh.normals = normales;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		mesh.RecalculateBounds();
		mesh.Optimize();
	}


	/*

	void CreatePartialSphere()
	{
		Mesh mesh = m_meshFilter.mesh;
		mesh.Clear();
		
		#region Vertices
		Vector3[] vertices = new Vector3[(longitudeSteps+1) * latitudeSteps + 2];
		float xStep = (float) (longitudeTo-longitudeFrom) /(float)longitudeSteps ;
		float yStep = (float) (latitudeTo-latitudeFrom) /(float)latitudeSteps ;

		vertices[0] = Vector3.up * radius;
		for( int lat = 0; lat < latitudeSteps; lat++ )
		{
			float y = latitudeFrom + lat * yStep;
			
			for( int lon = 0; lon <= longitudeSteps; lon++ )
			{
				float x = longitudeFrom + lon * xStep;
				Vector3 vector = ArcMath.Vector(x, y);
				
				vertices[ lon + lat * (longitudeSteps + 1) + 1] = vector * radius;
			}
		}
		vertices[vertices.Length-1] = Vector3.up * -radius;
		#endregion
		
		#region Normales		
		Vector3[] normales = new Vector3[vertices.Length];
		for( int n = 0; n < vertices.Length; n++ )
			normales[n] = (m_invertNormals) ? -vertices[n].normalized: vertices[n].normalized;
		#endregion
		
		#region UVs
		Vector2[] uvs = new Vector2[vertices.Length];
		uvs[0] = Vector2.up;
		uvs[uvs.Length-1] = Vector2.zero;
		for( int lat = 0; lat < latitudeSteps; lat++ )
			for( int lon = 0; lon <= longitudeSteps; lon++ )
				uvs[lon + lat * (longitudeSteps + 1) + 1] = new Vector2( (float)lon / longitudeSteps, 1f - (float)(lat) / (latitudeSteps-1) );
		#endregion
		
		#region Triangles
		int nbFaces = vertices.Length;
		int nbTriangles = nbFaces * 2;
		int nbIndexes = nbTriangles * 3;
		int[] triangles = new int[ nbIndexes ];
		
		//Top Cap

		int i = 0;
		for( int lon = 0; lon < longitudeSteps; lon++ )
		{
			triangles[i++] = lon+2;
			triangles[i++] = lon+1;
			triangles[i++] = 0;
		}

	
	//Middle
	for( int lat = 0; lat < latitudeSteps - 1; lat++ )
	{
		for( int lon = 0; lon < longitudeSteps; lon++ )
		{
			int current = lon + lat * (longitudeSteps + 1) + 1;
			int next = current + longitudeSteps + 1;
			
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
	
	//Bottom Cap

		for( int lon = 0; lon < longitudeSteps; lon++ )
		{
			triangles[i++] = vertices.Length - 1;
			triangles[i++] = vertices.Length - (lon+2) - 1;
			triangles[i++] = vertices.Length - (lon+1) - 1;
		}

	#endregion
	
	mesh.vertices = vertices;
	mesh.normals = normales;
	mesh.uv = uvs;
	mesh.triangles = triangles;
	
	mesh.RecalculateBounds();
	mesh.Optimize();
}
	*/
}
