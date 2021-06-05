using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidScript : MonoBehaviour
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
            // увеличиваем/уменьшаем значение hp в зависимости от сложности
            if (GameController.instance.diff == 0)
            {
                hp = 2;
            } else if (GameController.instance.diff == 1)
            {
                hp = 4;
            } else if (GameController.instance.diff == 2)
            {
                hp = 6;
            }
        }
    }
    // вызывается при столкновении с др коллайдером
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
            if (GameController.instance.inv == 2 || GameController.instance.inv2 ==2)
            {
                return;
            } 
            if (GameController.instance.inv == 3 || GameController.instance.inv == 1 || GameController.instance.inv2 == 3 || GameController.instance.inv2 == 1)
            {
                // при попадании астероида в корабль игрока, проигрываем анимацию взрыва игрока, завершаем спаун астероидов
                Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
                GameController.instance.score -= (int)(10 * scale);
                GameController.instance.isStarted = false;
                GameController.instance.RestartText.text = "Tap to restart";
                GameController.instance.Restart = true;
                other.gameObject.SetActive(false);  // убираем со сцены корабль
            }  
         
           
        }
        
        // увеличить количество очков
        GameController.instance.score += (int)(10 * scale);
        GameController.instance.BestScore();
        if (hp > 1)
        {
            hp -= 1;        // при попадании в большой астероид снизить хп на 1
        }
        else
        {
            Destroy(gameObject); // уничтожаем астероид, если его хп меньше 1 или его размер меньше 1.5f
            GameController.instance.DestrAster();
        }      
      Destroy(other.gameObject);  // уничтожаем то, с чем столкнулся

    }

}
