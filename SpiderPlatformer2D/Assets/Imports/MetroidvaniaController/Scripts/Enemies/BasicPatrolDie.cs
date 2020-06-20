using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrolDie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<BasicPatrol>().Die();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1200));
        }
       
    }
}
