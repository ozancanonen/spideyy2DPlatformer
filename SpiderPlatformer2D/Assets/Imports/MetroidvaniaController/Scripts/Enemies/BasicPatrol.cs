using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicPatrol : MonoBehaviour
{
    #region Public Variables
    public float moveSpeed;
    public Transform leftLimit;
    public Transform rightLimit;
    public Animator anim;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool isDead;
    public Collider2D dieCollider;
    public Collider2D bodyCollider;
    BoxCollider2D boxCollider2D;
    #endregion

    #region Private Variables

    private float distance; //Store the distance b/w enemy and player
    private Rigidbody2D rb;
    #endregion

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        SelectTarget();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;

        Move();
        if (!InsideOfLimits())
        {
            SelectTarget();
        }


    }

    void Move()
    {
        //anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        //Ternary Operator
        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {

            rotation.y = 0;
        }

        //Ternary Operator
        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
    public void Die()
    {
        isDead = true;
        dieCollider.enabled = false;
        bodyCollider.enabled = false;
        anim.SetTrigger("getSquashed");
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;   
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!isDead && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().UpdateHealth(10);
        }

        if(collision.gameObject.CompareTag("WebBullet"))
        {
            Die();
        }
    }
}
