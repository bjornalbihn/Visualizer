using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour 
{
	public static Core Instance;
	[SerializeField] private AudioProcessor m_audioProcessor;
	[SerializeField] private AudioSource m_audioSource;

	public static AudioProcessor AudioProcessor {get{return Instance.m_audioProcessor;}}
	public static AudioSource AudioSource {get{return Instance.m_audioSource;}}

	public void Awake()
	{
		Instance = this;
	}
}
