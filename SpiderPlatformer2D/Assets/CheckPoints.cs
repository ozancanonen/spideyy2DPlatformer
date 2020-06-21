using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player prefs");
            PlayerPrefsController.Instance.SavePosition(transform.position.x, transform.position.y);
            var wormlingHealth = FindObjectsOfType<WormlingHealth>();
            if(wormlingHealth!=null)
            {
                foreach(WormlingHealth wormling in wormlingHealth)
                {
                    wormling.RestoreHealth();
                }
            }
            collision.gameObject.GetComponent<PlayerController>().GetHealed();
            
        }
    }
}
