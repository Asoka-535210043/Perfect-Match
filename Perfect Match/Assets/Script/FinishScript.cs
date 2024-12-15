using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public int totalPairObject;
    public bool hasGameManager;
    public GameObject resultUI;

    void Start()
    {
        if (hasGameManager)
        {
            GameObject getGameManager = GameObject.Find("GameManager");
            totalPairObject = getGameManager.GetComponent<GameManagerLevel2_5>().totalPairObj;
        }
    }

    public void Matches3()
    {
        totalPairObject--;
        if  (totalPairObject <= 0)
        {
            resultUI.SetActive(true);
        }
    }

}
