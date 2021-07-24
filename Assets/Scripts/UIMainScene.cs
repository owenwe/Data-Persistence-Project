using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainScene : MonoBehaviour
{
    public static UIMainScene Instance { get; private set; }
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public GameObject MainMenuButton;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void UpdateHighScore()
    {
        HighScoreText.text = $"High Score: {ScoreManager.Instance.GetHighestScorePlayerName()} {ScoreManager.Instance.GetHighestScore()}";
    }

    public void UpdateScore(int points)
    {
        ScoreText.text = $"Score: {points}";
        ScoreManager.Instance.Score = points;
        if (points > ScoreManager.Instance.GetHighestScore())
        {
            HighScoreText.text = $"High Score: {ScoreManager.Instance.PlayerName} {points}";
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}