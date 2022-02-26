using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public LevelBox curMap = new LevelBox();

    void Awake()
    {
        TextAsset jsonText = (TextAsset)Resources.Load<TextAsset>("Map-01");
        
        if(jsonText == null)
        {
            Debug.Log("gg");
            return;
        }

        curMap = JsonUtility.FromJson<LevelBox>(jsonText.text);
        curMap.ReverseLevelsMap();
    }
}
