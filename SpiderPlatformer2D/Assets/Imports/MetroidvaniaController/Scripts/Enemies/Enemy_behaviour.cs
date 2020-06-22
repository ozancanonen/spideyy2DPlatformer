using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    public Animator anim;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if Player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    [HideInInspector] public bool isDead;
    public Collider2D dieCollider;
    public Collider2D bodyCollider;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public GameObject attackParticle;
    public float attackDamage;
    public AudioSource dieAudio;
    #endregion

    #region Private Variables

    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;

    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    private Rigidbody2D rb;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead) return;
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }

       

        if (inRange)
        {
            EnemyLogic();
        }
    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {

            StopAttack();

        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
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

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        Debug.Log("Girmesi lazım");
        cooling = true;
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
    public void AttackAnimationEvent()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        //Play("SpiderBossMelee");
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            GameObject bulletInstance = Instantiate(attackParticle, pos, Quaternion.identity);
            colInfo.GetComponent<PlayerController>().UpdateHealth(attackDamage);
        }
    }
    public void Die()
    {
        if(isDead) { return; }
        isDead = true;
        anim.SetTrigger("getSquashed");
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        dieCollider.enabled = false;
        bodyCollider.enabled = false;
        dieAudio.Play();
        var explodingant = GetComponent<ExplodingAnt>();
        if(explodingant!=null)
        {
            explodingant.InstantiateObjects();
        }
        Destroy(gameObject, 2);
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
