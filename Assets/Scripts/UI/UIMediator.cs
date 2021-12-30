using UnityEngine;

public class UIMediator : Singleton<UIMediator>
{
    [SerializeField] private ChangeLanguage translator;
    [SerializeField] private MenuUI menuUI;
    [SerializeField] private HudUI hudUI;
    [SerializeField] private Settings settings;

    protected override void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        menuUI.SubscribeTranslator(translator);
        hudUI.SubscribeTranslator(translator);
    }
    protected override void OnDestroy()
    {
        menuUI.UnsubscribeTranslator(translator);
        hudUI.UnsubscribeTranslator(translator);
    }

    public SettingsUI GetSettingsUI() {
        return settings.GetSettingsUI();
    }

    public void ShowMenuView() {
        menuUI.ShowMenuView();
    }

    public void HideMenuView() {
        menuUI.HideMenuView();
    }

    public void UpdateBestScore(int score) {
        menuUI.UpdateBestScore(score);
    }

    public void UpdateDestrAster(int value) {
        menuUI.UpdateDestrAster(value);
    }

    public void ShowShieldIndicator() {
        hudUI.ShowShieldIndicator();
    }

    public void HideShieldIndicator() {
        hudUI.HideShieldIndicator();
    }

    public void ShowTouchRestart() {
        hudUI.ShowTouchRestart();
    }

    public void SetDifficultyMode(string difficulty) {
        hudUI.SetDifficultyMode(difficulty);
    }

    public void UpdateHudBestScore(int score) {
        hudUI.UpdateHudBestScore(score);
    }

    public void UpdateCurrentScore(int score) {
        hudUI.UpdateCurrentScore(score);
    }

}
