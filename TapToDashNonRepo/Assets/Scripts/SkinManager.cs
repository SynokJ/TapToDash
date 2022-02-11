using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class SkinManager : MonoBehaviour
{

    private int skinId = 0;

    public GameObject mainMenu;
    public GameObject buyMenu;
    public TextMeshProUGUI price_text;

    public PlayerProgress pp;
    public List<Character> skins;
    public GameObject player_test;
    public TextMeshProUGUI player_name;
    public TextMeshProUGUI coins;


    private void Start()
    {
        // The first time of running the game
        if (PlayerPrefs.GetInt("HasPlayed") == 0)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("HasPlayed", 1);
            pp = null;
        }

        InitPlayerProgress();
        SetCurrentSkin();
        setCoins();
    }

    public void setCoins()
    {
        int coin_num = PlayerPrefs.GetInt("CoinNum", 0);

        if (PlayerPrefs.GetInt("CoinNumTemp", 0) != 0)
            coin_num += PlayerPrefs.GetInt("CoinNumTemp", 0);

        coins.text = "Money: " + coin_num.ToString();

        PlayerPrefs.SetInt("CoinNum", coin_num);
        PlayerPrefs.SetInt("CoinNumTemp", 0);
    }

    public void OnNextButtonClicked()
    {
        skinId++;
        if (skinId == skins.Count)
            skinId = 0;

        SetCurrentSkin();
    }

    public void InitPlayerProgress()
    {
        if (pp == null)
        {
            pp = new PlayerProgress();
            //CheckPlayerProgressFile();
        }
        else
        {
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Test.json");
            pp = JsonUtility.FromJson<PlayerProgress>(sr.ReadToEnd());

            sr.Close();
        }

        SavePlayerData();
        InitUnlockedSkins();
    }

    public void CheckPlayerProgressFile()
    {
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Test.json");
        Debug.Log(sr.ReadToEnd());
        sr.Close();
    }

    public void SavePlayerData()
    {
        InitUnlockedSkins();

        string data = JsonUtility.ToJson(pp);

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Test.json");
        sw.Write(data);
        sw.Close();
    }

    public void RefreshPlayerProgress()
    {
        InitUnlockedSkins();
        SavePlayerData();

        string res = JsonUtility.ToJson(pp);
    }

    public void InitUnlockedSkins()
    {
        foreach (Character ch in skins)
            if (Array.IndexOf(pp.skins, ch.name) >= 0)
                ch.isOpen = true;
            else
                ch.isOpen = false;
    }

    public void OpenSkinButtonClicked()
    {
        int coin_number = PlayerPrefs.GetInt("CoinNum", 0);

        if (skins[skinId].isOpen)
            return;

        if (coin_number - skins[skinId].cost >= 0)
        {
            pp.skins = UnlockCurrentSkin(pp.skins, skins[skinId].name);
            PlayerPrefs.SetInt("CoinNum", coin_number - skins[skinId].cost);
            coins.text = "Money: " + (coin_number - skins[skinId].cost).ToString();
        }

        RefreshPlayerProgress();
        mainMenu.SetActive(skins[skinId].isOpen);
        buyMenu.SetActive(!skins[skinId].isOpen);
    }

    public string[] UnlockCurrentSkin(string[] skin_box, string element)
    {
        List<string> temp_box = new List<string>();

        if (skin_box != null)
            foreach (string s in skin_box)
                temp_box.Add(s);

        if (!temp_box.Contains(element))
            temp_box.Add(element);

        return (string[])temp_box.ToArray();
    }

    public void OnPrevButtonClicked()
    {
        skinId--;
        if (skinId < 0)
            skinId = skins.Count - 1;

        SetCurrentSkin();
    }

    private void SetCurrentSkin()
    {
        // init new materials

        player_test.GetComponent<MeshRenderer>().material = skins[skinId].material;
        player_name.text = skins[skinId].name;

        if(skins[skinId].test_material != null)
        {
            player_test.GetComponent<MeshFilter>().mesh = skins[skinId].test_mesh;
            player_test.GetComponent<MeshRenderer>().materials = skins[skinId].test_material;
            player_test.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        mainMenu.SetActive(skins[skinId].isOpen);
        buyMenu.SetActive(!skins[skinId].isOpen);

        if (!skins[skinId].isOpen)
            price_text.text = "Price: " + skins[skinId].cost.ToString();
    }

    public void SaveCurrentSkinIdentifier()
    {
        PlayerPrefs.SetString("SkinName", player_name.text);
    }

    public Character GetCurSkin()
    {
        return skins[skinId];
    }
}


[Serializable]
public class PlayerProgress
{
    public string name;
    public int progressStage;
    public int cur_money;
    public string[] skins;

    public PlayerProgress()
    {
        name = "player";
        progressStage = 0;
        cur_money = 0;
        skins = new string[] { "def" };
    }
}

[Serializable]
public class GameRunstate
{
    public bool isFirstTime;
}