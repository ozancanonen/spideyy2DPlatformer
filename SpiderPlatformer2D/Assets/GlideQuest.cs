using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideQuest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Glide.canGlide = true;
        }
    }
}
