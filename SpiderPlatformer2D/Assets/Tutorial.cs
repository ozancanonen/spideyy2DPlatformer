using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialCanvas;
    [TextArea(10, 12)] public string textToShow;
    public int textOrder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("TutorialIndex") >= textOrder && PlayerPrefsController.hasTakenATutorial) { return; }
            PlayerPrefsController.hasTakenATutorial = true;
            PlayerPrefsController.Instance.SetTutorialCanvas(textOrder);
            tutorialCanvas.gameObject.SetActive(true);
            tutorialCanvas.GetComponentInChildren<SetImage>().SetImages(textOrder,textToShow);
        }
    }
}
