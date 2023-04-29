using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour, IShowable
{
    public static GameOverMenu Instance;

    private Score score;

    [SerializeField]
    private TMP_Text ScoreText;

    void Start()
    {
        Instance = this;

        score = Score.Instance;

        HideMenu();
    }

    public void ShowMenu()
    {
        this.gameObject.SetActive(true);

        ScoreText.text = "Score : " + score.ScoreCount;
    }
    public void HideMenu()
    {
        this.gameObject.SetActive(false);
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
