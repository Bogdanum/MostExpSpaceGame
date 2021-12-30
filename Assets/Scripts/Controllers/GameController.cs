using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : Singleton<GameController>
{
#region Values
    private static SecureInt _score;
    private static SecureInt _destrAster;
    private static int _difficulty;
    public static readonly int targetScoreBonus = 4000;
    public static readonly int targetScoreLevel2 = 10000;
    public static readonly int targetScoreLevel3 = 100000;
#endregion
#region Triggers
    private static bool _restart;
    private static bool _isStarted = false;
    private static bool _controlType;
    private static bool _unbreakable = false;
    private bool level2Trigger = true;
    private bool level3Trigger = true;
#endregion 
    public GameObject VictoryScreen;

    [SerializeField] private UIMediator uIMediator;
    [SerializeField] private PlayerShipSpawner shipSpawner;

    public event Action<int> OnBestScoreUpdate = default;
    public event Action<int> OnDestrAsterUpdate = default;

    public static SecureInt Score
    {
        get { return _score; }
        private set {
            if (value < 0) _score = 0;
            else _score = value;
        }
    }
    public static SecureInt DestroyedAsteroids
    {
        get { return _destrAster; }
        set {
            if (value < 0) _destrAster = 0;
            else _destrAster = value;
        }
    }
    public static int Difficulty
    {
        get { return _difficulty; }
        private set { _difficulty = value; }
    }
    public static bool ControlType
    {
        get { return _controlType; }
        private set { _controlType = value; }
    }
    public static bool IsStarted
    {
        get { return _isStarted; }
        private set { _isStarted = value; }
    }
    public static bool Restart
    {
        get { return _restart; }
        private set { _restart = value; }
    }
    public static bool Unbreakable
    {
        get { return _unbreakable; }
        private set { _unbreakable = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        SubscribeSettingsScrollbars();

        DestroyedAsteroids = PlayerData.DestrAster;
        ControlType = PlayerData.ControlType < .5f ? true : false;
    }

    private void SubscribeSettingsScrollbars()
    {
        SettingsUI settingsUI = uIMediator.GetSettingsUI();
        settingsUI.OnControlTypeChange += SetControlType;
        settingsUI.OnDifficultyChange += SetDifficultyLevel;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SettingsUI settingsUI = uIMediator.GetSettingsUI();
        settingsUI.OnControlTypeChange -= SetControlType;
        settingsUI.OnDifficultyChange -= SetDifficultyLevel;
    }

    private void SetControlType(bool isMobileInputType) {
        ControlType = isMobileInputType;
    }

    private void SetDifficultyLevel(int level){
        Difficulty = level;
        if (level == 0) {
            uIMediator.SetDifficultyMode("easy mode");
        } else if (level == 1) {
            uIMediator.SetDifficultyMode("hard mode");
        } else{
            uIMediator.SetDifficultyMode("normal mode");
        }
    }

    void Start()
    {
        Init();

        if (TouchRestart.hideMenu == 1)
        {
            if (PlayerPrefs.HasKey("firstInGame"))
            {
                TouchRestart.hideMenu = PlayerPrefs.GetInt("firstInGame");
                uIMediator.HideMenuView();
                IsStarted = true;
            }
        }

    }

    private void Init()
    {
        Score = 0;
        Restart = false;
        Unbreakable = false;

        shipSpawner.SpawnPlayerShip(1);
    }

    void Update()
    {
        uIMediator.UpdateHudBestScore(PlayerData.BestScore);
        uIMediator.UpdateBestScore(PlayerData.BestScore);
        uIMediator.UpdateCurrentScore(Score);
        uIMediator.UpdateDestrAster(DestroyedAsteroids);

        if (Restart)
            uIMediator.ShowTouchRestart();
    }

    public static void SetCheatStats(int score, int asteroids)
    {
        Score = score;
        DestroyedAsteroids = asteroids;
    }

    public void IncScore(float scale)
    {
        Score += (int)(10 * scale);
        BestScore();
        string scoreStatus = CheckForUpgrade();
        Debug.Log("Score status: " + scoreStatus);
    }

    private void BestScore()
    {
        if (Score > PlayerData.BestScore)
        {
            uIMediator.UpdateHudBestScore(Score);
            uIMediator.UpdateBestScore(Score);

            PlayerData.SaveBestScore(Score);
            PlayerData.Refresh();

            if (OnBestScoreUpdate != null)
                OnBestScoreUpdate(Score);
        }
    }

    private string CheckForUpgrade()
    {
        if (isMaxScore()) return "Max score";
        else
        if (isLevel3()) return "Level 3";
        else
        if (isLevel2()) return "Level 2";
        else
        if (isReadyToReceiveBonus()) return "Ready to receive bonus";
        else return " < 4000";
    }

    private bool isMaxScore()
    {
        if (Score > 2147483500)
        {
            IsStarted = false;
            PlayerData.SaveBestScore(1337);
            PlayerData.Refresh();
            if (OnBestScoreUpdate != null)
                OnBestScoreUpdate(1337);

            if (!VictoryScreen.activeSelf)
                VictoryScreen.SetActive(true);
            return true;
        }
        return false;
    }

    private bool isLevel3()
    {
        if (Score >= targetScoreLevel3 && level3Trigger)
        {
            level3Trigger = false;
            shipSpawner.SpawnPlayerShip(3);
            Unbreakable = true;
            ActivateShield();
            uIMediator.ShowShieldIndicator();
            return true;
        }
        return false;
    }

    private bool isLevel2()
    {
        if (Score >= targetScoreLevel2 && level2Trigger)
        {
            level2Trigger = false;
            shipSpawner.SpawnPlayerShip(2);
            Unbreakable = true;
            ActivateShield();
            uIMediator.ShowShieldIndicator();
            return true;
        }
        return false;
    }

    private void ActivateShield()
    {
        GameObject currentShip = shipSpawner.GetCurrentShip();
        WeaponSystem shipWS = currentShip.GetComponent<WeaponSystem>();

        shipWS.ActivateShield();
        StartCoroutine(DisableShieldAfterTime(shipWS));
    }

    private IEnumerator DisableShieldAfterTime(WeaponSystem shipWS)
    {
        yield return new WaitForSeconds(shipWS.GetShieldTime());

        Unbreakable = false;
        shipWS.DisableShield();
        uIMediator.HideShieldIndicator();
    }

    private bool isReadyToReceiveBonus()
    {
        if (Score >= targetScoreBonus)
        {
            return true;
        }
        return false;
    }

    public void DestrAster()
    {
        DestroyedAsteroids += 1;
        if (OnDestrAsterUpdate != null)
            OnDestrAsterUpdate(DestroyedAsteroids);
        PlayerData.SaveDestrAsterCount(DestroyedAsteroids);
        PlayerData.Refresh();
    }

    public static void StartGame()
    {
        IsStarted = true;
    }

    public static void GameOver(float scale)
    {
        Score -= (int)(10 * scale);
        IsStarted = false;
        Restart = true;
    }

    public static void GameOver()
    {
        IsStarted = false;
        Restart = true;
    }

    public static void ReloadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
