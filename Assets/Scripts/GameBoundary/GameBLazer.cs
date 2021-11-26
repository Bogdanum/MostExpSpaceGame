using UnityEngine;

public class GameBLazer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
        }
    }
}
