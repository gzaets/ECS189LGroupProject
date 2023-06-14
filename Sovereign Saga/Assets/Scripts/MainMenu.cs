using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject backButton;
    public GameObject smallTitle;
    public GameObject quitButton;
    public GameObject text;
    public GameObject loadingScreen;

    public static float transitionTime = 1f;

    public static bool isTutorial = false;

    private void Start()
    {
        backButton.SetActive(false);
        smallTitle.SetActive(false);
        text.SetActive(false);
        quitButton.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        isTutorial = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        isTutorial = false;
    }
}