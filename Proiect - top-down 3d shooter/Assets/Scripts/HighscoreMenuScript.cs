using TMPro;
using UnityEngine;


public class HighscoreMenuScript : MonoBehaviour
{
    public GameObject template;
    public HighscoreHandler highscoreHandler;

    void Start()
    {
        highscoreHandler.LoadHighscores();

        int x = highscoreHandler.highscoreList.Count;

        GameObject g;

        for(int i=0; i<x; i++)
        {
            g = Instantiate(template, transform);
            var texts = g.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = highscoreHandler.highscoreList[i].name;
            texts[1].text = highscoreHandler.highscoreList[i].score.ToString();
        }

        Destroy(template);
    }
}