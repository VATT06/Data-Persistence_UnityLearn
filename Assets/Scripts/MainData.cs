using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainData : MonoBehaviour
{
    public static MainData Instance;
    public int playerScore;
    public string playerName;

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
        public int score;
    }

    private void SaveData()
    {
       PlayerData data = new PlayerData();
        data.name = playerName;
        data.score = playerScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/PlayerData", json);
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            playerName = data.name;
            playerScore = data.score;
        }
    }
}
