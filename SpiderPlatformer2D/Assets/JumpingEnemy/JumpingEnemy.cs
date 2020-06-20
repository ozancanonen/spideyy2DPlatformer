using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] float jumpForce;
    [SerializeField] float rightJumpForce;
    Collider2D collider2D;
    Animator animator;
    public float jumpTime = 2f;
    private float timeCount = 0;
    private int multiplier = 1;
    private void Awake()
    {
        animator =GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Jump()
    {
        var randomNumber = Random.Range(0, 2);
        switch (randomNumber)
        {
            case 0:
                multiplier = 1;
                break;
            case 1:
                multiplier = -1;
                break;
        }

        Vector3 forceDirection = Vector3.up * jumpForce;
        forceDirection += rightJumpForce * multiplier*Vector3.right;
        rigidbody.AddForce(forceDirection);
    }

    private void Update()
    {
        if(collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            timeCount += Time.deltaTime;
            if(timeCount>=jumpTime)
            {
                Debug.Log("Sa");
                animator.SetTrigger("Jump");
                timeCount = 0;
            }
        }
    }
}
