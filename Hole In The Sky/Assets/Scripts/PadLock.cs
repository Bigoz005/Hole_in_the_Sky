using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PadLock : MonoBehaviour
{
    public Canvas safeCanvas;
    public GameObject playerObject;
    public GameObject chestObject;

    public AudioSource audioSource;
    public AudioClip upClickSound;
    public AudioClip downClickSound;
    public AudioClip unlockSound;

    private int number01 = 0;
    private int number02 = 0;
    private int number03 = 0;
    private int number04 = 0;

    private bool wasPlayed = false;

    private string currentCode = "0000";
    private string code01 = "2137";

    public Text number1Text;
    public Text number2Text;
    public Text number3Text;
    public Text number4Text;

    public Button[] buttons = new Button[8];


    private void Start()
    {
        safeCanvas.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowSafeCanvas()
    {
        safeCanvas.enabled = true;
        playerObject.GetComponent<FirstPersonController>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideSafeCanvas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        safeCanvas.enabled = false;
        playerObject.GetComponent<FirstPersonController>().enabled = true;
    }

    public void IncreaseNumber(int _number)
    {
        audioSource.PlayOneShot(upClickSound);

        if (_number == 1)
        {
            number01++;

            if (number01 > 9)
            {
                number01 = 0;
            }

            number1Text.text = number01.ToString();
        }
        if (_number == 2)
        {
            number02++;

            if (number02 > 9)
            {
                number02 = 0;
            }

            number2Text.text = number02.ToString();
        }
        if (_number == 3)
        {
            number03++;

            if (number03 > 9)
            {
                number03 = 0;
            }

            number3Text.text = number03.ToString();
        }
        if (_number == 4)
        {
            number04++;

            if (number04 > 9)
            {
                number04 = 0;
            }

            number4Text.text = number04.ToString();
        }

        currentCode = (number1Text.text + number2Text.text + number3Text.text + number4Text.text);
    }

    public void DecreaseNumber(int _number)
    {
        audioSource.PlayOneShot(downClickSound);
        if (_number == 1)
        {
            number01--;

            if (number01 < 0)
            {
                number01 = 9;
            }
            number1Text.text = number01.ToString();
        }

        if (_number == 2)
        {
            number02--;

            if (number02 < 0)
            {
                number02 = 9;
            }
            number2Text.text = number02.ToString();
        }

        if (_number == 3)
        {
            number03--;

            if (number03 < 0)
            {
                number03 = 9;
            }
            number3Text.text = number03.ToString();
        }

        if (_number == 4)
        {
            number04--;

            if (number04 < 0)
            {
                number04 = 9;
            }
            number4Text.text = number04.ToString();
        }

        currentCode = (number1Text.text + number2Text.text + number3Text.text + number4Text.text);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            HideSafeCanvas();
        }

        if (currentCode.Equals(code01))
        {
            foreach (Button button in buttons)
            {
                button.enabled = false;
            }

            chestObject.GetComponentInChildren<ChestDoor>().gameObject.layer = 8;
            HideSafeCanvas();
            BeforeDestroy();
        }
    }

    private void BeforeDestroy()
    {
        if (!wasPlayed)
        {
            audioSource.PlayOneShot(unlockSound);
            wasPlayed = true;
        }

        StartCoroutine("WaitForDestroy");
    }

    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(unlockSound.length);
        Destroy(gameObject);
    }

  

}
