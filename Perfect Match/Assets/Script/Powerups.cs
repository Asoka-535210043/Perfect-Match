using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Powerups : MonoBehaviour
{
    
    public AudioClip timeSfx;
    public AudioClip hammerSfx;
    public AudioClip wandSfx;

    public  TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI totalHammerText;
    public TextMeshProUGUI totalWandText;

    public GameObject[] changeObject;

    List<GameObject> objectNeedBreak;

    public GameObject timeTextEffect;
    public GameObject magicWandEffect;

    public TimeManager getTimeManagerScript;

    private List<GameObject> allBlockObject;
    private List<GameObject> draggableObject;

    private PMFinishScript getPMFinishScript;
    private FinishScript getFinishScript;

    private AudioSource getAudio;

    GameObject randomGameObject;
    GameObject randomBlockedObject;
    GameObject randomObjectChange;

    int randomIndex = 0;
    int currIndex = 0;
    float timeInc = 30f;
    bool isHammerBreakObj = false;


    public void IncreaseTime()
    {
        if (PlayerPrefs.GetInt("Time") > 0 && Input.touchCount < 2)
        {
            getTimeManagerScript.totalPlayTime +=  timeInc;
            int totalTime = PlayerPrefs.GetInt("Time") - 1;
            PlayerPrefs.SetInt("Time", totalTime);
            totalTimeText.text = PlayerPrefs.GetInt("Time").ToString("F0");
            Instantiate(timeTextEffect, new Vector3(8.52f, 0.04f, 1f), Quaternion.identity);

            getAudio.clip = timeSfx;
            getAudio.Play();
        }
    }

    private void HammerBreakObj()
    {
        allBlockObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlockedObject"));
        draggableObject = new List<GameObject>();

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 3)
            {
                draggableObject.Add(obj);
            }
            else if (obj.tag == "BlockedObject")
            {
                allBlockObject.Add(obj);
            }
        }

        // Hammer logic here
        if (allBlockObject.Count > 0)
        {
            // Debug.Log("Destroy blocked obj");
            // Hapus elemen null dari allBlockObject
            for (int i = allBlockObject.Count - 1; i >= 0; i--)
            {
                if (allBlockObject[i] == null)
                {
                    allBlockObject.RemoveAt(i);
                }
            }

            // Pastikan masih ada elemen setelah penghapusan
            if (allBlockObject.Count > 0)
            {
                // Pilih satu objek secara acak
                int randomIndex = Random.Range(0, allBlockObject.Count);
                GameObject randomBlockedObject = allBlockObject[randomIndex];
                // allBlockObject.RemoveAt(randomIndex); // Hapus objek dari list sebelum dihancurkan
                Destroy(randomBlockedObject);
                randomBlockedObject.GetComponent<BlockingObject>().kotak.GetComponent<Match3Script>().isBlocked = false;
            }
        }

        else if (draggableObject.Count > 0)
        {
            // Jika tidak terdapat blocked object, hancurkan 3 random object dengan tipe yang sama
            // Debug.Log("Destroy draggable obj");

            // Update list draggable object, menghapus yang null
            for (int i = 0; i < draggableObject.Count; i++)
            {
                if (draggableObject[i] == null)
                {
                    draggableObject.Remove(draggableObject[i]);
                }
            }

            // Mencari salah satu tipe draggable object secara acak
            randomIndex = Random.Range(0, draggableObject.Count);
            randomGameObject = draggableObject[randomIndex];

            Draggable GetDraggableScript = randomGameObject.GetComponent<Draggable>();

            // List untuk menampung draggable object yang setipe
            List<GameObject> sameTypeObjects = new List<GameObject>();
            objectNeedBreak = new List<GameObject>();

            // Mencari semua draggable object yang setipe dengan random draggable object yang dipilih
            foreach (GameObject obj in draggableObject)
            {
                if(!obj.GetComponent<Draggable>().enabled)
                {
                    obj.GetComponent<Draggable>().enabled = true;
                    if (obj.GetComponent<Draggable>().objectName == GetDraggableScript.objectName)
                    {
                        sameTypeObjects.Add(obj);
                    }
                    obj.GetComponent<Draggable>().enabled = false;
                } else
                {
                    if (obj.GetComponent<Draggable>().objectName == GetDraggableScript.objectName)
                    {
                        sameTypeObjects.Add(obj);
                    }
                }
                
            }

            // Jika ada setidaknya 3 draggable object dengan tipe yang sama
            if (sameTypeObjects.Count >= 3)
            {
                // Menghancurkan 3 random draggable object dari list yang setipe
                for (int i = 0; i < 3; i++)
                {
                    GameObject objToDestroy = sameTypeObjects[i];
                    GameObject objDropZone = objToDestroy.GetComponent<Draggable>().currDropZone;
                    if (objToDestroy.GetComponent<LayeredObject>() == null)
                    {
                        objDropZone.GetComponent<DropZone>().isEmpty = true;
                        objDropZone.GetComponent<DropZone>().objectName = "";
                        objDropZone.GetComponent<DropZone>().currDraggableObject = null;
                    }
                    objToDestroy.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    // Debug.Log(objToDestroy);
                    if (objToDestroy != null)
                    {
                        objectNeedBreak.Add(objToDestroy);
                        // objectNeedBreak[currIndex] = objToDestroy;
                        // currIndex++;
                    }
                    objToDestroy.GetComponent<Draggable>().enabled = false;
                    objToDestroy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    objToDestroy.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    Destroy(objToDestroy, 1f);
                }
                isHammerBreakObj = true;
                StartCoroutine(WaitForBreakObjFinish());
            }
        }
    }

    IEnumerator WaitForBreakObjFinish()
    {
        yield return new WaitForSeconds(1f);
        isHammerBreakObj = false;
        currIndex = 0;
        objectNeedBreak.Clear();
    }

    public void BreakHammer()
    {
        if (PlayerPrefs.GetInt("Hammer") > 0  && Input.touchCount < 2)
        {
            // code disini
            HammerBreakObj();
            if (PlayerPrefs.GetInt("Level") > 5)
            {
                getPMFinishScript.Matches3();
            }
            else
            {
                getFinishScript.Matches3();
            }
            int totalHammer = PlayerPrefs.GetInt("Hammer") - 1;
            PlayerPrefs.SetInt("Hammer", totalHammer);
            totalHammerText.text = PlayerPrefs.GetInt("Hammer").ToString("F0");

            getAudio.clip = hammerSfx;
            getAudio.Play();
        }
    }

    private void ChangeObject()
    {
        List<GameObject> spawnObjArr = new List<GameObject>();

        for (int m = 0; m < 2; m++)
        {
            draggableObject = new List<GameObject>();
            draggableObject.Clear();

            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.layer == 3)
                {
                    draggableObject.Add(obj);
                }
            }


            if (draggableObject.Count > 0)
            {

                // Mencari salah satu tipe draggable object secara acak
                randomIndex = Random.Range(0, draggableObject.Count);
                randomGameObject = draggableObject[randomIndex];

                // Debug.Log(randomIndex);
                // Debug.Log(randomGameObject.name);

                Draggable GetDraggableScript = randomGameObject.GetComponent<Draggable>();

                // List untuk menampung draggable object yang setipe
                List<GameObject> sameTypeObjects = new List<GameObject>();

                // Mencari semua draggable object yang setipe dengan random draggable object yang dipilih
                foreach (GameObject obj in draggableObject)
                {
                    if (!obj.GetComponent<Draggable>().enabled)
                    {
                        obj.GetComponent<Draggable>().enabled = true;
                        if (obj.GetComponent<Draggable>().objectName == GetDraggableScript.objectName)
                        {
                            sameTypeObjects.Add(obj);
                        }
                        obj.GetComponent<Draggable>().enabled = false;
                    }
                    else
                    {
                        if (obj.GetComponent<Draggable>().objectName == GetDraggableScript.objectName)
                        {
                            sameTypeObjects.Add(obj);
                        }
                    }
                }

                // Jika ada setidaknya 3 draggable object dengan tipe yang sama
                if (sameTypeObjects.Count >= 3)
                {
                    // random change object
                        int randomIndexObjChange = Random.Range(0, changeObject.Length);
                        GameObject randomObjChange = changeObject[randomIndexObjChange];

                    // Mengubah 3 random draggable object dari list yang setipe
                    for (int i = 0; i < 3; i++)
                    {
                        // get current object that need to change
                        GameObject objToChange = sameTypeObjects[i];
                        GameObject objDropZone = objToChange.GetComponent<Draggable>().currDropZone;

                        // spawn change object
                        GameObject changeObjSpawn = Instantiate(randomObjChange, objToChange.transform.position, Quaternion.identity);
                        changeObjSpawn.GetComponent<Draggable>().currDropZone = objDropZone;
                        spawnObjArr.Add(changeObjSpawn);
                        changeObjSpawn.layer = 1;
                        Instantiate(magicWandEffect, changeObjSpawn.transform.position, Quaternion.identity);

                        // check is layered object
                        if (objToChange.GetComponent<LayeredObject>() == null)
                        {
                            objDropZone.GetComponent<DropZone>().objectName = randomObjChange.GetComponent<Draggable>().objectName;
                            objDropZone.GetComponent<DropZone>().currDraggableObject = changeObjSpawn;
                        } else
                        {
                            changeObjSpawn.GetComponent<Draggable>().enabled = false;
                            changeObjSpawn.AddComponent<LayeredObject>();
                        }
                        objToChange.layer = 2;
                        Destroy(objToChange);
                    }
                }
            }
        }
        foreach (GameObject gObj in spawnObjArr)
        {
            gObj.layer = 3;
        }
    }

    public void MagicWand()
    {
        if (PlayerPrefs.GetInt("Wand") > 0 && Input.touchCount < 2)
        {
            // code disini
            ChangeObject();
            int totalWand = PlayerPrefs.GetInt("Wand") - 1;
            PlayerPrefs.SetInt("Wand", totalWand);
            totalWandText.text = PlayerPrefs.GetInt("Wand").ToString("F0");
            
            getAudio.clip = wandSfx;
            getAudio.Play();
        }
    }

    void Start()
    {
        totalTimeText.text = PlayerPrefs.GetInt("Time").ToString();
        totalHammerText.text = PlayerPrefs.GetInt("Hammer").ToString();
        totalWandText.text = PlayerPrefs.GetInt("Wand").ToString();

        GameObject sfxObject = GameObject.FindWithTag("Sfx");
        getAudio = sfxObject.GetComponent<AudioSource>();

        GameObject finishScriptGameObject = GameObject.FindWithTag("FinishScript");
        getFinishScript = finishScriptGameObject.GetComponent<FinishScript>();
        if (getFinishScript == null)
        {
            getPMFinishScript = finishScriptGameObject.GetComponent<PMFinishScript>();
        }

        allBlockObject = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlockedObject"));
        draggableObject = new List<GameObject>();
        
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == 3)
            {
                draggableObject.Add(obj);
            } else if(obj.tag == "BlockedObject")
            {
                allBlockObject.Add(obj);
            }
        }
        // Debug.Log(allBlockObject.Count);
        // Debug.Log(draggableObject.Count);
    }

    void Update()
    {
        if (isHammerBreakObj)
        {
            if (objectNeedBreak[0] != null && objectNeedBreak[1] != null && objectNeedBreak[2] != null)
            {
                objectNeedBreak[0].transform.position = Vector3.MoveTowards(objectNeedBreak[0].transform.position, new Vector3(0, 0, 0), 5f * Time.deltaTime);
                objectNeedBreak[1].transform.position = Vector3.MoveTowards(objectNeedBreak[1].transform.position, new Vector3(0, 0, 0), 5f * Time.deltaTime);
                objectNeedBreak[2].transform.position = Vector3.MoveTowards(objectNeedBreak[2].transform.position, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            }
        }
    }
}
