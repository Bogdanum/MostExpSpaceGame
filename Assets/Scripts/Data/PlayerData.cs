using UnityEngine;

public static class PlayerData
{
    //-------------- Data ---------------//
    public static int BestScore;
    public static int DestrAster;
    public static int Language;
    public static float Difficulty;
    public static float GraphicsLevel;
    public static float Sound;
    public static float ControlType;
    public static string PlayerName;

    //-------------- Keys ---------------//
    private const string KeyBestScore     = "SaveScore";
    private const string KeyDestrAster    = "SaveDestr";
    private const string KeyLanguage      = "Language";
    private const string KeyDifficulty    = "Difficulty";
    private const string KeyGraphicsLevel = "SaveQuality";
    private const string KeySound         = "SaveSound";
    private const string KeyControlType   = "SaveControl";
    private const string KeyPlayerName    = "PlayerName";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Load()
    {
        GraphicsLevel = LoadPref(KeyGraphicsLevel, 1f);
        DestrAster    = LoadPref(KeyDestrAster, 0);
        Language      = LoadPref(KeyLanguage, 0);
        Difficulty    = LoadPref(KeyDifficulty, 0f);
        BestScore     = LoadPref(KeyBestScore, 0);
        Sound         = LoadPref(KeySound, 1f);
        ControlType   = LoadPref(KeyControlType, 0f);
        PlayerName    = LoadPref(KeyPlayerName, string.Empty);
    }

    public static void Refresh() => Load();


    public static void SaveBestScore(int score)
    {
        SavePref(KeyBestScore, score);
    }

    public static void SaveDestrAsterCount(int value)
    {
        SavePref(KeyDestrAster, value);
    }

    public static void SaveLanguage(int id)
    {
        SavePref(KeyLanguage, id);
    }

    public static void SaveDifficulty(float level)
    {
        SavePref(KeyDifficulty, level);
    }

    public static void SaveGraphicsConfig(float value)
    {
        SavePref(KeyGraphicsLevel, value);
    }

    public static void SaveSound(float value)
    {
        SavePref(KeySound, value);
    }

    public static void SaveControlType(float value)
    {
        SavePref(KeyControlType, value);
    }

    public static void SavePlayerName(string name)
    {
        SavePref(KeyPlayerName, name);
    }

#region PLAYER PREFS

    private static void SavePref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    private static void SavePref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private static void SavePref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    private static string LoadPref(string key, string default_value)
    {
        return PlayerPrefs.GetString(key, default_value);
    }

    private static int LoadPref(string key, int default_value)
    {
        return PlayerPrefs.GetInt(key, default_value);
    }

    private static float LoadPref(string key, float default_value)
    {
        return PlayerPrefs.GetFloat(key, default_value);
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
#endregion
}
