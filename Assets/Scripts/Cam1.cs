using UnityEngine;
using System.Collections;

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
        if (GameController.instance.score < 10000)
        {
            if (player == null) { return; }
            else
            { offset = transform.position - player.transform.position; }
        }
        else if ((GameController.instance.score > 10000) && (GameController.instance.score < 100000))
        {
            if (player2 == null) { return; }
            else
            { offset = transform.position - player2.transform.position; }
        }
        else if (GameController.instance.score > 100000)
        {
            if (player3 == null) { return; }
            else
            { offset = transform.position - player3.transform.position; }
        }
    }

    void Update()
    {
      transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * smooth);       
    }

    void LateUpdate()
    {
        if (GameController.instance.score < 10000)
        {
            if (player == null) { return; }
            else
            { transform.position = player.transform.position + offset; }
        }
        else if ((GameController.instance.score > 10000) && (GameController.instance.score < 100000))
        {
            if (player2 == null) { return; }
            else
            { transform.position = player2.transform.position + offset; }
        }
        else if (GameController.instance.score > 100000)
        {
            if (player3 == null) { return; }
            else
            { transform.position = player3.transform.position + offset; }
        }
    }
}