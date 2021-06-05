using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBLazerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
        }
    }
}
