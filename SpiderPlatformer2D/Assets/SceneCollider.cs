using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneCollider : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] float timeDelayForNewScene = 0.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefsController.cameraChanged = false;
            PlayerPrefsController.bornInStartingPos = false;
            Ombie.npcEnder = false;
            StartCoroutine(DelayAndLoadScene(timeDelayForNewScene));
        }
    }

    IEnumerator DelayAndLoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }


}
