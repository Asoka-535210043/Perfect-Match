using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    int randomLevel;
    int totalScene = 1;
    public TextMeshProUGUI levelText;
    public GameObject LevelSelectionUI;
    public GameObject CharacterSelectionUI;
    public Image characterImg;
    public Sprite maleChar;
    public Sprite femaleChar;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level") <= 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (PlayerPrefs.GetInt("Char") == null || PlayerPrefs.GetInt("Char") == 0)
        {
            PlayerPrefs.SetInt("Char", 1);
        }


        levelText.text = "Level: " + PlayerPrefs.GetInt("Level");

        // Test Game / Level Selection
        PlayerPrefs.SetInt("Hammer", 5);
        PlayerPrefs.SetInt("Time", 5);
        PlayerPrefs.SetInt("Wand", 5);

        if(PlayerPrefs.GetInt("Level") == 1)
        {
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("Hammer", 1);
            PlayerPrefs.SetInt("Time", 1);
            PlayerPrefs.SetInt("Wand", 1);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Char") == 1)
        {
            characterImg.sprite = maleChar;
        } else 
        {
            characterImg.sprite = femaleChar;
        }
    }

    public void PlayGame()
    {
        int getLevel = PlayerPrefs.GetInt("Level");
        // Debug.Log(getLevel);
        if (getLevel <= 1)
        {
        SceneManager.LoadScene("MainGame");
        } else if (getLevel <= 5 && getLevel >= 2)
        {
            // randomize MainGame
            int randomLevel = Random.Range(1, 6);

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
            else if(randomLevel == 5)
            {
                SceneManager.LoadScene("MainGame2_5_4");
            }
        } else if (getLevel <= 18 && getLevel >= 6){
            int randomLevel = Random.Range(1, 9);
            
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
            else if(randomLevel == 5)
            {
                SceneManager.LoadScene("MainGame5");
            }
            else if(randomLevel == 6)
            {
                SceneManager.LoadScene("MainGame6");
            }
            else if(randomLevel == 7)
            {
                SceneManager.LoadScene("MainGame7");
            }
            else if(randomLevel == 8)
            {
                SceneManager.LoadScene("MainGame8");
            }
        } else {
            int randomLevel = Random.Range(1, 9);
            
            if(randomLevel == 1)
            {
                SceneManager.LoadScene("MainGame2");
            }
            else if(randomLevel == 2)
            {
                SceneManager.LoadScene("MainGame3");
            }
            else if(randomLevel == 3)
            {
                SceneManager.LoadScene("MainGame5");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame6");
            }
            else if(randomLevel == 5)
            {
                SceneManager.LoadScene("MainGame9");
            }
            else if(randomLevel == 6)
            {
                SceneManager.LoadScene("MainGame10");
            }
            else if(randomLevel == 7)
            {
                SceneManager.LoadScene("MainGame11");
            }
            else if(randomLevel == 8)
            {
                SceneManager.LoadScene("MainGame12");
            }
        }
    }

    public void CharacterSelect()
    {
        CharacterSelectionUI.SetActive(true);
    }

    public void LevelSelection()
    {
        LevelSelectionUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}