using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFrontTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.front = true;
            door.back = false;
        }
    }
}
