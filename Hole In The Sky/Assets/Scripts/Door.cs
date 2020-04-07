using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool isOpen = false;

    public float doorOpenAngle = 0f;
    public float doorClosedAngle = 90f;

    public float smooth = 2f;

    public void ChangeDoorState()
    {
        isOpen = !isOpen;
    }
    
    void Update()
    {
        if (isOpen)
        {
            // 1. Quaternions are used to represent rotations. Implemented in:UnityEngine.CoreModule
            // 2. deltaTime for frame independence movement
            Quaternion targetRotationOpen = Quaternion.Euler(-90, 0, doorOpenAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
            
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(-90, 0, doorClosedAngle);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
    }
}
