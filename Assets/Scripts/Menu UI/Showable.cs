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

    public void HideMenu() 
    {
        this.gameObject.SetActive(false);

        if(previousUI != null)
        {
            previousUI.ShowMenu();
        }
    }

    protected virtual void OnShow() { }

    protected virtual void PostStart() { }
}
