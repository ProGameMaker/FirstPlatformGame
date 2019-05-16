﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject boss = GameObject.Find("Boss");
        GetComponent<Rigidbody2D>().velocity = new Vector2(-boss.transform.localScale.x,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player" && collider.tag != "Enemy") Destroy(gameObject);
    }
}
