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
        if (levels == null)
            levels = new Level[] { new Level() };

        if (other == null)
            return;

        List<Level> temp = new List<Level>();

        foreach (Level level in levels)
            temp.Add(level);
        temp.Add(other);

        levels = temp.ToArray();
    }
}
