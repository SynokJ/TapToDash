using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

    private List<string> cmds;
    private string[] mapCmds;
    private string[] mapData = null;
    private int index = 0;

    public Level()
    {
        TextAsset level_map_data = (TextAsset)Resources.Load("Maps/Map-01/Level-00");

        if (level_map_data == null)
            return;

        string[] level_map = level_map_data.text.Split('\n');
        mapData = level_map;
        initCmdsContainer();
    }

    public Level(int levelIndex)
    {
        index = levelIndex;
        TextAsset level_map_data = null;

        if (index < 10)
            level_map_data = (TextAsset)Resources.Load("Maps/Map-01/Level-0" + index);
        else
            level_map_data = (TextAsset)Resources.Load("Maps/Map-01/Level-" + index);

        if (level_map_data == null)
        {
            level_map_data = (TextAsset)Resources.Load("Maps/Map-01/Level-00");
            string[] level_map_gg = level_map_data.text.Split('\n');
            mapData = level_map_gg;
            initCmdsContainer();

            return;
        }

        string[] level_map = level_map_data.text.Split('\n');
        mapData = level_map;
        initCmdsContainer();
    }

    private void initCmdsContainer()
    {
        cmds = new List<string>();

        for (int i = 0; i < mapData.Length; ++i)
        {
            if (mapData[i].Contains("cmds:"))
            {
                mapCmds = mapData[i + 1].Split(',');
                break;
            }

            cmds.Add(mapData[i]);
        }

        cmds.Reverse();
    }

    public List<string> getCmdsContainer()
    {
        return cmds;
    }

    public string[] getLevelCmds()
    {
        return mapCmds;
    }

    public float getLevelHeight()
    {
        return cmds.Count;
    }

    public float getLevelWidth()
    {
        return cmds[0].Length;
    }

    public int getLevelIndex()
    {
        return index;
    }

    public void setLevelIndex(int other)
    {
        index = other + 1;
    }
}
