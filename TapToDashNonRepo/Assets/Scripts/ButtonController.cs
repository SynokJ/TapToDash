using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLevelScene()
    {
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnContinButtonClicked()
    {
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnStartButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
