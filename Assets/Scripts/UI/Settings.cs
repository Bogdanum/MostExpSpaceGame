using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioSource Fx;
    [SerializeField] private AudioClip clickFx;
    [SerializeField] private SettingsUI settingsUI;

    public static bool GameIsPaused = false;

    public SettingsUI GetSettingsUI() {
        return settingsUI;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ClickSound()
    {
        Fx.PlayOneShot(clickFx);
    }

    public void GoHome(){
        Time.timeScale = 1f;
        HideSettingsPanel();
        GameController.ReloadScene(0);
        TouchRestart.hideMenu = 0;
    }

    public void ShowSettingsPanel() {
        settingsUI.ShowSettingsPanel();
    }

    public void HideSettingsPanel() {
        settingsUI.HideSettingsPanel();
    }
}
