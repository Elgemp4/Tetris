using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static Playfield Instance;

    private TetrominoesBar tetrominoesBar;

    private GameObject[,] BlockGrid;

    [SerializeField]
    private int Width, Height;

    private Tetromino CurrentFallingTetromino;

    
    void Start()
    {
        Instance = this;

        tetrominoesBar = TetrominoesBar.Instance;

        InstantiateNextTetromino();
    }

    public void PlaceTetromino()
    {


        InstantiateNextTetromino();
    }

    private void InstantiateNextTetromino()
    {
        GameObject tetromino = tetrominoesBar.GetNextTetromino();

        CurrentFallingTetromino = Instantiate(tetromino).GetComponent<Tetromino>();


    }

    public void ExecuteGameTick()
    {
        Debug.Log("tick");
        CurrentFallingTetromino.MoveDown();
    }
}
