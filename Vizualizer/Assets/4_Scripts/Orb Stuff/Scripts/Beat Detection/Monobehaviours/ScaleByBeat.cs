using UnityEngine;
using System.Collections;

public class ScaleByBeat : AudioInducedBehaviour 
{
	[SerializeField] private Vector3 m_onBeatScaleMax = Vector3.one;
	[SerializeField] private Vector3 m_onBeatScaleMin = Vector3.one;

	[SerializeField] private Vector3 m_normalScale = Vector3.one;

	[SerializeField] private float m_timeToReachNormal;
	[SerializeField] private AnimationCurve m_curve;

	private float m_time;
	private Vector3 m_lastBeatScale;

	public override void onBeatDetected ()
	{
		Vector3 scale = Vector3.one;

		scale.x = Random.Range(m_onBeatScaleMin.x, m_onBeatScaleMax.x);
		scale.y = Random.Range(m_onBeatScaleMin.y, m_onBeatScaleMax.y);
		scale.z = Random.Range(m_onBeatScaleMin.z, m_onBeatScaleMax.z);

		m_lastBeatScale = scale;
		transform.localScale = scale;
		m_time = 1;
	}

	private void Update()
	{
		if (m_time > 0 && m_timeToReachNormal > 0)
		{
			m_time -= Time.unscaledDeltaTime / m_timeToReachNormal;
			m_time = Mathf.Max(m_time, 0);

			float value = m_curve.Evaluate(m_time);
			transform.localScale = Vector3.Lerp(m_normalScale, m_lastBeatScale, m_time);
		}
	}
}
