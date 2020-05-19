using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 100;
    private float currentHp = 0;
    private bool isHealing = true;
    private bool isTakingDmg = false;
    private bool gettingHit = false;
    private Camera playerCamera;
    private ColorGrading colorGrading;
    private DepthOfField depthOfField;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        playerCamera = GetComponentInChildren<Camera>();
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
                Debug.Log("HP: " + currentHp);
            }

            if (playerCamera.fieldOfView > 63)
            {
                playerCamera.fieldOfView -= Time.deltaTime;
                Debug.Log("FOV: " + playerCamera.fieldOfView);
            }

            if (colorGrading.saturation.value < 15)
            {
                colorGrading.saturation.value += Time.deltaTime * 10;
                Debug.Log("SAT: " + colorGrading.saturation.value);
            }

            if (depthOfField.focusDistance.value < 7)
            {
                depthOfField.focusDistance.value += Time.deltaTime * 1.5f;
                Debug.Log("FOC: " + depthOfField.focusDistance.value);
            }
        }

        if (isTakingDmg)
        {
            if (gettingHit)
            {
                Camera.main.transform.position += Random.insideUnitSphere * 0.1f;
            }

            if (playerCamera.fieldOfView < 74)
            {
                playerCamera.fieldOfView += Time.deltaTime * 7;
            }
            if (colorGrading.saturation.value > -100)
            {
                colorGrading.saturation.value -= Time.deltaTime * 3;
            }

            if (depthOfField.focusDistance.value > 0)
            {
                depthOfField.focusDistance.value -= Time.deltaTime * 1.1f;
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        isHealing = false;
        isTakingDmg = true;
        gettingHit = true;

        StartCoroutine("HitDuration");
        currentHp -= _damage;

        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine("WaitForHealing");
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator WaitForHealing()
    {
        yield return new WaitForSecondsRealtime(10.0f);
        isHealing = true;
        isTakingDmg = false;
    }

    public IEnumerator HitDuration()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        gettingHit = false;
    }
}
