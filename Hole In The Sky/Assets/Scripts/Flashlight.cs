using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashLight;

    public AudioSource flashLightAudioSource;
    public AudioClip flashLightOn;
    public AudioClip flashLightOff;

    private bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        flashLight.enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {

        if (flashLight.enabled)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isActive = !isActive;
            flashLight.enabled = isActive;
            if (isActive)
            {
                flashLightAudioSource.PlayOneShot(flashLightOn);
            }
            else
            {
                flashLightAudioSource.PlayOneShot(flashLightOff);
            }
        }
    }
}
