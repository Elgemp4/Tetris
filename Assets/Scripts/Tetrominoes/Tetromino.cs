using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetromino : MonoBehaviour
{
    private const int NumberOfBlock = 4;

    private GameObject blockPrefab;

    public readonly Vector2 StartPosition = new Vector2 (0, 22);

    protected GameObject[] blocks;

    protected Vector2[,] rotations;

    private int RotationIndex = 0;

    private void Start()
    {
        this.transform.position = StartPosition;

        blockPrefab = Resources.Load(@"Tetrominoes/Block") as GameObject;
        blockPrefab.GetComponent<SpriteRenderer>().color = GetColor();

        GenerateRotations();

        InstantiateBlocks();
    }

    public void MoveDown()
    {
        Debug.Log(this.transform.position);
        transform.position = (Vector2)this.transform.position + Vector2.down;
        Debug.Log(this.transform.position);
        Debug.Log("desecnd");
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

        Debug.Log(blocks.Length);
        for (int i = 0; i < NumberOfBlock; i++)
        {
            Debug.Log(i);
            Debug.Log("Rotation : " + RotationIndex);
            blocks[i].transform.position = this.transform.position;
            blocks[i].transform.position += (Vector3)rotations[RotationIndex, i];
        }
    }

    public void RotateLeft() 
    {
        if (RotationIndex == 0)
        {
            RotationIndex = rotations.Length - 1;
            return;
        }

        RotationIndex--;
    }
    public void RotateRight()
    {
        RotationIndex = (RotationIndex + 1) % rotations.Length;
    }

    protected abstract Color GetColor();

    protected abstract void GenerateRotations();

    private void Update()
    {
        //MoveDown();
    }
}
