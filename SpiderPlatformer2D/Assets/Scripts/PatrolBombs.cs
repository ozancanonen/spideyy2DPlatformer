using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBombs : MonoBehaviour
{
    [SerializeField] GameObject[] patrolBombs;
    [SerializeField] float randomYMin = 30;
    [SerializeField] float randomYMax = 120;
    [SerializeField] float randomXMin = 30;
    [SerializeField] float randomXMax = 120;
    [SerializeField] float explodeTime = 1.5f;
   
    public void InstantiateBombs()
    {

        for (int i = 0; i < patrolBombs.Length; i++)
        {
            Debug.Log("working");
            var bomb = Instantiate(patrolBombs[i], transform.position, Quaternion.identity);
            bomb.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(randomXMin, randomXMax), Random.Range(randomYMin, randomYMax), 0));
            bomb.GetComponent<Explode>().ExplodeBombs(explodeTime);
        }
    }


}
