using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyHealth : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] Animator animator;
    bool isDead = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDead) { return; }
        if (collision.gameObject.GetComponent<Thorn>())
        {
            isDead = true;
            animator.SetTrigger("getSquashed");
            Destroy(gameObject,0.28f);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().UpdateHealth(damage);
        }
    }
}
