using UnityEngine;

public class AsteroidEmmiter : MonoBehaviour
{
    [Header ("Game objects")]
    public GameObject Asteroid;
    public GameObject RelBonus;
    public GameObject DeathSphere;
    public GameObject emmiter;
    [Header("Asteroids")]
    public float minReleaseDelay;
    public float maxReleaseDelay;
    float nextLaunchTime;
    [Header("Bonus")]
    public float minDelayBonus;
    public float maxDelayBonus;
    float nextLaunchTimeBonus;
    [Header("Death Sphere")]
    public float minDelayDeath;
    public float maxDelayDeath;
    float nextLaunchTimeDeath;

    float difficulty = 1.0f;

    private void Start()
    {
        difficulty = GetDifficulty();
    }

    private float GetDifficulty()
    {
         switch (GameController.instance.difficulty)
        {
            case 0:  return .8f;
            case 1:  return 1.0f;
            case 2:  return 1.15f;
            default: return .8f;
        }
    }

    void Update()
    {
        if (GameController.instance.isStarted == false)
        {
            return;
        }

        CalculateSphereDelay();
        ReleaseAsteroid();

        ReleaseBonusSphere();
        ReleaseDeathSphere();
    }

    private void ReleaseAsteroid()
    {
        if (Time.time > nextLaunchTime)
        {
            if (difficulty > 2.5f)
            {
                difficulty += 0.001f;
            }
            else
            {
                difficulty += 0.007f;
            }

            float PosY = 0;
            float PosZ = transform.position.z;
            float PosX = GetRandomPosX();

            Instantiate(Asteroid, new Vector3(PosX, PosY, PosZ), Quaternion.identity);

            nextLaunchTime = Time.time + Random.Range(minReleaseDelay, maxReleaseDelay) / difficulty;
        }
    }

    private void ReleaseBonusSphere()
    {
        if (Time.time > nextLaunchTimeBonus)
        {
            if (GameController.instance.score < 4000)
            {
                return;
            }
            float PosY = 0;
            float PosZ = transform.position.z;
            float PosX = GetRandomPosX();

            Instantiate(RelBonus, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
            nextLaunchTimeBonus = Time.time + Random.Range(minDelayBonus, maxDelayBonus);
        }
    }

    private void ReleaseDeathSphere()
    {
        if (Time.time > nextLaunchTimeDeath)
        {
            if (GameController.instance.score < 4000)
            {
                return;
            }
            float PosY = 0;
            float PosZ = transform.position.z;

            if (GameController.instance.difficulty == 1)
            {
                Instantiate(DeathSphere, new Vector3(GetRandomPosX(), PosY, PosZ), Quaternion.identity);
            }
            if (GameController.instance.difficulty == 2)
            {
                Instantiate(DeathSphere, new Vector3(GetRandomPosX(), PosY, PosZ), Quaternion.identity);
                Instantiate(DeathSphere, new Vector3(GetRandomPosX(), PosY, PosZ), Quaternion.identity);
            }
            nextLaunchTimeDeath = Time.time + Random.Range(minDelayDeath, maxDelayDeath);
        }
    }

    private float GetRandomPosX()
    {
        return Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
    }

    private void CalculateSphereDelay()
    {
        if (GameController.instance.score >= 10000 && GameController.instance.score < 100000)
        {
            IncrementDelay();
        }
        if (GameController.instance.score >= 100000)
        {
            IncrementDelay();
        }
    }

    private void IncrementDelay()
    {
        minDelayBonus *= 2;
        maxDelayBonus *= 2;
        minDelayDeath *= 1.3f;
        maxDelayDeath += 10;
    }
}
