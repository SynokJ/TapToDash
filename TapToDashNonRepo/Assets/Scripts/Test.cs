using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{

    public SkinManager sm;
    private MyClass test_01;
    private string data_text;

    void Start()
    {
        test_01 = new MyClass();
        test_01.name = "Name";
        test_01.progressStage = 1;
        test_01.skins = new string[] { "def" };

        data_text = JsonUtility.ToJson(test_01);

        CheckContainer(test_01.skins);
        test_01.skins = AddElementToContainer(test_01.skins, "rabbit");
        CheckContainer(test_01.skins);

        data_text = JsonUtility.ToJson(test_01);

        //File.WriteAllText(Application.persistentDataPath + "/Resources/Test.json", data_text);
        File.WriteAllText(Application.dataPath + "/Resources/Test.json", data_text);
    }

    public void CheckContainer(string[] box)
    {
        string res = "";

        foreach (string s in box)
            res += s + " ";

        Debug.Log(res);
    }

    public string[] AddElementToContainer(string[] skin_box, string element)
    {
        List<string> temp_box = new List<string>();
        foreach (string s in skin_box)
            temp_box.Add(s);
        temp_box.Add(element);

        return (string[])temp_box.ToArray();
    }

    public void UnlockSkin()
    {
        foreach (string s in test_01.skins)
            if (s == sm.GetCurSkin().name)
                Debug.Log(s);
            else
                Debug.Log(s + " is not open");

    }
}


[Serializable]
public class MyClass
{
    public string name;
    public int progressStage;
    public string[] skins;
}