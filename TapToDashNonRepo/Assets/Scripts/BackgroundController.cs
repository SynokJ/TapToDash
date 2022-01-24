using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public Map[] maps;
    public LevelLoader levelLoader;
    public LevelManagerJson lmJson;

    public AudioSource[] music;

    private GameObject background;

    void Start()
    {
        background = transform.GetChild(0).gameObject;
        ChangeMapStyle(lmJson.GetCurLevel() - 1);
    }

    public void ChangeMapStyle(int levelIndex)
    {
        foreach (Map cur_map in maps)
        {
            if (levelLoader.curMap.level[levelIndex].style == cur_map.name)
            {
                background.GetComponent<SpriteRenderer>().sprite = cur_map.img;

                if (!music[cur_map.musicIndex].isPlaying)
                {

                    music[cur_map.musicIndex].Play();

                    if (cur_map.musicIndex != 0)
                        music[cur_map.musicIndex - 1].Stop();
                }
            }
        }
    }
}
