using UnityEngine;
using System.Collections;

public class MicrophoneInput : MonoBehaviour {

    private float m_timeOut = 3;

    void Start()
    {
        InitiateMicInput();
    }

    void InitiateMicInput()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource.clip == null)
        {
            audioSource.playOnAwake = false;
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.spatialBlend = 0.0f;
            audioSource.clip = Microphone.Start("", true, 1, 44100);
            if (audioSource.clip == null)
            {
                Debug.LogWarning("Failed to initiate microphone.");
                return;
            }

            float timeOutTimer = Time.realtimeSinceStartup;
            while (Microphone.GetPosition("") <= 0)
            {
                if (Time.realtimeSinceStartup > timeOutTimer + m_timeOut)
                {
                    Debug.LogWarning("Initiating microphone timed out.");
                    return;
                }
            }

            audioSource.Play();
        }
    }
}
