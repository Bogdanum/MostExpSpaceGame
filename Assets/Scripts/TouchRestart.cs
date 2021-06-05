using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchRestart : MonoBehaviour
{
    public Text RestartText;
    public static int hideMenu = 0;

    void Update()
    {
        RestartText.color = new Color(RestartText.color.r, RestartText.color.g, RestartText.color.b, Mathf.PingPong(Time.time, 1.0f));

        if (GameController.instance.Restart == true)
        {
            hideMenu = 1;
            PlayerPrefs.SetInt("firstInGame", hideMenu);

            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("SampleScene");  // при уничтожении игрока, загружаем сцену
            }
        }
    }
}