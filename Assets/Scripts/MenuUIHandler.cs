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
        if (ScoreManager.Instance.PlayerName.Length > 0)
        {
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
        ScoreManager.Instance.SaveHighScore();
    }

    public void PlayerNameUpdate(string inputValue)
    {
        ScoreManager.Instance.PlayerName = inputValue.Trim();
    }
}