using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Portal : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip scarySound;
    public Light flashLight;
    public GameObject player;
    public bool hasEntered;
    public GameObject blackBackground;
    public Image endGameImage;
    private Color tempEndGameColor;
    public GameObject playerHud;

    void Start()
    {
        hasEntered = false;
        blackBackground.SetActive(false);
        tempEndGameColor = endGameImage.color;
        audioSource = GetComponent<AudioSource>();
        tempEndGameColor.a = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasEntered = true;
            blackBackground.SetActive(true);
            audioSource.PlayOneShot(scarySound);

            player.GetComponentInChildren<FirstPersonController>().enabled = false;
            HideHud();

            if (flashLight.enabled)
            {
                flashLight.enabled = false;
            }

            StartCoroutine("WaitForEnd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        endGameImage.color = tempEndGameColor;
        if (hasEntered)
        {
            tempEndGameColor.a += Time.deltaTime * 0.02f;
        }
    }

    void HideHud()
    {
        playerHud.transform.GetChild(0).gameObject.SetActive(false);
        playerHud.transform.GetChild(1).gameObject.SetActive(false);
        playerHud.transform.GetChild(2).gameObject.SetActive(false);
        playerHud.transform.GetChild(3).gameObject.SetActive(false);
    }

    public IEnumerator WaitForEnd()
    {
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene("MainMenu");
    }

}
