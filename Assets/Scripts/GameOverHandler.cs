using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    private Player _playerID;
    private bool _isGameOver;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerID = collision.GetComponent<Snake>().playerID; // Get the ID of player whose head collided with another snake's body
        if(_playerID == Player.Player1)
        {
            Debug.Log("Player 2 Won");
            _isGameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            Debug.Log("Player 1 Won");
            _isGameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
