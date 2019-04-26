using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_grunt : MonoBehaviour {

    //declaring variables
    public float health;
    public float speed;
    public float stopDist;
    private float damagePlayer = 5;
    private Transform target;

    public Transform checkGround;
    public float checkGroundRadius;

    public LayerMask whatIsGround;
    private bool isGrounded;

    GameObject other;

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        //tells the enemy to follow the player, and to stop once it gets to a certain distance
        if (Vector2.Distance(transform.position, target.position) > stopDist) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate() {
        //checks if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(checkGround.position, checkGroundRadius, whatIsGround);
    }

    private void OnCollisionEnter2D(Collision2D c) {
        
        //if enemy collides into the ai player, then the enemy ignores the ai's collider
        if (c.gameObject.tag == "AI") {
            Physics2D.IgnoreCollision(c.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    
        if (c.gameObject.tag == "Player") {
            other = c.gameObject;
            StartCoroutine(WaitToAttack());
        }
    }

    public void DamageTaken(float damage) {
        health -= damage;
        Debug.Log("enemy loses " + damage + " health");
    }

    IEnumerator WaitToAttack() {
        yield return new WaitForSeconds(0.3f);
        other.GetComponent<player_carter>().DamageTaken(damagePlayer);
    }
}
