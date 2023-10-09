using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolPickup : MonoBehaviour
{
    public GameObject player;
    public GameObject[] zombies;

    public void PickupPistol()
    {
        StartCoroutine("WaitForDestroy");
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSecondsRealtime(1/2);

        player.GetComponent<PlayerInventory>().SetPickedUp(true);
        player.GetComponent<PlayerInventory>().SetInHand(true);
        
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(true);
        }
        Destroy(gameObject);
    }
}
