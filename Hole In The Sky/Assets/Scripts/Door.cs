using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;

    public bool front = false;
    public bool back = false;

    public float doorOpenAngle = 0f;
    public float doorClosedAngle = 90f;

    public float smooth = 2f;

    private Quaternion targetRotationOpen;
    private AudioSource doorAudioSource;
    public AudioClip doorSound;

    public void ChangeDoorState()
    {
        open = !open;

        if (doorAudioSource != null)
        {
            doorAudioSource.PlayOneShot(doorSound);
        }
    }

    private void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (open)
        {
            // 1. Quaternions are used to represent rotations. Implemented in:UnityEngine.CoreModule
            // 2. deltaTime for frame independence movement
            if (doorClosedAngle == -90)
            {
                targetRotationOpen = Quaternion.Euler(-90, 0, doorClosedAngle + 90);
            }
            else
            {
                targetRotationOpen = Quaternion.Euler(-90, 0, doorClosedAngle * 2);
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(-90, 0, doorClosedAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);

        }
    }
}
