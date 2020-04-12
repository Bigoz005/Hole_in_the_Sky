using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDoor : MonoBehaviour
{
    public bool open = false;
    public bool isLocked = false;

    public float doorOpenAngle = 0f;
    public float doorClosedAngle = 90f;

    public float smooth = 2f;

    private Quaternion targetRotationOpen;
    private AudioSource doorAudioSource;
    public AudioClip chestDoorSound;


    public void ChangeDoorState()
    {
        if (!isLocked)
        {
            open = !open;

            if (doorAudioSource != null)
            {
                doorAudioSource.PlayOneShot(chestDoorSound);
            }
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
            targetRotationOpen = Quaternion.Euler(-52, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(0, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
    }
}
