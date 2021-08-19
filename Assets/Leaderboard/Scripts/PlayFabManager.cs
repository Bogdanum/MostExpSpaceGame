using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

public class PlayFabManager : MonoBehaviour
{
    #region UI

            #region Окно ввода для ввода ника

                [Header("Окно ввода ника")] public GameObject inputName;
                [Header("Элементы окна ввода ника")]
                public InputField inputField;
                public Button submitButton;
                public GameObject errorWindowText;
                public GameObject errorWindowForb;

                #endregion

                #region Поле с ником в меню

                [Header("Поле с ником в меню")] public Text nameMenu;

            #endregion

            #region Лидерборды

                [Header("Окно лидерборда")] public GameObject leaderboard;
                [Header("Префаб строки(Player)")] public GameObject rowPrefab;
                [Range(1, 1000)] public int playersCountAll = 100;
                [Range(1, 100)] public int aroundPlayersCount = 10;
                [Header("Содержимое окна лидерборда")]
                public Scrollbar changeLeaderboardType;
                public Text scrollbarText;
                public GameObject scoreBoard;
                public GameObject asterBoard;
                [Header("Цвет выделения игрока")] public Color playerColor;

                #region Score

                    [Header("Обьект leaderboard")] public Transform rowsParent;

                #endregion

                #region Asteroids

                    [Header("Обьект aster leaderboard")] public Transform rowsAsterParent;

                #endregion

            #endregion

            public Text position;
            [Space(20)]public GameObject ControlBlockPanel;
    #endregion

    #region Other
        [Header("Получение лучшего счета")]
        public string BestScoreKey = "BestScore";
        public string ScoreLBname = "BestPlayers";
        public string AsterLBname = "Asteroids";

        string loggedInPlayfabId;
        private TextAsset textAsset;
        private string[] forbiddenWords;
    #endregion

    void Start()
    {
        ControlBlockPanel.SetActive(true);
        Login();
    }

    #region LOG IN/CREATE USER ACCOUNT

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
                //name = result.InfoResultPayload.PlayerProfile.DisplayName;
                nameMenu.text = result.InfoResultPayload.PlayerProfile.DisplayName;
            }
            else
            {
                Debug.LogWarning("Open name window...");
                inputName.SetActive(true);
            }
            GetFuckingBestScore();
            GetFuckingBestAster();
            ControlBlockPanel.SetActive(false);
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
                nameMenu.text = NameGenerator();
            }

        }

    #endregion LOG IN/CREATE USER ACCOUNT

    #region USER NAME

    public void SubmitNameButton()
        {
            if (inputField.text == "" || CheckHighOrLowIqUser(inputField.text))
            {
                inputField.text = NameGenerator();
            }
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = CheckForbiddenWords(inputField.text),
            };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        }

        void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("Updated display name");
            nameMenu.text = result.DisplayName;
            if (result.DisplayName == inputField.text)
            inputName.SetActive(false);
            PlayerPrefs.SetString("PlayerName", result.DisplayName);
        }

    #endregion USER NAME

    #region LEADERBOARD SCORE

        public void GetLeaderboard()      
        {
            leaderboard.SetActive(true);

            var request = new GetLeaderboardRequest
            {
                StatisticName = ScoreLBname,               
                StartPosition = 0,
                MaxResultsCount = playersCountAll,
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

        public void GetLeaderboardAroundPlayer()               
        {
            var request = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = ScoreLBname,                        
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
                    texts[0].color = playerColor;                
                    texts[1].color = playerColor;
                    texts[2].color = playerColor;
                }

                Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                       item.Position, item.DisplayName, item.StatValue));
            }
       
        }

    #endregion LEADERBOARD SCORE

    #region LEADERBOARD ASTEROIDS

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
                    texts[0].color = playerColor;          
                    texts[1].color = playerColor;
                    texts[2].color = playerColor;
                }
            }
        }

        public void GetLeaderboardAsterAroundPlayer()               
        {
            var request = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = AsterLBname,                        
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
                    texts[0].color = playerColor;                
                    texts[1].color = playerColor;
                    texts[2].color = playerColor;
                }

                Debug.Log(string.Format("PLACE: {0} | ID: {1} | SCORE: {2}",
                       item.Position, item.DisplayName, item.StatValue));
            }
        }

    #endregion LEAERBOARD ASTEROIDS

    #region EXTERNAL FUNCTIONS

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
            Debug.Log("Sucessfull score leaderboard send");
        }

        public void SendAsterLeaderboard(int score)
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName = AsterLBname,
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

    #endregion EXTERNAL FUNCTIONS

    #region INTERNAL FUNCTIONS

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
                    position.text = (item.Position + 1).ToString();
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

        public string NameGenerator()
        {
            char[] vowels = "aeuoyi".ToCharArray();
            char[] consonants = "qwrtpsdfghjklzxcvbnm".ToCharArray();

            char[] newNickLength = new char[Random.Range(3, 10)];
            StringBuilder newNick = new StringBuilder();

            while (newNick.Length < newNickLength.Length)
            {
                bool firstVowel = Random.Range(0, 2) == 0 ? true : false;

                if (firstVowel)
                {
                    newNick.Append(vowels[Random.Range(0, vowels.Length)]);
                    newNick.Append(consonants[Random.Range(0, consonants.Length)]);
                }
                else
                {
                    newNick.Append(consonants[Random.Range(0, consonants.Length)]);
                    newNick.Append(vowels[Random.Range(0, vowels.Length)]);
                }
            }
            if (newNickLength.Length % 2 != 0) newNick.Remove(newNick.Length - 1, 1);
            newNick[0] = char.ToUpper(newNick[0]);
            nameMenu.text = newNick.ToString();
            return CheckForbiddenWords(newNick.ToString());
        }

        public string CheckForbiddenWords(string username)
        {
            textAsset = Resources.Load("ForbiddenWords/ForbiddenWords") as TextAsset;
            forbiddenWords = textAsset.text.Split('\n');
            foreach (string line in forbiddenWords)
            {
                string pattern = @"\S*" + line + @"\S*";
                if (Regex.IsMatch(username, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled))
                {
                    Debug.LogWarning("Ник " + username + " запрещен!");
                    errorWindowForb.SetActive(true);
                    return NameGenerator();
                }
            }
            textAsset = Resources.Load("ForbiddenWords/ForbiddenWordsArab") as TextAsset;
            forbiddenWords = textAsset.text.Split('\n');
            foreach (string line in forbiddenWords)
            {
                if (username.Contains(line))
                {
                    Debug.LogWarning("Ник " + username + " запрещен!");
                    errorWindowForb.SetActive(true);
                    return NameGenerator();
                }
            }
            return username;
        }

    #endregion INTERNAL FUNCTIONS

}
