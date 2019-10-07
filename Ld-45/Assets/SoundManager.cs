using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource audioSource;

    public List<AudioClip> clips;
    
    public void playSlideBackSound() {
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void playSlideFrontSound() {
        audioSource.clip = clips[1];
        audioSource.Play();
    }

    public void playWhistleSound() {
        audioSource.clip = clips[2];
        audioSource.Play();
    }
}
