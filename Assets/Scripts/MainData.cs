using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainData : MonoBehaviour
{
    public static MainData Instance;
    public int current_playerScore;
    public int new_playerScore;
    public string current_playerName;
    public string new_playerName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    [System.Serializable]
    class PlayerData
    {
        public string name;
        public string newName;
        public int score;
        public int newScore;
    }

    internal void SaveData()
    {
       PlayerData data = new PlayerData();
        data.name = current_playerName;
        data.newName = new_playerName;
        data.score = current_playerScore;
        data.newScore = new_playerScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData", json);
    }

    internal void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            current_playerName = data.name;
            current_playerScore = data.score;
            new_playerName = data.newName;
            new_playerScore = data.newScore;
        }
    }
}
