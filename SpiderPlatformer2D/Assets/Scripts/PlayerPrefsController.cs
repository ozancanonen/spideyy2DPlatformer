using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public Vector2 startingPos;
    public static bool bornInStartingPos;

    private static PlayerPrefsController instance;
    public static PlayerPrefsController Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning("There is no PlayerPrefs in the hieararchy");
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if(!bornInStartingPos)
        {
            PlayerPrefs.SetFloat("PlayerXPos", startingPos.x);
            PlayerPrefs.SetFloat("PlayerYPos", startingPos.y);
            bornInStartingPos = true;
        }
       
    }

 
    public void SavePosition(float x, float y)
    {
        PlayerPrefs.SetFloat("PlayerXPos", x);
        PlayerPrefs.SetFloat("PlayerYPos", y);
    }

    public Vector2 LastCheckPoint()
    {
        return new Vector2(PlayerPrefs.GetFloat("PlayerXPos"), PlayerPrefs.GetFloat("PlayerYPos"));
    }
}
