using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.Characters.FirstPerson;

public class PianoRiddle : MonoBehaviour
{
    public GameObject paperPage;
    public GameObject piano;
    public GameObject portal;
    public GameObject player;
    private Dictionary<string, Transform> pianoKeys = new Dictionary<string, Transform>();
    private Transform pianoKeyD;
    private Transform pianoKeyF;
    private Transform pianoKeyA;
    private bool pianoKeyDPressed;
    private bool pianoKeyFPressed;
    private bool pianoKeyAPressed;
    private bool resolved;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;

    void Start()
    {
        portal.SetActive(false);
        resolved = false;

        PostProcessVolume postProcessVolume = player.GetComponentInChildren<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings<ColorGrading>(out colorGrading);

        foreach (Transform t in piano.transform)
        {
            pianoKeys.Add(t.name, t);
        }

        pianoKeyD = pianoKeys["keyD2"];
        pianoKeyF = pianoKeys["keyF#2"];
        pianoKeyA = pianoKeys["keyA2"];

        pianoKeys.Remove("keyD2");
        pianoKeys.Remove("keyF#2");
        pianoKeys.Remove("keyA2");
        pianoKeys.Remove("GrandPianoBody");
    }

    void Update()
    {
        if (paperPage.activeSelf)
        {
            CheckKeys();
            CheckSolution();
            CheckOtherKeys();
        }

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

    public void CheckKeys()
    {
        if (pianoKeyD.GetComponent<PianoKey>().isPressed)
        {
            pianoKeyDPressed = true;
        }

        if (pianoKeyF.GetComponent<PianoKey>().isPressed)
        {
            pianoKeyFPressed = true;
        }

        if (pianoKeyA.GetComponent<PianoKey>().isPressed)
        {
            pianoKeyAPressed = true;
        }
    }

    public void CheckOtherKeys()
    {
        foreach (KeyValuePair<string, Transform> pianokey in pianoKeys)
        {
            if (pianokey.Value.GetComponent<PianoKey>().isPressed)
            {
                pianoKeyDPressed = false;
                pianoKeyFPressed = false;
                pianoKeyAPressed = false;
            }
        }
    }

    public void CheckSolution()
    {
        if (pianoKeyAPressed && pianoKeyDPressed && pianoKeyFPressed)
        {
            resolved = true;
            chromaticAberration.intensity.value = 1.0f;

            portal.SetActive(true);
        }
    }

}
