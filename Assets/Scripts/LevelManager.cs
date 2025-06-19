using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private bool isPause = false;
    private void Start()
    {
        pauseUI.SetActive(false);
    }
    private void Update()
    {
        HandlePause();
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            pauseUI.SetActive(isPause);
        }
        
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else if (!isGameOver)
        {
            Time.timeScale = 1f;
        }
    }

    public void SetPause(bool shouldPause)
    {
        isPause = shouldPause;
    }

}
