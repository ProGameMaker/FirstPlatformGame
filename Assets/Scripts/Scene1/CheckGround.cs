using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public Animator animator;
    public bool isCollide = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if (collider.tag == "Weakness") {

            GameObject parent = collider.gameObject.transform.parent.gameObject;
            Debug.Log(parent);
            if (parent.GetComponent<FinalBoss>()) parent.GetComponent<FinalBoss>().LoseHP();
            if (parent.GetComponent<Eagle>()) parent.GetComponent<Eagle>().LoseHP();
            if (parent.GetComponent<Frog>()) {
                Debug.Log("ENter");   
                parent.GetComponent<Frog>().LoseHP();
            }
            

            StartCoroutine(Collide());
        }
        
    }

    IEnumerator Collide() {

        isCollide = true;
        yield return new WaitForSeconds(2);
        isCollide = false;


    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Ground")
            animator.SetBool("jump", false);
    }

}
