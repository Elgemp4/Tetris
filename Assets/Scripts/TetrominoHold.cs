using UnityEngine;

public class TetrominoHold : MonoBehaviour
{
    public static TetrominoHold Instance;

    private Tetromino HoldedTetromino;
    
    void Awake()
    {
        Instance = this;

        HoldedTetromino = null;   
    }

    public Tetromino Switch(Tetromino newHoldedTetromino)
    { 
        Tetromino previousTetromino = HoldedTetromino;

        HoldedTetromino = newHoldedTetromino;

        HoldedTetromino.transform.position = transform.position;

        HoldedTetromino.ResetRotation();

        return previousTetromino;
    }
}
