using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip pianoKeySound;

    private bool isPressed;
    private float smooth = 5f;

    void Start()
    {
        isPressed = false;
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void playSound()
    {
        if (!isPressed)
        {
            isPressed = true;
            audioSource.PlayOneShot(pianoKeySound);
            StartCoroutine("WaitForEnd");
        }
    }

    public void playSoundMenu()
    {
        if (!isPressed)
        {
            isPressed = true;
            StartCoroutine("WaitForEnd");
        }
    }

    public void Update()
    {
        if (isPressed)
        {
            // 1. Quaternions are used to represent rotations. Implemented in:UnityEngine.CoreModule
            // 2. deltaTime for frame independence movement
            Quaternion targetRotationPressed = Quaternion.Euler(0, -0.5f, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationPressed, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationUnpressed = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationUnpressed, smooth * Time.deltaTime);
        }
    }

    public IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(pianoKeySound.length/3);
        isPressed = false;
    }
}
