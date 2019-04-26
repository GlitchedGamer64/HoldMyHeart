using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatScript : MonoBehaviour {
    //variables for attacking
    public Transform meleePos;
    public LayerMask whatIsEnemies;
    public float meleeRange;
    public int damage;

    //how long to charge up attacks
    private float chargeTime;
    private float startChargeTime;

    //initializing projectile spawn
    public projectile projectilePrefab;
    public Transform projectileSpawnPoint;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        if (!anim) {
            anim = gameObject.AddComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update () {
        //melee attack
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Carter used thunder punch!");
            Collider2D[] damageRadius = Physics2D.OverlapCircleAll(meleePos.position, meleeRange, whatIsEnemies);
            for (int i = 0; i < damageRadius.Length; i++) {
                damageRadius[i].GetComponent<enemy_grunt>().DamageTaken(damage);
            }
            anim.SetTrigger("Melee");
            StartCoroutine(WaitForRecover());
        }

        //ranged attack
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log("Charge up attack");
            chargeTime += Time.deltaTime;
            anim.SetBool("ifHold", true);
        }
        if (Input.GetButtonUp("Fire2")) {
            Debug.Log("Fire!");
            anim.SetBool("ifHold", false);
            anim.SetTrigger("Release");
            StartCoroutine(WaitForAnim());
            chargeTime = startChargeTime;
        }
        
    }

    /*private void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.tag == "Enemy") {
            c.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * meleeRange, ForceMode2D.Impulse);
        }
    }*/

    private void RangedAttack() {
        Debug.Log("Fired an electric projectile");

        if (projectilePrefab && projectileSpawnPoint) {
            projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            if (chargeTime < 2) {
                p.damage = 5.0f;
            }
            else {
                p.damage = 10.0f;
            }
        }
    }

    public void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(meleePos.position, meleeRange);
    }

    IEnumerator WaitForAnim() {
        yield return new WaitForSeconds(0.4f);
        RangedAttack();
    }

    IEnumerator WaitForRecover() {
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("Recover");
    }
}
