using UnityEngine;

public class Cam1 : MonoBehaviour
{
    private Transform currentPlayerShip;
    public Transform target;
    public float smooth = 5.0f;
    public Vector3 offset = new Vector3(0, 2, -5);

    public void SetCurrPlayerShip(Transform ship) {
        currentPlayerShip = ship;
    }

    void Start()
    {
        RefreshOffset();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);       
    }

    void LateUpdate()
    {
        if (NullPlayerShip()) return;

        transform.position = currentPlayerShip.position + offset;
    }

    private void RefreshOffset()
    {
        if (NullPlayerShip()) return;
        offset = transform.position - currentPlayerShip.position;
    }

    private bool NullPlayerShip() => currentPlayerShip == null;
}