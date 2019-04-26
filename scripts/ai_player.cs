using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_player : MonoBehaviour {
    
    //important variable declartions
    private float speed = 25;
    private float stopDist = 15;
    //private bool moving;

    private Transform target;

    public Transform checkGround;
    public float checkGroundRadius;

    public LayerMask whatIsGround;
    private bool isGrounded;

    Animator anim;

    // Use this for initialization
    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        //checks if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, whatIsGround);
    }

    // Update is called once per frame
    void Update() {
        if (Vector2.Distance(transform.position, target.position) > stopDist) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //anim.SetFloat("walk", speed);
            anim.SetBool("ifWalk", true);
        } else {

            anim.SetBool("ifWalk", false);
        }





        //tells the ai that if it's at a greater distance than the declared stop distance, then ai follows the player
        /*if (moving == false) {
            if (Vector2.Distance(transform.position, target.position) > stopDist) {
                MoveTowardsPlayer();
                if (transform.position == target.transform.position) {
                    moving = false;
                }

            } else {

                anim.SetBool("ifWalk", false);
            }
        }*/
    }

    /*private void MoveTowardsPlayer() {
        moving = true;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        anim.SetBool("ifWalk", true);

    }*/
}
