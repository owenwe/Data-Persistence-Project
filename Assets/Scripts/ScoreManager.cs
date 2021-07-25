using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string PlayerName;
    public int Score = 0;
    public PlayerScore HighScores = new PlayerScore("", 0);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadHighScore();
    }

    [System.Serializable]
    public struct PlayerScore
    {
        public string Name;
        public int Score; 

        public PlayerScore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override string ToString()
        {
            return $"[PlayerScore]: Name={Name}, Score={Score}";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public PlayerScore PlayerScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.PlayerScore = HighScores;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            HighScores = data.PlayerScore;
        }
    }

    public string GetHighScoreText()
    {
        return $"High Score: {HighScores.Name} {HighScores.Score}";
    }

    public int GetHighestScore()
    {
        return HighScores.Score;
    }
}