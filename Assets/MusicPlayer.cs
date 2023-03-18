using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    public Slider gameVolumeSlider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop=false;
        AudioListener.volume = 1.0f;
    }

    public void ChangeGameVolume()
    {
        AudioListener.volume = gameVolumeSlider.value;
    }

    private AudioClip GetRandomClip()
    {
        Random rnd = new Random();
        return clips[rnd.Next(0, clips.Length)];
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }
    }
}
