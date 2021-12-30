using UnityEngine;

public class GodSphere : MonoBehaviour
{
    public float tilt;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * tilt);
    }
}
