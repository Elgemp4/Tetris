using System.Collections.Generic;
using UnityEngine;

public class PieceSequence : MonoBehaviour
{
    public static PieceSequence Instance;

    private Queue<GameObject> NextTetrominoes;

    private List<GameObject> VisisbleTetrominoes;

    private static string[] TetrominoesIndex =
    {
        "I_Tetromino",
        "S_Tetromino",
        "Z_Tetromino",
        "L_Tetromino",
        "J_Tetromino",
        "T_Tetromino",
        "O_Tetromino"
    };

    void Start()
    {
        Instance = this;

        NextTetrominoes = new Queue<GameObject>();

        VisisbleTetrominoes = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            CreateTetrominoBag();
        }

        InstantiateFirstTetrominoes();
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
        string choosenTetromino = TetrominoesIndex[index];

        NextTetrominoes.Enqueue(Resources.Load("Tetrominoes/" + choosenTetromino) as GameObject);
    }

    private void InstantiateFirstTetrominoes()
    {
        for (int i = 0; i < 5; i++) 
        {
            InstintateNextVisibleTetromino(i);
        }
    }

    private void InstintateNextVisibleTetromino(int position)
    {
        GameObject tetrominoe = Instantiate(NextTetrominoes.Dequeue());

        tetrominoe.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 3 * position);

        VisisbleTetrominoes.Add(tetrominoe);
    }

    private void ShiftUpVisibleTetrominoes()
    { 
        foreach(GameObject tetrominoes in VisisbleTetrominoes)
        {
            tetrominoes.transform.position += Vector3.up * 3f;
        }
    }

    public GameObject GetNextTetromino()
    {
        GameObject nextTetromino = VisisbleTetrominoes[0];

        VisisbleTetrominoes.RemoveAt(0);
        
        ShiftUpVisibleTetrominoes();

        InstintateNextVisibleTetromino(4);

        if(NextTetrominoes.Count + VisisbleTetrominoes.Count <= 7 ) 
        {
            CreateTetrominoBag();
        }

        return nextTetromino;
    }
}
