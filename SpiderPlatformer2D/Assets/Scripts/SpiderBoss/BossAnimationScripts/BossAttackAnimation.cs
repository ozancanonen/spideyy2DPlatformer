using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackAnimation : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        animator.GetComponent<Boss>().LookAtPlayer();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
        rb.velocity = Vector3.zero;
    }
}
