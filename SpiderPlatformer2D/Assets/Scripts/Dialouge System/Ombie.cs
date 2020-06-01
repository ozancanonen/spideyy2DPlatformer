﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ombie : MonoBehaviour, INPC
{
    public int playersCame { get; set; } = 0;
    bool playerCompleted = false;
    public static int childCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playersCame++;
        if (collision.gameObject.tag == "Player")
            GetComponent<DialogueTrigger>().TriggerDialogue(playerCompleted, playersCame);
    }

    public void QuestCompleted()
    {
        playerCompleted = true;
    }
    
   
}
