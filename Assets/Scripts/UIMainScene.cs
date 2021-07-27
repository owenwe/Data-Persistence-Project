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
        HighScoreText.text = ScoreManager.Instance.GetHighScoreText();
    }

    public void UpdateScore(int points)
    {
        ScoreText.text = $"Score: {points}";
        ScoreManager.Instance.score = points;
        
        // check if current score beats the high score
        if (points > ScoreManager.Instance.GetHighestScore())
        {
            // update high score text to current player
            HighScoreText.text = $"High Score: {ScoreManager.Instance.playerName} {points}";
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}