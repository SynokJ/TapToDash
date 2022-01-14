using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    public GameObject cur_platform;
    public GameObject start_platform;
    public GameObject collectible;
    public GameObject arrow;

    //private int levelIndex = 1;
    private Level level;
    private int levelIndex;
    private float right_arrow_angle = 0;
    private float left_arrow_angle = 0;

    private void Awake()
    {
        drawMap();
    }

    public void drawMap()
    {
        level = new Level(levelIndex);

        float y_offset = level.getLevelHeight() / 2;
        float x_offset = level.getLevelWidth() / 2 - 1;

        // draw playground platforms
        for (int r = 0; r < level.getLevelHeight(); ++r)
            for (int c = 0; c < level.getLevelWidth(); ++c)
            {
                if (level.getCmdsContainer()[r][c] == '#')
                {
                    Vector3 block_pos = cur_platform.transform.position;
                    float temp_posX = (block_pos.x + 1) * c - x_offset;
                    float temp_posY = (block_pos.z + 1) * r - y_offset + cur_platform.transform.localScale.z / 2;

                    Vector3 new_pos = new Vector3(temp_posX, block_pos.y, temp_posY);

                    GameObject new_block = Instantiate(cur_platform, transform.position + new_pos, Quaternion.identity);
                    new_block.transform.SetParent(this.transform);

                    GameObject new_collectible = Instantiate(collectible, new_block.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);

                    new_block.transform.SetParent(this.transform);
                    new_collectible.transform.SetParent(new_block.transform);

                    // set arrow
                    //if (r + 1 != level.getLevelHeight() && level.getCmdsContainer()[r + 1][c] == '.')
                    if (canPasteArrowRight(r, c))
                    {
                        GameObject new_arrow = Instantiate(arrow, new_block.transform.position + new Vector3(0, 0.11f, 0), Quaternion.Euler(90, 0, right_arrow_angle));
                        new_arrow.transform.SetParent(new_block.transform);
                        new_collectible.SetActive(false);
                    }
                    else if (canPasteArrowLeft(r, c))
                    {
                        GameObject new_arrow = Instantiate(arrow, new_block.transform.position + new Vector3(0, 0.11f, 0), Quaternion.Euler(90, 0, left_arrow_angle));
                        new_arrow.transform.SetParent(new_block.transform);
                        new_collectible.SetActive(false);
                    }
                }
            }

        // draw start platform
        float pos_x = transform.position.x;
        float pos_y = transform.position.y;
        float pos_z = transform.position.z - y_offset - start_platform.transform.localScale.z / 2;

        // set the position of level and start platform
        Instantiate(start_platform, new Vector3(pos_x, pos_y, pos_z), Quaternion.identity).gameObject.transform.SetParent(this.transform);

        // first level translate  
        if (transform.position.z == 0)
            transform.Translate(new Vector3(0, 0, y_offset));
    }

    private bool canPasteArrowRight(int row, int col)
    {
        if (row == 0)
        {

            if (col == 0)
            {
                if (level.getCmdsContainer()[row][col + 1] == '#' && level.getCmdsContainer()[row + 1][col] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
            }
            else if (col != level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
        }
        else if (row == level.getLevelHeight() - 1)
        {
            if (col == 0)
            {
                if (level.getCmdsContainer()[row - 1][col] == '#' && level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
            else if (col != level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
            }
        }
        else
        {
            if (col == 0)
            {
                if (level.getCmdsContainer()[row + 1][col] == '#' && level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
                else if (level.getCmdsContainer()[row - 1][col] == '#' && level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
            else if (col != level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row + 1][col] == '#' && level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
                else if (level.getCmdsContainer()[row - 1][col] == '#' && level.getCmdsContainer()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
        }

        return false;
    }

    private bool canPasteArrowLeft(int row, int col)
    {
        if (row == 0)
        {

            if (col == level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row][col - 1] == '#' && level.getCmdsContainer()[row + 1][col] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }
        else if (row == level.getLevelHeight() - 1)
        {
            if (col != 0)
            {
                if (level.getCmdsContainer()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
            else if (col == level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row][col - 1] == '#' && level.getCmdsContainer()[row - 1][col] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }
        else
        {
            if (col == level.getLevelWidth() - 1)
            {
                if (level.getCmdsContainer()[row + 1][col] == '#' && level.getCmdsContainer()[row][col - 1] == '#')
                {
                    left_arrow_angle = 0;
                    return true;
                }
                else if (level.getCmdsContainer()[row - 1][col] == '#' && level.getCmdsContainer()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
            else if (col != 0)
            {
                if (level.getCmdsContainer()[row + 1][col] == '#' && level.getCmdsContainer()[row][col - 1] == '#')
                {
                    left_arrow_angle = 0;
                    return true;
                }
                else if (level.getCmdsContainer()[row - 1][col] == '#' && level.getCmdsContainer()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }

        return false;
    }

    // y_offset * 2 + start_platform.transform.localScale.z/2       ->      offset between levels
    public void translateByOffset(Vector3 prevLevelPos)
    {
        float pos_z = level.getCmdsContainer().Count / 2 * 2 + start_platform.transform.localScale.z;
        transform.position = prevLevelPos + new Vector3(0, 0, pos_z);
    }

    public void changeLevel(int prevLevelIndex)
    {

        levelIndex = prevLevelIndex + 1;

        for (int i = 0; i < transform.childCount; ++i)
            if (transform.GetChild(i).gameObject.tag != "Dead")
                Destroy(transform.GetChild(i).gameObject);

        drawMap();
    }

    public Level getLevel()
    {
        return level;
    }

    public Vector3 getObjPos()
    {
        return this.gameObject.transform.position;
    }
}
