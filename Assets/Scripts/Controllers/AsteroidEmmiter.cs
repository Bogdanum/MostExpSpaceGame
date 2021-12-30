using UnityEngine;

public class AsteroidEmmiter : MonoBehaviour
{
    [SerializeField] private GameObject Asteroid,
                                        RelBonus,
                                        DeathSphere;
    [SerializeField, Range(1, 50)]
    private float minReleaseDelay,
                  maxReleaseDelay,
                  minDelayBonus,
                  maxDelayBonus,
                  minDelayDeath,
                  maxDelayDeath;
    float nextLaunchTime;
    float nextLaunchTimeBonus;
    float nextLaunchTimeDeath;
    float difficulty = 1.0f;

    private void Start()
    {
        difficulty = GetDifficultyCoeff();
    }

    private float GetDifficultyCoeff()
    {
         switch (GameController.Difficulty)
        {
            case 0:  return .8f;
            case 1:  return 1.0f;
            case 2:  return 1.15f;
            default: return .8f;
        }
    }

    void Update()
    {
        if (!GameController.IsStarted)
        {
            return;
        }

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

            SpawnSomethingRandomPos(Asteroid);

            nextLaunchTime = Time.time + Random.Range(minReleaseDelay, maxReleaseDelay) / difficulty;
        }
    }

    private void ReleaseBonusSphere()
    {
        if (Time.time > nextLaunchTimeBonus)
        {
            if (GameController.Score < 4000)
            {
                return;
            }

            SpawnSomethingRandomPos(RelBonus);
            nextLaunchTimeBonus = Time.time + Random.Range(minDelayBonus, maxDelayBonus);
        }
    }

    private void ReleaseDeathSphere()
    {
        if (Time.time > nextLaunchTimeDeath)
        {
            if (GameController.Score < 4000)
            {
                return;
            }

            for (int i = 0; i < GameController.Difficulty; i++)
                SpawnSomethingRandomPos(DeathSphere);
        
            nextLaunchTimeDeath = Time.time + Random.Range(minDelayDeath, maxDelayDeath);
        }
    }

    private void SpawnSomethingRandomPos(GameObject go) {
        float PosX = GetRandomPosX();
        float PosY = 0;
        float PosZ = transform.position.z;
        Instantiate(go, new Vector3(PosX, PosY, PosZ), Quaternion.identity);
    }

    private float GetRandomPosX()
    {
        return Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
    }

}
