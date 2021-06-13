using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emmiterScript : MonoBehaviour
{
    [Header ("Game objects")]
    public GameObject Asteroid;  // берем модель астероида
    public GameObject RelBonus; // берем модель бонуса перезрядки
    public GameObject DeathSphere; // моделька сферы смерти
    public GameObject emmiter;
    [Header ("Ads")]
    private UnityAdsHelper _actionTarget;
    bool create = false;
    [Header ("Asteroids")]
    public float minDelay, maxDelay;  // задержка между запусками астероидов
    float nextLaunchTime; // время следующего запуска астероида
    [Header ("Bonus")]
    public float minDelayBonus, maxDelayBonus; // задержка между запусками бонусов
    public float minDelayDeath, maxDelayDeath;
    float nextLaunchTimeBonus; // время следующего запуска бонуса
    float nextLaunchTimeDeath;
    [Header ("Difficulty")]
    float difficulty = 1.0f; // дефолтная сложность

    private void Start()
    {
        _actionTarget = emmiter.GetComponent<UnityAdsHelper>();

        if (GameController.instance.diff == 0)     // меняем сложность в зависимости от выбора игрока
        {
            difficulty = 0.8f;
        } else if (GameController.instance.diff == 1)
        {
            difficulty = 1.0f;
        } else if (GameController.instance.diff == 2)
        {
            difficulty = 1.15f;
        }
    }

    void Update()
    {
        // балансные правки
        if (GameController.instance.score >= 10000 && GameController.instance.score < 100000)
        {
            minDelayBonus = 30;
            maxDelayBonus = 60;
            minDelayDeath = 20;
            maxDelayDeath = 40;
        }
        if (GameController.instance.score >= 100000)
        {
            minDelayBonus = 60;
            maxDelayBonus = 120;
            minDelayDeath = 30;
            maxDelayDeath = 50;
        }

        if (GameController.instance.isStarted == false)
        {
            if (!create)
            {
                if (GameController.instance.score > 1)
                {
                    _actionTarget.ShowVideoAd(); // запуск рекламы
                }
                create = true;
            }
                // останов эммитера
                return;
        }


        if (Time.time > nextLaunchTime)
        {
            // с каждым запуском астероида повышаем сложность
            if (difficulty > 2.5f)
            {
                difficulty += 0.001f;
            }
            else
            {
                difficulty += 0.007f;
            }
            // запуск астероидов
            float PosY = 0;
            float PosZ = transform.position.z;
            float PosX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            Instantiate(Asteroid, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
                    
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay) / difficulty;
        }

        if (Time.time > nextLaunchTimeBonus)
        {
            if (GameController.instance.score < 4000)
            {
                return;
            }
            else
            {
                // запуск бонусов
                float PosY = 0;
                float PosZ = transform.position.z;
                float PosX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                float PosXX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                float PosXXX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

                Instantiate(RelBonus, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
                if (GameController.instance.diff == 1)
                {
                   Instantiate(DeathSphere, new Vector3(PosXX, PosY, PosZ), Quaternion.identity);
                }
                if (GameController.instance.diff == 2)
                {
                    Instantiate(DeathSphere, new Vector3(PosXX, PosY, PosZ), Quaternion.identity);
                    Instantiate(DeathSphere, new Vector3(PosXXX, PosY, PosZ), Quaternion.identity);
                }
                nextLaunchTimeBonus = Time.time + Random.Range(minDelayBonus, maxDelayBonus);
            }
        }

        if (Time.time > nextLaunchTimeDeath)
        {
            if(GameController.instance.score < 4000)
            {
                return;
            }
            else
            {
                // запуск сфер смерти
                float PosY = 0;
                float PosZ = transform.position.z;
                float PosXX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                float PosXXX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
                if (GameController.instance.diff == 1)
                {
                    Instantiate(DeathSphere, new Vector3(PosXX, PosY, PosZ), Quaternion.identity);
                }
                if (GameController.instance.diff == 2)
                {
                    Instantiate(DeathSphere, new Vector3(PosXX, PosY, PosZ), Quaternion.identity);
                    Instantiate(DeathSphere, new Vector3(PosXXX, PosY, PosZ), Quaternion.identity);
                }
                nextLaunchTimeDeath = Time.time + Random.Range(minDelayDeath, maxDelayDeath);
            }
        }

    }
}
