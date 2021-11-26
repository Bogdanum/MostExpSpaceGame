using UnityEngine;
using UnityEngine.UI;

public class FPSctript : MonoBehaviour
{
    public Text FPS;
    public static float fps;
    float y;
    
    void Update()
    {
        y += 1;
        if (y / 15 == 1)
        {
            fps = 1.0f / Time.deltaTime;
            FPS.text = "FPS: " + (int)fps;
            y = 1;
        }
    }
}
