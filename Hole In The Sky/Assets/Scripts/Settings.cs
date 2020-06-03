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
    private GlobalSettingsData globalSettingsData;

    void Start()
    {
        globalSettingsData = GameObject.Find("GlobalSettings").gameObject.GetComponent<GlobalSettingsData>();
        sound = globalSettingsData.sound;
        slider.value = globalSettingsData.sliderValue;
        value = globalSettingsData.sliderValue;
        QualitySettings.shadows = globalSettingsData.shadowQuality;

        wasChanged = false;

        if (globalSettingsData.shadowQuality == ShadowQuality.All)
        {
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 2";
        }
        else if (globalSettingsData.shadowQuality == ShadowQuality.HardOnly)
        {
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 1";
        }
        else
        {
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 0";
        }

        if (sound)
        {
            soundText.GetComponentInChildren<Text>().text = "Sounds: 1";
        }
        else
        {
            soundText.GetComponentInChildren<Text>().text = "Sounds: 0";
        }
    }

    private void Update()
    {
        RenderSettings.ambientLight = new Color(value, value, value, 1.0f);
        RenderSettings.skybox.color = new Color(value, value, value, 1.0f);
        RenderSettings.reflectionIntensity = value;
        RenderSettings.ambientIntensity = value;
    }

    public void ChangeShadows()
    {
        if (QualitySettings.shadows == ShadowQuality.Disable && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.All;
            globalSettingsData.shadowQuality = ShadowQuality.All;
            QualitySettings.shadowmaskMode = ShadowmaskMode.DistanceShadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 2";
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.All && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
            globalSettingsData.shadowQuality = ShadowQuality.HardOnly;
            QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 1";
            wasChanged = true;
        }

        if (QualitySettings.shadows == ShadowQuality.HardOnly && !wasChanged)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            globalSettingsData.shadowQuality = ShadowQuality.Disable;
            QualitySettings.shadowmaskMode = ShadowmaskMode.Shadowmask;
            shadowText.GetComponentInChildren<Text>().text = "Shadows: 0";
            wasChanged = true;
        }

        wasChanged = false;
    }

    public void ChangeBrightness()
    {
        value = slider.value;
        globalSettingsData.sliderValue = slider.value;
    }

    public void ChangeSound()
    {
        sound = !sound;
        if (sound)
        {
            AudioListener.volume = 1.0f;
            globalSettingsData.sound = true;
            soundText.GetComponentInChildren<Text>().text = "Sounds: 1";
        }
        else
        {
            AudioListener.volume = 0f;
            globalSettingsData.sound = false;
            soundText.GetComponentInChildren<Text>().text = "Sounds: 0";
        }
    }
}
