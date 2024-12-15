using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // Dalam Detik
    public float totalPlayTime;
    public TextMeshProUGUI timeText;
    public GameObject gameOverUI;
    float second;
    float minutes;
    bool isStart = false;
    bool timesUp = false;

    
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("Level", 12);
        if (PlayerPrefs.GetInt("Level") == 1)
        {
            totalPlayTime = 60f;
        } else if (PlayerPrefs.GetInt("Level") >= 2 && PlayerPrefs.GetInt("Level") <= 5)
        {
            totalPlayTime = 90f;
        } else if (PlayerPrefs.GetInt("Level") >= 6 && PlayerPrefs.GetInt("Level") <= 12)
        {
            totalPlayTime = 140f;
        } else if (PlayerPrefs.GetInt("Level") >= 13 && PlayerPrefs.GetInt("Level") <= 18)
        {
            totalPlayTime = 180f;
        } else if (PlayerPrefs.GetInt("Level") >= 19 && PlayerPrefs.GetInt("Level") <= 25)
        {
            totalPlayTime = 240f;
        } else if (PlayerPrefs.GetInt("Level") >= 26 && PlayerPrefs.GetInt("Level") <= 50)
        {
            totalPlayTime = 360f;
        } else
        {
            totalPlayTime = 480f;
        } 

        timesUp = false;
        isStart = false;
        minutes = (int)totalPlayTime / 60;
        second = (int)totalPlayTime % 60;
        timeText.text = minutes.ToString("00") + ":" + second.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            isStart = true;
            // Debug.Log(isStart);
            // Debug.Log(timesUp);
        }

        if (isStart && !timesUp)

        {
            totalPlayTime -= Time.deltaTime;
            minutes = (int)totalPlayTime / 60;
            second = (int)totalPlayTime % 60;
            timeText.text = minutes.ToString("00") + ":" + second.ToString("00");
        }

        if (totalPlayTime <= 0 && !timesUp)
        {
            // Waktu Habis
            timesUp = true;
            gameOverUI.SetActive(true);
        }
    }

    public void checkEmptyDropZone()
    {
        int totalEmptyDropZone = 0;

        List<GameObject> dropZoneArray = new List<GameObject>(GameObject.FindGameObjectsWithTag("DropZone"));

        foreach (GameObject obj in dropZoneArray)
        {
            if (obj.GetComponent<DropZone>().isEmpty)
            {
                totalEmptyDropZone++;
            }
        }
        if (totalEmptyDropZone >= dropZoneArray.Count)
        {
            timesUp = true;
            gameOverUI.SetActive(true);
        }
    }
}
