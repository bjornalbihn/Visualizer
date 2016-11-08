using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour 
{
	public static Core Instance;

	[SerializeField] private Camera m_mainCamera;
	[SerializeField] private CameraController m_cameraController;
	[SerializeField] private VisualizerController m_visualizerController;
	[SerializeField] private AudioProcessor m_audioProcessor;
	[SerializeField] private AudioSource m_audioSource;

	public static Camera MainCamera {get{return Instance.m_mainCamera;}}
	public static CameraController CameraController {get{return Instance.m_cameraController;}}
	public static VisualizerController VisualizerController {get{return Instance.m_visualizerController;}}
	public static AudioProcessor AudioProcessor {get{return Instance.m_audioProcessor;}}
	public static AudioSource AudioSource {get{return Instance.m_audioSource;}}

	public void Awake()
	{
		Instance = this;
	}
}
