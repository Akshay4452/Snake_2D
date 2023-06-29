using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    // This script is used for moving between scenes
    public void BackToGame()
    {
        SceneManager.LoadScene("GameScene");
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
