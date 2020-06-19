using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAnimation : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float chargeForce;
    private Vector2 direction;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        Vector2 target = new Vector2(player.position.x, rb.velocity.y);
        direction = target - (Vector2)animator.transform.position;
        direction.y = 0;
        rb.AddForce(direction.normalized * chargeForce);
        animator.GetComponent<Boss>().LookAtPlayer();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        direction.x = -direction.x;
        rb.AddForce(direction.normalized * chargeForce/2);
    }
}
