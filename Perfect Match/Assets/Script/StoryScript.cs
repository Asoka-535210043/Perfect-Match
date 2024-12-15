using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScript : MonoBehaviour
{

    public GameObject StoryUI;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForVideoEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    IEnumerator WaitForVideoEnd()
    {
        yield return new WaitForSeconds(34f);
        StoryUI.SetActive(false);
    }
}
