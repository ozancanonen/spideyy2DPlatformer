using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemyHealth : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Thorn>())
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().UpdateHealth(damage);
        }
    }
}
