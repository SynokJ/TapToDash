using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Building_ButtonController : MonoBehaviour
{

    public Building_LevelLoader bll;

    public void OnSaveButtonCLicked()
    {
        //bll.GetLevelBox().AddLevel(bll.GetLevel());
        //string data = JsonUtility.ToJson(bll.GetLevelBox());

        //StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Custom.json");
        //Debug.Log(Application.persistentDataPath + "/Custom.json");
        //sw.Write(data);
        //sw.Close();

        //StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");
        //Debug.Log(sr.ReadToEnd());
        //sr.Close();

        Debug.Log("Button Controller");
    }


    //private void SaveCustomLevel()
    //{
    //    levelBox.AddLevel(level);
    //    string data = JsonUtility.ToJson(levelBox);

    //    StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Custom.json");
    //    Debug.Log(Application.persistentDataPath + "/Custom.json");
    //    sw.Write(data);
    //    sw.Close();

    //    StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");
    //    Debug.Log(sr.ReadToEnd());
    //    sr.Close();
    //}
}
