using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text highScoreText;
    public void StartGame()
    {
        if (ScoreManager.Instance.playerName.Length > 0)
        {
            ScoreManager.Instance.NewPlayer();
            SceneManager.LoadScene(1);
        }
    }
    
    void Start()
    {
        highScoreText.text = ScoreManager.Instance.GetHighScoreText();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
        ScoreManager.Instance.SaveHighScores();
    }

    public void PlayerNameUpdate(string inputValue)
    {
        ScoreManager.Instance.playerName = inputValue.Trim();
    }

    public void GoToHighScores()
    {
        SceneManager.LoadScene(2);
    }
}