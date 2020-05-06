using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Slider slider;
    public Button shadowText;
    public Button soundText;
    private float value;
    private bool sound;
    private bool wasChanged;

    void Start()
    {
        sound = true;
        wasChanged = false;
        QualitySettings.shadows = ShadowQuality.All;
        shadowText.GetComponentInChildren<Text>().text = "Shadows: 2";
        soundText.GetComponentInChildren<Text>().text = "Sounds: 1";
        slider.value = 0.5f;
        value = 0.5f;
    }

    private void Update()
    {
        RenderSettings.ambientLight = new Color(value, value, value, 1.0f);
    }

    public void ChangeShadows()
    {
        if (QualitySettings.shadows == ShadowQuality.Disable && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.All;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 2";
            Debug.Log("2");
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.All && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 1";
            Debug.Log("1");
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.HardOnly && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 0";
            Debug.Log("0");
            wasChanged = true;
        }

        wasChanged = false;
    }

    public void ChangeBrightness()
    {
        value = slider.value;
        Debug.Log(value);
    }

    public void ChangeSound()
    {
        sound = !sound;
        if (sound)
        {
            soundText.GetComponentInChildren<Text>().text = "Sounds: 1";
        }
        else
        {
            soundText.GetComponentInChildren<Text>().text = "Sounds: 0";
        }
    }
}
