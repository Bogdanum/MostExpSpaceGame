using UnityEngine;
using UnityEngine.UI;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] private Button cheatButton;
    [SerializeField] private InputField score,
                                        asteroids;

    private void Awake()
    {
        cheatButton.onClick.AddListener(delegate
        {
            CheckAndApplyCheat();
        });
    }

    private void CheckAndApplyCheat()
    {
        if (score.text != "" && asteroids.text != "")
        {
            if (long.Parse(score.text) > int.MaxValue)
                score.text = int.MaxValue.ToString();

            if (long.Parse(asteroids.text) > int.MaxValue)
                asteroids.text = int.MaxValue.ToString();

            GameController.SetCheatStats(int.Parse(score.text), int.Parse(asteroids.text));
        }
    }
}
