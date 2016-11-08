using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ColorByBeat : AudioInducedEffect 
{
	public Color Color {private set; get;} 
	public Action<Color> OnNewColor;

	[SerializeField] private Color[] m_colors;
	[SerializeField] private bool m_random;

	private int m_colorIndex;

	public override void Activate ()
	{
		base.Activate ();
		SetColor();
	}

	private void CycleColor()
	{
		m_colorIndex ++;

		if (m_colorIndex < m_colors.Length)
			m_colorIndex = 0;
		
		SetColor();
	}

	private void SetRandomColor()
	{
		m_colorIndex = UnityEngine.Random.Range(0,m_colors.Length);
	}
		
	private void SetColor()
	{
		if (m_colors.Length>0)
			Color = m_colors[m_colorIndex];
		else
			Color = Color.white;

		if (OnNewColor != null)
			OnNewColor(Color);
	}

	public override void onBeatDetected ()
	{
		if (m_random)
			SetRandomColor();
		else
			CycleColor();

		SetColor();
	}
}
