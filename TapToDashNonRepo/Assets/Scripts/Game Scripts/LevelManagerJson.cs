using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerJson : MonoBehaviour
{

    public LevelBuilderJson level_first;
    public LevelBuilderJson level_second;

    public PlayerController pc;
    public LevelLoader levelLoader;
    public AudioSource levelComplitedSound;
    public BackgroundController bc;

    public bool IsMapAccelerated = false;

    private Player player;
    private static int cur_level;
    private bool firstNext = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "CustomLevel")
            InitCurLevel();
        else
            InitCustomLevel();


        player = pc.getPlayer();
        player.RefreshCurrentCommands(level_first.GetLevel().cmds);
    }

    void Update()
    {
        if (pc.getPlayerPos().z > GetDestinationPointCoordinates())
        {
            if (IsFirstLevelForward() && firstNext)
            {
                player.RefreshCurrentCommands(level_first.GetLevel().cmds);
                bc.ChangeLevelAssete(cur_level);
                cur_level++;
                firstNext = false;

                SetLevelSpeed();
                
                levelComplitedSound.Play();
            }
            else if (!IsFirstLevelForward() && !firstNext)
            {
                player.RefreshCurrentCommands(level_second.GetLevel().cmds);
                bc.ChangeLevelAssete(cur_level);
                cur_level++;
                firstNext = true;

                SetLevelSpeed();

                levelComplitedSound.Play();
            }

        }

        SwapCurLevels();
    }

    private void SetLevelSpeed()
    {
        if (IsMapAccelerated)
            pc.IncreaseSpeed();
        else
            pc.DecreaseSpeed();
    }

    public void SetMapAccelerationStatus(bool status)
    {
        IsMapAccelerated = status;
    }

    public bool IsFirstLevelForward()
    {
        return level_first.gameObject.transform.position.z > level_second.transform.position.z;
    }

    public void SwapCurLevels()
    {

        float player_pos = pc.gameObject.transform.position.z;
        float first_pos = level_first.transform.position.z + level_first.GetLevel().GetHeight() / 2;
        float second_pos = level_second.transform.position.z + level_second.GetLevel().GetHeight() / 2;

        if (player_pos > first_pos + level_second.GetLevel().GetHeight() / 2)
        {
            level_first.SetLevelObject(cur_level + 1);
            level_first.CreateLevelOnPlayground();
            level_first.MoveLevelMainPointToTheCenter(level_second.gameObject.transform.position);
        }
        else if (player_pos > second_pos + level_second.GetLevel().GetHeight() / 2)
        {
            level_second.SetLevelObject(cur_level + 1);
            level_second.CreateLevelOnPlayground();
            level_second.MoveLevelMainPointToTheCenter(level_first.gameObject.transform.position);
        }
    }

    public float GetDestinationPointCoordinates()
    {
        float cur_lvl_pos = level_first.gameObject.transform.position.z + level_first.GetLevel().GetHeight() / 2;
        float next_lvl_pos = level_second.gameObject.transform.position.z + level_second.GetLevel().GetHeight() / 2;

        return (cur_lvl_pos < next_lvl_pos ? cur_lvl_pos : next_lvl_pos);
    }

    private void InitCurLevel()
    {
        int temp = PlayerPrefs.GetInt("CurLevel", -1);
        if (temp == -1)
        {
            cur_level = 1;
            PlayerPrefs.SetInt("CurLevel", cur_level);
        }
        else if (cur_level == 0 && temp != -1)
            cur_level = temp;

        level_first.SetLevelObject(cur_level);
        level_second.SetLevelObject(cur_level + 1);

        level_first.CreateLevelOnPlayground();
        level_second.CreateLevelOnPlayground();

        level_second.MoveLevelMainPointToTheCenter(level_first.gameObject.transform.position);
    }


    private void InitCustomLevel()
    {
        cur_level = 1;

        level_first.SetLevelObject(cur_level);
        level_second.SetLevelObject(cur_level + 1);

        level_first.CreateLevelOnPlayground();
        level_second.CreateLevelOnPlayground();

        level_second.MoveLevelMainPointToTheCenter(level_first.gameObject.transform.position);
    }

    public int GetCurLevel()
    {
        InitCurLevel();
        return cur_level;
    }
}
