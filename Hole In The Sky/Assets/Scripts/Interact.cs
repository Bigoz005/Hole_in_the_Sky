using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public string interactButton;

    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public Image interactIcon;

    public bool isInteracting;

    void Start()
    {
        if(interactIcon != null)
        {
            interactIcon.enabled = false;
        }
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // checks hits of ray within interactDistance in the interactLayer
        if(Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if (!isInteracting)
            {
                if(interactIcon != null)
                {
                    interactIcon.enabled = true;
                }
                
                if (Input.GetButtonDown(interactButton))
                {
                    if (hit.collider.CompareTag("Door"))
                    {
                        hit.collider.GetComponent<Door>().ChangeDoorState();
                        Debug.Log(hit.collider.GetComponent<Door>().open);
                    }
                }
            }
        }
    }
}
