using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GayzerSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem gayzerParticle;
    [SerializeField] float timeBetweenGayzer = 4f;
    void Start()
    {
        InvokeRepeating("PlayGayzer", 4f, timeBetweenGayzer);
    }

    private void PlayGayzer()
    {
        gayzerParticle.Play();
    }
}
