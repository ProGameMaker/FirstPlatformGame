using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float move;
    public float climb;

    public Move moveScript;
    public CharacterController2D controller;
    public Animator animator;
    public bool isJump = false;
    public bool isHurt = false;
    public bool canClimb = false;
    private bool ExitCollasp = false;

    private int health;
    void Start()
    {
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
            climb = Input.GetAxisRaw("Vertical") * speed;
            move = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump")) {
                isJump = true;
                animator.SetBool("jump", isJump);
            }


            animator.SetFloat("move",Mathf.Abs(move));

            if (health < 0) {
                FindObjectOfType<GameManagers>().Restart();
            }
    }

    void FixedUpdate() {

        controller.Move(move * Time.fixedDeltaTime, false, isJump);
        isJump = false;
    }
    

    void OnTriggerEnter2D(Collider2D collider) {

         if (collider.tag == "Bound") {
            FindObjectOfType<GameManagers>().Restart();
        }

         if (collider.tag == "Sign") {
            FindObjectOfType<GameManagers>().StartDialog();
        }

        
    }


    void OnTriggerStay2D(Collider2D collider) {

       

        if (FindObjectOfType<CheckGround>().isCollide) return;

        if ((collider.tag == "Enemy" || collider.tag == "Laser") && !ExitCollasp) {
            
            if (collider.tag == "Laser") Destroy(collider.gameObject);

            StartCoroutine(Hurt());
            string name = "Image" + health.ToString("0");
            
            GameObject image = GameObject.Find(name);
            Destroy(image);

        }

        if (collider.tag == "Ladder" && climb != 0) {
            Transform childTransform = collider.gameObject.transform.Find("Teleport");
            transform.position = childTransform.position;
           
        } 

        if (collider.tag == "Complete") {
            if (Input.GetButtonDown("Submit"))
                Application.LoadLevel("BossFight");
        }

    }

    void OnTriggerExit2D() {
        FindObjectOfType<GameManagers>().CloseDialog();
    }

    void ExitHurt() {
        animator.SetBool("hurt", false);
        moveScript.enabled = true;
    }

    IEnumerator Hurt() {
        animator.SetBool("hurt", true);
        health -= 1;
        moveScript.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ExitCollasp = true;
        yield return new WaitForSeconds(2);
        ExitCollasp = false;
    }

    /* void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bound") {
            
        }

    }*/

}
