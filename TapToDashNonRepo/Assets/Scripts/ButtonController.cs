using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLevelScene()
    {
        SetGlobalCoinNum();
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnContinButtonClicked()
    {
        SetGlobalCoinNum();
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnStartButtonClicked()
    {
        SetGlobalCoinNum();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnMenuButtonClicked()
    {
        SetGlobalCoinNum();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonClicked()
    {
        SetGlobalCoinNum();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void SetGlobalCoinNum()
    {
        int coin_num = PlayerPrefs.GetInt("CoinNum", 0) + PlayerPrefs.GetInt("CoinNumTemp", 0);
        PlayerPrefs.SetInt("CoinNum", coin_num);
    }
}
