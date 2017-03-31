using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotionGraphicsUI
{
	[RequireComponent (typeof (MeshFilter))]

	public class Arc : MonoBehaviour 
	{
		[SerializeField] private float _radius;
		[SerializeField] private float _width;
		[SerializeField, Range (0,1)] private float _pivot;
		[SerializeField] private Gradient _color;

		[SerializeField] private int _points = 10;  

		[SerializeField] [Range(-360,360)] private float m_from = -90;  
		[SerializeField] [Range(-360,360)] private float m_to = 90; 

		private MeshMaker _meshMaker;

		private void Awake()
		{
			_meshMaker = GetComponent<MeshMaker>();
		}

		private void Update()
		{
			List<MeshMaker.LineData> data = GetArc();
			_meshMaker.MakeLine(data);
		}

		private List<MeshMaker.LineData> GetArc()
		{
			List<MeshMaker.LineData> dataset = new List<MeshMaker.LineData>();

			float widthStep = (m_to-m_from) / _points-1;

			for (int w = 0; w < _points; w++)
			{
				MeshMaker.LineData data = new MeshMaker.LineData();

				float yaw = m_to + (w * widthStep);
				Vector3 pos = Vector(yaw,0);

				data.Position = pos * _radius;
				data.Color = _color.Evaluate((float) w / (float) _points);
				data.Normal = pos.normalized; // Vector3.Cross (pos.normalized, transform.up);
				data.Width = _width;
				data.Pivot = _pivot;

				dataset.Add(data);
			}

			return dataset;
		}

		public static Vector3 Vector(float yaw, float pitch)
		{
			yaw *= Mathf.Deg2Rad;
			pitch *= Mathf.Deg2Rad;

			float x = Mathf.Cos(yaw)*Mathf.Cos(pitch);
			float y = Mathf.Sin(yaw)*Mathf.Cos(pitch);
			float z = Mathf.Sin(pitch);

			return new Vector3(x,y,z);
		}

	}
}
