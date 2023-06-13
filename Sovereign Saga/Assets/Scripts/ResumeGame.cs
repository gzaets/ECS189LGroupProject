using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    public GameObject pauseMenu;

    public void GameResume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
