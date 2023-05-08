using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public static string menu = "Menu";
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverUI;

    void Start()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverUI.activeSelf)
        {
            gameIsPaused = !gameIsPaused;
            
            PauseGame();
        }

        highScoreText.text = PlayerPrefs.GetInt("highscore").ToString(); 
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}