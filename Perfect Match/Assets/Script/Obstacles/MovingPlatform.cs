using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    public Transform targetPos;
    public float speed = 1f;
    public Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime * 1000);
        if (transform.position ==  targetPos.position)
        {
            transform.position = spawnPos.position;
        }
    }
}
