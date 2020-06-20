using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Kamikaze : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float speed;
    public float nextWaypointDistance;
    public float maxChaseRange;
    public float attackRate;
    [SerializeField] private GameObject beeAttackParticle;
    [SerializeField] private Animator anim;
    [SerializeField] Transform explosionSpawnPos;


    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isChasing = false;
    bool isDead = false;
    bool canAttackDirectly = true;
    bool isAttackReady;
    float attackRateValue;
    float xScaleValue;
    [Header("Kamikaze")]
    [SerializeField] LayerMask layerMask;
    [SerializeField] float kamikazeRange = 0.2f;
    [SerializeField] float bombArea = 1f;
    [SerializeField] float kamikazeDamage = 10f;
    [SerializeField] float forceEffect = 10f;
    [SerializeField] GameObject bombParticle;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        xScaleValue = transform.localScale.x;
        attackRateValue = attackRate;

    }
    void UpdatePath()
    {
        if (seeker.IsDone() && isChasing && !isDead)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= kamikazeRange+3)
        {
            GetComponent<Animator>().SetTrigger("Explode");
        }
        if (distance < maxChaseRange && !isDead)
        {
            isChasing = true;
        }
        if (isChasing && !isDead)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 directionOfPlayer = ((Vector2)target.transform.position - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            float distanceBetweenWaypoints = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distanceBetweenWaypoints < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            if (rb.velocity.magnitude > 0.2)
            {
                if (Mathf.Sign(directionOfPlayer.x) > 0)
                {
                    transform.localScale = new Vector3(xScaleValue, transform.localScale.y, 1);
                }
                else if (Mathf.Sign(directionOfPlayer.x) < 0)
                {
                    transform.localScale = new Vector3(-xScaleValue, transform.localScale.y, 1);
                }
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, bombArea);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "WebBullet")
        {
            GetComponent<Animator>().SetTrigger("Explode");
        }

    }
    //private void OnCollisionStay2D(Collision2D col)
    //{

    //    if (col.gameObject.tag == "Player" && !isDead)
    //    {
    //        attackRateValue -= Time.deltaTime;
    //        if (attackRateValue <= 0)
    //        {
    //            DamagePlayer(col);
    //            attackRateValue = attackRate;
    //        }
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    StartCoroutine(AttackExitDelay(0.5f));
    //}

    //IEnumerator AttackExitDelay(float waitTime)
    //{
    //    yield return new WaitForSeconds(waitTime);
    //    canAttackDirectly = true;
    //}
    //void DamagePlayer(Collision2D col)
    //    {
    //    anim.SetTrigger("Attack");
    //    GameObject particle = Instantiate(beeAttackParticle, col.transform.position, Quaternion.identity);
    //        Destroy(particle, 0.7f);
    //        col.gameObject.GetComponent<PlayerController>().UpdateHealth(10);
    //    }

    public void OnNearbyEvent()
    {
        var playerCollider = Physics2D.OverlapCircle(transform.position, bombArea, layerMask);
        if (playerCollider != null)
        {
            Vector3 direction = playerCollider.transform.position - transform.position;
            playerCollider.transform.GetComponent<PlayerController>().UpdateHealth(kamikazeDamage);
            playerCollider.transform.GetComponent<Rigidbody2D>().AddForce(direction * forceEffect);

        }
        GameObject particle = Instantiate(bombParticle, explosionSpawnPos.position, Quaternion.identity);
        Destroy(particle, 0.7f);
    }
    public void Die()
    {

        Destroy(gameObject);
    }

}
