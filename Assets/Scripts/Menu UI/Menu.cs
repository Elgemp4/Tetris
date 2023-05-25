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
    /// Affiche le menu actuel et cache le menu pr�c�dent
    /// </summary>
    /// <param name="previousMenu">Le menu pr�c�dent</param>
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
    /// Cache le menu actuel et affiche le menu pr�c�dent
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
    /// M�thode appel�e quand le menu est affich�
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// M�thode appel�e apr�s le start
    /// </summary>
    protected virtual void PostStart() { }
}
