using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class GameController : MonoBehaviour
{
    [Header("UI Elements")]
    public Image menu;
    public Button touchRestart;
    public Button startButton;
    public Scrollbar scrollbar;
    public Scrollbar ContrType;
    public Text RestartText;
    public InputField inputField;
    public Text Reload;
    [Header("TextForTranslate")]
    public Text DiffMode;
    public Text DiffModeMenu;
    public Text BestScoreText;
    public Text DestrAstrText;
    public Text BstScoreText;
    public Text ScoreLabelText;
    public Text ContrTypeText;
    [HideInInspector] public string ReloadText;
    [HideInInspector] public string scoreText;
    [HideInInspector] public string bestScoreGameText;
    [HideInInspector] public string betstScoreMenuText;
    [HideInInspector] public string destrAsterText;
    [Header("Stats")]
    [HideInInspector] public SecureInt score;
    [HideInInspector] public SecureInt bestscore;
    [HideInInspector] public SecureInt destrAster;
    [HideInInspector] public int difficulty;
    [Header("Triggers")]
    [HideInInspector] public bool Restart;
    [HideInInspector] public bool isStarted = false;
    [HideInInspector] public bool ControlType;
    [HideInInspector] public int inv;
    [Header("GameObjects")]
    public GameObject Menu;
    public GameObject SpaceFighter1;
    public GameObject SpaceFighter2;
    public GameObject SpaceFighter3;
    public GameObject sphere;
    public GameObject sphere2;
    public GameObject circle;
    public GameObject circle2;
    public GameObject VictoryScreen;
    public GameObject BGSound;
    public GameObject SpaceFighters;

    [Header("Поля чит-меню")]
    [SerializeField] private InputField scoreCheat;
    [SerializeField] private InputField AsterCheat;

    [Header("Лидерборд")]
    public PlayFabManager playFabManager;

    public static GameController instance;


    private void Awake()
    {
        instance = this;

#if UNITY_ANDROID
        ContrType.value = PlayerPrefs.GetFloat("SaveControl", 0);
#elif UNITY_EDITOR
        ContrType.value = PlayerPrefs.GetFloat("SaveControl", 1);
#elif UNITY_IOS
        ContrType.value = PlayerPrefs.GetFloat("SaveControl", 0);
#endif

        if (PlayerPrefs.HasKey("SaveScore"))
        {
            bestscore = PlayerPrefs.GetInt("SaveScore");
            touchRestart.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SaveDestr"))
        {
            destrAster = PlayerPrefs.GetInt("SaveDestr");
        }

        if (ContrType.value < 0.5f)
        {
            ContrTypeText.text = "Swipe";
            ControlType = true;
        }
        else if (ContrType.value > 0.5f)
        {
            ContrTypeText.text = "Buttons";
            ControlType = false;

        }

    }

    public void GetScoreTexts()
    {
        ReloadText = Reload.text;
        scoreText = ScoreLabelText.text;
        betstScoreMenuText = BestScoreText.text;
        bestScoreGameText = BstScoreText.text;
        destrAsterText = DestrAstrText.text;
    }

    void Start()
    {
        circle.SetActive(false);
        circle2.SetActive(false);
        inv = 1;


        if (TouchRestart.hideMenu == 1)
        {
            if (PlayerPrefs.HasKey("firstInGame"))
            {
                TouchRestart.hideMenu = PlayerPrefs.GetInt("firstInGame");
                Menu.SetActive(false);
                isStarted = true;
                PlayerScript.shotDelay = 0.5f;
            }
        }

        Restart = false;

        RestartText.text = "";

        startButton.onClick.AddListener(delegate {

            menu.gameObject.SetActive(false);
            VictoryScreen.gameObject.SetActive(false);
            isStarted = true;
        });
    }


    void Update()
    {

        if ((ReloadText != "") || (scoreText != "") || (betstScoreMenuText != "") || (bestScoreGameText != "") || (destrAsterText != ""))
        {
            Reload.text = ReloadText + " " + Math.Round(PlayerScript.shotDelay, 2);
            ScoreLabelText.text = scoreText + " " + score;
            BestScoreText.text = betstScoreMenuText + " " + bestscore;
            BstScoreText.text = bestScoreGameText + " " + bestscore;
            DestrAstrText.text = destrAsterText + " " + destrAster;
        }

        if (Restart)
        {
            touchRestart.gameObject.SetActive(true);
        }

        if (score > 2147483500)
        {
            // экран победы
            isStarted = false;
            SpaceFighters.SetActive(false);
            bestscore = 1337;
            PlayerPrefs.SetInt("SaveScore", bestscore);
            if (VictoryScreen.gameObject.activeSelf == true) { return; } else { VictoryScreen.gameObject.SetActive(true); }
        }

        if (score >= 10000 && score < 100000)
        {
            if (inv == 1)
            {
                // включаем неуязвимость на 8 секунд
                inv = 0;
                SpaceFighter1.SetActive(false);
                SpaceFighter2.SetActive(true);
                sphere.SetActive(true);
                StartCoroutine(ExecuteAfterTime(8));
            }
            else return;
        }

        if (score >= 100000)
        {
            SpaceFighter3.SetActive(true);
            if (SpaceFighter1.activeSelf == true) { SpaceFighter1.SetActive(false); }
            if (inv == 2)
            {
                // включаем неуязвимость на 8 секунд
                inv = 0;
                SpaceFighter2.SetActive(false);
                sphere2.SetActive(true);
                StartCoroutine(ExecuteAfterTimeTwo(8));
            }
            else return;
        }

    }

    public IEnumerator ExecuteAfterTime(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        inv = 2;
        sphere.SetActive(false);
        if (circle.activeSelf == true)
        {
            circle.SetActive(false);
        }

    }

    public IEnumerator ExecuteAfterTimeTwo(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        inv = 3;
        sphere2.SetActive(false);
        if (circle2.activeSelf == true)
        {
            circle2.SetActive(false);
        }
    }

    public void BestScore()
    {
        if (score > bestscore)
        {
            bestscore = score;
            PlayerPrefs.SetInt("SaveScore", bestscore);
            playFabManager.SendLeaderboard(bestscore);
        }
    }

    public void DestrAster()
    {
        destrAster += 1;
        playFabManager.SendAsterLeaderboard(destrAster);
        PlayerPrefs.SetInt("SaveDestr", destrAster);
    }

    public void Difficulty()
    {
        if (scrollbar.value == 0)
        {
            difficulty = 0;
            DiffMode.text = "easy mode";
            DiffModeMenu.text = "Easy";
        } else if (scrollbar.value > 0 && scrollbar.value < 1)
        {
            difficulty = 1;
            DiffMode.text = "normal mode";
            DiffModeMenu.text = "Normal";
        } else if (scrollbar.value == 1)
        {
            difficulty = 2;
            DiffMode.text = "hard mode";
            DiffModeMenu.text = "Hard";
        }
    }


    public void Controll()
    {
        if (ContrType.value < 0.5f)
        {
            ContrTypeText.text = "Swipe";
            ContrType.value = 0;
            ControlType = true;
        }
        else if (ContrType.value > 0.5f)
        {
            ContrTypeText.text = "Buttons";
            ContrType.value = 1;
            ControlType = false;

        }
        PlayerPrefs.SetFloat("SaveControl", ContrType.value);
    }

    public void Cheat()
    {
        if (scoreCheat.text != "" && AsterCheat.text != "")
        {
            if (long.Parse(scoreCheat.text) > 2147483500) { score = 2147483500; }
            else if (long.Parse(AsterCheat.text) > 2147483500) { destrAster = 2147483500; }
            else { 
            score = int.Parse(scoreCheat.text);
            destrAster = int.Parse(AsterCheat.text); }
        }
    }

    public void GameOver(float scale)
    {
        score -= (int)(10 * scale);
        isStarted = false;
        RestartText.text = "Tap to restart";
        Restart = true;
    }

    public void GameOver()
    {
        isStarted = false;
        RestartText.text = "Tap to restart";
        Restart = true;
    }

    public void IncScore(float scale)
    {
        score += (int)(10 * scale);
        BestScore();
    }

    public void CloseVictScreen()
    {
        VictoryScreen.gameObject.SetActive(false);
    }

    public void ReloadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
