using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private static SaveSystem instance;
    public Vector3 lastCheckPointPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log(instance);
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
