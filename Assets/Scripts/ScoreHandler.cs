using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    private int increaseScoreBy; // amount by which score should be increased
    private int decreaseScroreBy;
    private int p1_score;
    private int p2_score;
    private TMP_Text scoreText;
    //[SerializeField] private Player playerName;
    
    // Start is called before the first frame update
    void Start()
    {
        p1_score = 0;
        p2_score = 0;
        increaseScoreBy = 5;
        decreaseScroreBy = 3;
        scoreText = GetComponent<TMP_Text>();
    }

    public void ScoreIncrease(Player _player)
    {
        if(_player == Player.Player2)
        {
            p2_score += increaseScoreBy;
            scoreText.text = p2_score.ToString(); // Update the UI
        } else
        {
            p1_score += increaseScoreBy;
            scoreText.text = p1_score.ToString(); // Update the UI
        }  
    }

    public void ScoreDecrease(Player _player)
    {
        if (_player == Player.Player2)
        {
            p2_score -= decreaseScroreBy;
            scoreText.text = p2_score.ToString(); // Update the UI
        }
        else
        {
            p1_score -= decreaseScroreBy;
            scoreText.text = p1_score.ToString(); // Update the UI
        }
    }

    public void ScoreBooster()
    {
        increaseScoreBy *= 2; 
    }

    public void DeactivateScoreBooster()
    {
        increaseScoreBy /= 2;
    }

    //public int GetScore()
    //{
    //    return score;
    //}
}
