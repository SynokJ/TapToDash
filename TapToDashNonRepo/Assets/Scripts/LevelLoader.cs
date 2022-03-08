using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    public LevelBox curMap = new LevelBox();

    void Awake()
    {

        if (SceneManager.GetActiveScene().name != "CustomLevel")
            InitGameLevels();
        else
            InitCustomLevels();
    }

    private void InitCustomLevels()
    {

        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");
        
        curMap = JsonUtility.FromJson<LevelBox>(sr.ReadToEnd());
        //curMap.ReverseLevelsMap();
        sr.Close();
    }

    private void InitGameLevels()
    {
        TextAsset jsonText = (TextAsset)Resources.Load<TextAsset>("Map-01");

        if (jsonText == null)
        {
            Debug.Log("gg");
            return;
        }

        curMap = JsonUtility.FromJson<LevelBox>(jsonText.text);
        curMap.ReverseLevelsMap();
    }
}
