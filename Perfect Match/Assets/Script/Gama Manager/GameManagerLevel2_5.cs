using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLevel2_5 : MonoBehaviour
{
    public GameObject[] draggableObject;
    public int totalPairObj;
    public GameObject[] kotak;

    GameObject[] objectA;
    DropZone getScriptDropZone;

    // Start is called before the first frame update
    void Start()
    {
        int totalObj = totalPairObj * 3;

        objectA = new GameObject[totalObj];

        // Generate 3 objects per pair
        for (int i = 0; i < totalObj; i++)
        {
            if (i % 3 == 0)
            {
                int randomizeObj = Random.Range(0, draggableObject.Length);
                objectA[i] = draggableObject[randomizeObj];
            }
            else
            {
                objectA[i] = objectA[i - 1];
            }
        }

        // Create a list to track available drop zones
        List<Transform> availableDropZones = new List<Transform>();

        // Add all drop zones for all kotaks
        for (int i = 0; i < kotak.Length; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                availableDropZones.Add(kotak[i].transform.GetChild(j));
            }
        }

        // Place each object randomly in the available drop zones
        for (int i = 0; i < totalObj; i++)
        {
            bool objekPlaced = false;

            while (!objekPlaced && availableDropZones.Count > 0)
            {
                // Pick a random drop zone from the available ones
                int randomIndex = Random.Range(0, availableDropZones.Count);
                Transform selectedDropZoneTransform = availableDropZones[randomIndex];
                GameObject dropZone = selectedDropZoneTransform.gameObject;
                objectA[i].GetComponent<Draggable>().currDropZone = dropZone;


                getScriptDropZone = dropZone.GetComponent<DropZone>();
                string objekName = objectA[i].GetComponent<Draggable>().objectName;

                // Untuk mendapatkan kotak index ke berapa dari array kotak
                int kotakIndex = FindKotakIndex(dropZone.transform.parent.gameObject);
                // Untuk mendapatkan child index dropzone dari kotak 
                int dropZoneIndex = dropZone.transform.GetSiblingIndex();
                // Mengecek apakah kotak sudah terisi atau belum dibagian kiri atau kanan yang sama dengan tengah
                bool isMatch3 = CheckIsMatch3(kotakIndex, objekName, dropZoneIndex);

                if (getScriptDropZone.isEmpty && !isMatch3)
                {
                    // Place the object in the drop zone
                    getScriptDropZone.isEmpty = false;
                    getScriptDropZone.objectName = objekName;
                    GameObject objSpawn = Instantiate(objectA[i], selectedDropZoneTransform.position, Quaternion.identity);
                    objSpawn.GetComponent<Draggable>().enabled = true;
                    getScriptDropZone.currDraggableObject = objSpawn;
                    

                    objekPlaced = true;
                    //Debug.Log(objectA[i].name);

                    // Remove this drop zone from the available list
                    availableDropZones.RemoveAt(randomIndex);
                }
            }
        }
    }

    // Helper function to find the kotak index based on the drop zone's parent
    int FindKotakIndex(GameObject kotakObject)
    {
        for (int i = 0; i < kotak.Length; i++)
        {
            if (kotak[i] == kotakObject)
            {
                return i;
            }
        }
        return -1; // Kotak not found
    }

    bool CheckIsMatch3(int indexKotak, string objName, int dropZoneIndex)
    {
        DropZone dropZoneA = null;
        DropZone dropZoneB = null;

        if (dropZoneIndex == 0)
        {
            dropZoneA = kotak[indexKotak].transform.GetChild(1).gameObject.GetComponent<DropZone>();
            dropZoneB = kotak[indexKotak].transform.GetChild(2).gameObject.GetComponent<DropZone>();
        }
        else if (dropZoneIndex == 1)
        {
            dropZoneA = kotak[indexKotak].transform.GetChild(0).gameObject.GetComponent<DropZone>();
            dropZoneB = kotak[indexKotak].transform.GetChild(2).gameObject.GetComponent<DropZone>();
        }
        else if (dropZoneIndex == 2)
        {
            dropZoneA = kotak[indexKotak].transform.GetChild(0).gameObject.GetComponent<DropZone>();
            dropZoneB = kotak[indexKotak].transform.GetChild(1).gameObject.GetComponent<DropZone>();
        }

        return dropZoneA.objectName == dropZoneB.objectName && dropZoneA.objectName == objName;
    }

}