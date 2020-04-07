using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoSoundTrigger : MonoBehaviour
{
    public AudioSource scaryPianoAudioSource;
    public AudioClip scarySound;
    public Light flashLight;
    

    private bool hasPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayed)
        {
            scaryPianoAudioSource.PlayOneShot(scarySound);
            hasPlayed = true;

            if (flashLight.enabled)
            {
                flashLight.enabled = false;
            }
        }
        
    }
}
