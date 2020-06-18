﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Listener : MonoBehaviour
{
    [SerializeField] GameObject stone;
    private void Awake()
    {
        SpiderBossIdle.CreateAllStones += DropStones;
    }
    public void DropStones()
    {
        foreach(Transform child in transform)
        {
            Instantiate(stone, child.transform.position, Quaternion.identity);
        }
    }
}