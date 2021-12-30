using UnityEngine;
using UnityEngine.UI;

public class shieldLoad : MonoBehaviour
{
    public Image circle;
    public float cooldown;
    [HideInInspector] public bool isCooldown;

    private void OnEnable()
    {
        isCooldown = true;
        circle = GetComponent<Image>();
    }

    void Update()
    {
        if (isCooldown)
        {
            circle.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (circle.fillAmount <= 0)
            {
                circle.fillAmount = 1;
                isCooldown = false;
            }
        }
    }
}
