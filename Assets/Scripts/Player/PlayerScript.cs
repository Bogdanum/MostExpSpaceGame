using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    Rigidbody ship;
    [Header ("Controls")]
    public SimpleTouchPad touchPad;
    [Range(1, 100)] public float speed;
    [Range(1, 100)] public float tilt;
    [Range(-100, 100)] public float xMin, xMax, zMin, zMax;
    [Header ("Shots")]
    public GameObject lazerShot;
    public GameObject lazerShotGreen;
    public GameObject lazerShotYellow;
    [SerializeField] private Transform[] laserGuns;
    float nextShotTime;
    public static float shotDelay = 0.5f;

    public static PlayerScript instance;

    void Start()
    {
        ship = GetComponent<Rigidbody>();

        if (GameController.instance.score > 10000 && GameController.instance.score < 100000)
        {
            shotDelay = 0.55f;
        }
        if (GameController.instance.score >= 100000)
        {
            shotDelay = 0.7f;
        }
    }

    void FixedUpdate()
    {
        if (!GameController.instance.isStarted)
        {
            return;
        }

        if (GameController.instance.ControlType)
        {
            SwipeMove();
        } else if (!GameController.instance.ControlType)
        {
            ButtonsMove();
        }

        if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time > nextShotTime) {

            Shot();
            nextShotTime = Time.time + shotDelay;
        }

    }

    private void ButtonsMove()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
        var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

        ship.rotation = Quaternion.Euler(moveVertical * tilt, 0, -moveHorizontal * tilt);
    }

    private void SwipeMove()
    {
        Vector2 direction = touchPad.GetDirection();

        ship.velocity = new Vector3(direction.x, 0f, direction.y) * speed;

        var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
        var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

        ship.rotation = Quaternion.Euler(direction.y * tilt, 0f, -direction.x * tilt);
    }

    private void Shot()
    {
        if (tag == "Player")
        {
            Instantiate(lazerShot, laserGuns[0].position, Quaternion.identity);
            Instantiate(lazerShot, laserGuns[1].position, Quaternion.identity);
        }

        if (tag == "SpaceFighter2")
        {
            Instantiate(lazerShotGreen, laserGuns[0].position, Quaternion.identity);
            Instantiate(lazerShotGreen, laserGuns[1].position, Quaternion.identity);
            Instantiate(lazerShotGreen, laserGuns[2].position, Quaternion.identity);
            Instantiate(lazerShotGreen, laserGuns[3].position, Quaternion.identity);
        }

        if (tag == "SpaceFighter3")
        {
            Instantiate(lazerShotYellow, laserGuns[0].position, Quaternion.identity);
            Instantiate(lazerShotYellow, laserGuns[1].position, Quaternion.identity);
            Instantiate(lazerShotYellow, laserGuns[2].position, Quaternion.identity);
            Instantiate(lazerShotYellow, laserGuns[3].position, Quaternion.identity);
            Instantiate(lazerShotYellow, laserGuns[4].position, Quaternion.identity);
            Instantiate(lazerShotYellow, laserGuns[5].position, Quaternion.identity);
        }
    }
}
