using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelInUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI levelText;

    void Start()
    {
        // levelText.text = "Level: " + LevelDesign.instance.GetLevel().ToString();
        levelText.text = "Level: " + PlayerPrefs.GetInt("Level").ToString();
    }    
}
