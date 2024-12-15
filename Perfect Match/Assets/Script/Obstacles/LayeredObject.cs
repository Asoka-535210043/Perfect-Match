using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayeredObject : MonoBehaviour
{
    private SpriteRenderer spriteColor;
    private Draggable getDraggableScript;
    private DropZone dropzoneKiri;
    private DropZone dropzoneTengah;
    private DropZone dropzoneKanan;
    public GameObject kotak;
    public DropZone currDropZone;
    public int layerIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteColor = GetComponent<SpriteRenderer>();
        getDraggableScript = GetComponent<Draggable>();
        dropzoneKiri = kotak.transform.GetChild(0).GetComponent<DropZone>();
        dropzoneTengah = kotak.transform.GetChild(1).GetComponent<DropZone>();
        dropzoneKanan = kotak.transform.GetChild(2).GetComponent<DropZone>();
        spriteColor.color = new  Color(1, 1, 1, 0.2f);
        getDraggableScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currDropZone.transform.position;
        if (dropzoneKiri.isEmpty && dropzoneTengah.isEmpty && dropzoneKanan.isEmpty && dropzoneKanan.currDraggableObject == null && dropzoneKiri.currDraggableObject == null && dropzoneTengah.currDraggableObject == null)
        {
            layerIndex--;
            Debug.Log(this.gameObject.name + ": " + layerIndex);
        }

        if (layerIndex <= 0)
        {
            spriteColor.color = new Color(1, 1, 1, 1);
            getDraggableScript.enabled = true;
            this.gameObject.layer = 3;
            SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 1;

            currDropZone.isEmpty = false;
            currDropZone.objectName = getDraggableScript.objectName;
            currDropZone.currDraggableObject = this.gameObject;
            
            GetComponent<LayeredObject>().enabled = false;
        }
    }
}