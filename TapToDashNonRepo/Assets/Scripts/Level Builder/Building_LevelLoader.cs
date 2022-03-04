using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Building_LevelLoader : MonoBehaviour
{

    public BuildingGrid bg;

    private Level level;
    private LevelBox levelBox;

    private void Start()
    {
        level = new Level();
        //levelBox = new LevelBox();

        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");

        levelBox = JsonUtility.FromJson<LevelBox>(sr.ReadToEnd());
        sr.Close();
    }

    public void OnSaveButtonClicked()
    {
        InitCurLevel();
        SaveCustomLevel();
    }

    private void SaveCustomLevel()
    {
        levelBox.AddLevel(level);
        string data = JsonUtility.ToJson(levelBox);

        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Custom.json");
        sw.Write(data);

        Debug.Log(data);
        sw.Close();
    }

    private void InitCurLevel()
    {

        level.name = "noname";
        level.style = "nostyle";
        level.cmds = new string[] { "run" };

        string[] map = bg.GetCustomLevel().ToArray();
        level.map = map;
    }


    private void TestContainer(string[] map)
    {
        string res = "";

        foreach (string s in map)
            res += s + "\n";

        Debug.Log(res);
    }

    public LevelBox GetLevelBox()
    {
        return levelBox;
    }

    public Level GetLevel()
    {
        return level;
    }
}

//public void OnResetButtonClicked()
//{
//    levelBox = new LevelBox();
//    string data = JsonUtility.ToJson(levelBox);

//    StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Custom.json");
//    Debug.Log("Reseted");
//    sw.Write(data);
//    sw.Close();
//}
