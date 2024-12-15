using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockingObject : MonoBehaviour
{
    public int health = 1;
    public TextMeshPro hpText;
    public GameObject kotak;

    // Start is called before the first frame update
    void Start()
    {
        hpText.sortingOrder = 4;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = kotak.transform.position;
        hpText.text = health.ToString();
        if (health <= 0)
        {
            kotak.GetComponent<Match3Script>().isBlocked = false;
            Destroy(gameObject);
        }
    }

    public void Damaged()
    {
        health -= 1;
    }

}
