using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Transform player;
    public Transform position;
    public Transform laser;
    bool IsRun = false;
    public float speed = 40f;
    Vector2 dicrection = Vector2.left;
    public Rigidbody2D cc;
    public Animator animator;
    public CharacterController2D controller;

    //float Jump = 0;
    public float JumpForce = 200f;
    public int jumpTime = 5;
    int time = 0;
    public float lazerTime = 1f;
    public Sprite lazeImage;
    bool didJump = false;
    private Vector3 m_Velocity = Vector3.zero;
    public int lazeRange = 5;
    public int hp = 3;
    int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.CircleCastAll(transform.position, 10f, dicrection, 8f);

        for (int i = 0; i < hits.Length; i++)
        {
           
            RaycastHit2D hit = hits[i];
            if (hit.collider.tag == "Player")
            {
                IsRun = true;
            }
        }
        
        animator.SetBool("run", IsRun);

        if (IsRun) {

            float distance = player.position.x - transform.position.x;
            time += 1;

            if (distance > 0) {
                speed = Mathf.Abs(speed);
                transform.localScale = new Vector3(-10,10,1);

                dicrection = Vector2.right;
            }
            else {
                speed = -Mathf.Abs(speed);
                transform.localScale = new Vector3(10,10,1);

                dicrection = Vector2.left;
            }

            if (Mathf.Abs(distance) > 5 && Mathf.Abs(distance) < 40 && time % jumpTime == 0) {
                Debug.Log(didJump);
                didJump = true;
                time = time % jumpTime;
            }

            if (Mathf.Abs(distance) > 10 && time % lazerTime == 0) {
                ShootLaser();
            }

        }

       if (currentHp == 0) animator.SetBool("death",true);
        
    }

    void FixedUpdate() {

        if (IsRun)
            Jump();
            //cc.AddForce(new Vector2(0f,JumpForce));
        didJump = false;
        //if (IsRun)
          //  cc.velocity = new Vector2(speed * Time.fixedDeltaTime,0);
    }

    void Jump() {


        Vector3 targetVelocity;
 
        if (didJump) cc.AddForce(new Vector2(speed * 20,JumpForce));
        if (didJump) targetVelocity = new Vector2(speed * 20, cc.velocity.y);
        else targetVelocity = new Vector2(speed/2, cc.velocity.y);
		// And then smoothing it out and applying it to the character
	    cc.velocity = Vector3.SmoothDamp(cc.velocity, targetVelocity, ref m_Velocity, 0.5f);

    }

    void ShootLaser() {

        //Random rnd = new Random();
        Instantiate(laser, position.position, Quaternion.identity);




    }

    public void LoseHP() {
        currentHp -= 1;

        Transform Hp = GameObject.Find("Image").transform;
        Hp.localScale = new Vector3(Hp.localScale.x * currentHp / hp, Hp.localScale.y, Hp.localScale.z);

    }

    public void EnemyDestroy() {
        Destroy(gameObject);
    }



}
