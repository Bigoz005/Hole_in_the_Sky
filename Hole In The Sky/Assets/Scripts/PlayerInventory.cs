using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject pistol;
    public GameObject ammoText;
    public GameObject key;

    public AudioClip pistolPickupSound;
    public AudioClip pistolHideSound;
    private AudioSource audioSource;

    public bool isPistolInHand;
    private bool isPistolPickedUp;
    private bool isNoteChecked;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPistolInHand = false;
        isNoteChecked = false;
        isPistolPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("PistolEnabled"))
        {
            if (isPistolPickedUp)
            {
                isPistolInHand = !isPistolInHand;
                if (isPistolInHand)
                {
                    audioSource.PlayOneShot(pistolPickupSound);
                }
                else
                {
                    audioSource.PlayOneShot(pistolHideSound);
                }
            }
        }

        if (isPistolInHand)
        {
            crosshair.SetActive(true);
            pistol.SetActive(true);
            ammoText.SetActive(true);
        }
        else
        {
            crosshair.SetActive(false);
            pistol.SetActive(false);
            ammoText.SetActive(false);
        }

    }

    public void SetInHand(bool value)
    {
        isPistolInHand = value;
    }

    public void SetPickedUp(bool value)
    {
        isPistolPickedUp = value;
    }

    public void SetIsNoteChecked(bool value)
    {
        isNoteChecked = value;
    }

    public bool GetIsNoteChecked()
    {
        return isNoteChecked;
    }
}
