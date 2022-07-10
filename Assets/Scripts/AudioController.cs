using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioClip startAudio;
    public static AudioClip moveAudio;
    public static AudioSource audioSource;

    public AudioClip m_startAudio;
    public AudioClip m_moveAudio;
    public AudioSource m_audioSource;

    private void Start()
    {
        startAudio = m_startAudio;
        moveAudio = m_moveAudio;
        audioSource = m_audioSource;
    }

    public static void PlayAudio(int index)
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        switch (index)
        {
            case 0:
                audioSource.clip = startAudio;
                audioSource.Play();
                break;
            case 1:
                audioSource.clip = moveAudio;
                audioSource.Play();
                break;
        }
    }
}
