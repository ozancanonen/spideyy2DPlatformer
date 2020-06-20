using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    SaveSystem saveSystem;

    void Start()
    {
        saveSystem = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>();
        transform.position = saveSystem.lastCheckPointPos;
    }
}

  
