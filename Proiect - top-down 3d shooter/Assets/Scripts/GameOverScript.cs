using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScript: MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    private ScoreManager playerScoreManager;
    private Canvas playerCanvas;
    public static string menu = "Menu";
    public GameObject gameOverUI;
    public TMP_InputField inputName;
    private int score;
    public string playerName;
    public string path = "";
    public string persistentPath = "";
    public HighscoreHandler highscoreHandler;

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().currentHealth <= 0)
        {
            gameOverUI.SetActive(true);
            inputName.ActivateInputField();

            playerCanvas = GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>();
            playerScoreManager = playerCanvas.GetComponent<ScoreManager>();

            Time.timeScale = 0f;

            score = playerScoreManager.score;
            highScoreText.text = PlayerPrefs.GetInt("highscore").ToString();
            scoreText.text = score.ToString();
            playerName = inputName.text;
        }
    }

    public void LoadMenu()
    {
        List<HighscoreElement> highscoreList = new List<HighscoreElement>();
        highscoreList.Add(new HighscoreElement(playerName, score));
        highscoreHandler.AddHighscoreIfPossible(new HighscoreElement(playerName, score));

        Time.timeScale = 1;
        SceneManager.LoadScene(menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}