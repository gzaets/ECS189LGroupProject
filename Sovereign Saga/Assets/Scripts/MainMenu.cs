using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject backButton;
    public GameObject quitButton;

    private void Start()
    {
        backButton.SetActive(false);
        quitButton.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowHowToPlay()
    {
        backButton.SetActive(true);
        quitButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}