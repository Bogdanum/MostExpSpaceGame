using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float rotation;
    [SerializeField] private float maxSpeed, minSpeed;
    [SerializeField] private GameObject asteroidExplosion,
                                        playerExplosion;
    [SerializeField, Header("TriggerCollider")]
                     private List<string> ignoreTags;
    [SerializeField] private string playerTag;
    private Rigidbody asteroid;
    private float scale;
    private int hp;

    void Start()
    {
        asteroid = GetComponent<Rigidbody>();
        SetAsteroidParameters();
    }

    private void SetAsteroidParameters()
    {
        asteroid.angularVelocity = Random.insideUnitSphere * rotation;
        var speed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -speed);
        scale = Random.Range(0.5f, 2);
        asteroid.transform.localScale *= scale;

        if (scale > 1.5f)
        {
            hp = GetInitialHP();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string ignoreTag in ignoreTags)
            if (other.tag == ignoreTag) return;
 
        var explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= scale;

        if (other.tag == playerTag)
        {
            if (GameController.Unbreakable)
            {
                return;
            } 
                Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                GameController.GameOver(scale);
        }

        GameController.Instance.IncScore(scale);

        if (hp > 1)
        {
            hp -= 1;
        }
        else
        {
            Destroy(gameObject);
            GameController.Instance.DestrAster();
        }      
      Destroy(other.gameObject);
    }

    private int GetInitialHP()
    {
        switch (GameController.Difficulty)
        {
            case 0:  return 2;
            case 1:  return 4;
            case 2:  return 6;
            default: return 1;
        }
    }
}
