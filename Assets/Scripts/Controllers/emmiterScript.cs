using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emmiterScript : MonoBehaviour
{
    [Header ("Game objects")]
    public GameObject Asteroid;
    public GameObject RelBonus;
    public GameObject DeathSphere;
    public GameObject emmiter;
    bool create = false;
    [Header ("Asteroids")]
    public float minDelay, maxDelay;
    float nextLaunchTime; 
    [Header ("Bonus")]
    public float minDelayBonus, maxDelayBonus;
    public float minDelayDeath, maxDelayDeath;
    float nextLaunchTimeBonus;
    float nextLaunchTimeDeath;
    [Header ("Difficulty")]
    float difficulty = 1.0f;

    private void Start()
    {

        if (GameController.instance.diff == 0)
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
