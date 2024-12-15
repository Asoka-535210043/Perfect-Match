using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMFinishScript : MonoBehaviour
{
    int totalPairObject;
    public GameObject resultUI;

    void Start()
    {
        //GameObject getGameManager = GameObject.Find("GameManager");
        //totalPairObject = getGameManager.GetComponent<PMGameManager>().totalPairObj;
        // Debug.Log(totalPairObject);
        StartCoroutine(WaitForGetPMGM());
    }

    private void Awake()
    {
        StartCoroutine(WaitForGetPMGM());
    }

    public void Matches3()
    {
        totalPairObject--;
        if  (totalPairObject <= 0)
        {
            resultUI.SetActive(true);
        }
    }
    IEnumerator WaitForGetPMGM()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject getGameManager = GameObject.Find("GameManager");
        totalPairObject = getGameManager.GetComponent<PMGameManager>().totalPairObj;
        // Debug.Log(totalPairObject);
    }
}
