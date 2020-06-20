using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Grapple.canGrapple = true;
        }
    }

}
