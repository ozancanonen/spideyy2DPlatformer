using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public Vector2 startingPos;
    public static bool bornInStartingPos;
    public static bool hasTakenATutorial = false;
    public GameObject[] cameraArray;

    public static bool cameraChanged;
    public int startingCameraIndex;

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
        if (!bornInStartingPos)
        {
            PlayerPrefs.SetFloat("PlayerXPos", startingPos.x);
            PlayerPrefs.SetFloat("PlayerYPos", startingPos.y);
            bornInStartingPos = true;
        }

        if (!cameraChanged)
        {
            PlayerPrefs.SetInt("CameraIndex", startingCameraIndex);
            for (int i = 0; i < cameraArray.Length; i++)
            {
                if (startingCameraIndex == i)
                {
                    cameraArray[i].SetActive(true);
                }
                else
                {
                    cameraArray[i].SetActive(false);
                }
            }
        }
        else
        {
            ChangeCameraState();
        }

    }

    public void SetTutorialCanvas(int value)
    {
        PlayerPrefs.SetInt("TutorialIndex",value);
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

    public void ChangeCameraIndex(int index)
    {
        PlayerPrefs.SetInt("CameraIndex", index);
        ChangeCameraState();
    }

    private void ChangeCameraState()
    {
        for(int i =0;i<cameraArray.Length;i++)
        {
            if (i == PlayerPrefs.GetInt("CameraIndex"))
            {
                cameraArray[i].SetActive(true);
            }
            else
            {
                cameraArray[i].SetActive(false);
            }
        }
    }

}
