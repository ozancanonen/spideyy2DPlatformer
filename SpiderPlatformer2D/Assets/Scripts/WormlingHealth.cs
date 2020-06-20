using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormlingHealth : MonoBehaviour
{
    public float health = 20f;
    private float warmlingStartingHealth;
    public float damagePoint = 2f;
    [SerializeField] GameObject healthSlider;
    private void Start()
    {
        warmlingStartingHealth = health;
        healthSlider.GetComponent<Slider>().maxValue = warmlingStartingHealth;
    }

    public void GetDamage()
    {
        health -= damagePoint;
        SetSlider(health);
        if (health <= 0)
        {
            Debug.Log("Wormling öldü");
        }
    }

    public void RestoreHealth()
    {
        health = warmlingStartingHealth;
        SetSlider(health);
    }

    private void SetSlider(float health)
    {
        healthSlider.GetComponent<Slider>().value = health;
    }
}
