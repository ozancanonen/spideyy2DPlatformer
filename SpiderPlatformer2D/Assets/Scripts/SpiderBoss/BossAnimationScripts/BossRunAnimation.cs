
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRunAnimation : StateMachineBehaviour
{
	public delegate void DestroySomeStuff();
	public static event DestroySomeStuff DestroyBossBoxesInTheScene;
	public delegate void DestroyWeb();
    public static event DestroyWeb DestroyBossWebs;
    public float speedValue;

	public static float speed;
	Transform player;
	Rigidbody2D rb;
	Boss boss;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        speed = speedValue;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		boss.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

	
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state

}