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

        StreamReader sr = new StreamReader(Application.persistentDataPath + "/Custom.json");
        levelBox = JsonUtility.FromJson<LevelBox>(sr.ReadToEnd());
        sr.Close();

        if (levelBox == null)
            OnResetCustomLevelsButtonCLicked();
    }

    public void OnSaveButtonClicked()
    {
        InitCurLevel();
        SaveCustomLevel();
    }

    public void OnResetCustomLevelsButtonCLicked()
    {
        levelBox = new LevelBox();
        OnSaveButtonClicked();
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

    enum CmdType
    {
        // normal way
        right_01,
        left_01,

        // rotate to 90 degree
        right_02,
        left_02,
        up_01,  // from bottom 
        up_02,  // from left
        up_03   // from right 
    }

    private void InitCurLevel()
    {
        level = new Level();

        level.name = "noname";
        level.style = "nostyle";

        if (bg.GetCustomLevel() == null)
        {
            Debug.Log("gg");
            return;
        }

        string[] map = bg.GetCustomLevel().ToArray();


        level.map = map;

        List<string> cmds = new List<string>();

        for (int r = 0; r < map.Length; ++r)
        {

            List<CmdType> cmds_row = new List<CmdType>();
            for (int c = 0; c < map[0].Length; ++c)
            {
                if (map[r][c] == '#')
                {
                    IsGap(r, c, map, cmds_row);

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c] == map[r][c] && map[r + 1][c] == map[r][c])
                        if (c < map[0].Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        {
                            cmds_row.Add(CmdType.right_01);
                            cmds_row.Add(CmdType.left_02);
                            cmds_row.Add(CmdType.left_01);
                            cmds_row.Add(CmdType.right_02);
                            r++;
                            break;
                        }

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        if (c > 0 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        {
                            cmds_row.Add(CmdType.left_01);
                            cmds_row.Add(CmdType.right_02);
                            cmds_row.Add(CmdType.right_01);
                            cmds_row.Add(CmdType.left_02);
                            r++;
                            break;
                        }

                    if (r > 0 && c < map[r].Length - 1 && map[r - 1][c] == map[r][c] && map[r][c + 1] == map[r][c])
                        cmds_row.Add(CmdType.right_01);
                    else if (c < map[r].Length - 1 && r < map.Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        cmds_row.Add(CmdType.right_02);
                    else if (r > 0 && c > 0 && map[r - 1][c] == map[r][c] && map[r][c - 1] == map[r][c])
                        cmds_row.Add(CmdType.left_01);
                    else if (c > 0 && r < map.Length - 1 && map[r][c - 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        cmds_row.Add(CmdType.left_02);
                }
            }

            if (cmds_row.Count == 0)
                continue;

            if (cmds_row[0] == CmdType.right_02 && cmds_row[cmds_row.Count - 1] == CmdType.left_01)
            {
                cmds_row.Reverse();

                foreach (CmdType cmd in cmds_row)
                {
                    switch (cmd)
                    {
                        case CmdType.left_01:
                        case CmdType.left_02: cmds.Add("left"); break;
                        case CmdType.right_01:
                        case CmdType.right_02: cmds.Add("right"); break;
                        case CmdType.up_01:
                        case CmdType.up_03: cmds.Add("up"); break;
                    }
                }
            }
            else
            {
                foreach (CmdType cmd in cmds_row)
                {
                    switch (cmd)
                    {
                        case CmdType.left_01:
                        case CmdType.left_02: cmds.Add("left"); break;
                        case CmdType.right_01:
                        case CmdType.right_02: cmds.Add("right"); break;
                        case CmdType.up_01:
                        case CmdType.up_02: cmds.Add("up"); break;
                    }
                }
            }
        }


        // After collecting the data
        if (cmds.Count == 0)
            level.cmds = new string[] { "run" };
        else
            level.cmds = cmds.ToArray();
    }

    private void IsGap(int r, int c, string[] map, List<CmdType> cmds)
    {
        if (r == 0 || r == map.Length - 1)
            return;

        if (c == 0 && map[r + 1][c] == '.' && map[r][c + 1] == '.')
            cmds.Add(CmdType.up_01);
        else if (c == map[0].Length - 1 && map[r + 1][c] == '.' && map[r][c - 1] == '.')
            cmds.Add(CmdType.up_01);
        else
        {
            if (map[r + 1][c] == '.' && map[r][c + 1] == '.' && map[r][c - 1] == '.')
                cmds.Add(CmdType.up_01);
            else if (map[r + 1][c] == '.' && map[r][c + 1] == '.' && map[r - 1][c] == '.')
                cmds.Add(CmdType.up_02);
            else if (map[r + 1][c] == '.' && map[r - 1][c] == '.' && map[r][c - 1] == '.')
                cmds.Add(CmdType.up_03);
        }
    }

    private void TestContainer(string[] map)
    {
        string res = "";

        foreach (string s in map)
            res += s + " size : " + s.Length + "\n";

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
