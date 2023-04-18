using System;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playfield : MonoBehaviour
{
    public static Playfield Instance;

    private CinemachineImpulseSource CameraShakeSource;

    private Audio AudioPlayer;

    private Score score;

    private PieceSequence PieceSequence;

    private GameObject[,] BlockGrid;

    private TetrominoHold TetrominoHold;

    [SerializeField]
    private int Width, Height;

    private bool HaveHold = false;

    public Tetromino CurrentFallingTetromino { private set; get; }

    private Ghost_Tetromino GhostTetromino;

    void Start()
    {
        Instance = this;

        GhostTetromino = Instantiate((GameObject)Resources.Load("Tetrominoes/Ghost_Tetromino")).GetComponent<Ghost_Tetromino>();

        CameraShakeSource = GetComponent<CinemachineImpulseSource>();

        AudioPlayer = Audio.Instance;

        PieceSequence = PieceSequence.Instance;

        TetrominoHold = TetrominoHold.Instance;

        BlockGrid = new GameObject[Height, Width];

        score = Score.Instance;

        GetNextTetromino();

        GhostTetromino.ShowAtTheBottom();
    }

    #region Movement
    public void TryMove(Vector2 direction)
    {
        CurrentFallingTetromino.Move(direction);
        if (!IsTetrominoInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.Move(-direction);
        }
        else
        {
            AudioPlayer.PlayMoveAudio();
            GhostTetromino.ShowAtTheBottom();
        }
    }

    public void TryMoveDown()
    {
        if (HasLanded(CurrentFallingTetromino))
        {
            PlaceTetromino();

            CheckForDestroyedLines();

            GetNextTetromino();

            HaveHold = false;
        }
        else
        {
            CurrentFallingTetromino.Move(Vector2.down);
        }

    }

    public void HardDrop()
    {
        while (!HasLanded(CurrentFallingTetromino))
        {
            TryMoveDown();
        }

        TryMoveDown();

        CameraShakeSource.GenerateImpulseWithForce(0.15f);

        AudioPlayer.PlayHardDropAudio();
    }

    public void TryRotate(int direction)
    {
        if (Math.Abs(direction) != 1) { return; }

        CurrentFallingTetromino.Rotate(direction);

        if (!IsTetrominoInBound() || IsOverlapping())
        {
            CurrentFallingTetromino.Rotate(-direction);
        }
        else
        {
            AudioPlayer.PlayRotateAudio();
            GhostTetromino.ShowAtTheBottom();
        }
    }

    public bool HasLanded(Tetromino tetromino) 
    {
        foreach (GameObject block in tetromino.blocks) 
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

        CameraShakeSource.GenerateImpulseWithForce(0.05f);
    }

    public void SetFallingTetromino(Tetromino newTetromino)
    {
        if (newTetromino == null)
        {
            GetNextTetromino();
            return;
        }

        CurrentFallingTetromino = newTetromino;

        CurrentFallingTetromino.SetAtStart();

        GhostTetromino.SetReplicated_Tetromino(CurrentFallingTetromino);

        GhostTetromino.ShowAtTheBottom();
    }

    private void GetNextTetromino()
    {
        SetFallingTetromino(PieceSequence.GetNextTetromino().GetComponent<Tetromino>());
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

    #endregion

    #region LineClearing

    public void CheckForDestroyedLines()
    {
        int destroyedLines = 0;


        List<int> clearedLines = new List<int>();

        for (int y = 0; y < Height; y++)
        {
            if (IsLineFull(y))
            {
                DestroyLine(y);
                destroyedLines++;
                clearedLines.Add(y);
            }
        }

        if (destroyedLines > 0)
        {
            AudioPlayer.PlayLineClear(destroyedLines);

            score.AddScore(destroyedLines);

            CameraShakeSource.GenerateImpulseWithForce(destroyedLines * 0.5f);

            foreach(int line in clearedLines)
            {
                DropBlocks(line);
            }

            CheckForDestroyedLines();
        }
    }

    private bool IsLineFull(int y)
    {
        for (int x = 0; x < Width; x++)
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

        for (int x = 0; x < Width; x++)
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

    #endregion

    #region Misc

    public bool IsABlockPresent(int x, int y)
    {
        return BlockGrid[y, x] != null;
    }

    public void Hold()
    {
        if (HaveHold)
        {
            return;
        }

        Tetromino newTetromino = TetrominoHold.Switch(CurrentFallingTetromino);

        HaveHold = true;

        SetFallingTetromino(newTetromino);

        AudioPlayer.PlayHoldAudio();
    }

    #endregion
}