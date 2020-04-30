using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private ParticleSystem toiletWater;
    public GameObject toiletWaterObj;

    private void Start()
    {
        toiletWater = toiletWaterObj.GetComponent("ParticleSystem") as ParticleSystem;
        audioSource = GetComponent<AudioSource>();
    }

    public void startWater()
    {
        if (toiletWater != null)
        {
            toiletWater.Play();
            audioSource.PlayOneShot(audioClip);
            StartCoroutine("WaitForMusicEnd");
        }
    }

    IEnumerator WaitForMusicEnd()
    {
        yield return new WaitForSeconds(audioClip.length - 3);
        toiletWater.Stop();
    }
}
