using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ReloadBonus : MonoBehaviour
{
    public float minSpeed, maxSpeed;
    public GameObject BonusExpl;

    [SerializeField]
    private float startBonusEasyMode,
                  startBonusMediumMode,
                  startBonusHardMode;
   
    void Start()
    {
        var RelBonus = GetComponent<Rigidbody>();
        var speed = Random.Range(minSpeed, maxSpeed);
        RelBonus.velocity = new Vector3(0, 0, -speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid" || other.tag == "GameBoundary" || other.tag == "RelBonus" || other.tag == "Lazer")
        {
            return;
        } else
        if (other.tag == "Player")
        {
            Instantiate(BonusExpl, transform.position, Quaternion.identity);

            GameObject playerGameObj = other.gameObject;
            Player player = playerGameObj.GetComponent<Player>();
            UpgradeShotDelay(player, GameController.Difficulty);

            Destroy(gameObject);
        }

    }

    private void UpgradeShotDelay(Player player, int difficulty)
    {
        ILaserGun laserGun = player.GetLaserGun();

        switch (difficulty)
        {
            case 0:
                ApplyNewShotDelay(laserGun, startBonusEasyMode);
                break;
            case 1:
                ApplyNewShotDelay(laserGun, startBonusMediumMode);
                break;
            case 2:
                ApplyNewShotDelay(laserGun, startBonusHardMode);
                break;
            default: break;
        }
    }

    private void ApplyNewShotDelay(ILaserGun laserGun, float delay)
    {
        if (laserGun.shotDelay < .2f) return;
        else
            if (laserGun.shotDelay > .4f)
                laserGun.shotDelay -= delay;
        else
            laserGun.shotDelay -= delay / 2;
    }
}
