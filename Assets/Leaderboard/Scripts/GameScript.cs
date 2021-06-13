///<summary>
/// пример вызова метода (стрелочками указаны элементы,
/// которые обязательно должны быть)
/// </summary>



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Text score;
    int currentScore = 0;
    public PlayFabManager playFabManager;  // <----------

    void Start()
    {
   
        score.text = "0";
    }

    public void GetPoint()
    {
        currentScore += 1;
        score.text = currentScore.ToString();
        if (currentScore > PlayerPrefs.GetInt("Best score", 0))
        {
            PlayerPrefs.SetInt("Best score", currentScore);
            playFabManager.SendLeaderboard(PlayerPrefs.GetInt("Best score"));    // <-------------
        }
    }
}
