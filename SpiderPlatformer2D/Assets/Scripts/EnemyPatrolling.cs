using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed = 10f;
    BoxCollider2D myFeet;
    [SerializeField] GameObject hitParticle;
    [SerializeField] Animator patrolAntAnim;
    [SerializeField] BoxCollider2D dieCollider;
    [SerializeField] BoxCollider2D bodyCollider;
    bool isDead;
    void Start()
    {
        myFeet = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector3(speed, 0, 0);

    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead&&collision.gameObject.tag == "Grappable" || collision.gameObject.tag == "Pullable")
            SetSpeed();
        if (!isDead && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().UpdateHealth(10);
            ContactPoint2D contact = collision.contacts[0];
            var particle = Instantiate(hitParticle, contact.point, Quaternion.identity);
            Destroy(particle, 1f);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        SetSpeed();
    }
  
    private void SetSpeed()
    {
        speed = speed * -1;
        rigidbody.velocity = new Vector3(speed, 0, 0);
        FlipSprite();
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    public void Die()
    {
        GetComponentInChildren<PatrolBombs>().InstantiateBombs();
        isDead = true;
        patrolAntAnim.SetTrigger("getSquashed");
        rigidbody.velocity = Vector3.zero;
        rigidbody.gravityScale = 0;
        dieCollider.enabled = false;
        bodyCollider.enabled = false;
        Destroy(gameObject,2);
    }
   
}
