using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinManager : MonoBehaviour
{

    private int skinId = 0;
    public List<Character> skins;
    public GameObject player_test;
    public TextMeshProUGUI player_name;
    public TextMeshProUGUI coins;


    class Player
    {
        public int coins;
        public string[] skins;
    }

    private void Start()
    {
        SetCurrentSkin();
        setCoins();

        TextAsset player_data = (TextAsset)Resources.Load<TextAsset>("PlayerData");

        if (player_data == null)
            Debug.Log("GG");

        Player player = JsonUtility.FromJson<Player>(player_data.text);
        Debug.Log(player.coins);

        string skins_res = "";

        foreach (string s in player.skins)
            skins_res += s + " ";

        Debug.Log(skins_res);
    }

    public void setCoins()
    {
        int coin_num = PlayerPrefs.GetInt("CoinNum", 0) + PlayerPrefs.GetInt("CoinNumTemp", 0);
        coins.text = "Money: " + coin_num.ToString();
        PlayerPrefs.SetInt("CoinNum", coin_num);
    }

    public void OnNextButtonClicked()
    {
        skinId++;
        if (skinId == skins.Count)
            skinId = 0;

        SetCurrentSkin();
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

        player_name.fontStyle = IsSkinOpen() ? FontStyles.Normal : FontStyles.Strikethrough;
    }

    public void SaveCurrentSkinIdentifier()
    {
        if (!IsSkinOpen())
            return;

        PlayerPrefs.SetString("SkinName", player_name.text);
    }

    private bool IsSkinOpen()
    {
        return skins[skinId].isOpen;
    }
}
