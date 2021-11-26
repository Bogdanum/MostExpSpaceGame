using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject settings;

    public AudioSource Fx;
    public AudioClip clickFx;

    public static bool GameIsPaused = false;

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
        settings.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        TouchRestart.hideMenu = 0;
    }
}
