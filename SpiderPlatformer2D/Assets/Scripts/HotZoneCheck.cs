using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private Enemy_behaviour enemyParent;
    private bool inRange;
    [SerializeField] Animator anim;
    [SerializeField] float newSpeed ;
    private float previousSpeed;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour>();
        newSpeed = enemyParent.moveSpeed + 4;
        previousSpeed = enemyParent.moveSpeed;
    }
    private void Update()
    {
        if (!enemyParent.isDead&&inRange /*anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack")*/)
        {
            enemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemyParent.isDead && collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
            enemyParent.moveSpeed = newSpeed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!enemyParent.isDead && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ant çıktı");
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
            enemyParent.moveSpeed = previousSpeed;
        }
    }
}
