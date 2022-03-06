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

    private void InitCurLevel()
    {

        level.name = "noname";
        level.style = "nostyle";

        string[] map = bg.GetCustomLevel().ToArray();

        level.map = map;

        List<string> cmds = new List<string>();

        for (int r = 0; r < map.Length; ++r)
        {

            List<Tuple<string, int>> cmds_row = new List<Tuple<string, int>>();
            for (int c = 0; c < map[0].Length; ++c)
            {
                if (map[r][c] == '#')
                {
                    if (IsGap(r, c, map))
                    {
                        //cmds.Add("up");
                        cmds_row.Add(new Tuple<string, int>("up", r));
                        //break;
                    }

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c] == map[r][c] && map[r + 1][c] == map[r][c])
                        if (c < map[0].Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        {
                            cmds_row.Add(new Tuple<string, int>("right", r));
                            cmds_row.Add(new Tuple<string, int>("left", r));
                            cmds_row.Add(new Tuple<string, int>("left", r));
                            cmds_row.Add(new Tuple<string, int>("right", r));
                            //cmds.Add("right");
                            //cmds.Add("left");
                            //cmds.Add("left");
                            //cmds.Add("right");
                            r++;
                            break;
                        }

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        if (c > 0 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        {
                            cmds_row.Add(new Tuple<string, int>("left", r));
                            cmds_row.Add(new Tuple<string, int>("right", r));
                            cmds_row.Add(new Tuple<string, int>("right", r));
                            cmds_row.Add(new Tuple<string, int>("left", r));
                            //cmds.Add("left");
                            //cmds.Add("right");
                            //cmds.Add("right");
                            //cmds.Add("left");
                            r++;
                            break;
                        }

                    if (r > 0 && c < map[r].Length - 1 && map[r - 1][c] == map[r][c] && map[r][c + 1] == map[r][c])
                    {
                        //cmds.Add("right");
                        cmds_row.Add(new Tuple<string, int>("right", r));
                    }
                    else if (c < map[r].Length - 1 && r < map.Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                    {
                        //cmds.Add("right");
                        cmds_row.Add(new Tuple<string, int>("right", r));
                    }
                    else if (r > 0 && c > 0 && map[r - 1][c] == map[r][c] && map[r][c - 1] == map[r][c])
                    {
                        //cmds.Add("left");
                        cmds_row.Add(new Tuple<string, int>("left", r));
                    }
                    else if (c > 0 && r < map.Length - 1 && map[r][c - 1] == map[r][c] && map[r + 1][c] == map[r][c])
                    {
                        //cmds.Add("left");
                        cmds_row.Add(new Tuple<string, int>("left", r));
                    }
                }
            }

            if (cmds_row.Count > 0)
            {
                if (cmds_row.Count == 1 && cmds_row[cmds_row.Count - 1].Item1 == "up")
                {
                    cmds.Add("up");
                    continue;
                }

                if (cmds.Count > 0)
                {
                    int temp_size = cmds.Count - 1;
                    string temp_cmd = cmds[temp_size];

                    while (temp_size > 0 && temp_cmd == "up")
                        temp_cmd = cmds[temp_size--];

                    if (cmds.Count > 0 && temp_cmd == "left")
                        for (int i = cmds_row.Count - 1; i >= 0; --i)
                            cmds.Add(cmds_row[i].Item1);
                    else if (cmds.Count > 0 && temp_cmd == "right")
                        for (int i = 0; i < cmds_row.Count; ++i)
                            cmds.Add(cmds_row[i].Item1);
                }
                else
                {
                    if (cmds_row[cmds_row.Count - 1].Item1 == "left")
                        for (int i = cmds_row.Count - 1; i >= 0; --i)
                            cmds.Add(cmds_row[i].Item1);
                    else if (cmds_row[cmds_row.Count - 1].Item1 == "right")
                        for (int i = 0; i < cmds_row.Count; ++i)
                            cmds.Add(cmds_row[i].Item1);
                }
            }
        }

        level.cmds = cmds.Count == 0 ? new string[] { "run" } : cmds.ToArray();

        TestContainer(map);

        string res = "";
        foreach (string cmd in level.cmds)
            res += cmds + " ";

        Debug.Log(res);
    }

    private bool IsGap(int r, int c, string[] map)
    {
        if (r == 0 || r == map.Length - 1)
            return false;

        if (c == 0 && map[r + 1][c] == '.' && map[r][c + 1] == '.')
            return true;
        else if (c == map[0].Length - 1 && map[r + 1][c] == '.' && map[r][c - 1] == '.')
            return true;
        else
        {
            if (map[r + 1][c] == '.' && map[r][c + 1] == '.' && map[r][c - 1] == '.')
                return true;
            else if (map[r + 1][c] == '.' && map[r][c + 1] == '.' && map[r - 1][c] == '.')
                return true;
            else if (map[r + 1][c] == '.' && map[r - 1][c] == '.' && map[r][c - 1] == '.')
                return true;
        }

        return false;
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
