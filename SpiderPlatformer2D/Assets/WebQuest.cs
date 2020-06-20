using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebQuest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Web_Projectile.canWeb = true;
        }
    }
}
