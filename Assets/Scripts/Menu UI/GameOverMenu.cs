using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour, IShowable
{
    public void ShowMenu()
    {
        throw new System.NotImplementedException();
    }
    public void HideMenu()
    {
        throw new System.NotImplementedException();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Tetris_Normal_Mode");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
