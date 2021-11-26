using UnityEngine;

public class GodSphere : MonoBehaviour
{
    public float tilt;

    void Update()
    {
        transform.Rotate(Vector3.up * tilt);
    }
}
