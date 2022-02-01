using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class Test : MonoBehaviour
{
    void Start()
    {
        MyClass test_01 = new MyClass();
        test_01.name = "Name";
        test_01.progressStage = 1;
        test_01.skins = new string[] { "def" };

        string data_text = JsonUtility.ToJson(test_01);
        Debug.Log(data_text);

        CheckContainer(test_01.skins);
        test_01.skins = AddElementToContainer(test_01.skins, "rabbit");
        CheckContainer(test_01.skins);

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
}


[Serializable]
public class MyClass
{
    public string name;
    public int progressStage;
    public string[] skins;
}