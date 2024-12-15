using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionScript : MonoBehaviour
{
    int chara = 0;

    void Start()
    {
        chara = PlayerPrefs.GetInt("Char");
    }

    public void CharMale()
    {
        chara = 1;
    }

    public void CharFemale()
    {
        chara = 2;
    }

    public void Saved()
    {
        PlayerPrefs.SetInt("Char", chara);
        gameObject.SetActive(false);
    }
}
