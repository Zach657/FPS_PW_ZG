using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/** 
 * Copyright (C) 2016 - Peter S. Wages
 **/
public class FPSController : MonoBehaviour {

    [SerializeField]
    private Text scoreLabel, highScoreLabel, winLoseMessageText;
    [SerializeField]
    private GameObject levelCompletedMessage;

    private string levelName = "_Scene_0";
    private string loseMessage = "An error in Android's runtime has caused the System.main to crash. Please restart now.";
    private int score = 0;
    private string highScorePlayerPrefKey = "FPSPSWJZG-Highscore";
    private int highScore = 0;

    // Set the completed message to false, set the high score key, and get the high score
    void Start()
    {
        levelCompletedMessage.SetActive(false);
        GetHighScore();
        TriggerWin();
    }

    // Update the player's score
    public void UpdateScore(int scoreChange)
    {
        score += scoreChange;
        scoreLabel.text = "Score: " + score;
    }

    // Gets the high score
    public void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt(highScorePlayerPrefKey, 0);
        highScoreLabel.text = "High Score: " + highScore;
    }

    // Updates the high score if neccessary
    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScorePlayerPrefKey, highScore);
            highScoreLabel.text = "High Score: " + highScore;
            PlayerPrefs.Save();
        }
    }

    // Triggers the level won message
    public void TriggerWin()
    {
        UnlockMouse();
        UpdateHighScore();
        levelCompletedMessage.SetActive(true);
    }

    public void TriggerLoss()
    {
        UnlockMouse();
        levelCompletedMessage.SetActive(true);
        winLoseMessageText.text = loseMessage;
    }

    // Loads the next level
    public void Restart()
    {
        SceneManager.LoadScene(levelName);
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
