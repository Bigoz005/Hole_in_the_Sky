using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoKey : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip pianoKeySound;
    private PianoRiddle parentComponent;
    public bool isPressed;
    private float smooth = 5f;

    void Start()
    {
        parentComponent = this.GetComponentInParent<PianoRiddle>();
        isPressed = false;
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void playSound()
    {
        if (!isPressed)
        {
            isPressed = true;
            parentComponent.CheckKeys(pianoKeySound.name);
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
