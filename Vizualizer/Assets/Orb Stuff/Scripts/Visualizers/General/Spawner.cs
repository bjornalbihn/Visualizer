using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	[SerializeField]
	private GameObject m_prefab;
	[SerializeField]
	private bool m_spawnOnStart = true;

	private void Start()
	{
		if (m_spawnOnStart)
			Spawn();
	}
	
	public void Spawn()
	{
		if (m_prefab)
		{
			GameObject copy = GameObject.Instantiate(m_prefab);

			copy.transform.parent = transform;
			copy.transform.localScale = Vector3.one;
			copy.transform.localPosition = Vector3.zero;
			copy.transform.localRotation = Quaternion.identity;
		}
		else
			Debug.LogWarning(string.Format("No prefab for spawner: {0}", name));
	}
}
