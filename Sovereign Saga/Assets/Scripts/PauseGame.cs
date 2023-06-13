using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isGamePaused = false;

    public GameObject pauseMenu;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (isGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
    }

}
