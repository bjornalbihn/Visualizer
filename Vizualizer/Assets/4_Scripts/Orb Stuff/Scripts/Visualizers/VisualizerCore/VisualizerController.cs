using UnityEngine;
using System.Collections;
using System;

public class VisualizerController : MonoBehaviour 
{
	public Visualizer CurrentVisualizer {get{return m_currentVisualizer;}}
	[SerializeField] private Visualizer[] m_visualizers;

	private int m_currentVisualizerIndex = -1;
	private Visualizer m_currentVisualizer;

	public Action<Visualizer> OnNewVisualizer;

	public void OnEnable()
	{
		foreach (Visualizer visualizer in m_visualizers)
			visualizer.SetEnabled(false);
		SetVisualizerByIndex(0);
	}

	public void Cycle (int offset)
	{
		m_currentVisualizerIndex += offset;

		if (m_currentVisualizerIndex >= m_visualizers.Length)
			m_currentVisualizerIndex = 0;
		
		if (m_currentVisualizerIndex < 0)
			m_currentVisualizerIndex = m_visualizers.Length-1;

		SetVisualizerByIndex(m_currentVisualizerIndex);
	}

	public void SetVisualizerByIndex(int index)
	{
		m_currentVisualizerIndex = index;

		if (m_currentVisualizer)
			m_currentVisualizer.SetEnabled(false);

		if (m_currentVisualizerIndex < m_visualizers.Length)
		{
			m_currentVisualizer = m_visualizers[m_currentVisualizerIndex];
			m_currentVisualizer.SetEnabled(true);
		}

		if (OnNewVisualizer != null)
			OnNewVisualizer(CurrentVisualizer);
	}

	public void SetVisualizerByID(int ID)
	{
		for (int i = 0; i<m_visualizers.Length; i++)
		{
			if (m_visualizers[i].Data.ID == ID)
				SetVisualizerByIndex(i);
		}
	}
}
