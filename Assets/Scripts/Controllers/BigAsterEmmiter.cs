using UnityEngine;

public class BigAsterEmmiter : MonoBehaviour
{
    public  GameObject BigAster;
    public  GameObject BigAsterV2;
    public  float minDelay, maxDelay;
    private float nextLaunchTime;
    private bool asterTypeTrigger = true;

    void Update()
    {
        if (GameController.instance.isStarted == false)
        {
            return;
        }
        ReleaseAsteroid();
    }

    private void ReleaseAsteroid()
    {
        if (Time.time > nextLaunchTime)
        {
            asterTypeTrigger = !asterTypeTrigger;
            float PosY = transform.position.y;
            float PosZ = transform.position.z;
            float PosX = transform.position.x;

            if (asterTypeTrigger)
            {
                Instantiate(BigAster, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
            }
            else if (!asterTypeTrigger)
            {
                Instantiate(BigAsterV2, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
            }
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
