using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;

    [SerializeField]
    private GameObject OptionMenu;

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
        MainMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }

    public void ShowMenu()
    {
        MainMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); //Ne marche pas dans Unity, besoin du jeu build
    }
}
