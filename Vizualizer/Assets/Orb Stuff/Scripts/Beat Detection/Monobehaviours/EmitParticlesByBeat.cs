using UnityEngine;
using System.Collections;

public class EmitParticlesByBeat : AudioInducedBehaviour 
{
	[SerializeField] private int m_minEmit = 0;
	[SerializeField] private int m_maxEmit = 15;
	[SerializeField] ParticleSystem m_particleSystem;

	public override void onBeatDetected ()
	{
		int amount = Random.Range(m_minEmit, m_maxEmit);
		m_particleSystem.Emit(amount);
	}
}  