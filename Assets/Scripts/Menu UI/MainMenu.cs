using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    private Menu OptionsUI;

    private Menu LeaderBoardUI;

    protected override void PostStart()
    {
        OptionsUI = OptionsMenu.Instance;

        LeaderBoardUI = LeaderBoardMenu.Instance;

        this.ShowMenu();
    }

    /// <summary>
    /// Démarre le jeu
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Tetris_Normal_Mode");
    }

    /// <summary>
    /// Affiche le menu des options
    /// </summary>
    public void ShowOptions()
    {
        OptionsUI.ShowMenu(this);
    }

    /// <summary>
    /// Affiche le menu des scores
    /// </summary>
    public void ShowLeaderBoard()
    {
        LeaderBoardUI.ShowMenu(this);
    }

    /// <summary>
    /// Quitte le jeu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit(); //Ne marche pas dans Unity, besoin du jeu build
    }

    
}
