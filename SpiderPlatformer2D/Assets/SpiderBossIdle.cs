using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossIdle : StateMachineBehaviour
{
    public delegate void DestroySomeStuff();
    public static event DestroySomeStuff DestroyBossBoxesInTheScene;
    public delegate void DestroyWeb();
    public static event DestroyWeb DestroyBossWebs;


    private int random;
    private Boss spiderBoss;
    public int ChaseMax;
    public int ChargeMax;
    public int PoisonMax;
    public int BombMax;
    public int Max;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (DestroyBossWebs != null)
        {
            DestroyBossWebs();
        }
        if (DestroyBossBoxesInTheScene != null)
        {
            DestroyBossBoxesInTheScene();
        }

        spiderBoss = animator.GetComponent<Boss>();
        random = Random.Range(0, Max);

        if (random < ChaseMax)
        {
            animator.SetTrigger("Chase");

        }

        else if (random >= ChaseMax && random < ChargeMax)
        {
            animator.SetTrigger("Charge");

        }
        else if (random >= ChargeMax && random < PoisonMax )
        {
            animator.SetTrigger("Poison");

        }
        else if (random >= PoisonMax && random < BombMax)
        {
            animator.SetTrigger("Bomb");

        }

        else if (random >= BombMax)
        {
            animator.SetTrigger("GrappleAttack");
            spiderBoss.grappling = true;
            
        }
    }

}
