using UnityEngine;
using System.Collections;
using System;

// Used for all kinds of animated floats, note that the Coroutine needs to be called from a monobehaviour in order to run
[Serializable]
public class CurveAnimatedFloat
{
	public virtual float Value {get{return m_value;}}

	[SerializeField] private AnimationCurve m_curve;
	[SerializeField] private float m_changeTime;
	protected float m_value = 0;

	public Action<float> onNewValue;

	public void SetValue(float value)
	{
		m_value = value;
		if (onNewValue != null)
			onNewValue(Value);
	}

	public IEnumerator SetValueOverTime(float target)
	{
		float start = m_value;
		float time = 0;

		while (time<1)
		{
			time += Time.unscaledDeltaTime / m_changeTime;
			time = Mathf.Min (time, 1);

			float amount = m_curve.Evaluate(time);
			SetValue(Mathf.Lerp(start, target, amount));

			yield return 0;
		}
	}
}

// Used for values that have a min max
[Serializable]
public class CurveAnimatedHighLow : CurveAnimatedFloat
{
	public override float Value { get { return Mathf.Lerp(m_low, m_high, m_value);}}

	[SerializeField] private float m_low;
	[SerializeField] private float m_high;
}
