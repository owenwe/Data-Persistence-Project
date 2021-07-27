
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreUIHandler : MonoBehaviour
{
    public TextMeshProUGUI[] Scores = new TextMeshProUGUI[6];

    private void Start()
    {
        // loop through each High Score Text Object
        ScoreManager.PlayerScore[] highScores;
        if (ScoreManager.Instance == null)
        {
            highScores = new ScoreManager.PlayerScore[] { new ScoreManager.PlayerScore("Player_1", 0) };
        }
        else
        {
            highScores = ScoreManager.Instance.highScores;
        }
        
        
        for (int i = 0; i < highScores.Length; i++)
        {
            Scores[i].gameObject.SetActive(true);
            Scores[i].text = $"{highScores[i].name} {highScores[i].score}";
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}