using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // The name of the scene you want to switch to

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}