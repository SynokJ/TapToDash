using System.Collections.Generic;

[System.Serializable]
public class LevelBox
{
    public Level[] levels;

    public void ReverseLevelsMap()
    {
        foreach (Level l in levels)
            l.ReverseMap();
    }

    public void AddLevel(Level other)
    {
        //if (System.Array.BinarySearch(levels, other) < 0)
        //    return;

        if (levels == null)
            levels = new Level[] { new Level() };


        List<Level> temp = new List<Level>();

        foreach (Level level in levels)
            temp.Add(level);
        temp.Add(other);

        levels = temp.ToArray();
    }
}
