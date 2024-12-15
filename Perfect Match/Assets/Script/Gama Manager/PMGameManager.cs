using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMGameManager : MonoBehaviour
{
    [HideInInspector]
    public int totalPairObj;
    public int totalPairObjMin;
    public int totalPairObjMax;
    public int totalEmptyObj = 3; // Jumlah drop zone yang harus disisakan
    public GameObject[] kotak;
    public GameObject blockingObj;
    public GameObject[] draggableObject;
    GameObject[] objectA;
    DropZone getScriptDropZone;

    void Start()
    {
        SpawnBlockingObject();
        // Inisialisasi jumlah objek pasangan
        // PlayerPrefs.SetInt("Level", 40);
        if (PlayerPrefs.GetInt("Level") >= 6 && PlayerPrefs.GetInt("Level") <= 12)
        {
            totalPairObjMax = 25;
            totalPairObjMin = 15;
            totalEmptyObj = 4;
        } else if (PlayerPrefs.GetInt("Level") >= 13 && PlayerPrefs.GetInt("Level") <= 18)
        {
            totalPairObjMax = 35;
            totalPairObjMin = 25;
            totalEmptyObj = 5;
        } else if (PlayerPrefs.GetInt("Level") >= 19 && PlayerPrefs.GetInt("Level") <= 25)
        {
            totalPairObjMax = 45;
            totalPairObjMin = 35;
            totalEmptyObj = 6;
        } else if (PlayerPrefs.GetInt("Level") >= 26 && PlayerPrefs.GetInt("Level") <= 36)
        {
            totalPairObjMax = 55;
            totalPairObjMin = 45;
            totalEmptyObj = 6;
        } else
        {
            totalPairObjMax = 65;
            totalPairObjMin = 55;
            totalEmptyObj = 6;
        }

        totalPairObj = Random.Range(totalPairObjMin, totalPairObjMax + 1);
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
        // Create list of layered object
        List<string> layeredObj = new List<string>();

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
                // Hitung jumlah drop zone kosong
                int emptyDropZoneCount = 0;
                foreach (var _dropZone in availableDropZones)
                {
                    DropZone dzScript = _dropZone.GetComponent<DropZone>();
                    if (dzScript.isEmpty)
                    {
                        emptyDropZoneCount++;
                    }
                }

                // Validasi: Jika sisa drop zone kosong kurang dari atau sama dengan totalEmptyObj, buat layered object
                if (emptyDropZoneCount <= totalEmptyObj)
                {
                    // Random kotak dan drop zone di dalamnya
                    int randomKotakIndex = Random.Range(0, kotak.Length);
                    int randomDropZoneIndex = Random.Range(0, 3);

                    GameObject selectedDropZone = kotak[randomKotakIndex].transform.GetChild(randomDropZoneIndex).gameObject;
                    GameObject spawnObjectA = Instantiate(objectA[i], selectedDropZone.transform.position, Quaternion.identity);
                    spawnObjectA.layer = 2;

                    // Disable draggable script pada objectA untuk layered object
                    spawnObjectA.GetComponent<Draggable>().currDropZone = selectedDropZone;
                    spawnObjectA.GetComponent<Draggable>().enabled = false;

                    // Tambahkan script LayeredObject
                    LayeredObject attachedLayeredObjScript = spawnObjectA.AddComponent<LayeredObject>();

                    int layeredIndex = 1;

                    // Atur kotak dan drop zone saat ini untuk LayeredObject
                    attachedLayeredObjScript.kotak = kotak[randomKotakIndex];
                    attachedLayeredObjScript.currDropZone = selectedDropZone.GetComponent<DropZone>();

                    // Cek apakah sudah ada layered object di posisi ini
                    for (int l = 0; l < layeredObj.Count; l++)
                    {
                        if (randomKotakIndex.ToString() + randomDropZoneIndex.ToString() + layeredIndex.ToString() == layeredObj[l])
                        {
                            layeredIndex++;
                            l = 0;
                        }
                    }

                    // Set layer index untuk LayeredObject
                    attachedLayeredObjScript.layerIndex = layeredIndex;

                    // Tambahkan ke daftar layered objects
                    layeredObj.Add(randomKotakIndex.ToString() + randomDropZoneIndex.ToString() + layeredIndex.ToString());

                    // Objek berhasil ditempatkan sebagai layered object
                    objekPlaced = true;
                }
                else
                {
                    // Pick a random drop zone from the available ones
                    int randomIndex = Random.Range(0, availableDropZones.Count);
                    Transform selectedDropZoneTransform = availableDropZones[randomIndex];
                    GameObject dropZone = selectedDropZoneTransform.gameObject;
                    objectA[i].GetComponent<Draggable>().currDropZone = dropZone;

                    getScriptDropZone = dropZone.GetComponent<DropZone>();
                    string objekName = objectA[i].GetComponent<Draggable>().objectName;

                    int kotakIndex = FindKotakIndex(dropZone.transform.parent.gameObject);
                    int dropZoneIndex = dropZone.transform.GetSiblingIndex();
                    bool isMatch3 = CheckIsMatch3(kotakIndex, objekName, dropZoneIndex);

                    if (getScriptDropZone.isEmpty && !isMatch3)
                    {
                        // Jika masuk sini, objek ditempatkan dan di-log
                        GameObject spawnObjectA = Instantiate(objectA[i], selectedDropZoneTransform.position, Quaternion.identity);
                        SpriteRenderer spriteRenderer = spawnObjectA.GetComponent<SpriteRenderer>();
                        spriteRenderer.sortingOrder = 1;
                        spawnObjectA.GetComponent<Draggable>().enabled = true;
                        getScriptDropZone.isEmpty = false;
                        getScriptDropZone.objectName = objectA[i].GetComponent<Draggable>().objectName;
                        getScriptDropZone.currDraggableObject = spawnObjectA;
                        // Debug.Log("Objek Berhasil");
                        objekPlaced = true;
                        availableDropZones.RemoveAt(randomIndex);
                    }
                    else
                    {
                        // Jika tidak masuk kondisi ini, objek tidak diinstansiasi
                        // Debug.Log("Kondisi tidak terpenuhi, objek tidak diinstansiasi");
                    }
                }
            }
        }
    }

    private void SpawnBlockingObject()
    {
        if (PlayerPrefs.GetInt("Level") >= 13)
        { 
            // Debug.Log("menspawn blocking object");
            int maxTotalBlocking = (int) (0.3f * kotak.Length);
            // Debug.Log(maxTotalBlocking);
            int minTotalBlocking = (int) maxTotalBlocking / 2;
            // Debug.Log(minTotalBlocking);
            int maxHPBlocking = (int) PlayerPrefs.GetInt("Level") / 3;
            // Debug.Log(maxHPBlocking);
            int minHPBlocking = (int) maxHPBlocking / 2;
            // Debug.Log(minHPBlocking);

            int totalBlockObj = Mathf.Clamp(Random.Range(minTotalBlocking, maxTotalBlocking), 1, 5);

            
            // Debug.Log(totalBlockObj);
            for (int i = 0; i < totalBlockObj; i++)
            {
                int randomHP = Random.Range(minHPBlocking, maxHPBlocking);
                int randKotakIndex = Random.Range(0, kotak.Length);

                GameObject spawnBlockObj = Instantiate(blockingObj, kotak[randKotakIndex].transform.position + new Vector3(0.05f, 0, -1), Quaternion.identity);
                spawnBlockObj.GetComponent<BlockingObject>().health = randomHP;
                spawnBlockObj.GetComponent<BlockingObject>().kotak = kotak[randKotakIndex];
                kotak[randKotakIndex].GetComponent<Match3Script>().isBlocked = true;
                // Debug.Log(i);
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

    // Cek apakah ada match3 di kotak yang sama
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
