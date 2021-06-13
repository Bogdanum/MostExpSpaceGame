using UnityEngine;
using UnityEngine.UI;

public class TranslatableText : MonoBehaviour
{
    public int textID;

    [HideInInspector] public Text UIText;

    private void Awake()
    {
        UIText = GetComponent<Text>();
        Translator.Add(this);
    }

    private void OnEnable()
    {
        Translator.UpdateTexts();
    }

    private void OnDestroy()
    {
        Translator.Delete(this);
    }
}
