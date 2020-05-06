using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoMainMenu : MonoBehaviour
{
    private int i = 1;
    public GameObject piano;
    private Random rand = new Random();
    private Dictionary<string, Transform> pianoKeys = new Dictionary<string, Transform>();

    void Start()
    {
        foreach (Transform t in piano.transform)
        {
            pianoKeys.Add(t.name, t);
        }
        StartCoroutine("WaitForStart");
    }

    public void Update()
    {
        if (Random.Range(0, 10) % 2 == 0 && i < 100)
        {
            if (i % 3 == 0)
            {
                StartCoroutine("WaitWhole", "A#3");
            }
            else if (i % 4 == 0)
            {
                StartCoroutine("WaitWhole", "C7");
                StartCoroutine("WaitWhole", "E5");
            }
            else if (i % 5 == 0)
            {
                StartCoroutine("WaitHalf", "G#4");
                StartCoroutine("WaitHalf", "G5");
            }
            else if (i % 7 == 0)
            {
                StartCoroutine("WaitWhole", "F5");
            }
            else if (i % 9 == 0)
            {
                StartCoroutine("WaitHalf", "C1");
                StartCoroutine("WaitHalf", "F4");
            }
            else
            {
                StartCoroutine("WaitWhole", "G4");
            }
        }
        else
        {
            if (i % 3 == 0)
            {
                StartCoroutine("WaitWhole", "F#7");
                StartCoroutine("WaitHalf", "G7");
            }
            else if (i % 4 == 0)
            {
                StartCoroutine("WaitWhole", "D4");
                StartCoroutine("WaitHalf", "G8");
            }
            else if (i % 5 == 0)
            {
                StartCoroutine("WaitHalf", "E2");
                StartCoroutine("WaitHalf", "G#6");
            }
            else if (i % 7 == 0)
            {
                StartCoroutine("WaitWhole", "D3");
                StartCoroutine("WaitHalf", "F8");
            }
            else if (i % 9 == 0)
            {
                StartCoroutine("WaitHalf", "D8");
            }
            else
            {
                StartCoroutine("WaitWhole", "G6");
            }
        }
        if (i >= 100)
        {
            i = 1;
        }
    }

    public IEnumerator WaitForStart()
    {
        yield return new WaitForSecondsRealtime(25);
        i++;
    }

    public IEnumerator WaitWhole(string pianoKeyName)
    {
        Transform pianoKey = pianoKeys["key" + pianoKeyName];
        pianoKey.GetComponent<PianoKey>().playSoundMenu();
        yield return new WaitForSecondsRealtime(2);
        i++;
    }

    public IEnumerator WaitHalf(string pianoKeyName)
    {
        Transform pianoKey = pianoKeys["key" + pianoKeyName];
        pianoKey.GetComponent<PianoKey>().playSoundMenu();
        yield return new WaitForSecondsRealtime(1);
        i++;
    }
}
