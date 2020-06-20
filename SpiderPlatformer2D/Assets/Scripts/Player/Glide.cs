using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : MonoBehaviour
{
    bool glideGravity;
    float defaultGravity;
    public float glidingGravity;
    public float glideTime;
    public GameObject glideTrail;
    private Rigidbody2D rb;
    public static bool canGlide = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
        glideTrail.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        if(!canGlide) { return; }

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y < -3f && glideGravity == false)
        {
            glideGravity = true;
            glideTrail.GetComponent<ParticleSystem>().Play();
            
            GetComponent<Rigidbody2D>().gravityScale = defaultGravity * glidingGravity;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (glideGravity)
            {
                glideTrail.GetComponent<ParticleSystem>().Stop();
                glideGravity = false;
                GetComponent<Rigidbody2D>().gravityScale = defaultGravity;
            }
        }

    }

}



