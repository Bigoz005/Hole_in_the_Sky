﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolPickup : MonoBehaviour
{
    public GameObject pistol;
    public GameObject crosshair;
    public Text ammoText;
    public GameObject[] zombies;

    private AudioSource audioSource;
    public AudioClip pistolPickupSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickUpSound()
    {
        audioSource.PlayOneShot(pistolPickupSound);
    }

    public void PickupPistol()
    {
        PlayPickUpSound();
        StartCoroutine("WaitForDestroy");
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(pistolPickupSound.length);
        pistol.SetActive(true);
        crosshair.SetActive(true);
        ammoText.gameObject.SetActive(true);
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(true);
        }
        Destroy(gameObject);
    }
}
