using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : Menu
{
    public static GameOverMenu Instance;

    private Score _Score;

    private LeaderBoardMenu _LeaderBoardMenu;

    [SerializeField]
    private TMP_Text ScoreText;

    protected override void PostStart()
    {
        Instance = this;

        _Score = Score.Instance;

        _LeaderBoardMenu = LeaderBoardMenu.Instance;
    }

    protected override void OnShow()
    {
        ScoreText.text = "Score : " + _Score.ScoreCount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Tetris_Normal_Mode");
    }

    public void ShowLeaderBoard()
    {
        _LeaderBoardMenu.ShowMenu(this);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
