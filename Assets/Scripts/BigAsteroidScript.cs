using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsteroidScript : MonoBehaviour
{
    public float rotation;
    public float speed;
    float scale;
    
    void Start()
    {
        var BigAsteroid = GetComponent<Rigidbody>();
        BigAsteroid.angularVelocity = Random.insideUnitSphere;
        BigAsteroid.velocity = new Vector3(0, 0, -speed);
        scale = Random.Range(1, 2);
        BigAsteroid.transform.localScale *= scale;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "GameBoundary")
        {
            return;
        }
    }
}
