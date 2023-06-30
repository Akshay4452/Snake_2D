using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameOverHandler gameOverHandler;  // reference of GameOverHandler
    [SerializeField] private TMP_Text playerWonText;
    
    // This script is used for moving between scenes
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SinglePlayerScene()
    {
        SceneManager.LoadScene("1PlayerScene");
    }

    public void MultiPlayerScene()
    {
        SceneManager.LoadScene("2PlayerScene");
    }
}
