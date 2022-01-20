using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderJson : MonoBehaviour
{

    public GameObject cur_platform;
    public GameObject start_platform;
    public GameObject collectible;
    public GameObject arrow;

    public LevelLoader levelLoader;

    private LevelLoader.Level level;
    private float right_arrow_angle = 0;
    private float left_arrow_angle = 0;

    void Start()
    {
        level = levelLoader.curMap.level[1];
        drawMap();
    }

    public void setLevel(int levelIndex)
    {

        if (levelIndex == levelLoader.curMap.level.Length)
            levelIndex = 0;
        
        level = levelLoader.curMap.level[levelIndex];

        cleanLevel();
        drawMap();
    }

    public void drawMap()
    {
        float y_offset = level.getHeight() / 2;
        float x_offset = level.getWidth() / 2;

        // draw playground platforms
        drawMapPlatforms(x_offset, y_offset);

        float pos_x = transform.position.x;
        float pos_y = transform.position.y;
        float pos_z = transform.position.z - y_offset - start_platform.transform.localScale.z / 2;

        Instantiate(start_platform, new Vector3(pos_x, pos_y, pos_z), Quaternion.identity).gameObject.transform.SetParent(this.transform);

        // first level translate  
        if (transform.position.z == 0)
            transform.Translate(new Vector3(0, 0, y_offset));
    }

    private void drawMapPlatforms(float x_offset, float y_offset)
    {
        for (int r = 0; r < level.getHeight(); ++r)
            for (int c = 0; c < level.getWidth(); ++c)
            {
                if (level.getMap()[r][c] == '#')
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
    }

    #region So Scarry Checkers
    private bool canPasteArrowRight(int row, int col)
    {
        if (row == 0)
        {

            if (col == 0)
            {
                if (level.getMap()[row][col + 1] == '#' && level.getMap()[row + 1][col] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
            }
            else if (col != level.getWidth() - 1)
            {
                if (level.getMap()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
        }
        else if (row == level.getHeight() - 1)
        {
            if (col == 0)
            {
                if (level.getMap()[row - 1][col] == '#' && level.getMap()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
            else if (col != level.getWidth() - 1)
            {
                if (level.getMap()[row][col + 1] == '#')
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
                if (level.getMap()[row + 1][col] == '#' && level.getMap()[row][col + 1] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
                else if (level.getMap()[row - 1][col] == '#' && level.getMap()[row][col + 1] == '#')
                {
                    right_arrow_angle = -90;
                    return true;
                }
            }
            else if (col != level.getWidth() - 1)
            {
                if (level.getMap()[row + 1][col] == '#' && level.getMap()[row][col + 1] == '#')
                {
                    right_arrow_angle = 0;
                    return true;
                }
                else if (level.getMap()[row - 1][col] == '#' && level.getMap()[row][col + 1] == '#')
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

            if (col == level.getWidth() - 1)
            {
                if (level.getMap()[row][col - 1] == '#' && level.getMap()[row + 1][col] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }
        else if (row == level.getHeight() - 1)
        {
            if (col != 0)
            {
                if (level.getMap()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
            else if (col == level.getWidth() - 1)
            {
                if (level.getMap()[row][col - 1] == '#' && level.getMap()[row - 1][col] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }
        else
        {
            if (col == level.getWidth() - 1)
            {
                if (level.getMap()[row + 1][col] == '#' && level.getMap()[row][col - 1] == '#')
                {
                    left_arrow_angle = 0;
                    return true;
                }
                else if (level.getMap()[row - 1][col] == '#' && level.getMap()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
            else if (col != 0)
            {
                if (level.getMap()[row + 1][col] == '#' && level.getMap()[row][col - 1] == '#')
                {
                    left_arrow_angle = 0;
                    return true;
                }
                else if (level.getMap()[row - 1][col] == '#' && level.getMap()[row][col - 1] == '#')
                {
                    left_arrow_angle = 90;
                    return true;
                }
            }
        }

        return false;
    }
    #endregion

    public void translateByOffset(Vector3 prevLevelPos)
    {
        float pos_z = level.getHeight() / 2 * 2 + start_platform.transform.localScale.z;
        transform.position = prevLevelPos + new Vector3(0, 0, pos_z);
    }

    public IEnumerator translateByOffsetCour(Vector3 prevLevelPos)
    {
        yield return new WaitForSeconds(1f);
        float pos_z = level.getHeight() / 2 * 2 + start_platform.transform.localScale.z;
        transform.position = prevLevelPos + new Vector3(0, 0, pos_z);
    }


    private void cleanLevel()
    {
        for (int i = 0; i < transform.childCount; ++i)
            if (transform.GetChild(i).gameObject.tag != "Dead")
                Destroy(transform.GetChild(i).gameObject);
    }

    public LevelLoader.Level getLevel()
    {
        return level;
    }
}


