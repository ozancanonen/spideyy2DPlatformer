using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ombie : MonoBehaviour, INPC
{
    public int playersCame { get; set; } = 0;
    bool playerCompleted = false;
    public static bool npcEnder;
    public static int childCount = 0;
    public int cameraIndex = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(npcEnder == true) { return; }
        playersCame++;
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(cameraChangeAfter());
            GetComponent<DialogueTrigger>().TriggerDialogue(playerCompleted, playersCame);

        }
    }

    IEnumerator cameraChangeAfter()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefsController.Instance.ChangeCameraIndex(cameraIndex);
        //oldVirtualCamera.SetActive(false);
        //newVirtualCamera.SetActive(true);
    }
    public void QuestCompleted()
    {
        playerCompleted = true;
        Web_Projectile.canWeb = true;
    }
    
   
}
