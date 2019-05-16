using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = -100f;
    public int health = 1;
    public GameObject player;

    Vector2 dicrection = new Vector2(-1,-1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime,0);
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, dicrection, 200f);

        for (int i = 0; i < hits.Length; i++)
        {
           
            RaycastHit2D hit = hits[i];
            if (hit.collider.tag == "Player")
            {
                 StartCoroutine(RealAttack());
            }
        }
         
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player" && collider.tag != "PlayerGround") {
            transform.localScale = new Vector3(-transform.localScale.x,3,1);
            speed = -speed;
            dicrection = new Vector2(-dicrection.x,-1);
        }
    }

    IEnumerator RealAttack() {

        float offsetX = (player.transform.position.x - transform.position.x) * 150f;
        float offsetY = (player.transform.position.y - transform.position.y) * 150f;
        
        
        rb.AddForce(new Vector2(offsetX,offsetY));
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(new Vector2(offsetX,-offsetY));
        
    }

    public void LoseHP() {
        health -= 1;
        if (health == 0) gameObject.GetComponent<Animator>().SetBool("destroy",true);
    }

     public void EnemyDestroy() {
        
        Destroy(gameObject);
    }

}
