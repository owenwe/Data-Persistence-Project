
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string PlayerName;
    public int Score = 0;
    private HighScore HighScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        // TODO load high scores from persistant source
        LoadHighScore();
    }
    
    [System.Serializable]
    class HighScore
    {
        public string Name;
        public int Score = 0;
    }

    public void SaveHighScore()
    {
        // TODO save high scores to persistent storage
        HighScores.Name = PlayerName;
        HighScores.Score = Score;
    }

    public void LoadHighScore()
    {
        // TODO load from persistent storage

        HighScores = new HighScore();
        HighScores.Name = PlayerName;
        HighScores.Score = Score;
    }

    public int GetHighestScore()
    {
        return HighScores.Score;
    }

    public string GetHighestScorePlayerName()
    {
        return HighScores.Name;
    }
}