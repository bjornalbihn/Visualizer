using UnityEngine;
using System.Collections;

public class Visualizer : MonoBehaviour 
{
	public AudioSource AudioSource {private set; get;}
	public VisualizerData Data {get {return m_data;}}

	[SerializeField] private VisualizerData m_data;

	public virtual void Setup()
	{
		AudioSource = Core.AudioSource;
	}

	public virtual void SetEnabled(bool state)
	{
		gameObject.SetActive(state);
	}
}
