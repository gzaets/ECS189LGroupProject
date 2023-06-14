using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject backButton;
    public GameObject smallTitle;
    public GameObject quitButton;
    public GameObject text;

    public static bool isTutorial = false;

    private void Start()
    {
        backButton.SetActive(false);
        smallTitle.SetActive(false);
        text.SetActive(false);
        quitButton.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
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