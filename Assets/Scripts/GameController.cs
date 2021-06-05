using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;


public class GameController : MonoBehaviour
{
    [Header ("UI Elements")]
    public Image menu;              
    public Button touchRestart;     
    public Button startButton;
    public Scrollbar scrollbar;
    public Scrollbar ContrType;
    public Text RestartText;
    public Dropdown SelectLang;
    public InputField inputField;
    public Text Reload;
    [Header ("TextForTranslate")]
    public Text scoreLabel, bestScore, BstScore, Destraster, DiffMode, DiffModeMenu,
    PlayButton, BestScoreText, DestrAstrText, BstScoreText, ScoreLabelText, InfoText,
    GameRulesText, RulesContent, SettingsText, QuitText, GameSettingsText, SelectLanguageText,
    DifficultyText, GraphicsText, CheatInputText, CheatButtonText, CheatMenuName, ContrTypeText,
    ControlTypeText, SoundText, RealBuyersText, BillionareText, BillionareListContent;
    string ReloadText;
    [Header ("Stats")]
    public int score = 0;
    public int bestscore;
    public int destrAster;
    public int diff;
    [Header ("Triggers")]
    public bool Restart;
    public bool isStarted = false;  // изначально игра не запущена
    public bool ControlType;
    public int inv, inv2;
    [Header ("GameObjects")]
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

    public static GameController instance;
  

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("SaveScore"))
        {
            bestscore = PlayerPrefs.GetInt("SaveScore");
            touchRestart.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SaveDestr"))
        {
            destrAster = PlayerPrefs.GetInt("SaveDestr");
        }

        if (PlayerPrefs.HasKey("SaveLang"))
        {
            SelectLang.value = PlayerPrefs.GetInt("SaveLang");
        }

        if (PlayerPrefs.HasKey("SaveControl"))
        {
            ContrType.value = PlayerPrefs.GetFloat("SaveControl");
            ContrTypeText.text = PlayerPrefs.GetString("SaveControl");
        }

        if (SelectLang.value == 0)
        {
            ReloadText = "Shots delay/s:";
            PlayButton.text = "Play";
            GameSettingsText.text = "Game settings";
            GameRulesText.text = "Game rules";
            RulesContent.text = "1. Shoot down asteroids to get points.\n2. Medium and small asteroids are destroyed with \n1 hit, large ones - with 3.\n3. Having collected 4000 points along with the asteroids, bonuses(green) that speed up the reloading of weapons \nand death orbs(red) begin to fly towards you.\n4. Gaining 10000 and 100000 points, your ship becomes more powerful\n5. The difficulty of the game gradually increases while you \nare alive.";
            BillionareListContent.text = "Elon Musk\nBill Gates\nVladimir Putin";

        } else if(SelectLang.value == 0)
        {
            ReloadText = "Задержка между выстр./с:";
            PlayButton.text = "Играть";
            GameSettingsText.text = "Игровые параметры";
            GameRulesText.text = "Правила игры";
            RulesContent.text = "1. Сбивайте астероиды, чтобы получать очки.\n2. Средние и малые астероиды уничтожаются с 1 попадания, большие и огромные — с 3 - х.\n3. Набрав 4000 очков вместе с астероидами к вам начинают лететь бонусы(зеленые) ускоряющие перезарядку орудий и сферы смерти(красные).\n4. Набрав 10000 и 100000 очков, ваш корабль\nстановится мощнее.\n5. Сложность игры постепенно возрастает пока вы живы.";
            BillionareListContent.text = "Elon Musk\nBill Gates\nVladimir Putin";
        }
    }

    void Start()
    {
        circle.SetActive(false);
        circle2.SetActive(false);
        inv = 1; inv2 = 1;


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
            isStarted = true;
	    });

      
    }

    
    void Update()
    {
        Reload.text = ReloadText + " " + PlayerScript.shotDelay;
        scoreLabel.text = "" + score;
        bestScore.text =  "" + bestscore;
        BstScore.text = "" + bestscore;
        Destraster.text = "" + destrAster;

        if (ContrType.value > 0.5f)
        {
            ContrTypeText.text = "Buttons";
            ControlType = false;
        }
        else if (ContrType.value < 0.5f)
        {
            ContrTypeText.text = "Swipe";
            ControlType = true;
        }

        if (diff == 0)
        {
            DiffMode.text = "easy mode";
            DiffModeMenu.text = "Easy";
        } else if (diff == 1)
        {
            DiffMode.text = "normal mode";
            DiffModeMenu.text = "Normal";
        } else if (diff == 2)
        {
            DiffMode.text = "hard mode";
            DiffModeMenu.text = "Hard";
        }


        if (Restart)
        {
            touchRestart.gameObject.SetActive(true);
        }

        if (score >= 10000 && score < 100000)
        {
            if (inv == 3)
            {
                inv = 3;
            } else
            {
                inv = 2;
                sphere.SetActive(true);
                if (circle.activeSelf == false)
                {
                    circle.SetActive(true);
                }
            }
                SpaceFighter1.SetActive(false);
                SpaceFighter2.SetActive(true);
                StartCoroutine(ExecuteAfterTime(8)); 
        }

        if (score >= 100000)
        {
            if (inv2 == 3)
            {
                inv2 = 3;
            }
            else
            {
                inv2 = 2;
                sphere2.SetActive(true);
                if (circle2.activeSelf == false)
                {
                    circle2.SetActive(true);
                }
            }
            SpaceFighter2.SetActive(false);
            SpaceFighter3.SetActive(true);
            StartCoroutine(ExecuteAfterTimeTwo(8));
        }

        if (score > 2147483547)
        {
            VictoryScreen.SetActive(true);
            isStarted = false;
            SpaceFighter1.SetActive(false);
            SpaceFighter2.SetActive(false);
            SpaceFighter3.SetActive(false);
        }

    }

    public IEnumerator ExecuteAfterTime(float timeInSec)
      {
        yield return new WaitForSeconds(timeInSec);
        inv = 3;
            sphere.SetActive(false);
        if (circle.activeSelf == true)
        {
            circle.SetActive(false);
        }
        
      }

    public IEnumerator ExecuteAfterTimeTwo(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        inv2 = 3;
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
        }
        PlayerPrefs.SetInt("SaveScore", bestscore);
    }

    public void DestrAster()
    {
        destrAster += 1;
        PlayerPrefs.SetInt("SaveDestr", destrAster);
    }
    
    public void Difficulty()
    {
        if (scrollbar.value == 0)
        {
            diff = 0;  // easy
         } else if (scrollbar.value > 0 && scrollbar.value < 1)
        {
            diff = 1;  // normal
        } else if (scrollbar.value == 1)
        {
            diff = 2; // hard
        }
    }

    public void selectLang()
    {
        if (SelectLang.value == 0)
        {
            BillionareListContent.text = "Elon Musk\nBill Gates\nVladimir Putin";
            RealBuyersText.text = "Real buyers";
            BillionareText.text = "Billionare list";
            SoundText.text = "Sound";
            ControlTypeText.text = "Control type";
            ReloadText = "Shots delay/s:";
            CheatMenuName.text = "Cheat menu";
            CheatButtonText.text = "Apply";
            CheatInputText.text = "Set score...";
            PlayButton.text = "Play";
            BestScoreText.text = "BEST SCORE:";
            DestrAstrText.text = "Destroyed asteroids:";
            BstScoreText.text = "Best score:";
            ScoreLabelText.text = "Score:";
            InfoText.text = "INFO";
            InfoText.fontSize = 65;
            SettingsText.text = "Settings";
            SettingsText.fontSize = 65;
            QuitText.text = "Quit Game";
            QuitText.fontSize = 65;
            GameSettingsText.text = "Game settings";
            SelectLanguageText.text = "Select language";
            DifficultyText.text = "Difficulty";
            GraphicsText.text = "Graphics";
            GameRulesText.text = "Game rules";
            RulesContent.text = "1. Shoot down asteroids to get points.\n2. Medium and small asteroids are destroyed with \n1 hit, large ones - with 3.\n3. Having collected 4000 points along with the asteroids, bonuses(green) that speed up the reloading of weapons \nand death orbs(red) begin to fly towards you.\n4. Gaining 10000 and 100000 points, your ship becomes more powerful\n5. The difficulty of the game gradually increases while you \nare alive.";
            
        }
        else if (SelectLang.value == 1)
        {
            BillionareListContent.text = "Elon Musk\nBill Gates\nVladimir Putin";
            RealBuyersText.text = "Реальные покупатели";
            BillionareText.text = "Список миллиардеров";
            SoundText.text = "Звук";
            ControlTypeText.text = "Тип управления";
            ReloadText = "Задержка между выстр./с:";
            CheatMenuName.text = "Чит меню";
            CheatButtonText.text = "Применить";
            CheatInputText.text = "Введите счет...";
            PlayButton.text = "Играть";
            BestScoreText.text = "ЛУЧШИЙ СЧЕТ:";
            DestrAstrText.text = "Астероидов сбито:";
            BstScoreText.text = "Лучший счет:";
            ScoreLabelText.text = "Счет:";
            InfoText.text = "Информация";
            InfoText.fontSize = 55;
            SettingsText.text = "Параметры";
            SettingsText.fontSize = 55;
            QuitText.text = "Выйти из игры";
            QuitText.fontSize = 55;
            GameSettingsText.text = "Игровые параметры";
            SelectLanguageText.text = "Выбор языка";
            DifficultyText.text = "Сложность";
            GraphicsText.text = "Графика";
            GameRulesText.text = "Правила игры";
            RulesContent.text = "1. Сбивайте астероиды, чтобы получать очки.\n2. Средние и малые астероиды уничтожаются с 1 попадания, большие и огромные — с 3 - х.\n3. Набрав 4000 очков вместе с астероидами к вам начинают лететь бонусы(зеленые) ускоряющие перезарядку орудий и сферы смерти(красные).\n4. Набрав 10000 и 100000 очков, ваш корабль\nстановится мощнее.\n5. Сложность игры постепенно возрастает пока вы живы.";
        }
        PlayerPrefs.SetInt("SaveLang", SelectLang.value);
    }

    public void Controll()
    {
        if (ContrType.value == 0)
        {
            ContrTypeText.text = "Swipe";
            ContrType.value = 0;
            ControlType = false;
        }
        else if (ContrType.value == 1)
        {
            ContrTypeText.text = "Buttons";
            ContrType.value = 1;
            ControlType = true;
        
        }
        PlayerPrefs.SetFloat("SaveControl", ContrType.value);
        PlayerPrefs.SetString("SaveControl", ContrTypeText.text);
    }

    public void Cheat()
    {
        if (inputField.text == "")
        {
            if (SelectLang.value == 1)
            {
                CheatInputText.text = "Вы дурак?";
            } else
            {
                CheatInputText.text = "You are stupid?";
            }
            
        } else
        {
            score = Int32.Parse(inputField.text);
            if (SelectLang.value == 1)
            {
                CheatInputText.text = "Введите счет...";
            } else
            {
                CheatInputText.text = "Input score...";
            }
        }
    }

}
