using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class SkinManager : MonoBehaviour
{

    private int skinId = 0;

    public TextMeshProUGUI test;

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
        pp = new PlayerProgress();
        pp.name = "player";
        pp.progressStage = 0;
        InitUnlockedSkins();

        string data = JsonUtility.ToJson(pp);

        //string path = Path.Combine(Application.persistentDataPath, "saved files", "data.json");

        //if (Application.persistentDataPath != null)
        //    //    File.WriteAllText(Application.persistentDataPath + "/Resources/Test.json", data);
        //    //else
        //    Debug.Log(path);

        //File.WriteAllText(Application.dataPath + "/Resources/Test.json", data);

        test.text = Application.persistentDataPath + "Test.json";
    }

    public void RefreshPlayerProgress()
    {
        InitUnlockedSkins();

        string data = JsonUtility.ToJson(pp);
        Debug.Log(data);

        //File.WriteAllText(Application.persistentDataPath + "/Resources/Test.json", data_text);
        //File.WriteAllText(Application.dataPath + "/Resources/Test.json", data);
    }

    public void InitUnlockedSkins()
    {

        //string res = "";

        foreach (Character ch in skins)
        {
            if (ch.isOpen)
            {
                //res += ch.name + "\t";
                pp.skins = UnlockCurrentSkin(pp.skins, ch.name);
            }
        }

        //Debug.Log("Open Skins : " + res);
    }

    public void OpenSkinButtonClicked()
    {
        int coin_number = PlayerPrefs.GetInt("CoinNum", 0);

        if (skins[skinId].isOpen)
            return;

        if (coin_number - skins[skinId].cost >= 0)
        {
            //Debug.Log("Open Skin : " + skins[skinId].name + "\tCost : " + skins[skinId].cost);
            skins[skinId].isOpen = true;
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
        player_test.GetComponent<MeshRenderer>().material = skins[skinId].material;
        player_name.text = skins[skinId].name;

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
    public string[] skins;
}