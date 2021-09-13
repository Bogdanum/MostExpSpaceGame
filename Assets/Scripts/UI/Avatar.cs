using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    public int ID;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            AvatarController.SelectedAvatar = ID;
            PlayFabManager.instance.avatarStorage[PlayFabManager.instance.loggedInPlayfabId] = ID;
            AvatarGetterMenu.instance.UpdateMenuAvatar();
            AvatarController.instance.SetUnSelectedAvatars();
            transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 110, 0, 255);
        });
    }

    private void Start()
    {
        if (ID == AvatarController.SelectedAvatar)
            transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 110, 0, 255);
    }
}
