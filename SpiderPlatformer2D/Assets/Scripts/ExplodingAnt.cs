using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingAnt : MonoBehaviour
{
    [SerializeField] GameObject explodeParticle;
    [SerializeField] float explodeRadius = 2f;
    [SerializeField] LayerMask whatToHit;
    [SerializeField] int explodeDamage = 20;
    [SerializeField] GameObject[] objectsToExplode;
    private int multiplier = 1;
    [SerializeField] float xForce;
    [SerializeField] float yForce;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);

    }
    public void InstantiateObjects()
    {
        foreach (GameObject objects in objectsToExplode)
        {
            var newObjects = Instantiate(objects, transform.position, Quaternion.identity);
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

            Vector3 forceDirection = transform.right * xForce * multiplier;
            forceDirection += Vector3.up * yForce;
            newObjects.GetComponent<Rigidbody2D>().AddForce(forceDirection);
            StartCoroutine(ExplodeObjects(newObjects));
        }
       
    }
    IEnumerator ExplodeObjects(GameObject explodeObject)
    {
        yield return new WaitForSeconds(1f);
        var hitCol = Physics2D.OverlapCircle(transform.position, explodeRadius, whatToHit);
        if (hitCol != null)
        {
            hitCol.GetComponent<PlayerController>().UpdateHealth(explodeDamage);
        }
        var newParticle = Instantiate(explodeParticle, explodeObject.transform.position, Quaternion.identity);
        AudioManager.Instance.Play("PatlamaSesi");
        Destroy(explodeObject);
        Destroy(newParticle, 1f);
       
        

    }
}
