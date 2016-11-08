using UnityEngine;
using System.Collections;
using System;

public class AudioInducedBehaviour : MonoBehaviour, AudioProcessor.AudioCallbacks
{
	protected virtual void OnEnable()
	{
		Core.AudioProcessor.addAudioCallback(this);
	} 

	public virtual void onBeatDetected()
	{
	}

	public virtual void onSpectrum(float[] spectrum)
	{
	}

	protected virtual void OnDisable()
	{
		Core.AudioProcessor.removeAudioCallback(this);
	} 

}

[Serializable]
public class AudioInducedEffect : AudioProcessor.AudioCallbacks
{		
	public virtual void Activate()
	{
		Core.AudioProcessor.addAudioCallback(this);
	}

	public virtual void onBeatDetected()
	{
	}

	public virtual void onSpectrum(float[] spectrum)
	{
	}

	public virtual void Deactivate()
	{
		Core.AudioProcessor.removeAudioCallback(this);
	}
}
