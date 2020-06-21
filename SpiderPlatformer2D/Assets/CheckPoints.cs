
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] int currentCameraIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerPrefsController.cameraChanged = true;
            PlayerPrefsController.Instance.ChangeCameraIndex(currentCameraIndex);
            //PlayerPrefsController.Instance.ChangeCameraState();
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
