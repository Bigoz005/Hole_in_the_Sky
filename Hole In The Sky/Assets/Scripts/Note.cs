﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Note : MonoBehaviour
{
    public Image noteImage;

    public AudioClip pickupSound;
    public AudioClip putdownSound;

    public GameObject noteButton;
    public GameObject playerObject;

    private bool isActive;

    void Start()
    {
        isActive = false;
        noteImage.enabled = false;
        noteButton.SetActive(false);
    }

    public void ShowNoteImage()
    {
        if (!isActive)
        {
            isActive = true;
            noteImage.enabled = true;
            noteButton.SetActive(true);
            playerObject.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<AudioSource>().PlayOneShot(pickupSound);
        }
    }

    public void HideNoteImage()
    {
        if (isActive)
        {
            isActive = false;
            noteImage.enabled = false;
            noteButton.SetActive(false);
            playerObject.GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<AudioSource>().PlayOneShot(putdownSound);
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                HideNoteImage();
            }
        }
    }
}