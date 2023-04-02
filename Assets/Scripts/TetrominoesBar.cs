using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoesBar : MonoBehaviour
{
    public static TetrominoesBar Instance;

    private Queue<GameObject> NextTetrominoes;

    void Start()
    {
        Instance = this;

        NextTetrominoes = new Queue<GameObject>();

        //Debug.Log("Creation des tetrominoes");
        for (int i = 0; i < 5; i++)
        {
            CreateRandomTetromino();
        }
    }

    private void CreateRandomTetromino()
    {
        int choosenIndex = Mathf.FloorToInt(Random.value * 7);

        string choosenTetromino = "";

        switch (choosenIndex)
        {
            case 0:
                choosenTetromino = "I_Tetromino";
                break;
            case 1:
                choosenTetromino = "S_Tetromino";
                break;
            case 2:
                choosenTetromino = "Z_Tetromino";
                break;
            case 3:
                choosenTetromino = "L_Tetromino";
                break;
            case 4:
                choosenTetromino = "J_Tetromino";
                break;
            case 5:
                choosenTetromino = "T_Tetromino";
                break;
            case 6:
                choosenTetromino = "O_Tetromino";
                break;
        }

        Debug.Log(choosenTetromino);

        NextTetrominoes.Enqueue(Resources.Load("Tetrominoes/" + choosenTetromino) as GameObject);
    }

    public GameObject GetNextTetromino()
    {
        //Debug.Log("Next tetromino");
        GameObject nextTetromino = NextTetrominoes.Dequeue();
        CreateRandomTetromino();
        return nextTetromino;
    }
}
