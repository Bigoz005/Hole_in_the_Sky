using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public int dmg = 15;
    public int ammo = 11;
    public int cardridges = 3;
    public float range = 50f;
    public float smoothHammer = 0.5f;
    public float angleHammer = -60.0f;
    private bool isReadyToShoot = true;
    public Text ammoText;

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

    private AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
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
        ammo = 11;
        cardridges = 3;
    }

    void Update()
    {
        ammoText.text = ammo + " / " + (cardridges * 11).ToString();

        if (isReadyToShoot)
        {
            if (Input.GetButtonDown("Reload"))
            {
                if (cardridges > 0)
                {
                    if (ammo != 11)
                    {
                        ammo = 11;
                        cardridges--;
                        audioSource.PlayOneShot(reloadSound);
                    }
                }
            }

            if (Input.GetButtonDown("Fire1"))
            {
                isReadyToShoot = false;

                StartCoroutine("WaitForShoot");

                if (ammo > 0)
                {
                    Shoot();
                    StartCoroutine("WaitForHammer");
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
                hammer.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationHammer, 30f * Time.deltaTime);
            }
        }
    }

    void Shoot()
    {
        isShooting = true;
    }

    public IEnumerator WaitForHammer()
    {
        yield return new WaitForSeconds(noAmmoSound.length / 8);
        isShooting = false;
        StartCoroutine("WaitForShootEnd");
    }

    public IEnumerator WaitForShootEnd()
    {
        yield return new WaitForSeconds(shootSound.length / 5);

        Ray ray = new Ray(mainCamera.position, mainCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(dmg);
            }

            if (hit.collider.CompareTag("EnemyHead"))
            {
                hit.collider.gameObject.GetComponentInParent<Enemy>().TakeDamageHeadshot(dmg);
            }
        }

        audioSource.PlayOneShot(shootSound);

        slider.localPosition = new Vector3(slider.localPosition.x - sliderMove, slider.localPosition.y, slider.localPosition.z);
        StartCoroutine("WaitForSlider");
        ammo--;
    }

    public IEnumerator WaitForSlider()
    {
        yield return new WaitForSeconds(noAmmoSound.length / 4);
        slider.localPosition = Vector3.MoveTowards(trigger.localPosition, new Vector3(slider.localPosition.x + sliderMove, slider.localPosition.y, slider.localPosition.z), smoothSlider * Time.deltaTime);

    }

    public IEnumerator WaitForTrigger()
    {
        yield return new WaitForSeconds(noAmmoSound.length / 5);
        trigger.localPosition = Vector3.MoveTowards(trigger.localPosition, new Vector3(trigger.localPosition.x + triggerMove, trigger.localPosition.y, trigger.localPosition.z), smoothTrigger * Time.deltaTime);
    }

    public IEnumerator WaitForShoot()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        isReadyToShoot = true;
    }
}

