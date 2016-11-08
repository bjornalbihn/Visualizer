using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour 
{
	[SerializeField] private int m_renderQueue = 3000;
	[SerializeField] private Renderer m_renderer;

	// Update is called once per frame
	void Update () 
	{
		if (m_renderer.material.renderQueue != m_renderQueue)
			m_renderer.material.renderQueue = m_renderQueue;
	}
}
