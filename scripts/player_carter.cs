using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_carter : MonoBehaviour {

    //initializing important variables and game components
    private float playerSpeed = 25;
    private float jumpHeight = 40;
    private float moveVelocity;
    public bool faceRight;
    private float maxHealth = 20;
    public float currentHealth;

    //declarations for canvas components
    public Canvas Pause;
    public Canvas GameEnd;
    public Canvas HUD;

    //health bar
    public Image healthBar;

    //this is to see whether the character is alive during gameplay
    bool ifLife;

    //variables to check for a ground
    public Transform checkGround;
    public float checkGroundRadius;

    public LayerMask whatIsGround;
    private bool isGrounded;

    //animation variable instantiation
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start () {
        ifLife = true;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //setting player's health
        currentHealth = maxHealth;

        //enabling/disabling UI
        Pause.enabled = false;
        GameEnd.enabled = false;
        HUD.enabled = true;
    }

    private void FixedUpdate() {
        //checks if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, whatIsGround);
        anim.SetBool("ifGround", isGrounded);
    }
	
	// Update is called once per frame
	void Update () {
        if (ifLife) {
            //displays the healthbar with the player's currrentHealth
            healthBar.fillAmount = currentHealth / maxHealth;

            //how to jump
            if (Input.GetButton("Jump") && isGrounded) {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }

            //player animation
            if (moveVelocity > 0.0f || moveVelocity < 0) {
                anim.SetBool("ifWalk", true);
            } else {
                anim.SetBool("ifWalk", false);
            }

            //player movement
            moveVelocity = playerSpeed * Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);

            //flipping the sprite
            if (moveVelocity > 0.0f && faceRight) {
                Flip();
            } else if (moveVelocity < 0.0f && !faceRight) {
                Flip();
            }

            //Player Input to enable PauseMenu
            if (Input.GetButtonDown("Cancel")) {
                Debug.Log("Game Paused.");
                Paused();
            }

            //If Player's Health reaches 0
            if (currentHealth <= 0) {
                GameOver();
            }

        } else {
            GameOver();
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
            SceneManager.LoadScene("test_2");
        }
    }

    public void DamageTaken(float damagePlayer) {
        currentHealth -= damagePlayer;
        Debug.Log("Carter loses " + damagePlayer + " health");
    }

    //Pauses Game
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

    //Game Over
    void GameOver() {
        Debug.Log("Game Over.");
        ifLife = false;
        Debug.Log("Game Over");
        HUD.enabled = false;
        GameEnd.enabled = true;
        GameObject.Find("bgAudio").GetComponent<AudioSource>().Stop();
        Time.timeScale = 0;
    }
}
