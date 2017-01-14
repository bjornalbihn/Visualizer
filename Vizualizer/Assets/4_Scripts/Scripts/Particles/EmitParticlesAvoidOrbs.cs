using UnityEngine;
using System.Collections;

public class EmitParticlesAvoidOrbs : MonoBehaviour 
{
	[SerializeField] private ParticleSystem m_system;
	
	[SerializeField] private float m_size = 100;
	
	[SerializeField] private int m_amountPerFrame;
	[SerializeField] private int m_startAmount;
	
	[SerializeField] private float m_orbSize;
	[SerializeField] private Transform[] m_orbs;
	
	private void Start()
	{
		m_system.Clear();
		for (int i = 0; i<m_startAmount; i++)
		{
			Emit();
		}
		
		ParticleSystem.Particle[] particles = new ParticleSystem.Particle[m_system.particleCount];
		m_system.GetParticles(particles);
		for (int i = 0; i<particles.Length; i++)
		{
			particles[i].remainingLifetime = Random.value * particles[i].startLifetime;
		}
		m_system.SetParticles(particles, particles.Length);
	}
	
	private void Update()
	{
		for (int i = 0; i<m_amountPerFrame; i++)
		{
			Emit();
		}
	}
	
	private void Emit()
	{
		Vector3 pos = Random.insideUnitSphere * m_size;
		if (NoOrbsInRange(pos))
			m_system.Emit(pos, Vector3.zero, m_system.startSize, m_system.startLifetime, m_system.startColor);
	}

	private bool NoOrbsInRange(Vector3 pos)
	{
		for (int i = 0; i< m_orbs.Length; i++)
		{
			if (m_orbs[i].gameObject.activeInHierarchy)
			{
				if ((m_orbs[i].position - pos).sqrMagnitude < m_orbSize* m_orbSize)
					return false;
			}
		}
		return true;
	}
	
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue * .5f;
		Gizmos.DrawWireSphere( transform.position, m_size);
		for (int i = 0; i< m_orbs.Length; i++)
		{
			if (m_orbs[i].gameObject.activeInHierarchy)
			{
				Gizmos.color = Color.yellow * .5f;
				Gizmos.DrawWireSphere( m_orbs[i].position, m_orbSize);
			}
		}
	}
}
