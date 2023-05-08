using System.Collections.Generic;
using UnityEngine;


public class HighscoreHandler : MonoBehaviour
{
    public List<HighscoreElement> highscoreList = new List<HighscoreElement>();
    public int maxCount = 7;
    string filename = "highscores.json";
    public delegate void OnHighscoreListChanged(List<HighscoreElement> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    void Start()
    {
        LoadHighscores();
    }

    public void LoadHighscores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement>(filename);

        while (highscoreList.Count > maxCount)
        {
            highscoreList.RemoveAt(maxCount);
        }

        if (onHighscoreListChanged != null)
        {
            onHighscoreListChanged.Invoke(highscoreList);
        }
    }

    private void SaveHighscore()
    {
        FileHandler.SaveToJSON<HighscoreElement>(highscoreList, filename);
    }

    public void AddHighscoreIfPossible(HighscoreElement element)
    {
        for(int i=0; i<maxCount; i++)
        {
            if(i>= highscoreList.Count || element.score >= highscoreList[i].score)
            {
                //add new highscore
                highscoreList.Insert(i, element);
                
                while (highscoreList.Count > maxCount)
                {
                    highscoreList.RemoveAt(maxCount);
                }
               
                SaveHighscore();

                if (onHighscoreListChanged != null)
                {
                    onHighscoreListChanged.Invoke(highscoreList);
                }

                break;
            }
        }
    }
}