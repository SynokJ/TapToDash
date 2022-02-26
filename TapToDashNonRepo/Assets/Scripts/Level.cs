[System.Serializable]
public class Level
{
    public string name;
    public string style;
    public string[] cmds;
    public string[] map;

    private int width;
    private int height;

    public int GetWidth()
    {
        return map[0].Length;
    }

    public int GetHeight()
    {
        return map.Length;
    }

    public string[] GetMap()
    {
        return map;
    }

    public void ReverseMap()
    {
        for (int i = 0, j = map.Length - 1; i < j; ++i, --j)
        {
            string temp = map[i];
            map[i] = map[j];
            map[j] = temp;
        }
    }
}
