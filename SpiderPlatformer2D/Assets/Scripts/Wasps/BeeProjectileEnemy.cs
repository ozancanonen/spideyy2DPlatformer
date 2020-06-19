using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeeProjectileEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float speed;
    public float escapeSpeed;
    public float nextWaypointDistance;
    public float maxChaseRange;
    public float minShootRange;
    public float maxShootRange;
    public float attackRate;
    public float bulletSpeed;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource dieAudio;

    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool isChasing = false;
    bool isDead = false;
    float attackRateValue;
    float xScaleValue;
    float speedValueHolder;
    Vector3 escapePos;
    Vector3 targetDestination;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetDestination = target.position;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        xScaleValue = transform.localScale.x;
        speedValueHolder = speed;

    }
    void UpdatePath()
    {
        if (seeker.IsDone() && isChasing && !isDead)
            seeker.StartPath(rb.position, targetDestination, OnPathComplete);
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
        attackRateValue -= Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > maxShootRange && distance < maxChaseRange && !isDead)
        {

            isChasing = true;
            speed = speedValueHolder;
            targetDestination = target.position;

        }
        else if (distance > minShootRange && distance < maxShootRange && !isDead)
        {

            ShootTimer();
            return;
        }
        else if (distance < minShootRange&&!isDead)
        {
            escapePos = transform.position +transform.position - target.position;
            targetDestination = escapePos;
            speed = escapeSpeed;
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
            Vector2 directionOfPlayer = ((Vector2)target.position - rb.position).normalized;
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
        Gizmos.DrawWireSphere(transform.position, minShootRange);
        Gizmos.DrawWireSphere(transform.position, maxShootRange);
        Gizmos.DrawWireSphere(transform.position, maxChaseRange);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "WebBullet" && !isDead)
        {
            Die();
        }

    }

    private void ShootTimer()
    {
        if (!isDead)
        {
            
            if (attackRateValue <= 0)
            {
                anim.SetTrigger("ProjectileShoot");
                attackRateValue = attackRate;
            }
        }
    }

    public void Shoot()
    {
        Vector3 difference = target.position - shootPoint.position;
        float angleZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(0, 0, angleZ);
        Vector3 bulletFaceDirection=shootPoint.eulerAngles + new Vector3(0, 0, -90);
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position,Quaternion.Euler( bulletFaceDirection));
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bulletSpeed);
    }
    public void Die()
    {
        isDead = true;
        dieAudio.Play();
        anim.SetTrigger("Die");
        Destroy(gameObject, 1.1f);
        rb.gravityScale = 2;
    }

}
