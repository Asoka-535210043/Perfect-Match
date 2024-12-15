using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWandEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.5f);
        Renderer textRenderer = GetComponent<Renderer>();
        textRenderer.sortingOrder = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
