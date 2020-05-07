using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuInGame : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;
    public GameObject playerObject;
    private FirstPersonController freezePlayer;
    private Pistol pistol;
    private bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        freezePlayer = playerObject.GetComponent<FirstPersonController>();
        pistol = playerObject.transform.GetChild(0).GetChild(2).GetComponentInChildren<Pistol>();
        isGamePaused = false;
        CloseCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isGamePaused)
            {
                CloseCanvas();
            }
            else
            {
                GoToMainMenu();
            }
        }
    }

    public void GoToSettings()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        freezePlayer.enabled = false;
        pistol.enabled = false;

        settingsCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        isGamePaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 0.0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        freezePlayer.enabled = false;
        pistol.enabled = false;

        mainMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        isGamePaused = true;
    }

    public void CloseCanvas()
    {
        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        freezePlayer.enabled = true;
        pistol.enabled = true;

        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = false;

        isGamePaused = false;
    }

    public void SceneChange(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }
}
