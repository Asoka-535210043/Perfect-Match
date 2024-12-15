using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesign : MonoBehaviour
{
    public GameObject MainMenuUI;

    public void Level1()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene("MainGame");
    }

    public void Level2_5()
    {
        PlayerPrefs.SetInt("Level", 2);
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
    }

    public void Level6_12()
    {
        PlayerPrefs.SetInt("Level", 6);

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
    }

    public void Level13_18()
    {
        PlayerPrefs.SetInt("Level", 13);
        
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
    }

    public void Level19_25()
    {
        PlayerPrefs.SetInt("Level", 19);

        int randomLevel = Random.Range(1, 5);
            
            if(randomLevel == 1)
            {
                SceneManager.LoadScene("MainGame9");
            }
            else if(randomLevel == 2)
            {
                SceneManager.LoadScene("MainGame10");
            }
            else if(randomLevel == 3)
            {
                SceneManager.LoadScene("MainGame11");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame12");
            }
    }

    public void Level26_OnWards()
    {
        PlayerPrefs.SetInt("Level", 26);

        int randomLevel = Random.Range(1, 5);
            
            if(randomLevel == 1)
            {
                SceneManager.LoadScene("MainGame9");
            }
            else if(randomLevel == 2)
            {
                SceneManager.LoadScene("MainGame10");
            }
            else if(randomLevel == 3)
            {
                SceneManager.LoadScene("MainGame11");
            }
            else if(randomLevel == 4)
            {
                SceneManager.LoadScene("MainGame12");
            }
    }

    public void Back()
    {
        MainMenuUI.SetActive(true);
        gameObject.SetActive(false);
    }
}