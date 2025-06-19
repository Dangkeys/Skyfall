using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void LoadGameScene() 
    {
        SceneManager.LoadScene("GameScene");
    }
}
