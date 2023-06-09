using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    public static GameMenuManager Instance;

    private Menu PauseMenuUI;

    private Menu GameOverMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        PauseMenuUI = PauseMenu.Instance;

        GameOverMenuUI = GameOverMenu.Instance;

        Debug.Log("Start GameMenuManager");
    }

    public void OpenPauseMenu()
    {
        PauseMenuUI.ShowMenu();
    }

    public void OpenGameOverMenu() 
    { 
        GameOverMenuUI.ShowMenu();
    }
}
