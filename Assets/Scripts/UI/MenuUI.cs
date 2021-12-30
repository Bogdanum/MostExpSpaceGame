using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuView;

    [SerializeField] private Button startButton;
    [SerializeField] private Text bestScoreText,
                                  destrAsterText;
    private string bestScoreTranslatable,
                   destAsterTranslatable;

    private void Awake()
    {
        startButton.onClick.AddListener(delegate {
            HideMenuView();
            GameController.StartGame();
	    });
    }

    public void ShowMenuView() {
        menuView.SetActive(true);
    }
    public void HideMenuView() {
        menuView.SetActive(false);
    }

    public void UpdateBestScore(int score) {
        bestScoreText.text = bestScoreTranslatable +" "+ score;
    }
    public void UpdateDestrAster(int value) {
        destrAsterText.text = destAsterTranslatable + " " + value;
    }

    public void SubscribeTranslator(ChangeLanguage translator)
    {
        translator.OnUpdateTranslate += Refresh;
    }
    public void UnsubscribeTranslator(ChangeLanguage translator)
    {
        translator.OnUpdateTranslate -= Refresh;
    }

    public void Refresh()
    {
        bestScoreTranslatable = bestScoreText.text;
        destAsterTranslatable = destrAsterText.text;
    }
}
 