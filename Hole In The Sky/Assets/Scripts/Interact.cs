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
                    switch (hit.collider.gameObject.tag)
                    {
                        case "Door":
                            hit.collider.GetComponent<Door>().ChangeDoorState();
                            break;
                        case "Key":
                            hit.collider.GetComponent<Key>().UnlockDoor();
                            break;
                        case "PadLock":
                            hit.collider.GetComponent<PadLock>().ShowSafeCanvas();
                            break;
                        case "Chest":
                            hit.collider.GetComponentInChildren<ChestDoor>().ChangeDoorState();
                            break;
                        case "Sink":
                            hit.collider.GetComponent<Sink>().startWater();
                            break;
                        case "Bathtub":
                            hit.collider.GetComponent<Bathtub>().startWater();
                            break;
                        case "Toilet":
                            hit.collider.GetComponent<Toilet>().startWater();
                            break;
                        case "Note":
                            hit.collider.GetComponent<Note>().ShowNoteImage();
                            break;
                        case "PianoKey":
                            hit.collider.GetComponent<PianoKey>().playSound();
                            break;
                        case "Pistol":
                            hit.collider.GetComponent<PistolPickup>().PickupPistol();
                            break;
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
