using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tetromino : MonoBehaviour
{
    private const int NumberOfBlock = 4;

    protected Playfield playfield;

    private GameObject blockPrefab;

    public readonly Vector2 StartPosition = new Vector2(5, 20);

    public GameObject[] blocks { private set; get; }

    public Vector2[,] rotations { protected set; get; }

    public int RotationIndex { protected set; get; } = 0;

    private void Awake()
    {
        this.playfield = Playfield.Instance;

        blockPrefab = Resources.Load(@"Tetrominoes/Block") as GameObject;

        GenerateRotations();

        InstantiateBlocks();
    }

    public void ResetRotation()
    {
        RotationIndex = 0;

        ActualizeBlockPosition();
    }

    public void SetAtStart()
    {
        ChangePosition(StartPosition);
    }

    public void MoveDown()
    {
        ChangePosition((Vector2)this.transform.position + Vector2.down);
        ActualizeBlockPosition();
    }

    public void MoveUp()
    {
        ChangePosition((Vector2)this.transform.position + Vector2.up);
    }

    protected void InstantiateBlocks()
    {
        blocks = new GameObject[NumberOfBlock];
        
        blockPrefab.GetComponent<SpriteRenderer>().color = GetColor();
        
        for (int i = 0; i < NumberOfBlock; i++)
        {
            blocks[i] = Instantiate(blockPrefab, this.transform, false); //Creation des blocs avec une position relative au tetromino
        }

        ActualizeBlockPosition();
    }

    public void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;

        ActualizeBlockPosition();
    }

    protected void ActualizeBlockPosition()
    {
        if (blocks == null || blocks.Length == 0)
        {
            return;
        }


        for (int i = 0; i < rotations.GetLength(1); i++)
        {
            blocks[i].transform.position = this.transform.position;
            Debug.Log(RotationIndex);
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
