using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    
    Rigidbody ship;
    [Header ("Controls")]
    public SimpleTouchPad touchPad;
    public float speed;
    public float tilt;
    public float xMin, xMax, zMin, zMax;
    [Header ("Shots")]
    public GameObject lazerShot;
    public GameObject lazerShotGreen;
    public GameObject lazerShotYellow;
    public Transform lazerGun;
    public Transform lazerGun2;
    public Transform lazerGun3;
    public Transform lazerGun4;
    public Transform lazerGun5;
    public Transform lazerGun6;
    float nextShotTime; // время до след. выстрела
    public static float shotDelay = 0.5f; // задержка между выстрелами

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

    void Update()
    {

        if (GameController.instance.isStarted == false)
        {
            return;
        }

        if (GameController.instance.ControlType == true)
        {
            Vector2 direction = touchPad.GetDirection();

            ship.velocity = new Vector3(direction.x, 0f, direction.y) * speed;

            var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
            var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

            ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

            ship.rotation = Quaternion.Euler(direction.y * tilt, 0f, -direction.x * tilt);
        } else if (GameController.instance.ControlType == false)
        {
            var moveHorizontal = Input.GetAxis("Horizontal");           
            var moveVertical = Input.GetAxis("Vertical");
            
            ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

            var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
            var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

            ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

            ship.rotation = Quaternion.Euler(moveVertical * tilt, 0, -moveHorizontal * tilt);
        }

        if ((Input.GetButton("Fire1") || Input.GetButton("Jump")) && Time.time > nextShotTime) {

            if (tag == "Player")
            {
                Instantiate(lazerShot, lazerGun.position, Quaternion.identity);
                Instantiate(lazerShot, lazerGun2.position, Quaternion.identity);
            }

            if (tag == "SpaceFighter2") {
                Instantiate(lazerShotGreen, lazerGun.position, Quaternion.identity);
                Instantiate(lazerShotGreen, lazerGun2.position, Quaternion.identity);
                Instantiate(lazerShotGreen, lazerGun3.position, Quaternion.identity);
                Instantiate(lazerShotGreen, lazerGun4.position, Quaternion.identity);
            }

            if (tag == "SpaceFighter3")
            {
                Instantiate(lazerShotYellow, lazerGun.position, Quaternion.identity);
                Instantiate(lazerShotYellow, lazerGun2.position, Quaternion.identity);
                Instantiate(lazerShotYellow, lazerGun3.position, Quaternion.identity);
                Instantiate(lazerShotYellow, lazerGun4.position, Quaternion.identity);
                Instantiate(lazerShotYellow, lazerGun5.position, Quaternion.identity);
                Instantiate(lazerShotYellow, lazerGun6.position, Quaternion.identity);
            }

            nextShotTime = Time.time + shotDelay;
        }

        
    }
}
