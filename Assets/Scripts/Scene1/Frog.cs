using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Rigidbody2D cc;
    public Animator animator;
    public Transform player;

    public float speed = 10f;
    public bool IsRun = false;
    public int health = 1;
    bool IsFollow = false;

    private bool rayEnable = true;
    bool ExitCollapse = true;
    Vector2 dicrection = Vector2.left;
    Vector3 _position;
    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, dicrection, 8f);

        for (int i = 0; i < hits.Length; i++)
        {
           
            RaycastHit2D hit = hits[i];
            if (hit.collider.tag == "Player" && rayEnable && ExitCollapse)
            {
                IsRun = true;
                IsFollow = true;
           
            }
        }
        
        animator.SetBool("run", IsRun);
    }

    void FixedUpdate() {
        if (IsRun && ExitCollapse && IsFollow) {

            if (player.position.x > transform.position.x) {
                speed = Mathf.Abs(speed);
                transform.localScale = new Vector3(-3,3,1);

                dicrection = Vector2.right;
            }
            else {
                speed = -Mathf.Abs(speed);
                transform.localScale = new Vector3(3,3,1);

                dicrection = Vector2.left;
            }

               
            cc.velocity = new Vector2(speed * Time.fixedDeltaTime,0);
        
        }

        
    }

    /* void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            //StartCoroutine(touchPlayer());
            IsRun = true;
        }
    }*/

    void OnTriggerStay2D(Collider2D collider) {

        if (collider.tag == "Player") {

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(touchPlayer());
        }
    }

    void OnTriggerExit2D() {
        
       ExitCollapse = true;
    }

    IEnumerator touchPlayer() {
        IsRun = false; rayEnable = false; ExitCollapse = false;
        yield return new WaitForSeconds(3);
        IsRun = true; rayEnable = true;  
    }

    public void StopRunning() {
        IsRun = false;
        IsFollow = false;
        rayEnable = false;
        ExitCollapse = false;
        cc.velocity = Vector2.zero;
        StartCoroutine(ReturnPosition());
    }

    IEnumerator ReturnPosition() {

        yield return new WaitForSeconds(1);
        IsRun = true;
        cc.AddForce(new Vector2((_position.x - transform.position.x) * 30f,(_position.y - transform.position.y) * 30f));

        rayEnable = true;
        ExitCollapse = true;

        transform.localScale = new Vector3(-3,3,1);
        dicrection = Vector2.right;

        yield return new WaitForSeconds(2);

        if (transform.position.x > _position.x) cc.velocity = Vector2.zero;
        IsRun = false;
        transform.localScale = new Vector3(3,3,1);
        dicrection = Vector2.left;
    }

    public void LoseHP() {
        health -= 1;
        if (health == 0) animator.SetBool("destroy",true);
    }

    public void EnemyDestroy() {
        Destroy(gameObject);
    }


}
