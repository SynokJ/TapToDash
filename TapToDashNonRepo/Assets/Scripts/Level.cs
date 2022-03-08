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

        int max = 0;

        foreach (string s in map)
            if (max < s.Length)
                max = s.Length;


        return max;
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
