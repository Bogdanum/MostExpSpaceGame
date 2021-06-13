using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    [Header("Окно ввода ника и таблица лидеров")]
    public GameObject inputName;
    public GameObject leaderboard;

    [Header("Обьект leaderboard и префаб строки(Player)")]
    public GameObject rowPrefab;
    public Transform rowsParent;
    [Range(1, 1000)] public int playersCountAll = 100;
    [Range(1, 100)] public int aroundPlayersCount = 10;


    [Header("Обьект aster leaderboard")]
    public Transform rowsAsterParent;

    [Header("Содержимое окна лидерборда")]
    public Scrollbar changeLeaderboardType;
    public Text scrollbarText;
    public GameObject scoreBoard;
    public GameObject asterBoard;

    [Header("Элементы окна ввода ника")]
    public InputField inputField;
    public Button submitButton;
    public GameObject errorWindowText;

    [Header("Поле с ником в меню")]
    public Text nameMenu;

    [Header("Получение лучшего счета")]
    public string BestScoreKey = "BestScore";
    public string ScoreLBname = "BestPlayers";
    public string AsterLBname = "Asteroids";

    [Header("Цвет выделения игрока")]
    public Color playerColor;
  
    string loggedInPlayfabId;
    

    void Start()
    {
        Login();
    }

    string CheckUserID()
    {
        string deviceID;
        if (PlayerPrefs.GetString("DeviceID", "") == "")
        {
            deviceID = SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString("DeviceID", deviceID);
        }
        else
        {
            deviceID = PlayerPrefs.GetString("DeviceID");
        }
        return deviceID;
    }

    bool CheckHighOrLowIqUser(string input)
    {
        if (input.IndexOf(' ') > -1)
        {
            return true;
        }
        else { return false; }
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = CheckUserID(),
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        loggedInPlayfabId = result.PlayFabId;
        Debug.Log("Successful login/accaunt create!");
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            nameMenu.text = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        else
        {
            Debug.LogWarning("Open name window...");
            inputName.SetActive(true);
        }
        GetFuckingBestScore();
        GetFuckingBestAster();

    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
        errorWindowText.SetActive(true);
        if (PlayerPrefs.GetString("PlayerName") != null)
        {
            nameMenu.text = PlayerPrefs.GetString("PlayerName");
            
        }
        else
        {
            nameMenu.text = "Player_" + Random.Range(0, 1000000);
        }

    }

    public void SendLeaderboard(int score)             
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = ScoreLBname,      
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sucessfull leaderboard send");
    }

    public void GetLeaderboard()      
    {
        leaderboard.SetActive(true);

        var request = new GetLeaderboardRequest
        {
            StatisticName = ScoreLBname,               
            StartPosition = 0,
            MaxResultsCount = playersCountAll                     
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach(Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
            foreach (var item in result.Leaderboard)
            {
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                Text[] texts = newGo.GetComponentsInChildren<Text>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();
                
                Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                    item.Position, item.DisplayName, item.StatValue));

                if (item.PlayFabId == loggedInPlayfabId)
                {
                    texts[0].color = playerColor;        
                    texts[1].color = playerColor;
                    texts[2].color = playerColor;
                }
        }

    }

    public void SubmitNameButton()             // привязать к кнопке (в форме для ввода ника)
    {
        if (inputField.text == "" || CheckHighOrLowIqUser(inputField.text))
        {
            inputField.text = "Player_" + Random.Range(0, 1000000);
        }
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = inputField.text,
            };
        nameMenu.text = inputField.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name");
        inputName.SetActive(false);
        PlayerPrefs.SetString("PlayerName", result.DisplayName);
    }


    public void GetLeaderboardAroundPlayer()               // привязать к кнопке "показать свою позицию" в лидерборде
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = ScoreLBname,                        // повторить строки 90 и 92 (должны совпадать значения)
            MaxResultsCount = aroundPlayersCount                             
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInPlayfabId)
            {
                texts[0].color = playerColor;                // цвет текста авторизованного пользователя в таблице лидеров
                texts[1].color = playerColor;
                texts[2].color = playerColor;
            }

            Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                   item.Position, item.DisplayName, item.StatValue));
        }
       
    }
    // ----------- раздел с вторым типом лидерборда ------------ //

    public void GetAsterLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = AsterLBname,                
            StartPosition = 0,
            MaxResultsCount = aroundPlayersCount                      
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardAsterGet, OnError);
    }

    void OnLeaderboardAsterGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsAsterParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsAsterParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                item.Position, item.DisplayName, item.StatValue));

            if (item.PlayFabId == loggedInPlayfabId)
            {
                texts[0].color = playerColor;          // цвет текста авторизованного пользователя в таблице лидеров
                texts[1].color = playerColor;
                texts[2].color = playerColor;
            }
        }
    }

    public void GetLeaderboardAsterAroundPlayer()               // привязать к кнопке "показать свою позицию" в лидерборде
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = AsterLBname,                        // повторить строки 90 и 92 (должны совпадать значения)
            MaxResultsCount = aroundPlayersCount
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAsterAroundPlayerGet, OnError);
    }

    void OnLeaderboardAsterAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        foreach (Transform item in rowsAsterParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsAsterParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            if (item.PlayFabId == loggedInPlayfabId)
            {
                texts[0].color = playerColor;                // цвет текста авторизованного пользователя в таблице лидеров
                texts[1].color = playerColor;
                texts[2].color = playerColor;
            }

            Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                   item.Position, item.DisplayName, item.StatValue));
        }
    }

    public void SendAsterLeaderboard(int score)             // вызвать в методе, где находится сохранение рекорда
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = AsterLBname,            // изменить название таблицы лидеров
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardAsterUpdate, OnError);
    }

    void OnLeaderboardAsterUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sucessfull asteroids leaderboard send");
    }

    public void ChooseLeaderboard()
    {
        if (changeLeaderboardType.value < 0.5f)
        {
            scrollbarText.text = "Score";
            scoreBoard.SetActive(true);
            asterBoard.SetActive(false);
        }
        else if (changeLeaderboardType.value > 0.5f)
        {
            scrollbarText.text = "Asteroids";
            scoreBoard.SetActive(false);
            asterBoard.SetActive(true);
        }
    }

    void GetFuckingBestScore()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = ScoreLBname,
            StartPosition = 0,
            MaxResultsCount = playersCountAll
        };
        PlayFabClientAPI.GetLeaderboard(request, GetFuckingNigerBestScore, OnError);
    }

    void GetFuckingNigerBestScore(GetLeaderboardResult result)
    {
        if (loggedInPlayfabId == "") { Login(); }

        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == loggedInPlayfabId)
            {
                PlayerPrefs.SetInt(BestScoreKey, item.StatValue);
                GameController.instance.bestscore = PlayerPrefs.GetInt(BestScoreKey);
            }
        }
    }
    void GetFuckingBestAster()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = AsterLBname,
            StartPosition = 0,
            MaxResultsCount = playersCountAll
        };
        PlayFabClientAPI.GetLeaderboard(request, GetFuckingNigerBestAster, OnError);
    }

    void GetFuckingNigerBestAster(GetLeaderboardResult result)
    {
        if (loggedInPlayfabId == "") { Login(); }

        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == loggedInPlayfabId)
            {
                PlayerPrefs.SetInt(BestScoreKey, item.StatValue);
               GameController.instance.destrAster = PlayerPrefs.GetInt(BestScoreKey);
            }
        }
    }

}
