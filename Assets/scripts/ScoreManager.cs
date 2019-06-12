using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    int currentScore = 0 , highScore;
    public TextMeshProUGUI  currentScoreText;
    public TextMeshProUGUI  highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = highScore.ToString();
        currentScoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int sc){
        currentScore += sc;
        currentScoreText.text = currentScore.ToString();
        if(currentScore > highScore){
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore" , highScore);
            highScoreText.text = highScore.ToString();
        }
    }
}
