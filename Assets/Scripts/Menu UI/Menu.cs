using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Menu : MonoBehaviour 
{
    private Menu PreviousMenu = null;

    private void Start()
    {
        this.HideMenu();

        PostStart();
    }

    /// <summary>
    /// Affiche le menu actuel et cache le menu précédent
    /// </summary>
    /// <param name="previousMenu">Le menu précédent</param>
    public void ShowMenu(Menu previousMenu = null) 
    {
        this.gameObject.SetActive(true);

        if(previousMenu != null) 
        { 
            this.PreviousMenu = previousMenu;

            previousMenu.HideMenu();
        }

        OnShow();
    }

    /// <summary>
    /// Cache le menu actuel et affiche le menu précédent
    /// </summary>
    public void HideMenu() 
    {
        this.gameObject.SetActive(false);

        if(PreviousMenu != null)
        {
            PreviousMenu.ShowMenu();
        }
    }

    /// <summary>
    /// Méthode appelée quand le menu est affiché
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// Méthode appelée après le start
    /// </summary>
    protected virtual void PostStart() { }
}
