using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public GameObject player;
    [HideInInspector] public bool grappling;
    [HideInInspector] public bool isFlipped = false;
    bool isInVulnearable = false;
    [SerializeField] Animator wallAnim;
    public Slider bossHealthSlider;
    public float bossHealth;
    public int attackDamage;
    public float chargeForce;
    public int enragedAttackDamage = 40;
    private float maxBossHealth;
    public GameObject bossAttackParticle;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    Animator animator;
    [Header("Poison Process")]
    [SerializeField] GameObject bullet;
    [SerializeField] float poisonSmokeSpeed;
    [SerializeField] Transform shootPoint;
    [SerializeField] float bombThrowForce;
    [SerializeField] int bombCount = 3;
    [SerializeField] GameObject bomb;


    float speedValue;
    [SerializeField] GameObject defaultCamera;
    [SerializeField] GameObject bossCamera;
    private void Start()
    {
        maxBossHealth = bossHealth;
        bossHealthSlider.maxValue = bossHealth;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            GameObject bulletInstance = Instantiate(bossAttackParticle, player.transform.position, Quaternion.identity);
            colInfo.GetComponent<PlayerController>().UpdateHealth(attackDamage);
        }
    }

    public void ChargeAttack() //Calling from Animation Event
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerController>().UpdateHealth(enragedAttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    public Vector3 BossPos()
    {
        return transform.position;
    }
    public void getDamage(float damage)
    {
        if (isInVulnearable || bossHealth < 0) { return; }
        if (bossHealth > 0)
        {
            bossHealth -= damage;
            bossHealthSlider.value = bossHealth;
        }
        //if(bossHealth<=maxBossHealth/2)
        //{
        //    this.gameObject.GetComponent<Animator>().SetBool("isEnrage", true);
        //}
        else
        {
            //boss dead animation sounds etc.
            animator.SetTrigger("Die");
            wallAnim.SetBool("isClosed", false);
            defaultCamera.SetActive(true);
            bossCamera.SetActive(false);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;
            bossHealthSlider.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "WebBullet")
        {
            getDamage(10);
            StartCoroutine(slowFor(2));
        }
        if (col.gameObject.tag == "Player")
        {
            animator.SetTrigger("Attack");
        }
    }
    IEnumerator slowFor(float time)
    {
        speedValue = BossRunAnimation.speed;
        if (speedValue <= 9) { yield break; }
        BossRunAnimation.speed = speedValue - 5;
        yield return new WaitForSeconds(time);
        BossRunAnimation.speed = speedValue;
    }

    public void PoisonEvent() // Calling from Animation EVENT
    {
        Vector3 difference = player.transform.position - shootPoint.position;
        float angleZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shootPoint.rotation = Quaternion.Euler(0, 0, angleZ);
        GameObject poisonSmoke = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        poisonSmoke.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * poisonSmokeSpeed);
    }

    public void ChargeEvent()
    {
        Vector2 directionPLayer = player.transform.position -transform.position;
        directionPLayer.y = 0;
        GetComponent<Rigidbody2D>().AddForce(directionPLayer.normalized * chargeForce);
        animator.GetComponent<Boss>().LookAtPlayer();
    }

    public void BombEvent()
    {

        Vector3 difference = player.transform.position - shootPoint.position;
        float angleZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        for (int i =0; i<bombCount;i++)
        {

            shootPoint.rotation = Quaternion.Euler(0, 0, angleZ+Random.Range(-10,10));
            var newBomb = Instantiate(bomb, shootPoint.transform.position, Quaternion.identity);
            newBomb.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * bombThrowForce);
            newBomb.GetComponent<Explode>().ExplodeBombs(2);
        }
    }
}