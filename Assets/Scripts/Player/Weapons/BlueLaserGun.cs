using UnityEngine;

public class BlueLaserGun : MonoBehaviour, ILaserGun
{
    [SerializeField] private GameObject LaserShot;
    private float nextShotTime;

    public float shotDelay
    {
        get => PlayerPrefs.GetFloat("BlueLaserGunDelay", .5f);
        set => PlayerPrefs.SetFloat("BlueLaserGunDelay", value);
    }

    public void Shot()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(LaserShot, transform.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
    }
}
