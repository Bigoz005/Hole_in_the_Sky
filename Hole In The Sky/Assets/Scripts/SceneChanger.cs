using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneChanger : MonoBehaviour
{
    public GameObject settingsCanvas;
    public GameObject mainMenuCanvas;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);       
        RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        RenderSettings.skybox.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        QualitySettings.shadows = ShadowQuality.All;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToSettings()
    {
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void GoToMainMenu()
    {
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
