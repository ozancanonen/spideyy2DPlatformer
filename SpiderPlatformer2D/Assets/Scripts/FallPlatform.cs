using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fallPlatformParticle;
    public GameObject tempColliderObject;
    private GameObject playerObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerObject = collision.gameObject;
            StartCoroutine(SetPlayerGrasvityLower());
            tempColliderObject.SetActive(false);
            fallPlatformParticle.SetActive(true);
        }
    }
    IEnumerator SetPlayerGrasvityLower()
    {
        float playerGravityScale = playerObject.GetComponent<Rigidbody2D>().gravityScale;
        playerObject.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
        yield return new WaitForSeconds(1f);
        playerObject.GetComponent<Rigidbody2D>().gravityScale = playerGravityScale;
    }
}
