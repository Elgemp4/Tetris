using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Showable
{
    public static PauseMenu Instance;

    private ControlsManager Controls;

    private OptionsMenu OptionsUI;

    protected override void PostStart()
    {
        Instance = this;

        OptionsUI = OptionsMenu.Instance;
        
        Controls = ControlsManager.Instance;

        HideMenu();
    }

    public void Resume()
    {
        Controls.ResumeGame();
        HideMenu();
    }

    public void ShowOptions()
    { 
        OptionsUI.ShowMenu(this);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
