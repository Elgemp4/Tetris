using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IShowable
{
    [SerializeField]
    private GameObject MainMenuPanel;

    [SerializeField]
    private GameObject OptionMenuPanel;

    [SerializeField]
    private Slider MusicVolumeSlider;

    [SerializeField]
    private Slider EffectVolumeSlider;

    void Start()
    {
        DataTransferer.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        DataTransferer.SetEffectVolume(PlayerPrefs.GetFloat("EffectVolume"));

        MusicVolumeSlider.value = DataTransferer.MusicVolume;
        EffectVolumeSlider.value = DataTransferer.EffectVolume;

        this.ShowMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Tetris_Normal_Mode");
    }

    public void ModifyMusicVolume(float volume)
    { 
        DataTransferer.SetMusicVolume(volume);
        
    }

    public void ModifyEffectVolume(float volume)
    { 
        DataTransferer.SetEffectVolume(volume);
    }

    public void ShowOptions()
    {
        MainMenuPanel.SetActive(false);
        OptionMenuPanel.SetActive(true);
    }

    public void ShowMenu()
    {
        MainMenuPanel.SetActive(true);
        OptionMenuPanel.SetActive(false);
    }

    public void HideMenu()
    {
        MainMenuPanel.SetActive(false);
        OptionMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); //Ne marche pas dans Unity, besoin du jeu build
    }

    
}
