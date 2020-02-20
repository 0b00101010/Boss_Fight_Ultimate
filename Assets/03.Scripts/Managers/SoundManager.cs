using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioClip audioClip;
    private AudioSource audioSource;

    private float volume;

    public float Volume {
        get => volume;
        set {
            volume = value;
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("Volume", volume);
        }
    }

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("Volume"))
            volume = PlayerPrefs.GetFloat("Volume");
        else
            volume = 0.5f;
   
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
