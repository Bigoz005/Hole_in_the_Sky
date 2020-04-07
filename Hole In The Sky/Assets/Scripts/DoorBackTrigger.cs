using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBackTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.back = true;
            door.front = false;
        }
    }
}
