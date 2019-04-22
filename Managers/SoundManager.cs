using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip audioClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void MusicChange(AudioClip clip)
    {
        this.audioClip = clip;
        audioSource.clip = audioClip;
    }

    public void MusicQueue()
    {
        audioSource.Play();
    }

    public void MusicStop() {
        audioSource.Stop();
    }
}
