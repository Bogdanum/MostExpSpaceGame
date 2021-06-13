using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAsterEmmiterScript : MonoBehaviour
{
    public GameObject BigAster;
    public GameObject BigAsterV2;

    public float minDelay, maxDelay; // задержка между запусками
    float nextLaunchTime;
    public bool x = true;

    void Update()
    {
        if (GameController.instance.isStarted == false)
        {
            return;  // останов эммитера, если игра окончена
        }
       
        if (Time.time > nextLaunchTime)
        {
            x = !x;
            // запуск астероидов
            float PosY = transform.position.y;
            float PosZ = transform.position.z;
            float PosX = transform.position.x;

            if (x == true)
            {
                Instantiate(BigAster, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
            }
            else if (x == false)
            {
                Instantiate(BigAsterV2, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
            }

            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
