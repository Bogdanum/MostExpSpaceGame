using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    public Scrollbar qualityLevel;
    public Text scrollbarText;
    public Text SoundText;

    public AudioMixerGroup Mixer;
    public Scrollbar Sound;

    private int count = 0;
    public GameObject CheatMenu;
    public GameObject GraphicsPanelMenu;

    private void Awake()
    {
        qualityLevel.value = PlayerPrefs.GetFloat("SaveQuality", 1);
        Sound.value = PlayerPrefs.GetFloat("SaveSound", 1);
    }
    void Start()
    {
        SetupQuality();
        SetupSound();
    }

    private void SetupQuality()
    {
        if (qualityLevel.value == 0)
        {
            QualitySettings.SetQualityLevel(0, true);
            scrollbarText.text = "Low";
        }
        else if (qualityLevel.value == 0.5f)
        {
            QualitySettings.SetQualityLevel(2, true);
            scrollbarText.text = "Medium";
        }
        else if (qualityLevel.value == 1)
        {
            QualitySettings.SetQualityLevel(5, true);
            scrollbarText.text = "Ultra";
        }
    }

    private void SetupSound()
    {
        if (Sound.value == 0)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
            SoundText.text = "OFF";
        }
        else if (Sound.value == 1)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", 0);
            SoundText.text = "ON";
        }
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
        PlayerPrefs.SetFloat("SaveQuality", qualityLevel.value);
    }

    public void ToggleMusic()
    {
        if (Sound.value < 0.5f)
        {
            Sound.value = 0;
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
            SoundText.text = "OFF";
        }
        else if (Sound.value > 0.5f)
        {
            Sound.value = 1;
            Mixer.audioMixer.SetFloat("MasterVolume", 0);
            SoundText.text = "ON";
        }
        PlayerPrefs.SetFloat("SaveSound", Sound.value);
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
}
