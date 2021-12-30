using UnityEngine;

[RequireComponent(typeof(Player), typeof(Rigidbody))]
public class MovementSystem : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float speed,
                  tilt;
    [SerializeField ,Range(-100, 100)]
    private float xMin, xMax, zMin, zMax;

    private SimpleTouchPad _touchPad;

    private Rigidbody ship;

    private void Awake()
    {
        ship = GetComponent<Rigidbody>();
    }

    public void SetTouchPad(SimpleTouchPad touchPad) {
        _touchPad = touchPad;
    }

    public void ButtonsMove()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

        var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
        var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

        ship.rotation = Quaternion.Euler(moveVertical * tilt, 0, -moveHorizontal * tilt);
    }

    public void SwipeMove()
    {
        Vector2 direction = _touchPad.GetDirection();

        ship.velocity = new Vector3(direction.x, 0f, direction.y) * speed;

        var clampedPositionX = Mathf.Clamp(ship.position.x, xMin, xMax);
        var clampedPositionZ = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(clampedPositionX, 0, clampedPositionZ);

        ship.rotation = Quaternion.Euler(direction.y * tilt, 0f, -direction.x * tilt);
    }
}
