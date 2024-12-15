using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public bool isEmpty = true;
    public string objectName;
    public GameObject currDraggableObject;
    float lastDrag;
    float delayTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currDraggableObject != null && !currDraggableObject.GetComponent<Draggable>().isDragging && Time.time - lastDrag >= delayTime)
        {
            // currDraggableObject.transform.position = transform.position;
        } else
        {
            lastDrag = Time.time;
        }
    }
}
