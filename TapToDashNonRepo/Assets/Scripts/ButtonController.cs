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
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnStartButtonClicked()
    {
        PlayerPrefs.DeleteKey("CurLevel");
        SceneManager.LoadSceneAsync("GameLevel");
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonClicked()
    {
        SetGlobalCoinNum();
        PlayerPrefs.DeleteKey("CurLevel");
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }

    public void SetGlobalCoinNum()
    {
        int coin_num = PlayerPrefs.GetInt("CoinNum", 0);

        if (PlayerPrefs.GetInt("CoinNumTemp", 0) != 0)
            coin_num += PlayerPrefs.GetInt("CoinNumTemp", 0);

        PlayerPrefs.SetInt("CoinNum", coin_num);
        PlayerPrefs.SetInt("CoinNumTemp", 0);
    }
}
