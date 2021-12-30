using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BigAsteroid : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody bigAsteroid;
    float scale;
    
    void Start()
    {
        bigAsteroid = GetComponent<Rigidbody>();
        SetAsteroidParameters(); 
    }

    private void SetAsteroidParameters()
    {
        float rotation = Random.Range(1, 2);
        bigAsteroid.angularVelocity = Random.insideUnitSphere / rotation;
        bigAsteroid.velocity = new Vector3(0, 0, -speed);
        scale = Random.Range(1, 2);
        bigAsteroid.transform.localScale *= scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBoundary")
        {
            return;
        }
    }
}
