using System.Collections.Generic;
using UnityEngine;

public class PieceSequence : MonoBehaviour
{
    public static PieceSequence Instance;

    private Queue<GameObject> NextTetrominoes;

    void Start()
    {
        Instance = this;

        NextTetrominoes = new Queue<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            CreateTetrominoBag();
        }
    }

    private void CreateTetrominoBag()
    {
        int[] bag = new int[7];
        
        for(int i = 0; i < bag.Length; i++) 
        {
            int index = Random.Range(0, bag.Length);

            if (bag[index] == 0)
            {
                bag[index] = i;
            }
            else 
            {
                i--;
            }
        }

        foreach(int index in bag)
        {
            CreateTetromino(index);
        }
    }

    private void CreateTetromino(int index)
    {
        string choosenTetromino = "";

        switch (index)
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
        GameObject nextTetromino = NextTetrominoes.Dequeue();

        if(NextTetrominoes.Count <= 7 ) 
        {
            CreateTetrominoBag();
        }
        return nextTetromino;
    }
}
