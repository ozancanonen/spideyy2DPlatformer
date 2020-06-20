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
    PlayerController pc;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
        glideTrail.GetComponent<ParticleSystem>().Stop();
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(!canGlide) { return; }

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y < -3f && glideGravity == false)
        {
            glideGravity = true;
            pc.audioManager.Play("Glide");
            glideTrail.GetComponent<ParticleSystem>().Play();
            
            GetComponent<Rigidbody2D>().gravityScale = defaultGravity * glidingGravity;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (glideGravity)
            {
                pc.audioManager.Pause("Glide");
                glideTrail.GetComponent<ParticleSystem>().Stop();
                glideGravity = false;
                GetComponent<Rigidbody2D>().gravityScale = defaultGravity;
            }
        }

    }

}



