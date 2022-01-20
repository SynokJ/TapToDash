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

    private static int cur_level;
    private bool isLost = false;

    void Start()
    {
        // move next aside cur level
        secondLevel.translateByOffset(firstLevel.getObjPos());
        player = pc.getPlayer();

        InitCurLevel();
        int start_level = cur_level;

        start_level = 3;

        // set level 
        //firstLevel.getLevel().setIndex(start_level);
        //secondLevel.getLevel().setIndex(start_level + 1);
        //Debug.Log("Map Level:\t" + firstLevel.getLevel().getLevelIndex());

        // generate the first two levels (Current active level objects)
        firstLevel.initLevel();
        secondLevel.initLevel();

        // set player cmds
        //player.addCmds(firstLevel.getLevel().getLevelCmds());
    }

    private void Update()
    {
        if (pc.getPlayerPos().z > getTheDestCoordinate())
        {
            swapLevels();
            cur_level++;

            if (!winSound.isPlaying)
                winSound.Play();
        }


        // write the cur best level 
        if (pc.getPlayerPos().y < -0.07f && !isLost)
        {
            PlayerPrefs.SetInt("CurLevel", cur_level);
            isLost = true;
        }
    }

    public void swapLevels()
    {
        // move cur to the next
        //if (firstLevel.getObjPos().z < secondLevel.getObjPos().z)
        //{
        //    // set the next level after the second one to the complited level 
        //    firstLevel.ChangeLevel(level_02.getLevelIndex());

        //    level_01 = firstLevel.getLevel();
        //    player.addCmds(level_02.getLevelCmds());
        //    //firstLevel.translateByOffset(secondLevel.getObjPos());
        //    StartCoroutine(redrawLevel(firstLevel, secondLevel));
        //}
        //else
        //{
        //    secondLevel.ChangeLevel(level_01.getLevelIndex());

        //    level_02 = secondLevel.getLevel();
        //    player.addCmds(level_01.getLevelCmds());

        //    StartCoroutine(redrawLevel(secondLevel, firstLevel));
        //}
    }

    private IEnumerator redrawLevel(LevelBuilder first, LevelBuilder second)
    {
        yield return new WaitForSeconds(1.5f);
        first.translateByOffset(second.getObjPos());
    }

    private void InitCurLevel()
    {
        int temp = PlayerPrefs.GetInt("CurLevel", -1);
        if (temp == -1)
            cur_level = 0;
        else if (cur_level == 0 && temp != -1)
            cur_level = temp;
    }

    public float getTheDestCoordinate()
    {
        float cur_lvl_pos = firstLevel.getObjPos().z + firstLevel.getLevel().getHeight() / 2;
        float next_lvl_pos = secondLevel.getObjPos().z + secondLevel.getLevel().getHeight() / 2;

        return (cur_lvl_pos < next_lvl_pos ? cur_lvl_pos : next_lvl_pos);
    }
}
