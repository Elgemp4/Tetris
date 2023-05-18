using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Showable : MonoBehaviour 
{
    private Showable previousUI = null;

    private void Start()
    {
        this.HideMenu();

        PostStart();
    }

    /// <summary>
    /// Affiche le menu actuel et cache le menu précédent
    /// </summary>
    /// <param name="previousUI">Le menu précédent</param>
    public void ShowMenu(Showable previousUI = null) 
    {
        this.gameObject.SetActive(true);

        if(previousUI != null) 
        { 
            this.previousUI = previousUI;

            previousUI.HideMenu();
        }

        OnShow();
    }

    /// <summary>
    /// Cache le menu actuel et affiche le menu précédent
    /// </summary>
    public void HideMenu() 
    {
        this.gameObject.SetActive(false);

        if(previousUI != null)
        {
            previousUI.ShowMenu();
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
