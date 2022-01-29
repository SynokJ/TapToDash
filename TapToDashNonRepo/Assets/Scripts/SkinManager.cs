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

    private void Start()
    {
        SetCurrentSkin();
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
    }

    public void SaveCurrentSkinIdentifier()
    {
        PlayerPrefs.SetString("SkinName", player_name.text);
    }
}
