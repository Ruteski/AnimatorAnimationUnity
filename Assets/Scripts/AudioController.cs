using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    public static AudioController current;
    public AudioClip sfx;
    public AudioClip anotherSfx;
    public AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip) {
        _audioSource.PlayOneShot(clip);
    }

    public void ChangeBGM(AudioClip clip) { 
        _audioSource.clip = clip;
    }

    
}
