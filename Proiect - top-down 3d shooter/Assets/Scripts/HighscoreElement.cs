using System;


[Serializable]
public class HighscoreElement
{
    public string name;
    public int score;

    public HighscoreElement(string pname, int score)
    {
        name = pname;
        this.score = score;
    }

    public override string ToString()
    {
        string x = name + " " + score.ToString();
        return x;
    }
}
