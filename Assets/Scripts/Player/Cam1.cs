using UnityEngine;

public class Cam1 : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public Transform target;
    public float smooth = 5.0f;
    public Vector3 offset = new Vector3(0, 2, -5);

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
        if (player.activeSelf)
            transform.position = player.transform.position + offset;
        if (player2.activeSelf)
            transform.position = player2.transform.position + offset;
        if (player3.activeSelf)
            transform.position = player3.transform.position + offset;
    }

    private void RefreshOffset()
    {
        if (player.activeSelf)
           offset = transform.position - player.transform.position;
        if (player2.activeSelf)
           offset = transform.position - player2.transform.position;
        if (player3.activeSelf)
            offset = transform.position - player3.transform.position;
    }
}