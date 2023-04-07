using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetromino : MonoBehaviour
{
    private const int NumberOfBlock = 4;

    private Playfield playfield;

    private GameObject blockPrefab;

    public readonly Vector2 StartPosition = new Vector2 (5, 20);

    public GameObject[] blocks { private set; get; }

    protected Vector2[,] rotations;

    private int RotationIndex = 0;

    private void Start()
    {
        this.playfield = Playfield.Instance;

        blockPrefab = Resources.Load(@"Tetrominoes/Block") as GameObject;
        blockPrefab.GetComponent<SpriteRenderer>().color = GetColor();

        GenerateRotations();

        InstantiateBlocks();
    }

    public void SetAtStart()
    {
        this.transform.position = StartPosition;
    }

    public void MoveDown()
    {
        transform.position = (Vector2)this.transform.position + Vector2.down;
        ActualizeBlockPosition();
    }

    public void MoveUp()
    {
        transform.position = (Vector2)this.transform.position + Vector2.up;
        ActualizeBlockPosition();
    }

    private void InstantiateBlocks()
    {
        blocks = new GameObject[NumberOfBlock];

        for (int i = 0; i < NumberOfBlock; i++)
        {
            blocks[i] = Instantiate(blockPrefab, this.transform, false); //Creation des blocs avec une position relative au tetromino
        }

        ActualizeBlockPosition();
    }

    private void ActualizeBlockPosition()
    {
        if (blocks == null || blocks.Length == 0)
        {
            return;
        }

        //Debug.Log(blocks.Length);
        for (int i = 0; i < NumberOfBlock; i++)
        {
            //Debug.Log(i);
            //Debug.Log("Rotation : " + RotationIndex);
            blocks[i].transform.position = this.transform.position;
            blocks[i].transform.position += (Vector3)rotations[RotationIndex, i];
        }
    }

    public void RotateLeft() 
    {
        if (RotationIndex == 0)
        {
            RotationIndex = rotations.GetLength(0) - 1;
        }
        else 
        {
            RotationIndex--;
        }

        ActualizeBlockPosition();
    }
    public void RotateRight()
    {
        RotationIndex = (RotationIndex + 1) % rotations.GetLength(0);

        ActualizeBlockPosition();
    }

    public void MoveLeft()
    { 
        this.transform.position = (Vector2)this.transform.position + Vector2.left;
    }


    public void MoveRight()
    {
        this.transform.position = (Vector2)this.transform.position + Vector2.right;
    }

    protected abstract Color GetColor();

    protected abstract void GenerateRotations();
}
