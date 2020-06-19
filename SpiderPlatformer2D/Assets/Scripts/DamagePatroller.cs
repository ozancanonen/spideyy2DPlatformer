using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePatroller : MonoBehaviour
{
    [SerializeField] Enemy_behaviour enemyPatrolling;
    //[SerializeField] PatrolBombs patrolBombs;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {

            //buralarda animasyon olucak lan mk particle marticle de olur.. lan amına koyim
            enemyPatrolling.Die();
            //patrolBombs.InstantiateBombs();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1200));
        }
    }
}
