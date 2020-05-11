using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuInGame : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingsCanvas;
    public GameObject playerObject;
    private FirstPersonController playerController;
    private Pistol pistol;
    private bool isGamePaused;

    // Start is called before the first frame update
    void Start()
    {
        playerController = playerObject.GetComponent<FirstPersonController>();
        pistol = playerObject.transform.GetChild(0).GetChild(2).GetComponentInChildren<Pistol>();
        isGamePaused = false;
        CloseCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        PrepareForMenu();

        settingsCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
        isGamePaused = true;
    }

    public void GoToMainMenu()
    {
        PrepareForMenu();

        mainMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
        isGamePaused = true;
    }

    public void CloseCanvas()
    {
        PrepereForGame();

        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = false;

        isGamePaused = false;
    }

    public void PrepareForMenu()
    {
        Time.timeScale = 0.0f;

        playerController.canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        playerController.enabled = false;
        pistol.enabled = false;
    }

    public void PrepereForGame()
    {
        Time.timeScale = 1.0f;

        playerController.canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerController.enabled = true;
        pistol.enabled = true;
    }

    public void SceneChange(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }
}
