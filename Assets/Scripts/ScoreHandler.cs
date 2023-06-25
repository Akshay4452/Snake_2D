using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    private int increaseScoreBy; // amount by which score should be increased
    private int decreaseScroreBy;
    private int score;
    private TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        increaseScoreBy = 5;
        decreaseScroreBy = 3;
        scoreText = GetComponent<TMP_Text>();
    }

    public void ScoreIncrease()
    {
        score += increaseScoreBy;
        scoreText.text = "Score: " + score.ToString(); // Update the UI
    }

    public void ScoreDecrease()
    {
        score -= decreaseScroreBy;
        scoreText.text = "Score: " + score.ToString(); // Update the UI
    }

    public int GetScore()
    {
        return score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
