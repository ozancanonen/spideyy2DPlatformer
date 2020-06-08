﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

	public GameObject player;
    [HideInInspector]public bool grappling;
    [HideInInspector] public bool isFlipped = false;
    bool isInVulnearable = false;
    [SerializeField] Animator wallAnim;
    public Slider bossHealthSlider;
    public float bossHealth;
    public int attackDamage;
    public int enragedAttackDamage = 40;
    private float maxBossHealth;
    public GameObject bossAttackParticle;
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    float speedValue;
    [SerializeField] GameObject defaultCamera;
    [SerializeField] GameObject bossCamera;
    private void Start()
    {
        speedValue = BossRunAnimation.speed;
        maxBossHealth = bossHealth;
        bossHealthSlider.maxValue = bossHealth;
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

    public void EnragedAttack()
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
    public  Vector3 BossPos()
    {
        return transform.position;
    }
    public void getDamage(float damage)
    {
        if(isInVulnearable|| bossHealth < 0) { return; }
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
            GetComponent<Animator>().SetTrigger("Die");
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
    }
    IEnumerator slowFor(float time)
    {
        BossRunAnimation.speed = speedValue - 5;
        yield return new WaitForSeconds(time);
        BossRunAnimation.speed = speedValue;
    }
}