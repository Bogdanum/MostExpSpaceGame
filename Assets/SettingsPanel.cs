using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour
{
    public Scrollbar scrollbar;
    public Text scrollbarText;
    public Text SoundText;

    public AudioMixerGroup Mixer;
    public Scrollbar Sound;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveQuality"))
        {
            scrollbar.value = PlayerPrefs.GetFloat("SaveQuality");
        }
        if (PlayerPrefs.HasKey("SaveSound"))
        {
            Sound.value = PlayerPrefs.GetFloat("SaveSound");
        }
    }
    void Start()
    {
        if (scrollbar.value == 0)
        {
            QualitySettings.SetQualityLevel(0, true);
            scrollbarText.text = "Low";
        } else if (scrollbar.value == 0.5f)
        {
            QualitySettings.SetQualityLevel(2, true);
            scrollbarText.text = "Medium";
        } else if (scrollbar.value == 1)
        {
            QualitySettings.SetQualityLevel(5, true);
            scrollbarText.text = "Ultra";
        }

       if (Sound.value == 0)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
            SoundText.text = "OFF";
        } else if (Sound.value == 1)
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
        if (scrollbar.value == 0)
        {
            scrollbar.value = 0;
            QualitySettings.SetQualityLevel(0, true);
            scrollbarText.text = "Low";
        }
        else if (scrollbar.value > 0 && scrollbar.value < 1)
        {
            scrollbar.value = 0.5f;
            QualitySettings.SetQualityLevel(2, true);
            scrollbarText.text = "Medium";
        }
        else if (scrollbar.value == 1)
        {
            scrollbar.value = 1;
            QualitySettings.SetQualityLevel(5, true);
            scrollbarText.text = "Ultra";
        }
        PlayerPrefs.SetFloat("SaveQuality", scrollbar.value);
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
}
