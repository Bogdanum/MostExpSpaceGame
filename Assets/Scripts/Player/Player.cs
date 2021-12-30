using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private WeaponSystem weaponSystem;

    void FixedUpdate()
    {
        if (!GameController.IsStarted) return;

        MoveShip(GameController.ControlType);
        CheckInputs();
    }

    private void MoveShip(bool swipeInputActive)
    {
        if (swipeInputActive)
            movementSystem.SwipeMove();
        else
            movementSystem.ButtonsMove();
    }

    private void CheckInputs()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Jump"))
            LaserGunsShot();
    }

    private void LaserGunsShot()
    {
        weaponSystem.LaserGunsShot();
    }

    public ILaserGun GetLaserGun()
    {
        return weaponSystem.GetLaserGun();
    }
}
