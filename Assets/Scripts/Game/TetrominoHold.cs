using System;
using UnityEngine;

/// <summary>
/// La class <c>Repr�sente la fonctionalit� de retenue des pi�ces, elle s'occupe de g�rer l'�change de pi�ce retenue</c>
/// </summary>
public class TetrominoHold : MonoBehaviour
{
    public static TetrominoHold Instance;

    private Tetromino HoldedTetromino;

    private bool HaveHold = false;

    void Awake()
    {
        Instance = this;

        HoldedTetromino = null;   
    }

    /// <summary>
    /// Permet d'�changer le <c>Tetromino</c> pass� en param�tre avec celui retenu
    /// </summary>
    /// <exception cref="Exception">
    /// Jet�e quand le tetromino a d�j� �t� retenu
    /// </exception>
    /// <param name="newHoldedTetromino">Le <c>Tetromino</c> que l'on souhaite stocker</param>
    /// <returns>Le <c>Tetromino</c> retenu<c>Tetromino</c></returns>
    public Tetromino Switch(Tetromino newHoldedTetromino)
    {   
        if(HaveHold) 
        {
            throw new Exception("Alread have a holded tetromino, tetromino must first land");
        }

        Tetromino previousTetromino = HoldedTetromino;

        HoldedTetromino = newHoldedTetromino;

        SetTetrominoToDisplayPosition();

        HaveHold = true;

        return previousTetromino;
    }

    /// <summary>
    /// Change la position du <c>Tetromino</c> retenu pour qu'il soit affich� dans la zone de retenue
    /// </summary>
    public void SetTetrominoToDisplayPosition()
    {
        HoldedTetromino.transform.position = transform.position;

        HoldedTetromino.ResetRotation();
    }

    public void ResetHold()
    {
        HaveHold = false;
    }
}
