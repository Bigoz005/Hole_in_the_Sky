using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    private ParticleSystem sinkWater;
    public GameObject sinkWaterObj;

    private void Start()
    {
        sinkWater = sinkWaterObj.GetComponent("ParticleSystem") as ParticleSystem;
        audioSource = GetComponent<AudioSource>();
    }

    public void startWater()
    {
        if (sinkWater != null)
        {
            sinkWater.Play();
            audioSource.PlayOneShot(audioClip);
            StartCoroutine("WaitForMusicEnd");
        }
    }

    IEnumerator WaitForMusicEnd()
    {
        yield return new WaitForSeconds(audioClip.length-1);
        sinkWater.Stop();
    }
}
