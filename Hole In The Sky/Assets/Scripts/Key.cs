using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door door;

    private AudioSource keyAudioSource;
    public AudioClip keySound;

    void Start()
    {
        keyAudioSource = GetComponent<AudioSource>();
    }

    public void PlayPickUpSound()
    {
        keyAudioSource.PlayOneShot(keySound);
    }

    public void UnlockDoor()
    {
        door.isLocked = false;
        PlayPickUpSound();

        StartCoroutine("WaitForDestroy");    
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(keySound.length);
        Destroy(gameObject);
    }
}
