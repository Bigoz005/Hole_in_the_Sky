using UnityEngine;
using System.Collections;

public class ClockSound : MonoBehaviour
{
    public float clockSpeed = 1.0f;

    int seconds;
    float msecs;
    private AudioSource audioSource;
    public AudioClip secondsClip;
    public AudioClip oddSecondsClip;

    void Start()
    {
        msecs = 0.0f;
        seconds = 0;
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            seconds++;
            if (seconds % 2 == 0)
            {
                audioSource.PlayOneShot(secondsClip);
            }
            else
            {
                audioSource.PlayOneShot(oddSecondsClip);
            }
        }
    }
}
