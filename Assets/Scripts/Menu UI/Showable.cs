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
    /// Affiche le menu actuel et cache le menu pr�c�dent
    /// </summary>
    /// <param name="previousUI">Le menu pr�c�dent</param>
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
    /// Cache le menu actuel et affiche le menu pr�c�dent
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
    /// M�thode appel�e quand le menu est affich�
    /// </summary>
    protected virtual void OnShow() { }

    /// <summary>
    /// M�thode appel�e apr�s le start
    /// </summary>
    protected virtual void PostStart() { }
}
