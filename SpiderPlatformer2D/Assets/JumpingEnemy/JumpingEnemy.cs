using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [SerializeField] float rightJumpForce;
    Collider2D collider2D;
    Rigidbody2D rigidbody;
    Animator animator;
    private int multiplier = 1;
    private float startingXScale;
    private void Awake()
    {
        startingXScale = transform.localScale.x;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator.speed = Random.Range(0.8f, 1.2f);
    }
    public void Jump()
    {
        var randomNumber = Random.Range(0, 2);
        switch (randomNumber)
        {
            case 0:
                multiplier = 1;
                transform.localScale = new Vector2(startingXScale , transform.localScale.y);
                break;
            case 1:
                multiplier = -1;
                transform.localScale = new Vector2(-startingXScale, transform.localScale.y);
                break;
        }

        Vector3 forceDirection = transform.right * rightJumpForce * multiplier;
        rigidbody.AddForce(forceDirection);
    }
    public void SetSpeedToZeroEvent()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0;
    }
    
}
