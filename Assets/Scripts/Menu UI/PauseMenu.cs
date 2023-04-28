using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, IShowable
{
    public static PauseMenu Instance;

    private ControlsManager controls;

    void Start()
    {
        Instance = this;
        
        controls = ControlsManager.Instance;

        HideMenu();

        Debug.Log("PauseMenu Start");
    }

    public void ShowMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void Resume()
    {
        controls.ResumeGame();
        HideMenu();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
