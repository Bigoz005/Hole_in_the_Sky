using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public string interactButton;

    public float interactDistance = 5f;
    public LayerMask interactLayer;
    public Image interactIcon;

    public bool isInteracting;

    void Start()
    {
        if (interactIcon != null)
        {
            interactIcon.enabled = false;
        }
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // checks hits of ray within interactDistance in the interactLayer
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (!isInteracting)
            {
                if (interactIcon != null)
                {
                    interactIcon.enabled = true;
                }

                if (Input.GetButtonDown(interactButton))
                {
                    if (hit.collider.CompareTag("Door"))
                    {
                        hit.collider.GetComponent<Door>().ChangeDoorState();
                    }
                    else if (hit.collider.CompareTag("Key"))
                    {
                        hit.collider.GetComponent<Key>().UnlockDoor();
                    }
                    else if (hit.collider.CompareTag("PadLock"))
                    {
                        hit.collider.GetComponent<PadLock>().ShowSafeCanvas();
                    }
                    else if (hit.collider.CompareTag("Chest"))
                    {
                        hit.collider.GetComponentInChildren<ChestDoor>().ChangeDoorState();
                    }
                    else if (hit.collider.CompareTag("PianoKey"))
                    {

                    }
                    else if (hit.collider.CompareTag("Sink"))
                    {
                        hit.collider.GetComponent<Sink>().startWater();
                    }
                    else if (hit.collider.CompareTag("Bathtub"))
                    {
                        hit.collider.GetComponent<Bathtub>().playSound();
                    }
                    else if (hit.collider.CompareTag("Toilet"))
                    {
                        hit.collider.GetComponent<Toilet>().startWater();
                    }
                }
            }
        }
        else
        {
            if (interactIcon != null)
            {
                interactIcon.enabled = false;
            }
        }
    }
}
