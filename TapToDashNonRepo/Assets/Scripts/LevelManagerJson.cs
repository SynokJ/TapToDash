using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerJson : MonoBehaviour
{

    public LevelBuilderJson level_first;
    public LevelBuilderJson level_second;
    public PlayerController pc;
    public LevelLoader levelLoader;
    public AudioSource levelComplitedSound;

    private Player player;
    private static int cur_level;
    private bool firstNext = false;

    void Start()
    {
        InitCurLevel();

        player = pc.getPlayer();
        player.addCmds(level_first.getLevel().cmds);
    }

    void Update()
    {
        if (pc.getPlayerPos().z > getTheDestCoordinate())
        {
            if (IsFirstForward() && firstNext)
            {
                player.addCmds(level_first.getLevel().cmds);
                cur_level++;
                firstNext = false;
                
                levelComplitedSound.Play();
            }
            else if (!IsFirstForward() && !firstNext)
            {
                player.addCmds(level_second.getLevel().cmds);
                cur_level++;
                firstNext = true;

                levelComplitedSound.Play();
            }
        }

        SwapLevels();
    }

    public bool IsFirstForward()
    {
        return level_first.gameObject.transform.position.z > level_second.transform.position.z;
    }

    public void SwapLevels()
    {

        float player_pos = pc.gameObject.transform.position.z;
        float first_pos = level_first.transform.position.z + level_first.getLevel().getHeight() / 2;
        float second_pos = level_second.transform.position.z + level_second.getLevel().getHeight() / 2;

        if (player_pos > first_pos + level_second.getLevel().getHeight() / 2)
        {
            level_first.setLevel(cur_level + 1);
            level_first.drawMap();
            level_first.translateByOffset(level_second.gameObject.transform.position);
        }
        else if (player_pos > second_pos + level_second.getLevel().getHeight() / 2)
        {
            level_second.setLevel(cur_level + 1);
            level_second.drawMap();
            level_second.translateByOffset(level_first.gameObject.transform.position);
        }
    }

    public float getTheDestCoordinate()
    {
        float cur_lvl_pos = level_first.gameObject.transform.position.z + level_first.getLevel().getHeight() / 2;
        float next_lvl_pos = level_second.gameObject.transform.position.z + level_second.getLevel().getHeight() / 2;

        return (cur_lvl_pos < next_lvl_pos ? cur_lvl_pos : next_lvl_pos);
    }

    private void InitCurLevel()
    {
        int temp = PlayerPrefs.GetInt("CurLevel", -1);
        if (temp == -1)
            cur_level = 1;
        else if (cur_level == 0 && temp != -1)
            cur_level = temp;

        level_first.setLevel(cur_level);
        level_second.setLevel(cur_level + 1);

        level_first.drawMap();
        level_second.drawMap();

        level_second.translateByOffset(level_first.gameObject.transform.position);
    }

    public int GetCurLevel()
    {
        return cur_level;
    }
}
