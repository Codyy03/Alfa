using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // odtwarza jeden dzwiêk 
    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayClickSound()
    {
        PlayClip(clickSound);
    }

    public void StopSound()
    {
        audioSource.Stop();
    
    }
}
