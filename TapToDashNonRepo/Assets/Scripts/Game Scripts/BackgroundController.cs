using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public Map[] maps;
    public LevelLoader levelLoader;
    public LevelManagerJson lmJson;

    public AudioSource music;
    public LevelManagerJson lmj;

    private GameObject background;
    private string last_music_name = "empty";

    void Start()
    {
        background = transform.GetChild(0).gameObject;
        ChangeLevelAssete(lmJson.GetCurLevel() - 1);
    }

    public void ChangeLevelAssete(int levelIndex)
    {
        foreach (Map cur_map in maps)
        {

            if (levelLoader.curMap.levels[levelIndex].style == cur_map.name)
            {
                music.clip = cur_map.background_music;
                background.GetComponent<SpriteRenderer>().sprite = cur_map.img;

                if (music.clip.name != last_music_name)
                {
                    music.Play();
                    last_music_name = music.clip.name;

                    lmj.SetMapAccelerationStatus(cur_map.IsAccelerated);

                    break;
                }
            }
        }
    }
}
