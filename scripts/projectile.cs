using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public float projectileLifeTime;
    public float projectileForce;
    public float damage;

    Rigidbody2D rb;
    Animator anim;

	// Use this for initialization
	void Start () {
		if (projectileLifeTime <= 0) {
            projectileLifeTime = 2.0f;
        }

        if (projectileForce <= 0) {
            projectileForce = 5.0f;
        }

        rb = GetComponent<Rigidbody2D>();
        if (!rb) {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        anim = GetComponent<Animator>();
        if (!anim) {
            anim = gameObject.AddComponent<Animator>();
        }

        Destroy(gameObject, projectileLifeTime);
        //StartCoroutine(FizzleOut());

        //setting what the gravity, mass, and force the projectile will have
        rb.gravityScale = 0;
        rb.mass = 0.1f;
        rb.AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
	}

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Enemy") {
            c.gameObject.GetComponent<enemy_grunt>().DamageTaken(damage);
            anim.SetTrigger("Hit");
            rb.velocity = Vector2.zero;
        }

        StartCoroutine(Destruction());
        
    }

    IEnumerator Destruction() {
        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);
    }

    /*IEnumerator FizzleOut() {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject, projectileLifeTime);
    }*/
}
