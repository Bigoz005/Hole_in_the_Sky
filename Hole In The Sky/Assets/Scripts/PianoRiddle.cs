using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoRiddle : MonoBehaviour
{
    public GameObject paperPage;
    public GameObject piano;
    private Dictionary<string, Transform> pianoKeys = new Dictionary<string, Transform>();
    private Transform pianoKeyD;
    private Transform pianoKeyF;
    private Transform pianoKeyA;
    private bool pianoKeyDPressed;
    private bool pianoKeyFPressed;
    private bool pianoKeyAPressed;

    void Start()
    {
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
            Debug.Log("NICE");
        }
    }

}
