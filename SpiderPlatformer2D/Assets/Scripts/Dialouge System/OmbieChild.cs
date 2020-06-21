using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmbieChild : MonoBehaviour
{
    [SerializeField] BoxCollider2D interactCol;
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Animator wormlingAnim;
    bool isTouched = false;
    bool isHided;

    private void Start()
    {
        Ombie.childCount = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHided&& collision.gameObject.tag == "Player")
        {
            wormlingAnim.SetTrigger("Hide");
            isHided = true;
        }
            if (isTouched) { return; }
        if (collision.gameObject.tag == "InterObject")
        {
            isTouched = true;
            Ombie.childCount++;
            wormlingAnim.SetTrigger("Idle");
            Debug.Log("Working, number of child returned="+ Ombie.childCount);

            interactCol.enabled = false;
            StartCoroutine(StopRigidBodyAfter(3f));
            if (Ombie.childCount == 2)
            {
                FindObjectOfType<Ombie>().QuestCompleted();
            }
        }
    }
    IEnumerator StopRigidBodyAfter(float time)
    {
        //rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        rigidbody.gravityScale = 5;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Grapple>().ReleaseGrapple();
        yield return new WaitForSeconds(time);
        rigidbody.velocity = Vector3.zero;
        rigidbody.bodyType = RigidbodyType2D.Static;
        //GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
