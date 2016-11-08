using UnityEngine;
using System.Collections;

public class EmitParticlesSphericalLayer : MonoBehaviour 
{
	[SerializeField] private ParticleSystem m_system;

	[SerializeField] private float m_near = 10;
	[SerializeField] private float m_far = 100;

	[SerializeField] private int m_amountPerFrame;
	[SerializeField] private int m_startAmount;

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
			particles[i].lifetime = Random.value * particles[i].startLifetime;
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
		float random = ((m_far-m_near) * Random.value) + m_near;
		Vector3 pos = Random.insideUnitSphere.normalized * random;
		m_system.Emit(pos, Vector3.zero, m_system.startSize, m_system.startLifetime, m_system.startColor);
	}


	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue * .5f;
		Gizmos.DrawWireSphere( transform.position, m_far);
		Gizmos.color = Color.yellow * .5f;
		Gizmos.DrawWireSphere( transform.position, m_near);

	}
}
