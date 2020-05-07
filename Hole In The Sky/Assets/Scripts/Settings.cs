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
        RenderSettings.skybox.color = new Color(value, value, value, 1.0f);
        RenderSettings.reflectionIntensity = value;
        RenderSettings.ambientIntensity = value;
        /*RenderSettings.ambientEquatorColor = new Color(value, value, value, 1.0f);
        RenderSettings.ambientGroundColor = new Color(value, value, value, 1.0f);
        RenderSettings.ambientSkyColor = new Color(value, value, value, 1.0f);
        */
    }

    public void ChangeShadows()
    {
        if (QualitySettings.shadows == ShadowQuality.Disable && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.All;
            QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 2";
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.All && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 1";
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.HardOnly && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 0";
            wasChanged = true;
        }

        wasChanged = false;
    }

    public void ChangeBrightness()
    {
        value = slider.value;
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
