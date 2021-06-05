using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundaryLazer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
        } else { return; }
    }
}
