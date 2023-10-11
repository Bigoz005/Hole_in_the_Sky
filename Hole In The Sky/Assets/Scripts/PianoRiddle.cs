using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PianoRiddle : MonoBehaviour
{
    public GameObject paperPage;
    public GameObject piano;
    public GameObject portal;
    public GameObject portalRoom;
    public GameObject player;

    private bool isMainMenu = false;
    private ArrayList keysD = new ArrayList() { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };
    private ArrayList keysFis = new ArrayList() { "F#1", "F#2", "F#3", "F#4", "F#", "F#6", "F#7", "F#8", "F#9" };
    private ArrayList keysA = new ArrayList() { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9" };
    private bool pianoKeyDPressed;
    private bool pianoKeyFisPressed;
    private bool pianoKeyAPressed;
    private bool resolved;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;

    void Start()
    {
        isMainMenu = SceneManager.GetActiveScene().name.Equals("MainMenu");
        
        if (!isMainMenu)
        {
            portal.SetActive(false);
            portalRoom.SetActive(false);
            resolved = false;
        }

        PostProcessVolume postProcessVolume = player.GetComponentInChildren<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);
    }

    void Update()
    {
        if (resolved)
        {
            if (colorGrading.saturation.value < 100.0f)
            {
                colorGrading.saturation.value += Time.deltaTime * 7.0f;

            }

            if (colorGrading.postExposure.value < 3.0f)
            {
                colorGrading.postExposure.value += Time.deltaTime * 2.0f;
            }

            if (colorGrading.tint.value < 100.0f)
            {
                colorGrading.tint.value += Time.deltaTime * 4.0f;
            }

            Camera.main.transform.position += Random.insideUnitSphere * 0.05f;
            player.GetComponent<FirstPersonController>().canJump = false;
            player.GetComponent<FirstPersonController>().canRun = false;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 1.4f;
        }

    }


    public void CheckKeys(string pianoKeyName)
    {

        Debug.Log(pianoKeyName);
        if (keysD.IndexOf(pianoKeyName) != -1)
        {
            pianoKeyDPressed = true;
            Debug.Log("pianoKeyDPressed");
        }
        else
        {
            if (keysFis.IndexOf(pianoKeyName) != -1)
            {
                pianoKeyFisPressed = true;
                Debug.Log("pianoKeyFisPressed");
            }
            else
            {
                if (keysA.IndexOf(pianoKeyName) != -1)
                {
                    pianoKeyAPressed = true;
                    Debug.Log("pianoKeyAPressed");
                }
                else
                {
                    Debug.Log("wrong");
                    pianoKeyDPressed = false;
                    pianoKeyFisPressed = false;
                    pianoKeyAPressed = false;
                }
            }
        }

        CheckSolution();
    }

    public void CheckSolution()
    {
        if (pianoKeyAPressed && pianoKeyDPressed && pianoKeyFisPressed && player.GetComponent<PlayerInventory>().GetIsNoteChecked())
        {
            resolved = true;
            chromaticAberration.intensity.value = 1.0f;

            portal.SetActive(true);
            portalRoom.SetActive(true);
        }
    }

}
