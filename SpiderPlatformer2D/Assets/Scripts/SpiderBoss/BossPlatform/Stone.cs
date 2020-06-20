using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    public LayerMask whatToHit;
    [SerializeField] float damage = 30;
    [SerializeField] GameObject particle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = Physics2D.OverlapCircle(transform.position, radius, whatToHit);
        if (col != null)
        {
            col.transform.GetComponent<PlayerController>().UpdateHealth(damage);
        }
        var newParticle = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(newParticle,3f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
