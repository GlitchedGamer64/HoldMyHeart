using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_juliet : MonoBehaviour {
    
    //initializing important variables and game components!
    private float playerSpeed = 25;
    private float jumpHeight = 40;
    private float moveVelocity;
    public bool faceRight;

    //declarations for the canvas components
    public Canvas Pause;
    public Canvas Focus;

    public Transform checkGround;
    public float checkGroundRadius;

    public LayerMask whatIsGround;
    private bool isGrounded;

    Rigidbody2D rb;
    Animator anim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Pause.enabled = false;
        Focus.enabled = false;
	}

    private void FixedUpdate() {
        //checks if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, whatIsGround);
        anim.SetBool("ifGround", isGrounded);
    }

    // Update is called once per frame
    void Update () {
        //player movement
        moveVelocity = playerSpeed * Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

        //player animation
        if (moveVelocity > 0 || moveVelocity < 0) {
            anim.SetBool("ifWalk", true);
        }
        else {
            anim.SetBool("ifWalk", false);
        }

        //flipping the sprite
        if (moveVelocity > 0 && faceRight) {
            Flip();
        }
        else if (moveVelocity < 0 && !faceRight) {
            Flip();
        }

        //how to jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        if (Input.GetButtonDown("Cancel")) {
            Debug.Log("Game Paused.");
            Paused();
        }

        if (Input.GetButtonDown("Fire3")) {
            Debug.Log("focus is on.");
            FocusOn();
        }

    }

    //declaring the player flipping
    void Flip() {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D c) {
        //if player collides into the ai player, then the player ignores the ai's collider
        if (c.gameObject.tag == "AI") {
            Physics2D.IgnoreCollision(c.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (c.gameObject.tag == "Transport") {
            SceneManager.LoadScene("test");
        }
    }

    void Paused() {
        Pause.enabled = !Pause.enabled;
        if (Pause.enabled) {
            Time.timeScale = 0;
            GameObject.Find("bgAudio").GetComponent<AudioSource>().Pause();
        } else {
            Time.timeScale = 1;
            GameObject.Find("bgAudio").GetComponent<AudioSource>().UnPause();
        }
    }

    void FocusOn() {
        Focus.enabled = !Focus.enabled;
        if (Focus.enabled) {
            playerSpeed = 0;
            jumpHeight = 0;
        } else {
            playerSpeed = 25;
            jumpHeight = 40;
        }
    }
}
