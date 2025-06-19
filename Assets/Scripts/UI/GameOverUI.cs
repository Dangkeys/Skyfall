using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private const string HIGH_SCORE = "Highscore";

    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private int currentHighScore;
    private void Start() {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        LoadHighScore();
        TrySetNewHighScore();
        UpdateGameOverUI();
    }
    private void TrySetNewHighScore()
    {
        int playerScore = player.GetScore();
        if (playerScore > currentHighScore)
        {
            currentHighScore = playerScore;
            PlayerPrefs.SetInt(HIGH_SCORE, currentHighScore);
            PlayerPrefs.Save();
        }
    }

    private void LoadHighScore()
    {
        currentHighScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
    }
    private void UpdateGameOverUI()
    {
        currentScoreText.text = "YOUR SCORE IS " + currentHighScore;
        highScoreText.text = "HIGHSCORE: " + player.GetScore();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
