using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    [System.Serializable]
    public class Level
    {
        public string name;
        public string style;
        public string[] cmds;
        public string[] map;

        private int width;
        private int height;

        public int getWidth()
        {
            return map[0].Length;
        }

        public int getHeight()
        {
            return map.Length;
        }

        public string[] getMap()
        {
            return map;
        }

        public void reverseMap()
        {
            for(int i = 0, j = map.Length - 1; i < j; ++i, --j)
            {
                string temp = map[i];
                map[i] = map[j];
                map[j] = temp;
            }
        }
    }

    [System.Serializable]
    public class LevelBox
    {
        public Level[] level;

        public void reverseLevels()
        {
            foreach (Level l in level)
                l.reverseMap();
        }
    }

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
        curMap.reverseLevels();
    }
}
