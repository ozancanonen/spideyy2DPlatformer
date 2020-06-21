using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntParticleBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints;
    private int currentIndex = 0;
    public float firstWayPointSpeed = 5;
    public float secondWayPointSpeed = 5;
    // Update is called once per frame
    void Update()
    {
        if (currentIndex < wayPoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentIndex].transform.position, Time.deltaTime * firstWayPointSpeed);
            if (transform.position == wayPoints[currentIndex].position)
            {
                currentIndex++;
                firstWayPointSpeed = secondWayPointSpeed;
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
