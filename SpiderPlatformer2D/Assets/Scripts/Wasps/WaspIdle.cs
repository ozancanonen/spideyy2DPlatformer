using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIdle : StateMachineBehaviour
{
    private int random;
    private WaspBoss waspBoss;
    public int ChaseMax;
    public int ChargeMax;
    public int Max;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waspBoss = animator.GetComponent<WaspBoss>();
        random = Random.Range(0, Max);

        if (random < ChaseMax)
        {
            animator.SetTrigger("Chase");
            waspBoss.isChasing = true;
            waspBoss.isCharging = false; 
        }

        else if (random >= ChaseMax && random < ChargeMax)
        {
            animator.SetTrigger("Charge");
            waspBoss.isCharging = true;
            waspBoss.isChasing = false;
        }

        else if (random >= ChargeMax)
        {
            animator.SetTrigger("Projectile");
            waspBoss.isChasing = false;
            waspBoss.isCharging = false;
        }
    }

}
