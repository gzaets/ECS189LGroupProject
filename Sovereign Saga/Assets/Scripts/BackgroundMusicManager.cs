using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip forestMusic;
    public AudioClip hillsMusic;
    public AudioClip snowMusic;
    public AudioClip dungeonMusic;

    public AudioClip forestFootSound;
    public AudioClip hillsFootSound;
    public AudioClip snowFootSound;
    public AudioClip dungeonFootSound;

    private AudioSource audioMusicSource;
    private AudioSource audioStepSource;

    public float volumeMusic = 1f; // Adjust the volume level here
    public float volumeStep = 1f; // Adjust the volume level here

    public float pitchStep = 2f;

    // Add a variable to track the player's last position
    private Vector3 lastPosition;

    private void Start()
    {
        audioMusicSource = gameObject.AddComponent<AudioSource>();
        audioMusicSource.volume = volumeMusic; // Set the initial volume
        audioMusicSource.loop = true; // Enable looping

        audioStepSource = gameObject.AddComponent<AudioSource>();
        audioStepSource.volume = volumeStep; // Set the initial volume
        audioStepSource.loop = false; // Disable looping
        audioStepSource.pitch = pitchStep; // Set the pitch to 2

        // Initialize lastPosition at Start to the current player's position
        lastPosition = transform.position;
    }

    private void Update()
    {
        float playerX = transform.position.x;
        float playerY = transform.position.y;

        // Check if the player has moved since the last frame
        if (transform.position != lastPosition)
        {
            if (playerX > 25f && playerY < 32.4f)
            {
                PlayBackgroundMusic(hillsMusic);
                PlayPlayerSound(hillsFootSound); 
            }
            else if (playerY > 32.4f && playerX > -59.2f)
            {
                PlayBackgroundMusic(snowMusic);
                PlayPlayerSound(snowFootSound); 
            }
            else if (playerX > -59.2f && playerX < 25f)
            {
                PlayBackgroundMusic(forestMusic);
                PlayPlayerSound(forestFootSound);
            }
            else
            {
                PlayBackgroundMusic(dungeonMusic);
                PlayPlayerSound(dungeonFootSound);
            }
        }
        
        // Update lastPosition for the next frame
        lastPosition = transform.position;
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        if (audioMusicSource.clip == clip) return; // Don't play the same music again

        audioMusicSource.clip = clip;
        audioMusicSource.Play();
    }

    private void PlayPlayerSound(AudioClip clip)
    {
        // The sound will play only if the clip is not null, and the AudioSource is not already playing another clip
        if (clip != null && !audioStepSource.isPlaying)
        {
            audioStepSource.clip = clip;
            audioStepSource.Play();
        }
    }
}