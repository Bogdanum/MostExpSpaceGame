using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotation;
    public float maxSpeed, minSpeed;
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    float scale;
    public int hp;

    void Start()
    {
        var Asteroid = GetComponent<Rigidbody>();
        Asteroid.angularVelocity = Random.insideUnitSphere * rotation;
        var speed = Random.Range(minSpeed, maxSpeed);
        Asteroid.velocity = new Vector3(0, 0, -speed);
        scale = Random.Range(0.5f, 2);
        Asteroid.transform.localScale *= scale;

        if (scale > 1.5f)
        {
            hp = GetInitialHP();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "GameBoundary" || other.tag == "RelBonus" || other.tag == "GameBoundaryLazer" || other.tag == "DeathSphere")
        {
            return;
        }
        var explosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= scale;

        if (other.tag == "Player" || other.tag == "SpaceFighter2" || other.tag == "SpaceFighter3")
        {
            if (GameController.instance.inv == 0)
            {
                return;
            } 
                Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
                GameController.instance.GameOver(scale);
                other.gameObject.SetActive(false); 
        }
        GameController.instance.IncScore(scale);
        if (hp > 1)
        {
            hp -= 1;
        }
        else
        {
            Destroy(gameObject);
            GameController.instance.DestrAster();
        }      
      Destroy(other.gameObject);
    }

    private int GetInitialHP()
    {
        switch (GameController.instance.difficulty)
        {
            case 0:  return 2;
            case 1:  return 4;
            case 2:  return 6;
            default: return 1;
        }
    }
}
