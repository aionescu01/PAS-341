using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score = 0;
    private int highscore = 0;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString();
    }

    void Update()
    {
        //metoda adaugat puncte
        if (Input.GetKeyDown(KeyCode.M))
            AddPoint();

        //resetare high score
        if (Input.GetKeyDown(KeyCode.N))
            PlayerPrefs.SetInt("highscore", 0);
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString();

        if (score>highscore)
            PlayerPrefs.SetInt("highscore",score);
    }
}