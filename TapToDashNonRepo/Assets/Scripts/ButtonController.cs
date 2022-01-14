using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void loadLevelScene()
    {
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void onContinButtonClicked()
    {
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void onStartButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void onMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void onRestartButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void onExitButtonClicked()
    {
        Application.Quit();
    }
}
