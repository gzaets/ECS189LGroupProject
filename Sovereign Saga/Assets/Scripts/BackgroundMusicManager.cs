using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip forestMusic;
    public AudioClip hillsMusic;
    public AudioClip snowMusic;
    public AudioClip dungeonMusic;

    private AudioSource audioSource;

    public float volume = 0.65f; // Adjust the volume level here

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume; // Set the initial volume
        audioSource.loop = true; // Enable looping
    }

    private void Update()
    {
        float playerX = transform.position.x;
        float playerY = transform.position.y;

        if (playerX > 25f && playerY < 32.4f)
        {
            PlayBackgroundMusic(hillsMusic);
        }
        else if (playerY > 32.4f && playerX > -59.2f)
        {
            PlayBackgroundMusic(snowMusic);
        }
        else if (playerX > -59.2f && playerX< 25f)
        {
            PlayBackgroundMusic(forestMusic);
        }
        else
        {
            PlayBackgroundMusic(dungeonMusic);
        }
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Don't play the same music again

        audioSource.clip = clip;
        audioSource.Play();
    }
}