using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class TestJson : MonoBehaviour
{
    private TestLevel test;

    void Start()
    {

        //TextAsset level_map_data = (TextAsset)Resources.Load("Maps/Map-01/Level-00");
        
        TextAsset temp = (TextAsset)Resources.Load("JsonTest/TestFolder/Test");
        //TextAsset temp = (TextAsset)Resources.Load("Maps/Map-01/Level-00");

        if (temp == null)
            Debug.Log("plak-plak");

        string jsonData = temp.text;

        test = JsonUtility.FromJson<TestLevel>(jsonData);

    }

    public void setText(TextMeshProUGUI text_test)
    {
        text_test.text = test.name;
    }
}


public class TestLevel
{
    public string name;
}