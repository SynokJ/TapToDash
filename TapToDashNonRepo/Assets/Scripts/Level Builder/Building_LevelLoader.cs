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

        List<Tuple<string, int>> cmds = new List<Tuple<string, int>>();

        for (int r = 0; r < map.Length; ++r)
        {

            List<Tuple<string, int, int>> cmds_row = new List<Tuple<string, int, int>>();
            for (int c = 0; c < map[0].Length; ++c)
            {
                if (map[r][c] == '#')
                {
                    if (IsGap(r, c, map))
                        cmds_row.Add(new Tuple<string, int, int>("up", r, c));

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c] == map[r][c] && map[r + 1][c] == map[r][c])
                        if (c < map[0].Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        {
                            cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                            r++;
                            break;
                        }

                    if (r > 0 && r < map.Length - 1 && map[r - 1][c + 1] == map[r][c] && map[r + 1][c + 1] == map[r][c])
                        if (c > 0 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        {
                            cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                            cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                            r++;
                            break;
                        }

                    if (r > 0 && c < map[r].Length - 1 && map[r - 1][c] == map[r][c] && map[r][c + 1] == map[r][c])
                        cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                    else if (c < map[r].Length - 1 && r < map.Length - 1 && map[r][c + 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        cmds_row.Add(new Tuple<string, int, int>("right", r, c));
                    else if (r > 0 && c > 0 && map[r - 1][c] == map[r][c] && map[r][c - 1] == map[r][c])
                        cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                    else if (c > 0 && r < map.Length - 1 && map[r][c - 1] == map[r][c] && map[r + 1][c] == map[r][c])
                        cmds_row.Add(new Tuple<string, int, int>("left", r, c));
                }
            }


            // From left to right or vice versa? 
            if (cmds.Count == 0)
            {
                int first_move_col = cmds_row[0].Item3;
                int second_move_col = cmds_row[cmds.Count - 1].Item3;

                if (first_move_col < second_move_col && cmds_row[0].Item1 == "right")
                {
                    for (int i = 0; i < cmds_row.Count; ++i)
                        cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
                }
                else
                {
                    for (int i = cmds_row.Count - 1; i >= 0; --i)
                        cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
                }
            }
            //TODO

            //else
            //{
            //    int first_move_col = cmds_row[0].Item3;
            //    int second_move_col = cmds_row[cmds.Count - 1].Item3;

            //    int last_col = cmds[cmds.Count - 1].Item2;

            //    if (first_move_col == last_col)
            //    {
            //        if (cmds_row[0].Item1 == "right")
            //            for (int i = 0; i < cmds_row.Count; ++i)
            //                cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
            //        else if(cmds_row[0].Item1 == "left")
            //            for (int i = cmds_row.Count - 1; i >= 0; --i)
            //                cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
            //    }
            //    else
            //    {
            //        if (cmds_row[cmds.Count - 1].Item1 == "right")
            //            for (int i = 0; i < cmds_row.Count; ++i)
            //                cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
            //        else if (cmds_row[cmds.Count - 1].Item1 == "left")
            //            for (int i = cmds_row.Count - 1; i >= 0; --i)
            //                cmds.Add(new Tuple<string, int>(cmds_row[i].Item1, cmds_row[i].Item3));
            //    }
            //}

        }

        if (cmds.Count == 0)
            level.cmds = new string[] { "run" };
        else
        {
            List<string> cmds_res = new List<string>();

            foreach (Tuple<string, int> t in cmds)
                cmds_res.Add(t.Item1);

            level.cmds = cmds_res.ToArray();
        }

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
