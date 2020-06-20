using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    Animator animator;
    PlayerController player;
    private int rightForceMultiplier = 1;
    public float jumpForce = 300f;
    public float rightForce = 300f;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask whatToHit;
    public Transform particlePos;
    [SerializeField] GameObject particle;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                rightForceMultiplier = 1;
            }
            else
            {
                rightForceMultiplier = -1;
            }
            animator.SetTrigger("playerJump");
        }
        
    }

    public void ForcePlayer()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, whatToHit);
        if (colInfo != null)
        {
            var newParticle = Instantiate(particle, particlePos.position, Quaternion.identity);
            Destroy(newParticle, 0.7f);
            //GameObject bulletInstance = Instantiate(bossAttackParticle, player.transform.position, Quaternion.identity);
            //colInfo.GetComponent<PlayerController>().UpdateHealth(attackDamage);
            Vector3 forceDirection = Vector3.up * jumpForce;
            forceDirection += Vector3.right * rightForceMultiplier*rightForce;
            player.GetComponent<Rigidbody2D>().AddForce(forceDirection);
        }
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
