using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new()
        {
            TeamColor = TeamColor
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(GetPath(), json);
    }

    private static string GetPath() => $"{Application.persistentDataPath} /savefile.json";

    public void LoadColor()
    {
        string path = GetPath();
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }

    public static MainManager Instance { get; private set; }

    public Color TeamColor { get; set; }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }
}
