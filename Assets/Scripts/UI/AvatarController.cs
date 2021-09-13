using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    public static AvatarController instance;
    public Avatar[] avatars;

    public static int SelectedAvatar
    {
        get { return PlayerPrefs.GetInt("SelectedAvatar", 0); }
        set {
            PlayerPrefs.SetInt("SelectedAvatar", value);
            PlayFabManager.SetUserAvatarID(value);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetUnSelectedAvatars()
    {
        foreach (var a in avatars)
            a.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
