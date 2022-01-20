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

    void Start()
    {
        cur_level = 1;
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
            Debug.Log("Level Complited");

            if (cur_level % 2 == 0)
            {
                Debug.Log("Move Second and Init First");
                //level_second.translateByOffset(level_first.gameObject.transform.position);
                //StartCoroutine(moveFirstLevel());

                StartCoroutine(level_second.translateByOffsetCour(level_first.gameObject.transform.position));

                level_first.setLevel(cur_level + 1);
                player.addCmds(level_first.getLevel().cmds);
            }
            else
            {
                Debug.Log("Move First and Init Second");
                //level_first.translateByOffset(level_second.gameObject.transform.position);
                //StartCoroutine(moveSecondLevel());

                StartCoroutine(level_first.translateByOffsetCour(level_second.gameObject.transform.position));

                level_second.setLevel(cur_level + 1);
                player.addCmds(level_second.getLevel().cmds);
            }

            cur_level++;
            player.checkCurCmds();
            isMoved = true;
        }
    }

    IEnumerator moveFirstLevel()
    {
        yield return new WaitForSeconds(0);
        level_first.translateByOffsetCour(level_second.gameObject.transform.position);
    }

    IEnumerator moveSecondLevel()
    {
        yield return new WaitForSeconds(0);
        level_second.translateByOffsetCour(level_first.gameObject.transform.position);
    }

    public float getTheDestCoordinate()
    {
        float cur_lvl_pos = level_first.gameObject.transform.position.z + level_first.getLevel().getHeight() / 2;
        float next_lvl_pos = level_second.gameObject.transform.position.z + level_second.getLevel().getHeight() / 2;

        return (cur_lvl_pos < next_lvl_pos ? cur_lvl_pos : next_lvl_pos);
    }
}
