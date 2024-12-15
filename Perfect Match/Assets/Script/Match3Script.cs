using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3Script : MonoBehaviour
{
    int value = 100;
    bool isPMGM = false;
    public bool isBlocked = false;
    public DropZone dropZoneKiri;
    public DropZone dropZoneTengah;
    public DropZone dropZoneKanan;
    public AudioClip match3Sfx;
    private AudioSource getAudio;
    private PMFinishScript getPMFinishScript;
    private ScoreDesign getScore;
    private FinishScript getFinishScript;
    private List<GameObject> allBlockedObject;
    private GameObject closestBlockedObject;
    private string blockedTag =  "BlockedObject";
    private BlockingObject GetBlockingObjectScript;
    
    // Start is called before the first frame update
    void Start()
    {
        //  Get all blocked objects

        allBlockedObject = new List<GameObject>(GameObject.FindGameObjectsWithTag(blockedTag));

        GameObject sfxObject = GameObject.FindWithTag("Sfx");
        getAudio = sfxObject.GetComponent<AudioSource>();
        
        GameObject getGameObjectScore = GameObject.FindWithTag("ScoreDesign");
        getScore = getGameObjectScore.GetComponent<ScoreDesign>();
        GameObject finishScriptGameObject = GameObject.FindWithTag("FinishScript");
        getFinishScript = finishScriptGameObject.GetComponent<FinishScript>();
        if (getFinishScript == null)
        {
            isPMGM = true;
            getPMFinishScript = finishScriptGameObject.GetComponent<PMFinishScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //   Untuk Match-3
        if(dropZoneKanan.objectName == dropZoneKiri.objectName && dropZoneKanan.objectName == dropZoneTengah.objectName && dropZoneKiri.objectName == dropZoneTengah.objectName && !dropZoneKanan.isEmpty && !dropZoneKiri.isEmpty && !dropZoneTengah.isEmpty)
        {

            dropZoneKanan.isEmpty = true;
            dropZoneKanan.objectName = "";
            // Destroy(dropZoneKanan.currDraggableObject);
            dropZoneTengah.isEmpty = true;
            dropZoneTengah.objectName = "";
            // Destroy(dropZoneTengah.currDraggableObject);
            dropZoneKiri.isEmpty = true;
            dropZoneKiri.objectName = "";
            // Destroy(dropZoneKiri.currDraggableObject);
            getScore.AddScore(value);
            if (isPMGM)
            {
                getPMFinishScript.Matches3();
            } else {
                getFinishScript.Matches3();
            }
            
            BlockedObjectHPDec();

            StartCoroutine(WaitForMatch3());

            getAudio.clip = match3Sfx;
            getAudio.Play();
        }
    }

    void BlockedObjectHPDec()
    {
        allBlockedObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlockedObject"));
        //  Mengurangi Blocked object
        Debug.Log(allBlockedObject.Count);
        // allBlockedObject = new List<GameObject>(GameObject.FindGameObjectsWithTag(blockedTag));
        // for (int i=0; i < allBlockedObject.Count; i++)
        // {
        //     if (allBlockedObject[i] == null)
        //     {
        //         allBlockedObject.Remove(allBlockedObject[i]);
        //     }
        // }
        closestBlockedObject = ClosestObject();
        GetBlockingObjectScript = closestBlockedObject.GetComponent<BlockingObject>();
        if (GetBlockingObjectScript != null)
        {
            GetBlockingObjectScript.Damaged();
        }
    }

    GameObject ClosestObject()
    {
        // Mereturn closest blocked object terdekat
        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;

        foreach (var blocked in allBlockedObject)
        {
            float distanceHere = Vector3.Distance(transform.position, blocked.transform.position);

            if(distanceHere < leastDistance)
            {
                leastDistance = distanceHere;
                closestHere = blocked;
            }
        }

        return closestHere;
    }

    IEnumerator WaitForMatch3()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(dropZoneKanan.currDraggableObject);
        Destroy(dropZoneTengah.currDraggableObject);
        Destroy(dropZoneKiri.currDraggableObject);
    }
}
