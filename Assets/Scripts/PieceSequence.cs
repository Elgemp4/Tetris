using System.Collections.Generic;
using UnityEngine;

public class PieceSequence : MonoBehaviour
{
    public static PieceSequence Instance;

    private Queue<GameObject> NextTetrominoes;

    private List<GameObject> VisisbleTetrominoes;

    void Start()
    {
        Instance = this;

        NextTetrominoes = new Queue<GameObject>();

        VisisbleTetrominoes = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            CreateTetrominoBag();
        }

        InstantiateFollowingTetrominoes();
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

        

        NextTetrominoes.Enqueue(Resources.Load("Tetrominoes/" + choosenTetromino) as GameObject);
    }

    private void InstantiateFollowingTetrominoes()
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

    private void ShiftVisibleTetrominoes()
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

        

        ShiftVisibleTetrominoes();

        InstintateNextVisibleTetromino(4);

        if(NextTetrominoes.Count + NextTetrominoes.Count <= 7 ) 
        {
            CreateTetrominoBag();
        }

        Debug.Log(nextTetromino);
        return nextTetromino;
    }
}
