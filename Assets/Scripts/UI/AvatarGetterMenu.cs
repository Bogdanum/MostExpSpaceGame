using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AvatarGetterMenu : MonoBehaviour
{
    [SerializeField] private Image currentAvatar;
    [SerializeField] private Image status;
    private static Color32 StatusColor;

    public static AvatarGetterMenu instance;

    public static string Status
    {
        set { StatusColor = GetStatusColor(value);
        }
    }

    private void Awake() => instance = this;
    private void Start() => StartCoroutine(SetMenuPlayerStatus());
    public void UpdateMenuAvatar() => StartCoroutine(SetMenuPlayerStatus());

    private static Color32 GetStatusColor(string status)
    {
        switch (status)
        {
            case "Simple": return new Color32(255, 255, 255, 0);
            case "Gold":   return new Color32(255, 215, 0, 255);
            case "Silver": return new Color32(192, 192, 192, 255);
            case "Bronze": return new Color32(205, 127, 50, 255);
            default:       return new Color32(255, 255, 255, 0);
        }
    }

    private IEnumerator SetMenuPlayerStatus()
    {
        yield return new WaitUntil(() => StatusColor != new Color(255,255,255,255));
        currentAvatar.sprite = AvatarController.instance.avatars[AvatarController.SelectedAvatar].
        transform.GetChild(1).gameObject.GetComponent<Image>().sprite;
        status.color = StatusColor;
    }
}