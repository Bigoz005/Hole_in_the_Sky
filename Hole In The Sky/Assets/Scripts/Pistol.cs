using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public int dmg = 15;
    public int ammo = 22;
    public float range = 50f;
    public float smoothHammer = 0.5f;
    public float angleHammer = -60.0f;

    public float smoothSlider = 20f;
    public float smoothTrigger = 20f;
    public float sliderMove = 0.1f;
    public float triggerMove = 0.02f;
    private Vector3 firstPositionSlider;
    private Vector3 firstPositionTrigger;

    private Transform mainCamera;
    private Transform hammer;
    private Transform slider;
    private Transform trigger;
    private bool isShooting = false;
    private bool isTrigger = false;

    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip noAmmoSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        hammer = transform.Find("Handgun_M1911A (Model)").transform.Find("Hammer").transform;
        slider = transform.Find("Handgun_M1911A (Model)").transform.Find("Slider").transform;
        trigger = transform.Find("Handgun_M1911A (Model)").transform.Find("Trigger").transform;
        firstPositionSlider = slider.position;
        firstPositionTrigger = trigger.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isTrigger = false;
            if (ammo > 0)
            {
                Shoot();
            }
            else
            {
                audioSource.PlayOneShot(noAmmoSound);
            }
            trigger.localPosition = new Vector3(trigger.localPosition.x - triggerMove, trigger.localPosition.y, trigger.localPosition.z);
            StartCoroutine("WaitForTrigger");
        }

        if (isShooting)
        {
            Quaternion targetRotationHammer = Quaternion.Euler(0, angleHammer, 0);
            hammer.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationHammer, smoothHammer * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationHammer = Quaternion.Euler(0, 0, 0);
            hammer.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationHammer, 20f * Time.deltaTime);
        }

    }

    void Shoot()
    {
        isShooting = true;
        StartCoroutine("WaitForShootEnd");
    }

    public IEnumerator WaitForShootEnd()
    {
        yield return new WaitForSeconds(shootSound.length / 4);

        Ray ray = new Ray(mainCamera.position, mainCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                //Dmg Enemy
            }
        }
        audioSource.PlayOneShot(shootSound);
        slider.localPosition = new Vector3(slider.localPosition.x - sliderMove, slider.localPosition.y, slider.localPosition.z);
        StartCoroutine("WaitForSlider");
        ammo--;
        isShooting = false;
    }

    public IEnumerator WaitForSlider()
    {
        yield return new WaitForSeconds(shootSound.length / 4);
        slider.localPosition = Vector3.MoveTowards(trigger.localPosition, new Vector3(slider.localPosition.x + sliderMove, slider.localPosition.y, slider.localPosition.z), smoothSlider * Time.deltaTime);

    }
    public IEnumerator WaitForTrigger()
    {
        yield return new WaitForSeconds(noAmmoSound.length / 2);
        trigger.localPosition = Vector3.MoveTowards(trigger.localPosition, new Vector3(trigger.localPosition.x + triggerMove, trigger.localPosition.y, trigger.localPosition.z), smoothTrigger * Time.deltaTime);
    }
}
