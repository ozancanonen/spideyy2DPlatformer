using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIndexChanger : MonoBehaviour
{
    public int cameraIndexChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefsController.Instance.ChangeCameraIndex(cameraIndexChanger);
        }
    }
}
