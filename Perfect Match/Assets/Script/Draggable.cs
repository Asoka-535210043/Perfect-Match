using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{
    Vector3 offset;
    Collider2D col2D;
    Vector3 curPos;
    public string objectName;
    private AudioSource getAudio;
    public AudioClip dropSfx;
    [HideInInspector]
    public bool isDragging = false;
    public GameObject currDropZone;
    int draggingFingerId = -1;  // Finger ID to track the touch during drag
    LayerMask layerMask = 3;
    // LayerMask layerMask = LayerMask.GetMask("ObjekA");
    DropZone getScriptDropZone = null;
    DropZone getScriptDropZonePlace = null;

    private void Start()
    {
        col2D = GetComponent<Collider2D>();
        GameObject sfxObject = GameObject.FindWithTag("Sfx");
        getAudio = sfxObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Use the first touch for now

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchDown(touch);
                    break;

                case TouchPhase.Moved:
                    OnTouchDrag(touch);
                    break;

                case TouchPhase.Ended:
                    OnTouchUp(touch);
                    break;
            }
        }

        if (!isDragging)
        {
            transform.position = currDropZone.transform.position;
        }
    }

    private void OnTouchDown(Touch touch)
    {
        // Check if touch started on the object
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, layerMask);


        if (hit.collider != null && hit.transform.tag == "DropZone")  // Pastikan collider tidak null
        {
            // Debug.Log(hit.collider.gameObject.name);
            Collider2D dropZoneCol = hit.collider.gameObject.GetComponent<Collider2D>();

            if (dropZoneCol != null)  // Pastikan komponen collider ada
            {
                dropZoneCol.enabled = false;  // Mematikan collider sementara
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, layerMask);  // Coba raycast lagi
                dropZoneCol.enabled = true;  // Menyalakan collider kembali
            }
        }

        if (hit.collider != null && hit.collider.gameObject == this.gameObject)
        {
            GameObject getKotakParent = currDropZone.transform.parent.gameObject;

            if (!getKotakParent.GetComponent<Match3Script>().isBlocked)
            {
                offset = transform.position - TouchWorldPosition(touch.position);
                curPos = transform.position;
                col2D.enabled = false;
                isDragging = true;
                draggingFingerId = touch.fingerId;  // Track the touch by its ID

                // Check if it's in a drop zone
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, TouchWorldPosition(touch.position) - transform.position);

                if (hitInfo && hitInfo.transform.tag == "DropZone")
                {
                    getScriptDropZone = hitInfo.collider.gameObject.GetComponent<DropZone>();
                    // Debug.Log(getScriptDropZone);
                }
                col2D.enabled = true;
            }

           
        }
        // Debug.Log(transform.position);
    }

    private void OnTouchDrag(Touch touch)
    {
        if (isDragging && touch.fingerId == draggingFingerId)  // Ensure we're dragging with the right touch
        {
            transform.position = TouchWorldPosition(touch.position) + offset;
        }
    }

    void clearDropZone()
    {
        // Cek apakah drop zone lama sudah diisi
        if (getScriptDropZone != null && !getScriptDropZone.isEmpty)
        {
            // Mengosongkan dropzone lama
            // Debug.Log("Mengosongkan dropzone lama: " + getScriptDropZone.gameObject.name);
            getScriptDropZone.isEmpty = true;
            getScriptDropZone.objectName = "";
            getScriptDropZone.currDraggableObject = null;
        }
    }

    bool checkDropZone(GameObject hitDropZone)
    {
        GameObject kotakParent = hitDropZone.transform.parent.gameObject;
        // Debug.Log(kotakParent.name);
        if (hitDropZone.name == "Kiri")
        {
            if (kotakParent.transform.GetChild(1).GetComponent<DropZone>().isEmpty)
            {
                clearDropZone();

                // Debug.Log("Tengah");
                
                DropZone getHitDropZoneScript = kotakParent.transform.GetChild(1).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(1).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(1).position + new Vector3(0, 0, -0.01f);
                return true;
            } else if (kotakParent.transform.GetChild(2).GetComponent<DropZone>().isEmpty) {
                clearDropZone();

                // Debug.Log("Kanan");
                
                DropZone getHitDropZoneScript =kotakParent.transform.GetChild(2).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(2).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(2).position + new Vector3(0, 0, -0.01f);
                return true;
                
            } else {
                return false;
            }

        } else if (hitDropZone.name == "Kanan")
        {
            if (kotakParent.transform.GetChild(0).GetComponent<DropZone>().isEmpty)
            {
                clearDropZone();

                // Debug.Log("Kiri");
                
                DropZone getHitDropZoneScript = kotakParent.transform.GetChild(0).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(0).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(0).position + new Vector3(0, 0, -0.01f);
                return true;
            } else if (kotakParent.transform.GetChild(1).GetComponent<DropZone>().isEmpty) {
                clearDropZone();

                // Debug.Log("Tengah");
                
                DropZone getHitDropZoneScript = kotakParent.transform.GetChild(1).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(1).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(1).position + new Vector3(0, 0, -0.01f);
                return true;
            } else {
                return false;
            }

        } else if (hitDropZone.name == "Tengah")
        {
            if (kotakParent.transform.GetChild(0).GetComponent<DropZone>().isEmpty)
            {
                clearDropZone();

                // Debug.Log("Kiri");
                
                DropZone getHitDropZoneScript = kotakParent.transform.GetChild(0).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(0).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(0).position + new Vector3(0, 0, -0.01f);
                return true;
            } else if (kotakParent.transform.GetChild(2).GetComponent<DropZone>().isEmpty) {
                clearDropZone();

                // Debug.Log("Kanan");
                
                DropZone getHitDropZoneScript = kotakParent.transform.GetChild(2).GetComponent<DropZone>();

                // Mengisi dropzone baru
                currDropZone = kotakParent.transform.GetChild(2).gameObject;
                getHitDropZoneScript.isEmpty = false;
                getHitDropZoneScript.objectName = objectName;
                getHitDropZoneScript.currDraggableObject = this.gameObject;
                this.transform.position = kotakParent.transform.GetChild(2).position + new Vector3(0, 0, -0.01f);
                return true;
            } else {
                return false;
            }
            
        }
        else {
            return false;
        }
    }

    private void OnTouchUp(Touch touch)
    {
        
        if (isDragging && touch.fingerId == draggingFingerId)
        {
            col2D.enabled = false;
            isDragging = false;
            draggingFingerId = -1;

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero); // Updated raycast

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("DropZone"))
            {

                GameObject getKotakParent = hitInfo.collider.gameObject.transform.parent.gameObject;

                if (!getKotakParent.GetComponent<Match3Script>().isBlocked)
                {
                    getScriptDropZonePlace = hitInfo.collider.gameObject.GetComponent<DropZone>();

                    getAudio.clip = dropSfx;
                    getAudio.Play();

                    if (getScriptDropZonePlace != null) // Ensure dropzone is not null
                    {
                        clearDropZone();

                        if (getScriptDropZonePlace.isEmpty)
                        {
                            currDropZone = hitInfo.collider.gameObject;
                            getScriptDropZonePlace.isEmpty = false;
                            getScriptDropZonePlace.objectName = objectName;
                            getScriptDropZonePlace.currDraggableObject = this.gameObject;
                            this.transform.position = hitInfo.transform.position + new Vector3(0, 0, -0.01f);
                        }
                        else
                        {
                            bool checkingDropZone = checkDropZone(hitInfo.collider.gameObject);
                            if (!checkingDropZone)
                            {
                                this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                            }
                        }
                    }
                    else
                    {
                        this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                    }
                }
            }
            else if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == 3)
            {
                GameObject getDropZone = hitInfo.collider.gameObject.GetComponent<Draggable>().currDropZone;
                GameObject getKotakParent = getDropZone.transform.parent.gameObject;

                getAudio.clip = dropSfx;
                getAudio.Play();

                if (!getKotakParent.GetComponent<Match3Script>().isBlocked)
                {
                    Collider2D objectCol = hitInfo.collider.gameObject.GetComponent<Collider2D>();

                    if (objectCol != null)
                    {
                        objectCol.enabled = false;
                        hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, layerMask);
                        objectCol.enabled = true;
                    }

                    if (hitInfo.collider != null && hitInfo.collider.gameObject != null
                        && hitInfo.collider.gameObject.GetComponent<DropZone>() != null)
                    {
                        bool checkingDropZone = checkDropZone(hitInfo.collider.gameObject);
                        if (!checkingDropZone)
                        {
                            this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                        }
                    }
                    else
                    {
                        // Debug.Log("Drop zone or collider component is NULL.");
                    }
                }
            }
            else
            {
                this.transform.position = curPos + new Vector3(0, 0, -0.01f);
            }

            col2D.enabled = true;
        }
    }


    /*
    private void OnTouchUp(Touch touch)
    {
        if (isDragging && touch.fingerId == draggingFingerId)
        {
            col2D.enabled = false;
            isDragging = false;
            draggingFingerId = -1;

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.zero); // Memperbaiki raycast

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("DropZone"))
            {
                getScriptDropZonePlace = hitInfo.collider.gameObject.GetComponent<DropZone>();
                
                if (getScriptDropZonePlace != null) // Pastikan dropzone tidak null
                {
                    clearDropZone();

                    // Mengisi dropzone baru
                    if (getScriptDropZonePlace.isEmpty)
                    {
                        currDropZone = hitInfo.collider.gameObject;
                        getScriptDropZonePlace.isEmpty = false;
                        getScriptDropZonePlace.objectName = objectName;
                        getScriptDropZonePlace.currDraggableObject = this.gameObject;
                        this.transform.position = hitInfo.transform.position + new Vector3(0, 0, -0.01f);
                    }
                    else
                    {
                        // Jika drop zone baru sudah terisi, kembalikan ke posisi awal
                        bool checkingDropZone = checkDropZone(hitInfo.collider.gameObject);
                        if (!checkingDropZone)
                        {
                            this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                        }
                    }
                }
                else
                {
                    // Jika getScriptDropZonePlace adalah null, kembalikan ke posisi awal
                    this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                }
            }
            else if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == 3)
            {
                // Debug.Log(hit.collider.gameObject.name);
                Collider2D objectCol = hitInfo.collider.gameObject.GetComponent<Collider2D>();

                if (objectCol != null)  // Pastikan komponen collider ada
                {
                    objectCol.enabled = false;  // Mematikan collider sementara
                    hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, layerMask);  // Coba raycast lagi
                    objectCol.enabled = true;  // Menyalakan collider kembali
                }

                if (hitInfo.collider.gameObject != null)
                {
                    bool checkingDropZone = checkDropZone(hitInfo.collider.gameObject);
                    if (!checkingDropZone)
                    {
                        this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                    }
                } else
                {
                    Debug.Log("INI NULLLLLLLLL");
                }
            }
            else if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == 3)
            {
                // Debug.Log(hit.collider.gameObject.name);
                Collider2D objectCol = hitInfo.collider.gameObject.GetComponent<Collider2D>();

                if (objectCol != null)  // Ensure the collider component exists
                {
                    objectCol.enabled = false;  // Temporarily disable collider
                    hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero, layerMask);  // Try raycast again
                    objectCol.enabled = true;  // Enable collider again
                }

                // Additional null check to ensure hitInfo.collider is still valid after re-assigning hitInfo
                if (hitInfo.collider.gameObject != null && hitInfo.collider.gameObject.GetComponent<DropZone>() != null)
                {
                    bool checkingDropZone = checkDropZone(hitInfo.collider.gameObject);
                    if (!checkingDropZone)
                    {
                        this.transform.position = curPos + new Vector3(0, 0, -0.01f);
                    }
                }
                else
                {
                    Debug.Log("INI NULLLLLLLLL");
                }
            }
            else
            {
                this.transform.position = curPos + new Vector3(0, 0, -0.01f);
            }

            // Debug.Log(this.transform.position);
            col2D.enabled = true;
        }
    }
    */

    Vector3 TouchWorldPosition(Vector3 touchPosition)
    {
        var touchScreenPos = touchPosition;
        touchScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(touchScreenPos);
    }
}