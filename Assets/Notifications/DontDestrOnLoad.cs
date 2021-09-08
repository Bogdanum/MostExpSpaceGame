using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestrOnLoad : MonoBehaviour
{ 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
