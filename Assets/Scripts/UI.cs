using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;

    [SerializeField]
    private GameObject OptionMenu;

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
        Application.Quit();
    }
}
