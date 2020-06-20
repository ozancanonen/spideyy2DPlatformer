using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public int hitPoints = 3;
    [SerializeField] ParticleSystem destroyParticle;
    [SerializeField] ParticleSystem hitParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WebBullet")
        {
            hitPoints--;
            switch (hitPoints)
            {
                case 2:
                    //anim.SetBool("damage1",true);
                    //hitParticle.Play();
                    break;
                case 1:
                    //anim.SetBool("damage2",true);
                    //hitParticle.Play();
                    break;
                case 0:
                    //anim.SetBool("damage3",true);
                    //particle.Play();
                    //Destroy(gameObject, 0.7f);
                    break;
            }
        }
    }
}
