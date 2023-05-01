using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Showable
{
    private Showable OptionsUI;

    private Showable LeaderBoardUI;

    protected override void PostStart()
    {
        OptionsUI = OptionsMenu.Instance;

        LeaderBoardUI = LeaderBoardMenu.Instance;

        this.ShowMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Tetris_Normal_Mode");
    }

    public void ShowOptions()
    {
        OptionsUI.ShowMenu(this);
    }

    public void ShowLeaderBoard()
    {
        LeaderBoardUI.ShowMenu(this);
    }

    public void QuitGame()
    {
        Application.Quit(); //Ne marche pas dans Unity, besoin du jeu build
    }

    
}
