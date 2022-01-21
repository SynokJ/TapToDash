using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerJson : MonoBehaviour
{

    public LevelBuilderJson level_first;
    public LevelBuilderJson level_second;
    public PlayerController pc;
    public LevelLoader levelLoader;

    private Player player;
    private static int cur_level;
    private bool isMoved;

    private float prev_pos_first;
    private float prev_pos_second;

    private float cur_pos_first;
    private float cur_pos_second;

    void Start()
    {
        InitCurLevel();

        level_first.setLevel(cur_level);
        level_second.setLevel(cur_level);

        level_second.translateByOffset(level_first.gameObject.transform.position);

        player = pc.getPlayer();
        player.addCmds(level_first.getLevel().cmds);
        player.checkCurCmds();
    }

    void Update()
    {
        if (pc.getPlayerPos().z > getTheDestCoordinate() && !isMoved)
        {
            if (cur_level % 2 == 0)
            {
                StartCoroutine(level_second.translateByOffsetCour(level_first.gameObject.transform.position));

                level_first.setLevel(cur_level + 1);
                player.addCmds(level_first.getLevel().cmds);
            }
            else
            {
                StartCoroutine(level_first.translateByOffsetCour(level_second.gameObject.transform.position));

                level_second.setLevel(cur_level + 1);
                player.addCmds(level_second.getLevel().cmds);
            }

            cur_level++;
            player.checkCurCmds();

            prev_pos_first = level_first.gameObject.transform.position.z;
            prev_pos_second = level_second.gameObject.transform.position.z;

            isMoved = true;
        }

        cur_pos_first = level_first.gameObject.transform.position.z;
        cur_pos_second = level_second.gameObject.transform.position.z;

        if (IsDifferent(cur_pos_first, prev_pos_first) || IsDifferent(cur_pos_second, prev_pos_second))
            isMoved = false;
    }

    private bool IsDifferent(float pos_01, float pos_02)
    {
        return pos_01 != pos_02;
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
    }

    public int GetCurLevel()
    {
        return cur_level;
    }
}
