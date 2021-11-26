using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LaserShot : MonoBehaviour
{
    public float speed;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
    }
}
