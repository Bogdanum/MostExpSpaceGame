using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Scrollbar qualityLevel;
    [SerializeField] private Text scrollbarText;

    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private Scrollbar sound;
    [SerializeField] private Text soundText;

    [SerializeField] private Scrollbar controlType;
    [SerializeField] private Text controlTypeText;

    [SerializeField] private Scrollbar difficulty;
    [SerializeField] private Text difficultyText;

    [SerializeField] private GameObject CheatMenu;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject GraphicsPanelMenu;

    private int count = 0;

    public event Action<bool> OnControlTypeChange = default;
    public event Action<int> OnDifficultyChange = default;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        controlType.value = PlayerData.ControlType;
        qualityLevel.value = PlayerData.GraphicsLevel;
        sound.value = PlayerData.Sound;
        difficulty.value = PlayerData.Difficulty;

        QualityLevel();
        ToggleMusic();
        ControlTypeToggle();
        DifficultyModeToggle();

        qualityLevel.onValueChanged.AddListener(delegate {
            QualityLevel();
	    });
        sound.onValueChanged.AddListener(delegate {
            ToggleMusic();
        });
        controlType.onValueChanged.AddListener(delegate {
            ControlTypeToggle();
        });
        difficulty.onValueChanged.AddListener(delegate {
            DifficultyModeToggle();
	    });
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QualityLevel()
    {
        if (qualityLevel.value == 0)
        {
            qualityLevel.value = 0;
            QualitySettings.SetQualityLevel(0, true);
            scrollbarText.text = "Low";
        }
        else if (qualityLevel.value > 0 && qualityLevel.value < 1)
        {
            qualityLevel.value = 0.5f;
            QualitySettings.SetQualityLevel(2, true);
            scrollbarText.text = "Medium";
        }
        else if (qualityLevel.value == 1)
        {
            qualityLevel.value = 1;
            QualitySettings.SetQualityLevel(5, true);
            scrollbarText.text = "Ultra";
        }
        PlayerData.SaveGraphicsConfig(qualityLevel.value);
        PlayerData.Refresh();
    }

    public void ToggleMusic()
    {
        if (sound.value < 0.5f)
        {
            sound.value = 0;
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
            soundText.text = "OFF";
        }
        else if (sound.value > 0.5f)
        {
            sound.value = 1;
            Mixer.audioMixer.SetFloat("MasterVolume", 0);
            soundText.text = "ON";
        }
        PlayerData.SaveSound(sound.value);
        PlayerData.Refresh();
    }

    public void ControlTypeToggle()
    {
        if (controlType.value < 0.5f)
        {
            controlTypeText.text = "Swipe";
            controlType.value = 0;
            if (OnControlTypeChange != null)
                OnControlTypeChange(true);
        }
        else if (controlType.value > 0.5f)
        {
            controlTypeText.text = "Buttons";
            controlType.value = 1;
            if (OnControlTypeChange != null)
                OnControlTypeChange(false);

        }
        PlayerData.SaveControlType(controlType.value);
        PlayerData.Refresh();
    }

    public void DifficultyModeToggle()
    {
        int difficultyLevel;
        if (difficulty.value < 0.4f)
        {
            difficultyText.text = "Easy";
            difficultyLevel = 0;
        } else
        if (difficulty.value > 0.6f) {
            difficultyText.text = "Hard";
            difficultyLevel = 1;
        } else {
            difficultyText.text = "Medium";
            difficultyLevel = 2;
        }
        if (OnDifficultyChange != null)
            OnDifficultyChange(difficultyLevel);
        PlayerData.SaveDifficulty(difficulty.value);
        PlayerData.Refresh();
    }

    public void OpenCheatMenu()
    {
        count++;
        if (count >= 5) {
            CheatMenu.SetActive(true);
            GraphicsPanelMenu.SetActive(false);
            count = 0;
        }
    }

    public void ShowSettingsPanel() {
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel() {
        settingsPanel.SetActive(false);
    }
}
