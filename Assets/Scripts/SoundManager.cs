using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    /// <summary>
    /// Holds a reference to an AudioSource for sound effects.
    /// </summary>
	public AudioSource EffectsSrc;

    /// <summary>
    /// Holds a reference to an AudioSource for music.
    /// </summary>
    public AudioSource MusicSrc;


    private void Awake()
    {
        // Implements singleton pattern.
        if (Instance == null)
            Instance = this;


        // Sets SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Play a single clip through the sound effects source.
    /// </summary>
    public void Play(AudioClip clip)
    {
        EffectsSrc.clip = clip;
        EffectsSrc.Play();
    }

    /// <summary>
    /// Play a single clip through the music source.
    /// </summary>
    public void PlayMusic(AudioClip clip)
    {
        MusicSrc.clip = clip;
        MusicSrc.Play();
    }

    /// <summary>
    /// Stops the music source.
    /// </summary>
    public void StopMusic()
    {
        MusicSrc.Stop();
    }
}
