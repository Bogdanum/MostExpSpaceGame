using UnityEngine;

public class PlayerShipSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] SpaceFighters;
    [SerializeField] private Cam1 playerCamera;
    [SerializeField] private SimpleTouchPad touchPad;
    private Transform baseTransform;

    private void Awake()
    {
        baseTransform = GetComponent<Transform>();
    }

    public void SpawnPlayerShip(int level)
    {
        Clear();

        GameObject spaceFighterPrefab = SpaceFighters[level - 1];
        GameObject spaceFighter = Instantiate(spaceFighterPrefab, baseTransform);
        InitCamera(spaceFighter.transform);
        InitTouchPad(spaceFighter);
    }

    private void Clear()
    {
        foreach (Transform ship in baseTransform)
            Destroy(ship.gameObject);
    }

    private void InitCamera(Transform spaceFighter) {
        playerCamera.SetCurrPlayerShip(spaceFighter);
    }

    private void InitTouchPad(GameObject spaceFighter) {
        MovementSystem ms = spaceFighter.GetComponent<MovementSystem>();
        ms.SetTouchPad(touchPad);
    }

    public GameObject GetCurrentShip() {
        int childIntex = baseTransform.childCount > 1 ? 1 : 0;
        return baseTransform.GetChild(childIntex).gameObject;
    }
}
