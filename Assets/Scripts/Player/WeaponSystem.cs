using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] laserGunsObjects;
    [SerializeField] private bool hasShieldSystem = false;
    [SerializeField, Range(1, 30)] private float shieldTime = 8;
    private Transform shieldSphere;

    private List<ILaserGun> laserGuns;

    private void Awake()
    {
        InitCollections();
        if (hasShieldSystem)
            shieldSphere = GetComponent<Transform>().Find("Sphere");
    }

    private void InitCollections()
    {
        laserGuns = new List<ILaserGun>(laserGunsObjects.Length);
        foreach (GameObject gun in laserGunsObjects)
            laserGuns.Add(gun.GetComponent<ILaserGun>());
    }

    public void LaserGunsShot()
    {
        foreach (ILaserGun gun in laserGuns)
            gun.Shot();
    }

    public ILaserGun GetLaserGun()
    {
        return laserGuns[0];
    }

    public void ActivateShield() {
        shieldSphere.GetChild(0).gameObject.SetActive(true);
    }

    public void DisableShield() {
        shieldSphere.GetChild(0).gameObject.SetActive(false);
    }

    public float GetShieldTime() {
        return shieldTime;
    }

}
