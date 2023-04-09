using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Playfield : MonoBehaviour
{
    public static Playfield Instance;

    private CinemachineImpulseSource source;

    private Audio audio;

    private PieceSequence tetrominoesBar;

    private GameObject[,] BlockGrid;

    [SerializeField]
    private int Width, Height;

    public Tetromino CurrentFallingTetromino { private set; get; }
    
    void Start()
    {
        Instance = this;

        source = GetComponent<CinemachineImpulseSource>();

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
        source.GenerateImpulseWithForce(0.05f);
    }

    public void CheckForDestroyedLines()
    {
        int destroyedLines = 0;

        int highestClearedLine = 0;

        for (int y =  0; y < Height; y++)
        {
            if (IsLineFull(y))
            {
                DestroyLine(y);
                destroyedLines++;
                highestClearedLine = y;
            }
        }

        Debug.Log("Destroyed : " + destroyedLines);
        Debug.Log("Highest : " + highestClearedLine);
        if (destroyedLines > 0)
        {
            audio.PlayLineClear(destroyedLines);

            source.GenerateImpulseWithForce(destroyedLines * 0.5f);

            DropBlocks(highestClearedLine);
        }
        
    }

    private bool IsLineFull(int y)
    { 
        for(int x = 0; x < Width; x++) 
        {
            if (BlockGrid[y, x] == null) 
            {
                return false;
            }
        }

        return true;
    }

    private void DestroyLine(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            Destroy(BlockGrid[y, x]);
            BlockGrid[y, x] = null;
        }
    }

    public void DropBlocks(int highestClearedLine)
    {
        List<Aglomerate> blockAglomerate = new List<Aglomerate>();

        for(int x = 0; x < Width; x++) 
        {
            if (BlockGrid[highestClearedLine + 1, x] != null)
            { 
                List<GameObject> blocks = CreateAglomerate(highestClearedLine + 1, x);

                blockAglomerate.Add(new Aglomerate(blocks));
            }
        }

        foreach (Aglomerate aglomerate in blockAglomerate)
        {
            aglomerate.Drop();

            foreach (GameObject block in aglomerate.blockList)
            {
                BlockGrid[(int)block.transform.position.y, (int)block.transform.position.x] = block;
            }
        }
    }

    public List<GameObject> CreateAglomerate(int y, int x)
    {
        List<GameObject> aglomerateBlocks = new List<GameObject>();

        GameObject block = null;

        if (IsInBound(x, y)) 
        {
            block = BlockGrid[y, x];
        }
        
        if (block != null) 
        {
            aglomerateBlocks.Add(block);
            BlockGrid[y, x] = null;

            aglomerateBlocks.AddRange(CreateAglomerate(y + 1, x));
            aglomerateBlocks.AddRange(CreateAglomerate(y - 1, x));
            aglomerateBlocks.AddRange(CreateAglomerate(y, x + 1));
            aglomerateBlocks.AddRange(CreateAglomerate(y, x - 1));
        }
        
        return aglomerateBlocks;
    }

    public bool IsABlockPresent(int x, int y)
    {
        return BlockGrid[y, x] != null;
    }

    private void GetNextTetromino()
    {
        CurrentFallingTetromino = tetrominoesBar.GetNextTetromino().GetComponent<Tetromino>();

        CurrentFallingTetromino.SetAtStart();
    }

    public void TryMoveLeft()
    {
        CurrentFallingTetromino.MoveLeft();

        if (!IsTetrominoInBound() || IsOverlapping())
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

        if (!IsTetrominoInBound() || IsOverlapping())
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

        if (!IsTetrominoInBound() || IsOverlapping())
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

        if (!IsTetrominoInBound() || IsOverlapping())
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

        source.GenerateImpulseWithForce(0.15f);

        audio.PlayHardDropAudio();
    }

    public void TryMoveDown()
    {
        if (HasLanded())
        {
            PlaceTetromino();

            CheckForDestroyedLines();

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

    private bool IsTetrominoInBound()
    {
        foreach (GameObject block in CurrentFallingTetromino.blocks)
        {
            Vector3 position = block.transform.position;

            if (!IsInBound((int)position.x, (int)position.y))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsInBound(int x, int y)
    {
        return x >= 0 && x < 10 && y >= 0;
    }
}
