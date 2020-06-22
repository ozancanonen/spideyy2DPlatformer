using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    [SerializeField] GameObject[] Images;
    [SerializeField] Text tutorialText;
    [SerializeField] GameObject tutorialCanvas;

    public void SetImages(int number, string text)
    {
        tutorialText.text = text;
        for (int i = 0; i < Images.Length; i++)
        {
            if (i == number)
            {
                Images[i].SetActive(true);
            }
            else
            {
                Images[i].SetActive(false);
            }
        }
    }
    public void SetCanvasDeactive()
    {
        tutorialCanvas.gameObject.SetActive(false);
    }
}
