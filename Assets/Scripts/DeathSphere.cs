using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSphere : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    public GameObject playerExpl;

    void Start()
    {
        if (GameController.instance.score > 4000)
        {
            var DeathSphere = GetComponent<Rigidbody>();
            var speed = Random.Range(minSpeed, maxSpeed);
            DeathSphere.velocity = new Vector3(0, 0, -speed);
        }
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
            GameController.instance.isStarted = false;
            GameController.instance.RestartText.text = "Tap to restart";
            GameController.instance.Restart = true;
            Destroy(other.gameObject); // уничтожаем корабль
        }
        Destroy(gameObject);
    }
}
