using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject oldCameraBorder;
    public GameObject newCameraBorder;
    public GameObject fallPlatformParticle;
    public GameObject tempColliderObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            tempColliderObject.SetActive(false);
            fallPlatformParticle.SetActive(true);
        }
    }
}
