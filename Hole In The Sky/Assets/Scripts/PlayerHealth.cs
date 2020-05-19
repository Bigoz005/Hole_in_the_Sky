using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private float currentHp = 0;
    private bool isHealing = true;
    private bool isTakingDmg = false;
    private bool gettingHit = false;
    private Camera playerCamera;
    private FirstPersonController playerController;
    private ColorGrading colorGrading;
    private DepthOfField depthOfField;

    // Start is called before the first frame update
    void Start()
    {
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

            if (depthOfField.focusDistance.value < 7)
            {
                depthOfField.focusDistance.value += Time.deltaTime * 0.9f;
            }
            
            if (playerController.m_WalkSpeed < 2)
            {
                playerController.m_WalkSpeed += Time.deltaTime * 0.8f;
            }

            if (playerController.m_RunSpeed < 3)
            {
                playerController.m_RunSpeed += Time.deltaTime * 0.8f;
            }
        }

        if (gettingHit)
        {
            Camera.main.transform.position += Random.insideUnitSphere * 0.05f;
        }

        if(currentHp <= 40)
        {
            Camera.main.transform.position += Random.insideUnitSphere * 0.04f;
        }

        if (currentHp <= 20)
        {
            Camera.main.transform.position += Random.insideUnitSphere * 0.06f;
        }
    }

    public void TakeDamage(int _damage)
    {
        isHealing = false;

        StartCoroutine("WaitForHit");

        currentHp -= _damage;

        CheckHP();

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine("WaitForHealing");
        }
    }

    public void CheckHP()
    {
        if(currentHp <= 100 && currentHp > 80)
        {
            playerCamera.fieldOfView = 63;
            colorGrading.saturation.value = 15;
            depthOfField.focusDistance.value = 7;
            playerController.m_WalkSpeed = 2;
            playerController.m_RunSpeed = 3;
        }

        if (currentHp <= 80 && currentHp > 60)
        {
            playerCamera.fieldOfView = 68;
            colorGrading.saturation.value = -10;
            depthOfField.focusDistance.value = 4;
            playerController.m_WalkSpeed = 1.7f;
            playerController.m_RunSpeed = 2.5f;
        }

        if (currentHp <= 60 && currentHp > 40)
        {
            playerCamera.fieldOfView = 73;
            colorGrading.saturation.value = -35;
            depthOfField.focusDistance.value = 1.5f;
            playerController.m_WalkSpeed = 1.5f;
            playerController.m_RunSpeed = 2.2f;
        }

        if (currentHp <= 40 && currentHp > 20)
        {
            playerCamera.fieldOfView = 78;
            colorGrading.saturation.value = -60;
            depthOfField.focusDistance.value = 0.8f;
            playerController.m_WalkSpeed = 1.3f;
            playerController.m_RunSpeed = 1.8f;
        }

        if (currentHp <= 20 && currentHp > 0)
        {
            playerCamera.fieldOfView = 83;
            colorGrading.saturation.value = -85;
            depthOfField.focusDistance.value = 0.4f;
            playerController.m_WalkSpeed = 1.0f;
            playerController.m_RunSpeed = 1.5f;
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine("WaitForDie");
    }

    public IEnumerator WaitForHealing()
    {
        yield return new WaitForSecondsRealtime(15.0f);
        isHealing = true;
    }

    public IEnumerator WaitForHit()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        gettingHit = true;
        StartCoroutine("HitDuration");
    }

    public IEnumerator HitDuration()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        gettingHit = false;
    }

    public IEnumerator WaitForDie()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        SceneManager.LoadScene("MainMenu");
    }

}


