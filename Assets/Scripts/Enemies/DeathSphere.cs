using UnityEngine;

public class DeathSphere : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    public GameObject playerExpl;

    void Start()
    {
        var DeathSphere = GetComponent<Rigidbody>();
        var speed = Random.Range(minSpeed, maxSpeed);
        DeathSphere.velocity = new Vector3(0, 0, -speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "GameBoundary" || other.tag == "RelBonus" || other.tag == "Lazer" || other.tag == "DeathSphere" || other.tag == "GameBoundaryLazer")
        {
            return;
        }

        if (other.tag == "Player" || other.tag == "SpaceFighter2" || other.tag == "SpaceFighter3")
        {
            Instantiate(playerExpl, other.transform.position, Quaternion.identity);
            GameController.instance.GameOver();
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
