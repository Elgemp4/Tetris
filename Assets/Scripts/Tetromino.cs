using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetromino : MonoBehaviour
{
    private const int NumberOfBlock = 4;

    private GameObject blockPrefab;

    public readonly Vector2 StartPosition = new Vector2 (5, 22);

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
        this.transform.position += Vector3.down;

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
        for (int i = 0; i < NumberOfBlock; i++)
        {
            blocks[i].transform.position = this.transform.position;
            blocks[i].transform.position += (Vector3)rotations[RotationIndex, i];
        }
    }

    public void RotateLeft() 
    {
        if (RotationIndex == 0)
        {
            RotationIndex = NumberOfBlock - 1;
            return;
        }

        RotationIndex--;
    }
    public void RotateRight()
    {
        RotationIndex = (RotationIndex + 1) % NumberOfBlock;
    }

    protected abstract Color GetColor();

    protected abstract void GenerateRotations();

    // Update is called once per frame
    private void Update()
    {
        
    }
}
