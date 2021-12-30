using System.Collections.Generic;
using UnityEngine;

public class BigAsterEmmiter : MonoBehaviour
{
    [SerializeField] private List<GameObject> bigAsteroidsVariants;
    [SerializeField] private float minDelay, maxDelay;
    private float nextLaunchTime;
    private int variantID;

    void Update()
    {
        if (!GameController.IsStarted)
        {
            return;
        }
        ReleaseAsteroid();
    }

    private void ReleaseAsteroid()
    {
        if (Time.time > nextLaunchTime)
        {
            variantID = Random.Range(0, bigAsteroidsVariants.Count);

            float PosY = transform.position.y;
            float PosZ = transform.position.z;
            float PosX = transform.position.x;

            Instantiate(bigAsteroidsVariants[variantID], new Vector3(PosX, PosY, PosZ), Quaternion.identity);

            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
