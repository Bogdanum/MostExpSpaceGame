using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private GameObject shieldIndicator,
                                        touchToRestart;
    [SerializeField]
    private Text difficultyMode,
                                  bestScoreText,
                                  currentScoreText;
    private string bestScoreTramslatable,
                   currentScoreTranslatable;

    private void Awake()
    {
        Button touchRestart = touchToRestart.GetComponent<Button>();
        touchRestart.onClick.AddListener(delegate {
            GameController.ReloadScene(0);
        });
    }

    public void ShowShieldIndicator() {
        shieldIndicator.SetActive(true);
    }
    public void HideShieldIndicator() {
        shieldIndicator.SetActive(false);
    }

    public void ShowTouchRestart() {
        touchToRestart.SetActive(true);
    }

    public void SetDifficultyMode(string difficulty) {
        difficultyMode.text = difficulty;
    }

    public void UpdateHudBestScore(int score) {
        bestScoreText.text = bestScoreTramslatable + " " + score;
    }
    public void UpdateCurrentScore(int score) {
        currentScoreText.text = currentScoreTranslatable + " " + score;
    }

    public void SubscribeTranslator(ChangeLanguage translator) {
        translator.OnUpdateTranslate += Refresh;
    }
    public void UnsubscribeTranslator(ChangeLanguage translator) {
        translator.OnUpdateTranslate -= Refresh;
    }

    public void Refresh()
    {
        bestScoreTramslatable = bestScoreText.text;
        currentScoreTranslatable = currentScoreText.text;
    }

}
