using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private SaveSystem saveSystem;

    private void Start()
    {
        saveSystem = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            saveSystem.lastCheckPointPos = transform.position;
            Debug.Log(saveSystem.lastCheckPointPos);
        }
    }
}
