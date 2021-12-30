using UnityEngine;

public class RedLaserGun : MonoBehaviour, ILaserGun
{
    [SerializeField] private GameObject LaserShot;
    private float nextShotTime;

    public float shotDelay
    {
        get => PlayerPrefs.GetFloat("RedLaserGunDelay", .7f);
        set => PlayerPrefs.SetFloat("RedLaserGunDelay", value);
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
