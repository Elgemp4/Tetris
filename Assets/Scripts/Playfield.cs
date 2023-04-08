using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Playfield : MonoBehaviour
{
    public static Playfield Instance;

    private Audio audio;

    private PieceSequence tetrominoesBar;

    private GameObject[,] BlockGrid;

    [SerializeField]
    private int Width, Height;

    public Tetromino CurrentFallingTetromino { private set; get; }
    
    void Start()
    {
        Instance = this;

        audio = Audio.Instance;

        tetrominoesBar = PieceSequence.Instance;

        BlockGrid = new GameObject[Height, Width];


        GetNextTetromino();
    }

    public bool HasLanded() 
    {
        foreach (GameObject block in CurrentFallingTetromino.blocks) 
        {
            Vector2 blockPosition = (Vector2)block.transform.position;

            if (blockPosition.y == 0 || BlockGrid[(int)blockPosition.y - 1, (int)blockPosition.x] != null)
            {
                return true;
            }
        }

        return false;
    }

    public void PlaceTetromino()
    {
        foreach (GameObject block in CurrentFallingTetromino.blocks)
        {
            this.BlockGrid[(int)block.transform.position.y, (int)block.transform.position.x] = Instantiate(block, block.transform.position, block.transform.rotation, this.transform);
        }

        Destroy(CurrentFallingTetromino.gameObject);
    }

    private void GetNextTetromino()
    {
        CurrentFallingTetromino = tetrominoesBar.GetNextTetromino().GetComponent<Tetromino>();

        CurrentFallingTetromino.SetAtStart();
    }

    public void TryMoveLeft()
    {
        CurrentFallingTetromino.MoveLeft();

        if (!IsInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.MoveRight();
        }
        else 
        {
            audio.PlayMoveAudio();
        }
    }

    public void TryMoveRight()
    {
        CurrentFallingTetromino.MoveRight();

        if (!IsInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.MoveLeft();
        }
        else 
        {
            audio.PlayMoveAudio();
        }
    }

    public void TryRotateLeft()
    { 
        CurrentFallingTetromino.RotateLeft();

        if (!IsInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.RotateRight();
        }
        else
        {
            audio.PlayRotateAudio();
        }
    }

    public void TryRotateRight()
    {
        CurrentFallingTetromino.RotateRight();

        if (!IsInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.RotateLeft();
        }
        else
        {
            audio.PlayRotateAudio();
        }
    }

    public void HardDrop()
    {
        while (!HasLanded())
        {
            TryMoveDown();
        }

        TryMoveDown();

        audio.PlayHardDropAudio();
    }

    public void TryMoveDown()
    {
        if (HasLanded())
        {
            PlaceTetromino();

            GetNextTetromino();
        }
        else 
        {
            CurrentFallingTetromino.MoveDown();
        }
        
    }

    private bool IsOverlapping()
    {
        foreach (GameObject block in CurrentFallingTetromino.blocks)
        {
            Vector3 position = block.transform.position;

            if (BlockGrid[(int)position.y, (int)position.x] != null)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsInBound()
    {
        foreach (GameObject block in CurrentFallingTetromino.blocks)
        {
            Vector3 position = block.transform.position;

            if (position.x < 0 || position.x >= 10 || position.y < 0)
            {
                return false;
            }
        }

        return true;
    }
}
