using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public GameObject ErrorWindowText;
    public Button[] buttons;
    public Sprite[] sprites;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian) PlayerPrefs.SetInt("Language", 1); // русский - номер языка
            else if (Application.systemLanguage == SystemLanguage.Chinese || Application.systemLanguage == SystemLanguage.ChineseTraditional) PlayerPrefs.SetInt("Language", 2);
            else if (Application.systemLanguage == SystemLanguage.German) PlayerPrefs.SetInt("Language", 3);
            else if (Application.systemLanguage == SystemLanguage.Japanese) PlayerPrefs.SetInt("Language", 4);
            else if (Application.systemLanguage == SystemLanguage.French) PlayerPrefs.SetInt("Language", 5);
            else if (Application.systemLanguage == SystemLanguage.Portuguese) PlayerPrefs.SetInt("Language", 6);
            else if (Application.systemLanguage == SystemLanguage.Spanish) PlayerPrefs.SetInt("Language", 7);
            else if (Application.systemLanguage == SystemLanguage.Turkish) PlayerPrefs.SetInt("Language", 8);
            else if (Application.systemLanguage == SystemLanguage.Ukrainian) PlayerPrefs.SetInt("Language", 9);
            else PlayerPrefs.SetInt("Language", 0); // дефолтный язык - английский
        }

        Translator.SelectLanguage(PlayerPrefs.GetInt("Language"));
        GameController.instance.GetScoreTexts();
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != PlayerPrefs.GetInt("Language"))
            {
                buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
            else
            {
                buttons[PlayerPrefs.GetInt("Language")].GetComponent<Image>().sprite = buttons[PlayerPrefs.GetInt("Language")].spriteState.selectedSprite;
            }
        }
       
    }

    public void LanguageChange(int languageID)
    {
        PlayerPrefs.SetInt("Language", languageID);
        Translator.SelectLanguage(PlayerPrefs.GetInt("Language"));
        GameController.instance.GetScoreTexts();
    }

    public void ShowErrorText()
    {
        ErrorWindowText.SetActive(true);
        ErrorWindowText.GetComponent<Text>().text = Translator.GetText(20);
    }
}
