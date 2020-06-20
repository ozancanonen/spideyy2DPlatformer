using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossIdle : StateMachineBehaviour
{
    public delegate void DestroySomeStuff();
    public static event DestroySomeStuff DestroyBossBoxesInTheScene;
    public delegate void DestroyWeb();
    public static event DestroyWeb DestroyBossWebs;

    public delegate void CreateStones();
    public static event CreateStones CreateAllStones;
    PlayerController playerController;


    private int random;
    private Boss spiderBoss;
    public int ChaseMax;
    public int ChargeMax;
    public int PoisonMax;
    public int BombMax;
    public int Max;
    public int yDif;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (DestroyBossWebs != null)
        {
            DestroyBossWebs();
        }
        if (DestroyBossBoxesInTheScene != null)
        {
            DestroyBossBoxesInTheScene();
        }
        if (playerController != null)
        {
            var randomNum = Random.Range(0, 10);
            if (playerController.isTouchingPlatforms && randomNum <= 2)
            {
                //CreateAllStones();
                animator.SetTrigger("HittingGround");
                return;
            }
        }
        bool playerIsJumping = playerController.transform.position.y > animator.transform.position.y+yDif;
        if (playerIsJumping)
        {
            var randomNumber = Random.Range(0, 3);
            switch (randomNumber)
            {
                case 0:
                    animator.SetTrigger("Poison");
                    break;
                case 1:
                    animator.SetTrigger("Bomb");
                    break;
                case 2:
                    //CreateAllStones();
                    animator.SetTrigger("HittingGround");
                    break;
            }
            return;
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
