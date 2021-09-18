using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestrOnLoad : MonoBehaviour
{
    private static DontDestrOnLoad instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        
    }
}
