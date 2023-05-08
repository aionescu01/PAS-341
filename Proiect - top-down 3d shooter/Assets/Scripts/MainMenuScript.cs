using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    private static string PlayScene = "SampleScene";
    
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(PlayScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}