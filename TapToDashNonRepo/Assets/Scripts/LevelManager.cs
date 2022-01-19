using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelBuilder firstLevel;
    public LevelBuilder secondLevel;
    public PlayerController pc;
    public AudioSource winSound;

    private Player player;
    private Level level_01;
    private Level level_02;

    private static int cur_level;
    private bool isLost = false;

    void Start()
    {
        // move next aside cur level
        secondLevel.translateByOffset(firstLevel.getObjPos());

        player = pc.getPlayer();

        int temp = PlayerPrefs.GetInt("CurLevel", -1);
        if (temp == -1)
            cur_level = 0;
        else if (cur_level == 0 && temp != -1)
            cur_level = temp;

        int start_level = cur_level;

        firstLevel.changeLevel(start_level);
        secondLevel.changeLevel(start_level + 1);

        level_01 = firstLevel.getLevel();
        level_02 = secondLevel.getLevel();

        player.addCmds(level_01.getLevelCmds());
    }

    private void Update()
    {
        if (pc.getPlayerPos().z > getTheDestCoordinate())
        {
            swapLevels();
            cur_level++;
            winSound.Play();
        }

        if (pc.getPlayerPos().y < -0.07f && !isLost)
        {
            PlayerPrefs.SetInt("CurLevel", cur_level);
            isLost = true;
        }
    }

    public void swapLevels()
    {


        // move cur to the next
        if (firstLevel.getObjPos().z < secondLevel.getObjPos().z)
        {
            firstLevel.changeLevel(level_02.getLevelIndex());
            level_01 = firstLevel.getLevel();
            player.addCmds(level_02.getLevelCmds());
            firstLevel.translateByOffset(secondLevel.getObjPos());
        }
        else
        {
            secondLevel.changeLevel(level_01.getLevelIndex());
            level_02 = secondLevel.getLevel();
            player.addCmds(level_01.getLevelCmds());
            secondLevel.translateByOffset(firstLevel.getObjPos());
        }
    }

    public float getTheDestCoordinate()
    {
        float cur_lvl_pos = firstLevel.getObjPos().z + firstLevel.getLevel().getLevelHeight() / 2;
        float next_lvl_pos = secondLevel.getObjPos().z + secondLevel.getLevel().getLevelHeight() / 2;

        return (cur_lvl_pos < next_lvl_pos ? cur_lvl_pos : next_lvl_pos);
    }

    private IEnumerator redrawLevel(LevelBuilder first, LevelBuilder second)
    {
        yield return new WaitForSeconds(1.5f);
        first.translateByOffset(second.getObjPos());
    }
}
