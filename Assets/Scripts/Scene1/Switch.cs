using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D() {

        GetComponent<SpriteRenderer>().sprite = image;
        GameObject ladder = GameObject.Find("MainLadder");
        StartCoroutine(TriggerLadder(ladder));
    }

    IEnumerator TriggerLadder(GameObject ladder) {

        ladder.GetComponent<Rigidbody2D>().gravityScale = 1;
        ladder.GetComponent<BoxCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(5f);

        ladder.GetComponent<Rigidbody2D>().gravityScale = 0;
        ladder.GetComponent<BoxCollider2D>().isTrigger = true;

    } 
}
