using System;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    private int maxHighScores = 6;
    private Guid currentPlayerId;
    
    public static ScoreManager Instance;
    public string playerName;
    public int score = 0;

    // I remember games used to limit the number of
    // high scores to somewhere lower than 10
    // otherwise we need to make a paging system
    public PlayerScore[] highScores = new PlayerScore[]
    {
        new PlayerScore("Dragon", 0)
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadHighScores();
    }

    [System.Serializable]
    public struct PlayerScore
    {
        public Guid id;
        public string name;
        public int score; 

        public PlayerScore(string pName, int pScore)
        {
            id = new Guid();
            name = pName;
            score = pScore;
        }

        public PlayerScore(Guid pId, string pName, int pScore)
        {
            id = pId;
            name = pName;
            score = pScore;
        }

        public override string ToString()
        {
            return $"[PlayerScore]: Name={name}, Score={score}";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public PlayerScore[] playerScores;
    }

    public void NewPlayer()
    {
        currentPlayerId = new Guid();
        score = 0;
    }

    public int InHighScores()
    {
        int newHighScoreIndex = -1;
        for (int i = 0; i < highScores.Length; i++)
        {
            if (score >= highScores[i].score)
            {
                newHighScoreIndex = i;
                break;
            }
        }
        
        return newHighScoreIndex;
    }

    public void CheckPlayerScore()
    {
        int scoreIndex = InHighScores();
        if (scoreIndex > -1)
        {
            PlayerScore player = new PlayerScore(currentPlayerId, playerName, score);
            
            int newHighScoresSize = highScores.Length == maxHighScores ? maxHighScores : highScores.Length + 1;
            
            PlayerScore[] newHighScores = new PlayerScore[newHighScoresSize];
            
            // if scoreIndex == 0: copy player, then scoreIndex to Length
            // if scoreIndex == Length: copy 0 to Length - 1, then player
            // else: copy 0 to scoreIndex, then player, then scoreIndex to Length

            if (scoreIndex == 0)
            {
                newHighScores[scoreIndex] = player;
                Array.Copy(highScores, 0, newHighScores, 1, newHighScoresSize - 1);
            } else if (scoreIndex == (maxHighScores - 1))
            {
                Array.Copy(highScores, 0, newHighScores, 0, newHighScoresSize - 1);
                newHighScores[newHighScores.Length - 1] = player;
            }
            else
            {
                Array.Copy(highScores, 0, newHighScores, 0, scoreIndex);
                newHighScores[scoreIndex] = player;
                Array.Copy(highScores, scoreIndex, newHighScores, scoreIndex + 1, (newHighScoresSize - (scoreIndex + 1)));
            }

            highScores = newHighScores;
        }
    }

    public void SaveHighScores()
    {
        SaveData data = new SaveData();
        data.playerScores = highScores;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScores = data.playerScores;
        }
    }

    public string GetHighScoreText()
    {
        return $"High Score: {highScores[0].name} {highScores[0].score}";
    }

    public int GetHighestScore()
    {
        return highScores[0].score;
    }
}