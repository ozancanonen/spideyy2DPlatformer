using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornKütle : MonoBehaviour
{
    [SerializeField] GameObject damageparticle;
    [SerializeField] GameObject kütleDestroyByRockParticle;
    [SerializeField] Transform kütleDestroyPos;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DamageParticles(col);
            Debug.Log(gameObject.name);
            col.gameObject.GetComponent<PlayerController>().UpdateHealth(10);
        }

        if (col.gameObject.name == "Warmling")
        {
            col.gameObject.GetComponent<WormlingHealth>().GetDamage();
        }
        if (col.gameObject.tag == "Pullable")
        {
            if (col.gameObject.GetComponent<BeeEnemy>() != null)
            {
                col.gameObject.GetComponent<BeeEnemy>().Die();
                DamageParticles(col);
            }
            if (col.gameObject.GetComponent<ifStone>() != null)
            {
                //Debug.Log(col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
                //if (col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.001f)
                //{
                    GameObject particle = Instantiate(kütleDestroyByRockParticle, kütleDestroyPos.position, Quaternion.identity);
                    particle.SetActive(true);
                    Destroy(particle, 3f);
                    Destroy(gameObject);
                
            }
        }
        if (col.gameObject.tag == "Grappable")
        {
            if (col.gameObject.GetComponent<EnemyPatrolling>() != null)
            {
                col.gameObject.GetComponent<EnemyPatrolling>().Die();
                DamageParticles(col);
            }
        }
    }
    void DamageParticles(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        GameObject particle = Instantiate(damageparticle, contact.point, Quaternion.identity);
        Destroy(particle, 0.7f);
    }
}
