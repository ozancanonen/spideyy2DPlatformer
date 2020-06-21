using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEnder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //in the future this will check which NPC player is talking with.
        if(collision.gameObject.CompareTag("Player") )
        {
            if(!Web_Projectile.canWeb) { return; }
            Ombie.npcEnder = true;
        }
    }
}
