using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public void onResetButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestartButtonClicked()
    {
        SetGlobalCoinNum();
        PlayerPrefs.DeleteKey("CurLevel");
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRestarCustomButtonClicked()
    {
        SceneManager.LoadScene("CustomLevel");
    }

    public void OnExitButtonClicked()
    {
        PlayerPrefs.Save();
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

    public void OnBuilderLoadButtonClicked()
    {
        SceneManager.LoadScene("MapBuilder");
    }

    public void OnTestButtonClicked()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");
        Debug.Log(sr.ReadToEnd());
        sr.Close();

        SceneManager.LoadScene("CustomLevel");
    }

    public void OnCloseHelpButtonClicked(GameObject helpPanel)
    {
        helpPanel.gameObject.SetActive(false);
    }

    public void OnHelpButtonClicked(GameObject helpPanel)
    {
        helpPanel.gameObject.SetActive(true);
    }
}
