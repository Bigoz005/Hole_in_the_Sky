using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private float currentHp = 0;
    private bool isHealing = true;
    //private bool isTakingDmg = false;
    private bool gettingHit = false;
    private bool isDying = false;
    private bool readyToShow = false;
    private Camera playerCamera;
    private FirstPersonController playerController;
    private ColorGrading colorGrading;
    private DepthOfField depthOfField;
    public Image bloodImage;
    private Color tempBloodColor;
    public Image gameOverImage;
    private Color tempGameOverColor;
    public AudioClip hitAudio;
    private AudioSource audioSource;
    private Animator animator;
    private PlayerInventory playerInventory;
    public GameObject playerHud;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        animator.enabled = false;
        playerInventory = GetComponent<PlayerInventory>();
        tempBloodColor = bloodImage.color;
        tempGameOverColor = gameOverImage.color;
        currentHp = maxHp;
        playerCamera = GetComponentInChildren<Camera>();
        playerController = GetComponent<FirstPersonController>();
        PostProcessVolume postProcessVolume = GetComponentInChildren<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);
    }

    // Update is called once per frame
    void Update()
    {
        bloodImage.color = tempBloodColor;
        gameOverImage.color = tempGameOverColor;

        if (!isDying)
        {
            //if less than 50hp then can't run and jump
            if (currentHp < 50)
            {
                playerController.canRun = false;
                playerController.canJump = false;
            }
            else
            {
                playerController.canRun = true;
                playerController.canJump = true;
            }

            if (isHealing)
            {
                if (currentHp < maxHp)
                {
                    currentHp += Time.deltaTime * 5;
                }

                if (playerCamera.fieldOfView > 63)
                {
                    playerCamera.fieldOfView -= Time.deltaTime * 0.9f;
                }

                if (colorGrading.saturation.value < 15)
                {
                    colorGrading.saturation.value += Time.deltaTime * 7.0f;
                }

                if (playerController.m_WalkSpeed < 2)
                {
                    playerController.m_WalkSpeed += Time.deltaTime * 0.8f;
                }

                if (playerController.m_RunSpeed < 3)
                {
                    playerController.m_RunSpeed += Time.deltaTime * 0.8f;
                }

                if (tempBloodColor.a > 0.0f)
                {
                    tempBloodColor.a -= Time.deltaTime * 0.01f;
                }
            }

            if (depthOfField.focusDistance.value < 7)
            {
                depthOfField.focusDistance.value += Time.deltaTime * 0.9f;
            }

            if (gettingHit)
            {
                Camera.main.transform.position += Random.insideUnitSphere * 0.045f;
            }

            if (currentHp <= 40)
            {
                Camera.main.transform.position += Random.insideUnitSphere * 0.02f;
            }

            if (currentHp <= 20)
            {
                Camera.main.transform.position += Random.insideUnitSphere * 0.03f;
            }
        }
        else
        {
            depthOfField.focusDistance.value -= Time.deltaTime * 0.9f;

            Camera.main.transform.position += Random.insideUnitSphere * 0.02f;

            tempBloodColor.a -= Time.deltaTime * 0.008f;
            if (readyToShow)
            {
                tempGameOverColor.a += Time.deltaTime * 0.15f;
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        if (!isDying)
        {
            audioSource.PlayOneShot(hitAudio);
            isHealing = false;

            StartCoroutine("WaitForHit");

            currentHp -= _damage;

            if (currentHp <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine("WaitForHealing");
            }
            tempBloodColor.a += 0.005f;
            CheckHP();
        }
    }

    public void CheckHP()
    {
        if (currentHp <= 100 && currentHp > 80)
        {
            playerCamera.fieldOfView = 63;
            colorGrading.saturation.value = 15;
            //depthOfField.focusDistance.value = 7;
            depthOfField.focusDistance.value = 0.5f;
            playerController.m_WalkSpeed = 2;
            playerController.m_RunSpeed = 3;
        }

        if (currentHp <= 80 && currentHp > 50)
        {
            playerCamera.fieldOfView = 68;
            colorGrading.saturation.value = -10;
            depthOfField.focusDistance.value = 0.5f;
            //depthOfField.focusDistance.value = 4;
            playerController.m_WalkSpeed = 1.7f;
            playerController.m_RunSpeed = 2.5f;
        }

        if (currentHp <= 50 && currentHp > 40)
        {
            playerCamera.fieldOfView = 73;
            colorGrading.saturation.value = -35;
            depthOfField.focusDistance.value = 0.5f;
            //depthOfField.focusDistance.value = 1.5f;
            playerController.m_WalkSpeed = 1.5f;
            playerController.m_RunSpeed = 2.2f;
        }

        if (currentHp <= 40 && currentHp > 20)
        {
            playerCamera.fieldOfView = 78;
            colorGrading.saturation.value = -60;
            depthOfField.focusDistance.value = 0.5f;
            //depthOfField.focusDistance.value = 0.8f;
            playerController.m_WalkSpeed = 1.3f;
            playerController.m_RunSpeed = 1.8f;
        }

        if (currentHp <= 20 && currentHp > 0)
        {
            playerCamera.fieldOfView = 83;
            colorGrading.saturation.value = -85;
            depthOfField.focusDistance.value = 0.5f;
            //depthOfField.focusDistance.value = 0.4f;
            playerController.m_WalkSpeed = 1.0f;
            playerController.m_RunSpeed = 1.5f;
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        animator.enabled = true;
        playerInventory.isPistolInHand = false;
        playerHud.transform.GetChild(0).gameObject.SetActive(false);
        playerHud.transform.GetChild(1).gameObject.SetActive(false);
        playerHud.transform.GetChild(2).gameObject.SetActive(false);
        playerHud.transform.GetChild(3).gameObject.SetActive(false);

        playerController.enabled = false;
        isDying = true;

        StartCoroutine("WaitForShow");
        StartCoroutine("WaitForDie");
    }

    public IEnumerator WaitForHealing()
    {
        yield return new WaitForSecondsRealtime(15.0f);
        isHealing = true;
    }

    public IEnumerator WaitForHit()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        gettingHit = true;
        StartCoroutine("HitDuration");
    }

    public IEnumerator HitDuration()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        gettingHit = false;
    }

    public IEnumerator WaitForShow()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        readyToShow = true;
    }

    public IEnumerator WaitForDie()
    {
        yield return new WaitForSecondsRealtime(13.0f);
        SceneManager.LoadScene("MainMenu");
    }

}


