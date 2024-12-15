using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultScript : MonoBehaviour
{
    public ScoreDesign scoreScript;
    public TextMeshProUGUI playerScoreText;
    public TimeManager getScriptTime;
    float startTime = 0;

    void OnEnable()
    {
        Time.timeScale = 0;
        int score = scoreScript.GetScore();

        if (PlayerPrefs.GetInt("Level") == 1)
        {
            startTime = 60f;
        } else if (PlayerPrefs.GetInt("Level") >= 2 && PlayerPrefs.GetInt("Level") <= 5)
        {
            startTime = 90f;
        } else if (PlayerPrefs.GetInt("Level") >= 6 && PlayerPrefs.GetInt("Level") <= 10)
        {
            startTime = 140f;
        } else if (PlayerPrefs.GetInt("Level") >= 11 && PlayerPrefs.GetInt("Level") <= 20)
        {
            startTime = 210f;
        } else if (PlayerPrefs.GetInt("Level") >= 21 && PlayerPrefs.GetInt("Level") <= 35)
        {
            startTime = 300f;
        } else if (PlayerPrefs.GetInt("Level") >= 36 && PlayerPrefs.GetInt("Level") <= 50)
        {
            startTime = 420f;
        } else
        {
            startTime = 510f;
        } 

        int bonusScore = (int) ((getScriptTime.totalPlayTime / startTime) * 1000);

        if (bonusScore % 10 != 0)
        {
            bonusScore -= (bonusScore % 10);
        }
        score += bonusScore;
        

        int getCurrScore = PlayerPrefs.GetInt("Score");
        int getCurrLevel = PlayerPrefs.GetInt("Level") + 1;

        PlayerPrefs.SetInt("Level", getCurrLevel);
        PlayerPrefs.SetInt("Score", getCurrScore += score);

        if(getCurrLevel % 5 == 0)
        {
            int totalTime = PlayerPrefs.GetInt("Time") + 1;
            PlayerPrefs.SetInt("Time", totalTime);
            int totalHammer = PlayerPrefs.GetInt("Hammer") + 1;
            PlayerPrefs.SetInt("Hammer", totalHammer);
            int totalWand = PlayerPrefs.GetInt("Wand") + 1;
            PlayerPrefs.SetInt("Wand", totalWand);
        }



        playerScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        // Debug.Log(score);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("Level") <= 5)
        {
            // randomize MainGame
            int randomLevel = Random.Range(1, 5);

            if(randomLevel == 1)
            {
                SceneManager.LoadScene("MainGame2_5");
            }
            else if(randomLevel == 2)
            {
                SceneManager.LoadScene("MainGame2_5_1");
            }
            else if(randomLevel == 3)
            {
                SceneManager.LoadScene("MainGame2_5_2");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame2_5_3");
            }
        } else {
            int randomLevel = Random.Range(1, 6);
            
            if(randomLevel == 1)
            {
                SceneManager.LoadScene("MainGame1");
            }
            else if(randomLevel == 2)
            {
                SceneManager.LoadScene("MainGame2");
            }
            else if(randomLevel == 3)
            {
                SceneManager.LoadScene("MainGame3");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame4");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame5");
            }
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
