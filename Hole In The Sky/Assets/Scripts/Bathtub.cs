using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathtub : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private ParticleSystem bathtubWater;
    public GameObject bathtubWaterObj;

    private void Start()
    {
        bathtubWater = bathtubWaterObj.GetComponent("ParticleSystem") as ParticleSystem;
        audioSource = GetComponent<AudioSource>();
    }

    public void startWater()
    {
        if (bathtubWater != null)
        {
            bathtubWater.Play();
            audioSource.PlayOneShot(audioClip);
            StartCoroutine("WaitForMusicEnd");
        }
    }

    IEnumerator WaitForMusicEnd()
    {
        yield return new WaitForSeconds(audioClip.length - 2);
        bathtubWater.Stop();
    }
}
