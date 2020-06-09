using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] GameObject explodeParticle;
    [SerializeField] float explodeRadius = 2f;
    [SerializeField] LayerMask whatToHit;
    [SerializeField] int explodeDamage = 20;
    public void ExplodeBombs(float time)
    {
        StartCoroutine(ExplodeAfterDelay(time));
    }

    IEnumerator ExplodeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ExplodeBomb();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);

    }
    public void ExplodeBomb()
    {
        var particle = Instantiate(explodeParticle, transform.position, Quaternion.identity);
        Destroy(particle, 1f);
        var hitCol = Physics2D.OverlapCircle(transform.position, explodeRadius, whatToHit);
        if (hitCol != null)
        {
            hitCol.GetComponent<PlayerController>().UpdateHealth(explodeDamage);
        }
        Destroy(gameObject);
    }
}
