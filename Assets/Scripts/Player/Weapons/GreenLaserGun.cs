using UnityEngine;

public class GreenLaserGun : MonoBehaviour, ILaserGun
{
    [SerializeField] private GameObject LaserShot;
    private float nextShotTime;

    public float shotDelay {
        get => PlayerPrefs.GetFloat("GreenLaserGunDelay", .55f);
        set => PlayerPrefs.SetFloat("GreenLaserGunDelay", value);
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
