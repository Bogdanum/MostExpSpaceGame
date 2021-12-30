using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public GameObject ErrorWindowText;
    public Button[] buttons;
    public Sprite[] sprites;

    public event Action OnUpdateTranslate = default;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian) PlayerData.SaveLanguage(1); // russian - language id
            else if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseTraditional) PlayerData.SaveLanguage(2);
            else if (Application.systemLanguage == SystemLanguage.German) PlayerData.SaveLanguage(3);
            else if (Application.systemLanguage == SystemLanguage.Japanese) PlayerData.SaveLanguage(4);
            else if (Application.systemLanguage == SystemLanguage.French) PlayerData.SaveLanguage(5);
            else if (Application.systemLanguage == SystemLanguage.Portuguese) PlayerData.SaveLanguage(6);
            else if (Application.systemLanguage == SystemLanguage.Spanish) PlayerData.SaveLanguage(7);
            else if (Application.systemLanguage == SystemLanguage.Turkish) PlayerData.SaveLanguage(8);
            else if (Application.systemLanguage == SystemLanguage.Ukrainian) PlayerData.SaveLanguage(9);
            else PlayerData.SaveLanguage(0); // default language - EN
            PlayerData.Refresh();
        }

        Translator.SelectLanguage(PlayerData.Language);
        if (OnUpdateTranslate != null)
            OnUpdateTranslate();
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != PlayerData.Language)
            {
                buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
            else
            {
                buttons[PlayerData.Language].GetComponent<Image>().sprite = buttons[PlayerData.Language].spriteState.selectedSprite;
            }
        }
       
    }

    public void LanguageChange(int languageID)
    {
        PlayerData.SaveLanguage(languageID);
        PlayerData.Refresh();
        Translator.SelectLanguage(PlayerData.Language);
        if (OnUpdateTranslate != null)
            OnUpdateTranslate();
    }

    public void ShowErrorText()
    {
        ErrorWindowText.SetActive(true);
        ErrorWindowText.GetComponent<Text>().text = Translator.GetText(20);
    }
}
